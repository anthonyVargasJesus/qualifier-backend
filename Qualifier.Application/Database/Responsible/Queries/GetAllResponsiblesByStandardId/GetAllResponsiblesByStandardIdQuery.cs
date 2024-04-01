using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Responsible.Queries.GetAllResponsiblesByStandardId
{
    internal class GetAllResponsiblesByStandardIdQuery : IGetAllResponsiblesByStandardIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllResponsiblesByStandardIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int standardId)
        {
            try
            {
                var entities = await (from responsible in _databaseService.Responsible
                                      where ((responsible.isDeleted == null || responsible.isDeleted == false) && responsible.standardId == standardId)
                                      select new ResponsibleEntity
                                      {
                                          responsibleId = responsible.responsibleId,
                                          name = responsible.name,
                                      }).ToListAsync();

                BaseResponseDto<GetAllResponsiblesByStandardIdDto> baseResponseDto = new BaseResponseDto<GetAllResponsiblesByStandardIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllResponsiblesByStandardIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

