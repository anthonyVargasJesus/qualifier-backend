using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;

namespace Qualifier.Application.Database.RiskTreatment.Commands.DeleteRiskTreatment
{
    public class DeleteRiskTreatmentCommand : IDeleteRiskTreatmentCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IRiskTreatmentRepository _riskTreatmentRepository;
        private readonly IRiskRepository _riskRepository;

        public DeleteRiskTreatmentCommand(IDatabaseService databaseService, IRiskTreatmentRepository riskTreatmentRepository, IRiskRepository riskRepository)
        {
            _databaseService = databaseService;
            _riskTreatmentRepository = riskTreatmentRepository;
            _riskRepository = riskRepository;
        }

        public async Task<Object> Execute(int id, int updateUserId)
        {
            try
            {
                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var riskTreatment = await (from item in _databaseService.RiskTreatment
                                                where ((item.isDeleted == null || item.isDeleted == false) && item.riskTreatmentId == id)
                                                select new RiskAssessmentEntity()
                                                {
                                                    riskId = item.riskId,
                                                }).FirstOrDefaultAsync();

                    if (riskTreatment != null)
                    {
                        const int STATUS_IN_EVALUATION_ID = 2;
                        await _riskRepository.UpdateRiskStatusId(riskTreatment.riskId, STATUS_IN_EVALUATION_ID, updateUserId);
                    }

                    await _riskTreatmentRepository.Delete(id, updateUserId);

                    scope.Complete();
                }

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
            int total = await _databaseService.RiskTreatment.CountAsync(item => item.riskTreatmentId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

    }
}

