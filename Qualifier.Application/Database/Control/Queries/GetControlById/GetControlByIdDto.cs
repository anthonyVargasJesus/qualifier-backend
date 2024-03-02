namespace Qualifier.Application.Database.Control.Queries.GetControlById
{
    public class GetControlByIdDto
    {
        public int controlId { get; set; }
        public int number { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int controlGroupId { get; set; }
        public int standardId { get; set; }

    }
}

