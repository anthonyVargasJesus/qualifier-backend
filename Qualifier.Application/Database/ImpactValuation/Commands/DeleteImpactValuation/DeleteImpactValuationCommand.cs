using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Interfaces;

namespace Qualifier.Application.Database.ImpactValuation.Commands.DeleteImpactValuation
{
    public class DeleteImpactValuationCommand : IDeleteImpactValuationCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IImpactValuationRepository _impactValuationRepository;

        public DeleteImpactValuationCommand(IDatabaseService databaseService, IImpactValuationRepository impactValuationRepository)
        {
            _databaseService = databaseService;
            _impactValuationRepository = impactValuationRepository;
        }

        public async Task<Object> Execute(int id, int updateUserId)
        {
            try
            {
                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _impactValuationRepository.Delete(id, updateUserId);

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
            int total = await _databaseService.ImpactValuation.CountAsync(item => item.impactValuationId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

    }
}

