namespace Qualifier.Application.Database.ActivesInventory.Queries.GetActivesInventoriesByCompanyId
{
    public class GetActivesInventoriesByCompanyIdDto
    {
        public int activesInventoryId { get; set; }
        public string number { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal valuation { get; set; }
        public GetActivesInventoriesByCompanyIdActiveTypeDto? activeType { get; set; }
        public GetActivesInventoriesByCompanyIdCustodianDto? custodian { get; set; }
        public GetActivesInventoriesByCompanyIdLocationDto? location { get; set; }
        public GetActivesInventoriesByCompanyIdMacroprocessDto? macroprocess { get; set; }
        public GetActivesInventoriesByCompanyIdOwnerDto? owner { get; set; }
        public GetActivesInventoriesByCompanyIdSubprocessDto? subprocess { get; set; }
        public GetActivesInventoriesByCompanyIdSupportTypeDto? supportType { get; set; }
        public GetActivesInventoriesByCompanyIdUsageClassificationDto? usageClassification { get; set; }
    }
    public class GetActivesInventoriesByCompanyIdActiveTypeDto
    {
        public string name { get; set; }

    }
    public class GetActivesInventoriesByCompanyIdCustodianDto
    {
        public string name { get; set; }

    }
    public class GetActivesInventoriesByCompanyIdLocationDto
    {
        public string abbreviation { get; set; }
        public string name { get; set; }

    }
    public class GetActivesInventoriesByCompanyIdMacroprocessDto
    {
        public string code { get; set; }
        public string name { get; set; }

    }
    public class GetActivesInventoriesByCompanyIdOwnerDto
    {
        public string code { get; set; }
        public string name { get; set; }

    }
    public class GetActivesInventoriesByCompanyIdSubprocessDto
    {
        public string code { get; set; }
        public string name { get; set; }

    }
    public class GetActivesInventoriesByCompanyIdSupportTypeDto
    {
        public string name { get; set; }

    }
    public class GetActivesInventoriesByCompanyIdUsageClassificationDto
    {
        public string name { get; set; }

    }
}
