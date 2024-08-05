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

                var entity = _mapper.Map<EvaluationEntity>(model);
                await _evaluationRepository.Update(id, entity);

                    if (entity.isCurrent)
                    {
                        var evaluations = await (from evaluation in _databaseService.Evaluation
                                                 join evaluationState in _databaseService.EvaluationState 
                                                 on evaluation.evaluationState equals evaluationState
                                                 where ((evaluation.isDeleted == null || evaluation.isDeleted == false) 
                                                 && evaluation.companyId == model.companyId)
                                                 && (evaluation.isCurrent == true)
                                                 select new EvaluationEntity
                                                 {
                                                     evaluationId = evaluation.evaluationId,
                                                 }
                                              ).ToListAsync();

                        foreach (var evaluation in evaluations.Where(e => e.evaluationId != entity.evaluationId))
                            await this._evaluationRepository.UpdateCurrentState(evaluation.evaluationId, false);

                    }
                

                return model;
            }
            catch (Exception ex)
            {
             return BaseApplication.getExceptionErrorResponse();
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

