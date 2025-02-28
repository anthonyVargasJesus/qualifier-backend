using System.Transactions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.Policy.Commands.UpdatePolicy
{
    public class UpdatePolicyCommand : IUpdatePolicyCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IPolicyRepository _policyRepository;

        public UpdatePolicyCommand(IDatabaseService databaseService, IMapper mapper, IPolicyRepository policyRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _policyRepository = policyRepository;
        }

        public async Task<Object> Execute(UpdatePolicyDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var entity = _mapper.Map<PolicyEntity>(model);
                    await _policyRepository.Update(id, entity);

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

        private async Task<Notification> existsValidationAsync(int id)
        {
            Notification notification = new Notification();
            int total = await _databaseService.Policy.CountAsync(item => item.policyId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdatePolicyDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

