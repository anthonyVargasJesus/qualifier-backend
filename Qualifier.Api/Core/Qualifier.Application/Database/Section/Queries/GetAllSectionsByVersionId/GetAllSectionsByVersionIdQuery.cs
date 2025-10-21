using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Application.Database.DefaultSection.Queries.GetAllDefaultSectionsByDocumentTypeId;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Section.Queries.GetAllSectionsByVersionId
{
    internal class GetAllSectionsByVersionIdQuery : IGetAllSectionsByVersionIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllSectionsByVersionIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int versionId)
        {
            try
            {
                var entities = await (from section in _databaseService.Section
                                      where ((section.isDeleted == null || section.isDeleted == false) && section.versionId == versionId)
                                      select new SectionEntity
                                      {
                                          sectionId = section.sectionId,
                                          numeration = section.numeration,
                                          name = section.name,
                                          level = section.level,
                                          parentId = section.parentId,
                                      }).ToListAsync();

                var data = _mapper.Map<List<GetAllSectionsByVersionIdDto>>(entities);

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

                BaseResponseDto<GetAllSectionsByVersionIdDto> baseResponseDto = new BaseResponseDto<GetAllSectionsByVersionIdDto>();
                baseResponseDto.data = data;
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

