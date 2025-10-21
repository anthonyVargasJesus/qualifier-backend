using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;

namespace Qualifier.Application.Database.RiskAssessment.Commands.DeleteRiskAssessment
{
    public class DeleteRiskAssessmentCommand : IDeleteRiskAssessmentCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IRiskAssessmentRepository _riskAssessmentRepository;
        private readonly IRiskRepository _riskRepository;

        public DeleteRiskAssessmentCommand(IDatabaseService databaseService, IRiskAssessmentRepository riskAssessmentRepository, IRiskRepository riskRepository)
        {
            _databaseService = databaseService;
            _riskAssessmentRepository = riskAssessmentRepository;
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
                    var riskAssessment = await (from item in _databaseService.RiskAssessment
                                        where ((item.isDeleted == null || item.isDeleted == false) && item.riskAssessmentId == id)
                                        select new RiskAssessmentEntity()
                                        {
                                            riskId = item.riskId,
                                        }).FirstOrDefaultAsync();

                    if (riskAssessment != null)
                    {
                        const int STATUS_REGISTERED_ID = 1;
                        await _riskRepository.UpdateRiskStatusId(riskAssessment.riskId, STATUS_REGISTERED_ID, updateUserId);
                    }

                    await _riskAssessmentRepository.Delete(id, updateUserId);

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
            int total = await _databaseService.RiskAssessment.CountAsync(item => item.riskAssessmentId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

    }
}

