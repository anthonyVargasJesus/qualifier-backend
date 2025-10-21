using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Application.Database.DefaultSection.Queries.GetDefaultSectionsByDocumentTypeId;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Section.Queries.GetSectionsByVersionId
{
    public class GetSectionsByVersionIdQuery : IGetSectionsByVersionIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetSectionsByVersionIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int versionId)
        {
            try
            {
                var allRequirements = await (from section in _databaseService.Section
                                             where ((section.isDeleted == null || section.isDeleted == false) && section.versionId == versionId)
                                             select new SectionEntity
                                             {
                                                 sectionId = section.sectionId,
                                                 numeration = section.numeration,
                                                 name = section.name,
                                                 description =  (section.description == null) ? "" : section.description,
                                                 level = section.level,
                                                 parentId = section.parentId,
                                             }).ToListAsync();

                const int FIRST_LEVEL = 1;
                const int SECOND_LEVEL = 2;
                const int THIRD_LEVEL = 3;

                var entities = await (from section in _databaseService.Section
                                      where ((section.isDeleted == null || section.isDeleted == false)
                                      && section.versionId == versionId && section.level == FIRST_LEVEL)
                                      && (section.name.ToUpper().Contains(search.ToUpper()))
                                      select new SectionEntity
                                      {
                                          sectionId = section.sectionId,
                                          numeration = section.numeration,
                                          name = section.name,
                                          description = (section.description == null) ? "" : section.description,
                                          level = section.level,
                                          parentId = section.parentId,
                                      })
                                      .OrderBy(x => x.numeration)
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetSectionsByVersionIdDto> baseResponseDto = new BaseResponseDto<GetSectionsByVersionIdDto>();


                List<GetSectionsByVersionIdDto> data = _mapper.Map<List<GetSectionsByVersionIdDto>>(entities);

                foreach (var requirement in data)
                {
                    if (hasChildren(allRequirements, requirement.sectionId, SECOND_LEVEL))
                    {
                        requirement.children = getChildren(allRequirements, requirement.sectionId, SECOND_LEVEL).OrderBy(x => x.numeration).ToList();

                        foreach (var item in requirement.children)
                            if (hasChildren(allRequirements, item.sectionId, THIRD_LEVEL))
                                item.children = getChildren(allRequirements, item.sectionId, THIRD_LEVEL).OrderBy(x => x.numeration).ToList();
                    }
                }

                baseResponseDto.data = data;
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, versionId), pageSize);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int versionId)
        {
            var total = await (from section in _databaseService.Section
                               where ((section.isDeleted == null || section.isDeleted == false) && section.versionId == versionId)
                               && (section.name.ToUpper().Contains(search.ToUpper()))
                               select new SectionEntity
                               {
                                   sectionId = section.sectionId,
                               }).CountAsync();
            return total;
        }

        bool hasChildren(List<SectionEntity> allSections, int idSection, int level)
        {
            return allSections.Count(x => x.parentId == idSection && x.level == level) > 0;
        }

        List<GetSectionsByVersionIdDto> getChildren(List<SectionEntity> allSections, int idSection, int level)
        {
            var entities = allSections.Where(x => x.parentId == idSection && x.level == level).ToList();
            return _mapper.Map<List<GetSectionsByVersionIdDto>>(entities);
        }


    }
}

