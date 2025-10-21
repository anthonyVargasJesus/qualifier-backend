namespace Qualifier.Application.Database.Standard.Queries.GetStandardById
{
    public class GetStandardByIdDto
    {
        public int standardId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int parentId { get; set; }
        public int companyId { get; set; }

    }
}

