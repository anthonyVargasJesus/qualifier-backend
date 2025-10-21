namespace Qualifier.Application.Database.Policy.Queries.GetPolicyById
{
    public class GetPolicyByIdDto
    {
        public int policyId { get; set; }
        public bool isCurrent { get; set; }
        public DateTime date { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public int? standardId { get; set; }

    }
}

