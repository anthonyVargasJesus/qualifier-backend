using System.Transactions;
using AutoMapper;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;

namespace Qualifier.Application.Database.RiskAssessment.Commands.CreateRiskAssessment

{
    public class CreateRiskAssessmentCommand : ICreateRiskAssessmentCommand
    {

        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IRiskRepository _riskRepository;

        public CreateRiskAssessmentCommand(IDatabaseService databaseService, IMapper mapper, IRiskRepository riskRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _riskRepository = riskRepository;
        }

        public async Task<Object> Execute(CreateRiskAssessmentDto model)
        {
            try
            {
                Notification notification = this.createValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var entity = _mapper.Map<RiskAssessmentEntity>(model);
                    entity.creationDate = DateTime.UtcNow;
                    entity.creationUserId = model.creationUserId;
                    await _databaseService.RiskAssessment.AddAsync(entity);
                    await _databaseService.SaveAsync();

                    const int STATUS_IN_EVALUATION_ID = 2;
                    await _riskRepository.UpdateRiskStatusId(entity.riskId, STATUS_IN_EVALUATION_ID, model.creationUserId);
                    scope.Complete();
                }
                return model;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        private Notification createValidation(CreateRiskAssessmentDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

