using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.BreachStatus.Queries.GetAllBreachStatussByCompanyId
{
    internal class GetAllBreachStatussByCompanyIdQuery : IGetAllBreachStatussByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllBreachStatussByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var entities = await (from breachStatus in _databaseService.BreachStatus
                                      where ((breachStatus.isDeleted == null || breachStatus.isDeleted == false) && breachStatus.companyId == companyId)
                                      select new BreachStatusEntity
                                      {
                                          breachStatusId = breachStatus.breachStatusId,
                                          name = breachStatus.name,
                                      }).ToListAsync();

                BaseResponseDto<GetAllBreachStatussByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetAllBreachStatussByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllBreachStatussByCompanyIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception ex)
            {
                throw ex;
                //return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

