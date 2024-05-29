using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Custodian.Queries.GetCustodiansByCompanyId
{
    public class GetCustodiansByCompanyIdQuery : IGetCustodiansByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetCustodiansByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int companyId)
        {
            try
            {
                var entities = await (from custodian in _databaseService.Custodian
                                      where ((custodian.isDeleted == null || custodian.isDeleted == false) && custodian.companyId == companyId)
                                      && (custodian.name.ToUpper().Contains(search.ToUpper()))
                                      select new CustodianEntity
                                      {
                                          custodianId = custodian.custodianId,
                                          code = custodian.code,
                                          name = custodian.name,
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetCustodiansByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetCustodiansByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetCustodiansByCompanyIdDto>>(entities);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, companyId), pageSize);
                return baseResponseDto;
            }
            catch (Exception ex)
            {
                throw ex;
                //return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int companyId)
        {
            var total = await (from custodian in _databaseService.Custodian
                               where ((custodian.isDeleted == null || custodian.isDeleted == false) && custodian.companyId == companyId)
                               && (custodian.name.ToUpper().Contains(search.ToUpper()))
                               select new CustodianEntity
                               {
                                   custodianId = custodian.custodianId,
                               }).CountAsync();
            return total;
        }

    }
}

