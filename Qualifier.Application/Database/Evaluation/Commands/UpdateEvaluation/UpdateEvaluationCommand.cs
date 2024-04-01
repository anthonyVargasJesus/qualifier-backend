using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.Evaluation.Commands.UpdateEvaluation
{
    public class UpdateEvaluationCommand : IUpdateEvaluationCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IEvaluationRepository _evaluationRepository;

        public UpdateEvaluationCommand(IDatabaseService databaseService, IMapper mapper, IEvaluationRepository evaluationRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _evaluationRepository = evaluationRepository;
        }

        public async Task<Object> Execute(UpdateEvaluationDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _evaluationRepository.Update(id, _mapper.Map<EvaluationEntity>(model));

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
            int total = await _databaseService.Evaluation.CountAsync(item => item.evaluationId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateEvaluationDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

