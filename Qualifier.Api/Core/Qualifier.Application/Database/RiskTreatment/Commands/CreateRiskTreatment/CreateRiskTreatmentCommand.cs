using System.Transactions;
using AutoMapper;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;

namespace Qualifier.Application.Database.RiskTreatment.Commands.CreateRiskTreatment

{
    public class CreateRiskTreatmentCommand : ICreateRiskTreatmentCommand
    {

        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IRiskRepository _riskRepository;

        public CreateRiskTreatmentCommand(IDatabaseService databaseService, IMapper mapper, IRiskRepository repository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _riskRepository = repository;
        }

        public async Task<Object> Execute(CreateRiskTreatmentDto model)
        {
            try
            {
                Notification notification = this.createValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var entity = _mapper.Map<RiskTreatmentEntity>(model);
                    entity.creationDate = DateTime.UtcNow;
                    entity.creationUserId = model.creationUserId;
                    await _databaseService.RiskTreatment.AddAsync(entity);
                    await _databaseService.SaveAsync();

                    const int STATUS_IN_TREATMENT_ID = 4;
                    await _riskRepository.UpdateRiskStatusId(entity.riskId, STATUS_IN_TREATMENT_ID, model.creationUserId);
                    scope.Complete();
                }

                return model;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        private Notification createValidation(CreateRiskTreatmentDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

