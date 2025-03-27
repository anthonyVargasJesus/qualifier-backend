using System.Transactions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;

namespace Qualifier.Application.Database.Scope.Commands.CreateScope

{
    public class CreateScopeCommand : ICreateScopeCommand
    {

        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IScopeRepository _scopeRepository;

        public CreateScopeCommand(IDatabaseService databaseService, IMapper mapper, IScopeRepository scopeRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _scopeRepository = scopeRepository;
        }

        public async Task<Object> Execute(CreateScopeDto model)
        {
            try
            {
                Notification notification = this.createValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var entity = _mapper.Map<ScopeEntity>(model);
                    entity.creationDate = DateTime.UtcNow;
                    entity.creationUserId = model.creationUserId;
                    await _databaseService.Scope.AddAsync(entity);
                    await _databaseService.SaveAsync();

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
            catch (Exception)
            {
             return BaseApplication.getExceptionErrorResponse();
            }
        }

        private Notification createValidation(CreateScopeDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

