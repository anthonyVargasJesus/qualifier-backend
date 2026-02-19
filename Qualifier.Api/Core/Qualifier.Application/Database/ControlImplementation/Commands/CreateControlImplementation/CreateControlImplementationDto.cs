using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.ControlImplementation.Commands.CreateControlImplementation
{
    public class CreateControlImplementationDto
    {
        public int controlImplementationId { get; set; }
        public int? riskId { get; set; }
        public string activities { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? verificationDate { get; set; }
        public int? responsibleId { get; set; }
        public string? observation { get; set; }
        public int? companyId { get; set; }
        public int? creationUserId { get; set; }
        public bool? isImplemented { get; set; }
        public bool? isEffective { get; set; }
        public void requiredFieldsValidation(Notification notification)
        {
            if (activities == null)
                notification.addError("El activities es obligatorio");

            if (startDate == null)
                notification.addError("El startDate es obligatorio");

            if (responsibleId == null && responsibleId <= 0)
                notification.addError("El responsibleId es obligatorio");

        }

    }
}

