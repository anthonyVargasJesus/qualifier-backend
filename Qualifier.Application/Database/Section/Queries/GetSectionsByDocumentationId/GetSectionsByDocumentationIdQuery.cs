using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Application.Database.Section.Queries.GetSectionsByVersionId;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;


namespace Qualifier.Application.Database.Section.Queries.GetSectionsByDocumentationId
{
    public class GetSectionsByDocumentationIdQuery : IGetSectionsByDocumentationIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetSectionsByDocumentationIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int documentationId)
        {             try
            {
                var allSections = await (from section in _databaseService.Section
                                             where ((section.isDeleted == null || section.isDeleted == false) && section.documentationId == documentationId
                                             && section.versionId == null)
                                             select new SectionEntity
                                             {
                                                 sectionId = section.sectionId,
                                                 numeration = section.numeration,
                                                 name = section.name,
                                                 description = (section.description == null) ? "" : section.description,
                                                 level = section.level,
                                                 parentId = section.parentId,
                                                 versionId = section.versionId,
                                             }).ToListAsync();

                const int FIRST_LEVEL = 1;
                const int SECOND_LEVEL = 2;
                const int THIRD_LEVEL = 3;

                var entities = await (from section in _databaseService.Section
                                      where ((section.isDeleted == null || section.isDeleted == false)
                                      && section.documentationId == documentationId && section.level == FIRST_LEVEL && section.versionId == null)
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
                                      .ToListAsync();

                BaseResponseDto<GetSectionsByDocumentationIdDto> baseResponseDto = new BaseResponseDto<GetSectionsByDocumentationIdDto>();


                List<GetSectionsByDocumentationIdDto> data = _mapper.Map<List<GetSectionsByDocumentationIdDto>>(entities);

                foreach (var requirement in data)
                {
                    if (hasChildren(allSections, requirement.sectionId, SECOND_LEVEL))
                    {
                        requirement.children = getChildren(allSections, requirement.sectionId, SECOND_LEVEL).OrderBy(x => x.numeration).ToList();

                        foreach (var item in requirement.children)
                            if (hasChildren(allSections, item.sectionId, THIRD_LEVEL))
                                item.children = getChildren(allSections, item.sectionId, THIRD_LEVEL).OrderBy(x => x.numeration).ToList();
                    }
                }

                baseResponseDto.data = data;
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }


        bool hasChildren(List<SectionEntity> allSections, int idSection, int level)
        {
            return allSections.Count(x => x.parentId == idSection && x.level == level) > 0;
        }

        List<GetSectionsByDocumentationIdDto> getChildren(List<SectionEntity> allSections, int idSection, int level)
        {
            var entities = allSections.Where(x => x.parentId == idSection && x.level == level).ToList();
            return _mapper.Map<List<GetSectionsByDocumentationIdDto>>(entities);
        }
    }
}
