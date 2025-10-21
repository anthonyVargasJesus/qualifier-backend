using System.Transactions;
using AutoMapper;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;

namespace Qualifier.Application.Database.ControlImplementation.Commands.CreateControlImplementation

{
    public class CreateControlImplementationCommand : ICreateControlImplementationCommand
    {

        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IRiskRepository _riskRepository;

        public CreateControlImplementationCommand(IDatabaseService databaseService, IMapper mapper, IRiskRepository riskRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _riskRepository = riskRepository;
        }

        public async Task<Object> Execute(CreateControlImplementationDto model)
        {
            try
            {
                Notification notification = this.createValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var entity = _mapper.Map<ControlImplementationEntity>(model);
                    entity.creationDate = DateTime.UtcNow;
                    entity.creationUserId = model.creationUserId;
                    await _databaseService.ControlImplementation.AddAsync(entity);
                    await _databaseService.SaveAsync();

                    model.controlImplementationId = entity.controlImplementationId;

                    //const int STATUS_WITH_CONTROL_ID = 5;
                    //await _riskRepository.UpdateRiskStatusId(entity.riskId, STATUS_WITH_CONTROL_ID, model.creationUserId);

                    scope.Complete();
                }

                return model;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        private Notification createValidation(CreateControlImplementationDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

