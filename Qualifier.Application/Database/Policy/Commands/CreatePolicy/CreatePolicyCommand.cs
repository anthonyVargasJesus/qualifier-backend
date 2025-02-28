using System.Transactions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;

namespace Qualifier.Application.Database.Policy.Commands.CreatePolicy

{
    public class CreatePolicyCommand : ICreatePolicyCommand
    {

        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IPolicyRepository _policyRepository;

        public CreatePolicyCommand(IDatabaseService databaseService, IMapper mapper, IPolicyRepository policyRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _policyRepository = policyRepository;
        }

        public async Task<Object> Execute(CreatePolicyDto model)
        {
            try
            {
                Notification notification = this.createValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

               


                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var entity = _mapper.Map<PolicyEntity>(model);
                    entity.creationDate = DateTime.UtcNow;
                    entity.creationUserId = model.creationUserId;
                    await _databaseService.Policy.AddAsync(entity);
                    await _databaseService.SaveAsync();

                    if (entity.isCurrent)
                    {
                        var policys = await (from policy in _databaseService.Policy
                                            where ((policy.isDeleted == null || policy.isDeleted == false)
                                            && policy.standardId == model.standardId)
                                            && (policy.isCurrent == true)
                                            select new PolicyEntity
                                            {
                                                policyId = policy.policyId,
                                            }
                                              ).ToListAsync();

                        foreach (var policy in policys.Where(e => e.policyId != entity.policyId))
                            await this._policyRepository.UpdateCurrentState(policy.policyId, false);

                    }

                    transactionScope.Complete();

                }

                return model;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        private Notification createValidation(CreatePolicyDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

