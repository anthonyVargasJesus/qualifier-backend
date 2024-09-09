using AutoMapper;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.EntityFrameworkCore;
using Qualifier.Application.Database.Section.Queries.GetAllSectionsByVersionId;
using Qualifier.Application.Database.Section.Queries.GetSectionsByDocumentationId;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qualifier.Application.Database.Section.Queries.GetAllSectionsByDocumentationId
{
    internal class GetAllSectionsByDocumentationIdQuery : IGetAllSectionsByDocumentationIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllSectionsByDocumentationIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int documentationId)
        {
            try
            {
                var entities = await (from section in _databaseService.Section
                                      where ((section.isDeleted == null || section.isDeleted == false) && section.documentationId == documentationId
                                      && section.versionId == null)
                                      select new SectionEntity
                                      {
                                          sectionId = section.sectionId,
                                          numeration = section.numeration,
                                          name = section.name,
                                          level = section.level,
                                          parentId = section.parentId,
                                      }).ToListAsync();

                var data = _mapper.Map<List<GetAllSectionsByDocumentationIdDto>>(entities);

                foreach (var item in data)
                    item.numerationToShow = item.numeration.ToString();

                foreach (var item in data)
                    foreach (var item2 in data)
                        if (item.parentId == item2.sectionId)
                        {
                            item.numerationToShow = item2.numeration + "." + item.numeration;
                            foreach (var item3 in data)
                                if (item2.parentId == item3.sectionId)
                                    item.numerationToShow = item3.numeration + "." + item.numerationToShow;
                        }

                data = data.OrderBy(x => x.numerationToShow).ToList();

                BaseResponseDto<GetAllSectionsByDocumentationIdDto> baseResponseDto = new BaseResponseDto<GetAllSectionsByDocumentationIdDto>();
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

        List<GetAllSectionsByDocumentationIdDto> getChildren(List<SectionEntity> allSections, int idSection, int level)
        {
            var entities = allSections.Where(x => x.parentId == idSection && x.level == level).ToList();
            return _mapper.Map<List<GetAllSectionsByDocumentationIdDto>>(entities);
        }

    }
}
