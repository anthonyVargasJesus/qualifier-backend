using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;
using System.Transactions;

namespace Qualifier.Application.Database.Evaluation.Commands.CreateEvaluation

{
    public class CreateEvaluationCommand : ICreateEvaluationCommand
    {

        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IEvaluationRepository _evaluationRepository;

        public CreateEvaluationCommand(IDatabaseService databaseService, IMapper mapper, IEvaluationRepository evaluationRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _evaluationRepository = evaluationRepository;
        }

        public async Task<Object> Execute(CreateEvaluationDto model)
        {
            try
            {
                Notification notification = this.createValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                if (model.isGapAnalysis != null)
                    if (model.isGapAnalysis.Value)
                    {
                        string title = "Evaluación no guardada";
                        Notification notification2 = await this.uniqueGapEvaluationValidation(model.startDate.Year);
                        if (notification2.hasErrors())
                            return BaseApplication.getApplicationErrorResponseWithTitle(notification2.errors, title);
                    }

                using (TransactionScope scope = new TransactionScope())
                {
                    var entity = _mapper.Map<EvaluationEntity>(model);
                    entity.startDate = DateTime.SpecifyKind(entity.startDate, DateTimeKind.Utc);
                    entity.endDate = DateTime.SpecifyKind(entity.endDate, DateTimeKind.Utc);

                    const int INICIAL_EVALUATION_STATE_ID = 1;
                    entity.evaluationStateId = INICIAL_EVALUATION_STATE_ID;

                    entity.creationDate = DateTime.UtcNow;
                    entity.creationUserId = model.creationUserId;
                    await _databaseService.Evaluation.AddAsync(entity);
                    await _databaseService.SaveAsync();

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

                    scope.Complete();
                }

                return model;
            }
            catch (Exception ex)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        private Notification createValidation(CreateEvaluationDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

        private async Task<Notification> uniqueGapEvaluationValidation(int currentYear)
        {
            int total = await (from evaluation in _databaseService.Evaluation
                               where ((evaluation.isDeleted == null || evaluation.isDeleted == false)
                               && evaluation.startDate.Year == currentYear && evaluation.isGapAnalysis == true)
                               select new EvaluationEntity
                               {
                                   evaluationId = evaluation.evaluationId,
                               }).CountAsync();


            Notification notification = new Notification();
            if (total > 0)
                notification.addError("Ya hay un análisis de brechas para el año " + currentYear);
            return notification;
        }


    }
}

