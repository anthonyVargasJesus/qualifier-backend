using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Approver.Queries.GetApproverById
{
    public class GetApproverByIdQuery : IGetApproverByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetApproverByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int approverId)
        {
            try
            {
                var entity = await (from item in _databaseService.Approver
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.approverId == approverId)
                                    select new ApproverEntity()
                                    {
                                        approverId = item.approverId,
                                        personalId = item.personalId,
                                        responsibleId = item.responsibleId,
                                        versionId = item.versionId,
                                        documentationId = item.documentationId,
                                        companyId = item.companyId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetApproverByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

