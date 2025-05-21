namespace Qualifier.Application.Database.ControlImplementation.Queries.GetControlImplementationById
{
    public class GetControlImplementationByIdDto
    {
        public int controlImplementationId { get; set; }
        public int? riskId { get; set; }
        public string activities { get; set; }
        public DateTime startDate { get; set; }
        public DateTime? verificationDate { get; set; }
        public int responsibleId { get; set; }
        public string? observation { get; set; }

    }
}

