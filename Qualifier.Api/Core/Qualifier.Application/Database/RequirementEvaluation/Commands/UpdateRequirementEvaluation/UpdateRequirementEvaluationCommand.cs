using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.RequirementEvaluation.Commands.UpdateRequirementEvaluation
{
    public class UpdateRequirementEvaluationCommand : IUpdateRequirementEvaluationCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IRequirementEvaluationRepository _requirementEvaluationRepository;
        private readonly IEvaluationRepository _evaluationRepository;

        public UpdateRequirementEvaluationCommand(IDatabaseService databaseService, IMapper mapper, IRequirementEvaluationRepository requirementEvaluationRepository, IEvaluationRepository evaluationRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _requirementEvaluationRepository = requirementEvaluationRepository;
            _evaluationRepository = evaluationRepository;
        }

        public async Task<Object> Execute(UpdateRequirementEvaluationDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _requirementEvaluationRepository.Update(id, _mapper.Map<RequirementEvaluationEntity>(model));


                if (model.referenceDocumentations != null)
                {

                    await _databaseService.ReferenceDocumentation.Where(c => c.requirementEvaluationId == id).ExecuteDeleteAsync();

                    foreach (var item in model.referenceDocumentations)
                    {
                        item.companyId = model.companyId;
                        var entity2 = _mapper.Map<ReferenceDocumentationEntity>(item);
                        entity2.requirementEvaluationId = id;
                        entity2.evaluationId = item.evaluationId;
                        entity2.creationDate = DateTime.UtcNow;
                        entity2.creationUserId = model.updateUserId;
                        await _databaseService.ReferenceDocumentation.AddAsync(entity2);
                        await _databaseService.SaveAsync();
                    }

                }

                var requirementEvaluation = await (from item in _databaseService.RequirementEvaluation
                                                   where ((item.isDeleted == null || item.isDeleted == false) && item.requirementEvaluationId == id)
                                                   select new RequirementEvaluationEntity()
                                                   {
                                                       evaluationId = item.evaluationId,
                                                   }).FirstOrDefaultAsync();

                const int EDITION_EVALUATION_STATE_ID = 2;
                if (requirementEvaluation != null)
                    await _evaluationRepository.UpdateState(requirementEvaluation.evaluationId, EDITION_EVALUATION_STATE_ID);

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
            int total = await _databaseService.RequirementEvaluation.CountAsync(item => item.requirementEvaluationId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateRequirementEvaluationDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

