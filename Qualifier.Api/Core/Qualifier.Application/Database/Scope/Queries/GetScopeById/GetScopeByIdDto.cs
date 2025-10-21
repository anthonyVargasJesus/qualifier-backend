namespace Qualifier.Application.Database.Scope.Queries.GetScopeById
{
    public class GetScopeByIdDto
    {
        public int scopeId { get; set; }
        public bool isCurrent { get; set; }
        public DateTime date { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public int? standardId { get; set; }

    }
}

