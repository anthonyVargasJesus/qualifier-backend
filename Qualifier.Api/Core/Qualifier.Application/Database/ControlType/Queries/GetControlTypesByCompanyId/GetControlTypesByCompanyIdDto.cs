namespace Qualifier.Application.Database.ControlType.Queries.GetControlTypesByCompanyId
{
    public class GetControlTypesByCompanyIdDto
    {
        public int controlTypeId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string abbreviation { get; set; }
        public string color { get; set; }
    }
}

