namespace Qualifier.Application.Database.Subprocess.Queries.GetSubprocesssByCompanyId
{
    public class GetSubprocesssByCompanyIdDto
    {
        public int subprocessId { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public int macroprocessId { get; set; }
        public GetSubprocesssByCompanyIdMacroprocessDto? macroprocess { get; set; }
    }
    public class GetSubprocesssByCompanyIdMacroprocessDto
    {
        public string code { get; set; }
        public string name { get; set; }

    }
}
