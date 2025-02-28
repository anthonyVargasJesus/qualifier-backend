using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Policy.Queries.GetPolicyById
{
    public class GetPolicyByIdQuery : IGetPolicyByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetPolicyByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int policyId)
        {
            try
            {
                var entity = await (from item in _databaseService.Policy
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.policyId == policyId)
                                    select new PolicyEntity()
                                    {
                                        policyId = item.policyId,
                                        isCurrent = item.isCurrent,
                                        date = item.date,
                                        name = item.name,
                                        description = item.description,
                                        standardId = item.standardId,
                                        companyId = item.companyId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetPolicyByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

