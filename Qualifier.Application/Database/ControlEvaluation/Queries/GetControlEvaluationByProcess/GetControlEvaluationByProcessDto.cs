using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qualifier.Application.Database.ControlEvaluation.Queries.GetControlEvaluationByProcess
{
    public class GetControlEvaluationsByProcessDto
    {
        public long controlEvaluationId { get; set; }
        public int maturityLevelId { get; set; }
        public decimal value { get; set; }
        public int responsibleId { get; set; }
        public string justification { get; set; }
        public string improvementActions { get; set; }
        public string controlDescription { get; set; }
        public string controlType { get; set; }
        public GetControlEvaluationsByProcessMaturityLevelDto? maturityLevel { get; set; }
        public GetControlEvaluationsByProcessResponsibleDto? responsible { get; set; }
        public GetControlEvaluationsByProcessControlDto? control { get; set; }
    }

    public class GetControlEvaluationsByProcessControlGroupDto
    {
        public int controlGroupId { get; set; }
        public int number { get; set; }
        public string name { get; set; }
        public List<GetControlEvaluationsByProcessControlDto> controls { get; set; }
    }

    public class GetControlEvaluationsByProcessControlDto
    {
        public int controlId { get; set; }
        public int number { get; set; }
        public string name { get; set; }
        public string numerationToShow { get; set; }
        public List<GetControlEvaluationsByProcessDto> controlEvaluations { get; set; }
    }


    public class GetControlEvaluationsByProcessMaturityLevelDto
    {
        public string abbreviation { get; set; }
        public string color { get; set; }

    }
    public class GetControlEvaluationsByProcessResponsibleDto
    {
        public string name { get; set; }

    }
}
