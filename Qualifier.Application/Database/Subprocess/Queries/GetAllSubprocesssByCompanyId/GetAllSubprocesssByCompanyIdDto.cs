
namespace Qualifier.Application.Database.Subprocess.Queries.GetAllSubprocesssByCompanyId
{
    public class GetAllSubprocesssByCompanyIdDto
    {
        public int subprocessId { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public int macroprocessId { get; set; }
        public GetAllSubprocesssByCompanyIdMacroprocessDto? macroprocess { get; set; }
    }
    public class GetAllSubprocesssByCompanyIdMacroprocessDto
    {
        public string code { get; set; }
        public string name { get; set; }

    }
}
