using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Interfaces;

namespace Qualifier.Application.Database.RequirementEvaluation.Commands.DeleteRequirementEvaluation
{
    public class DeleteRequirementEvaluationCommand : IDeleteRequirementEvaluationCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IRequirementEvaluationRepository _requirementEvaluationRepository;

        public DeleteRequirementEvaluationCommand(IDatabaseService databaseService, IRequirementEvaluationRepository requirementEvaluationRepository)
        {
            _databaseService = databaseService;
            _requirementEvaluationRepository = requirementEvaluationRepository;
        }

        public async Task<Object> Execute(int id, int updateUserId)
        {
            try
            {
                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _requirementEvaluationRepository.Delete(id, updateUserId);

                BaseResponseCommandDto baseResponseCommandDto = new BaseResponseCommandDto();
                baseResponseCommandDto.response = "¡Registro eliminado!";
                return baseResponseCommandDto;

            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        private async Task<Notification> existsValidationAsync(int id)
        {
            Notification notification = new Notification();
            int total = await _databaseService.RequirementEvaluation.CountAsync(item => item.requirementEvaluationId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

    }
}

