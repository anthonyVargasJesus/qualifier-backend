namespace Qualifier.Application.Database.ControlType.Queries.GetAllControlTypesByCompanyId
{
    public class GetAllControlTypesByCompanyIdDto
    {
        public int controlTypeId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string abbreviation { get; set; }
        public string color { get; set; }
    }
}

