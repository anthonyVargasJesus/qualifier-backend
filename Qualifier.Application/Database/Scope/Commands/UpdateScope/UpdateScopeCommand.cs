using System.Transactions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.Scope.Commands.UpdateScope
{
    public class UpdateScopeCommand : IUpdateScopeCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IScopeRepository _scopeRepository;

        public UpdateScopeCommand(IDatabaseService databaseService, IMapper mapper, IScopeRepository scopeRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _scopeRepository = scopeRepository;
        }

        public async Task<Object> Execute(UpdateScopeDto model, int id)
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
                    var entity = _mapper.Map<ScopeEntity>(model);
                    await _scopeRepository.Update(id, entity);

                    if (entity.isCurrent)
                    {
                        var scopes = await (from scope in _databaseService.Scope
                                            where ((scope.isDeleted == null || scope.isDeleted == false)
                                            && scope.standardId == model.standardId)
                                            && (scope.isCurrent == true)
                                            select new ScopeEntity
                                            {
                                                scopeId = scope.scopeId,
                                            }
                                              ).ToListAsync();

                        foreach (var scope in scopes.Where(e => e.scopeId != entity.scopeId))
                            await this._scopeRepository.UpdateCurrentState(scope.scopeId, false);

                    }

                    transactionScope.Complete();

                }


                return model;
            }
            catch (Exception ex)
            {
                throw ex;
                //return BaseApplication.getExceptionErrorResponse();
            }
        }

        private async Task<Notification> existsValidationAsync(int id)
        {
            Notification notification = new Notification();
            int total = await _databaseService.Scope.CountAsync(item => item.scopeId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateScopeDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

