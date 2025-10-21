using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using Qualifier.Application.Database.Requirement.Queries.GetRequirementsByStandardId;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.DefaultSection.Queries.GetDefaultSectionsByDocumentTypeId
{
    public class GetDefaultSectionsByDocumentTypeIdQuery : IGetDefaultSectionsByDocumentTypeIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetDefaultSectionsByDocumentTypeIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int documentTypeId)
        {
            try
            {
                var allRequirements = await (from defaultSection in _databaseService.DefaultSection
                                             where ((defaultSection.isDeleted == null || defaultSection.isDeleted == false) && defaultSection.documentTypeId == documentTypeId)
                                             select new DefaultSectionEntity
                                             {
                                                 defaultSectionId = defaultSection.defaultSectionId,
                                                 numeration = defaultSection.numeration,
                                                 name = defaultSection.name,
                                                 description = defaultSection.description,
                                                 level = defaultSection.level,
                                                 parentId = defaultSection.parentId,
                                             }).ToListAsync();

                const int FIRST_LEVEL = 1;
                const int SECOND_LEVEL = 2;
                const int THIRD_LEVEL = 3;

                var entities = await (from defaultSection in _databaseService.DefaultSection
                                      where ((defaultSection.isDeleted == null || defaultSection.isDeleted == false) 
                                      && defaultSection.documentTypeId == documentTypeId && defaultSection.level == FIRST_LEVEL)
                                      && (defaultSection.name.ToUpper().Contains(search.ToUpper()))
                                      select new DefaultSectionEntity
                                      {
                                          defaultSectionId = defaultSection.defaultSectionId,
                                          numeration = defaultSection.numeration,
                                          name = defaultSection.name,
                                          description = defaultSection.description,
                                          level = defaultSection.level,
                                          parentId = defaultSection.parentId,
                                      })
                                      .OrderBy(x=> x.numeration)
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetDefaultSectionsByDocumentTypeIdDto> baseResponseDto = new BaseResponseDto<GetDefaultSectionsByDocumentTypeIdDto>();


                List<GetDefaultSectionsByDocumentTypeIdDto> data = _mapper.Map<List<GetDefaultSectionsByDocumentTypeIdDto>>(entities);

                foreach (var requirement in data)
                {
                    if (hasChildren(allRequirements, requirement.defaultSectionId, SECOND_LEVEL))
                    {
                        requirement.children = getChildren(allRequirements, requirement.defaultSectionId, SECOND_LEVEL).OrderBy(x => x.numeration).ToList();

                        foreach (var item in requirement.children)
                            if (hasChildren(allRequirements, item.defaultSectionId, THIRD_LEVEL))
                                item.children = getChildren(allRequirements, item.defaultSectionId, THIRD_LEVEL).OrderBy(x => x.numeration).ToList();
                    }
                }

                baseResponseDto.data = data;




                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, documentTypeId), pageSize);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int documentTypeId)
        {
            var total = await (from defaultSection in _databaseService.DefaultSection
                               where ((defaultSection.isDeleted == null || defaultSection.isDeleted == false) && defaultSection.documentTypeId == documentTypeId)
                               && (defaultSection.name.ToUpper().Contains(search.ToUpper()))
                               select new DefaultSectionEntity
                               {
                                   defaultSectionId = defaultSection.defaultSectionId,
                               }).CountAsync();
            return total;
        }

        bool hasChildren(List<DefaultSectionEntity> allDefaultSections, int idDefaultSection, int level)
        {
            return allDefaultSections.Count(x => x.parentId == idDefaultSection && x.level == level) > 0;
        }

        List<GetDefaultSectionsByDocumentTypeIdDto> getChildren(List<DefaultSectionEntity> allDefaultSections, int idDefaultSection, int level)
        {
            var entities = allDefaultSections.Where(x => x.parentId == idDefaultSection && x.level == level).ToList();
            return _mapper.Map<List<GetDefaultSectionsByDocumentTypeIdDto>>(entities);
        }

    }
}

