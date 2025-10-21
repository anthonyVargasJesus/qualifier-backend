namespace Qualifier.Application.Database.Responsible.Queries.GetResponsibleById
{
    public class GetResponsibleByIdDto
    {
        public int responsibleId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int standardId { get; set; }
        public int companyId { get; set; }

    }
}

