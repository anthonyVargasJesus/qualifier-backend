using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Application.Database.Requirement.Queries.GetAllRequirementsByStandardId;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.DefaultSection.Queries.GetAllDefaultSectionsByDocumentTypeId
{
    internal class GetAllDefaultSectionsByDocumentTypeIdQuery : IGetAllDefaultSectionsByDocumentTypeIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllDefaultSectionsByDocumentTypeIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int documentTypeId)
        {
            try
            {
                var entities = await (from defaultSection in _databaseService.DefaultSection
                                      where ((defaultSection.isDeleted == null || defaultSection.isDeleted == false) && defaultSection.documentTypeId == documentTypeId)
                                      select new DefaultSectionEntity
                                      {
                                          defaultSectionId = defaultSection.defaultSectionId,
                                          numeration = defaultSection.numeration,
                                          name = defaultSection.name,
                                          level = defaultSection.level,
                                          parentId = defaultSection.parentId,
                                      }).ToListAsync();

                var data = _mapper.Map<List<GetAllDefaultSectionsByDocumentTypeIdDto>>(entities);

                foreach (var item in data)
                    item.numerationToShow = item.numeration.ToString();

                foreach (var item in data)
                    foreach (var item2 in data)
                        if (item.parentId == item2.defaultSectionId)
                        {
                            item.numerationToShow = item2.numeration + "." + item.numeration;
                            foreach (var item3 in data)
                                if (item2.parentId == item3.defaultSectionId)
                                    item.numerationToShow = item3.numeration + "." + item.numerationToShow;
                        }

                data = data.OrderBy(x => x.numerationToShow).ToList();

                BaseResponseDto<GetAllDefaultSectionsByDocumentTypeIdDto> baseResponseDto = new BaseResponseDto<GetAllDefaultSectionsByDocumentTypeIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllDefaultSectionsByDocumentTypeIdDto>>(data);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

