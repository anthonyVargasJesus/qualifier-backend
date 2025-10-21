using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Custodian.Queries.GetAllCustodiansByCompanyId
{
    internal class GetAllCustodiansByCompanyIdQuery : IGetAllCustodiansByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllCustodiansByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var entities = await (from custodian in _databaseService.Custodian
                                      where ((custodian.isDeleted == null || custodian.isDeleted == false) && custodian.companyId == companyId)
                                      select new CustodianEntity
                                      {
                                          custodianId = custodian.custodianId,
                                          code = custodian.code,
                                          name = custodian.name,
                                      }).ToListAsync();

                BaseResponseDto<GetAllCustodiansByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetAllCustodiansByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllCustodiansByCompanyIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

