using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.ControlEvaluation.Commands.UpdateControlEvaluation
{
    public class UpdateControlEvaluationCommand : IUpdateControlEvaluationCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IControlEvaluationRepository _controlEvaluationRepository;

        public UpdateControlEvaluationCommand(IDatabaseService databaseService, IMapper mapper, IControlEvaluationRepository controlEvaluationRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _controlEvaluationRepository = controlEvaluationRepository;
        }

        public async Task<Object> Execute(UpdateControlEvaluationDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _controlEvaluationRepository.Update(id, _mapper.Map<ControlEvaluationEntity>(model));

                if (model.referenceDocumentations != null)
                {

                    await _databaseService.ReferenceDocumentation.Where(c => c.controlEvaluationId == id).ExecuteDeleteAsync();

                    foreach (var item in model.referenceDocumentations)
                    {
                        item.companyId = model.companyId;
                        var entity2 = _mapper.Map<ReferenceDocumentationEntity>(item);
                        entity2.controlEvaluationId = id;
                        entity2.evaluationId = item.evaluationId;
                        entity2.creationDate = DateTime.UtcNow;
                        entity2.creationUserId = model.updateUserId;
                        await _databaseService.ReferenceDocumentation.AddAsync(entity2);
                        await _databaseService.SaveAsync();
                    }

                }

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
            int total = await _databaseService.ControlEvaluation.CountAsync(item => item.controlEvaluationId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateControlEvaluationDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

