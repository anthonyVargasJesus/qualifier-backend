using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.RiskAssessment.Commands.UpdateRiskAssessment
{
    public class UpdateRiskAssessmentCommand : IUpdateRiskAssessmentCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IRiskAssessmentRepository _riskAssessmentRepository;

        public UpdateRiskAssessmentCommand(IDatabaseService databaseService, IMapper mapper, IRiskAssessmentRepository riskAssessmentRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _riskAssessmentRepository = riskAssessmentRepository;
        }

        public async Task<Object> Execute(UpdateRiskAssessmentDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _riskAssessmentRepository.Update(id, _mapper.Map<RiskAssessmentEntity>(model));

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
            int total = await _databaseService.RiskAssessment.CountAsync(item => item.riskAssessmentId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateRiskAssessmentDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

