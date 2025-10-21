using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Custodian.Queries.GetCustodianById
{
    public class GetCustodianByIdQuery : IGetCustodianByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetCustodianByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int custodianId)
        {
            try
            {
                var entity = await (from item in _databaseService.Custodian
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.custodianId == custodianId)
                                    select new CustodianEntity()
                                    {
                                        custodianId = item.custodianId,
                                        code = item.code,
                                        name = item.name,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetCustodianByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

