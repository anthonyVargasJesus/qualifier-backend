using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.UsageClassification.Queries.GetUsageClassificationById
{
    public class GetUsageClassificationByIdQuery : IGetUsageClassificationByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetUsageClassificationByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int usageClassificationId)
        {
            try
            {
                var entity = await (from item in _databaseService.UsageClassification
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.usageClassificationId == usageClassificationId)
                                    select new UsageClassificationEntity()
                                    {
                                        usageClassificationId = item.usageClassificationId,
                                        name = item.name,
                                        companyId = item.companyId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetUsageClassificationByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

