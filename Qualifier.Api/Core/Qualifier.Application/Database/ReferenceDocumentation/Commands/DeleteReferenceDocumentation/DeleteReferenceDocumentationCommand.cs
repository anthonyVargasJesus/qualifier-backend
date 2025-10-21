using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Interfaces;

namespace Qualifier.Application.Database.ReferenceDocumentation.Commands.DeleteReferenceDocumentation
{
    public class DeleteReferenceDocumentationCommand : IDeleteReferenceDocumentationCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IReferenceDocumentationRepository _referenceDocumentationRepository;

        public DeleteReferenceDocumentationCommand(IDatabaseService databaseService, IReferenceDocumentationRepository referenceDocumentationRepository)
        {
            _databaseService = databaseService;
            _referenceDocumentationRepository = referenceDocumentationRepository;
        }

        public async Task<Object> Execute(int id, int updateUserId)
        {
            try
            {
                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _referenceDocumentationRepository.Delete(id, updateUserId);

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
            int total = await _databaseService.ReferenceDocumentation.CountAsync(item => item.referenceDocumentationId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

    }
}

