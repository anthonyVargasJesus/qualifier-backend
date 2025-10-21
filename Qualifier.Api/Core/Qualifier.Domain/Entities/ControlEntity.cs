using Qualifier.Common.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qualifier.Domain.Entities
{
    public class ControlEntity : BaseEntity
    {
        public int controlId { get; set; }
        public int number { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int controlGroupId { get; set; }
        public int standardId { get; set; }
        public int companyId { get; set; }
        public ControlGroupEntity controlGroup { get; set; }
        public StandardEntity standard { get; set; }
        public ICollection<ControlEvaluationEntity> controlEvaluations { get; set; }
        [NotMapped]
        public string numerationToShow { get; set; }
        public void setEvaluations(List<ControlEvaluationEntity> evaluations)
        {

                    controlEvaluations= evaluations.Where(x => x.controlId == controlId).ToList();

            if (controlEvaluations != null)
                if (controlEvaluations.Count == 0)
                    controlEvaluations.Add(getDefaultRequierementeEvaluation());

            //if (item.requirementEvaluations != null)
            //    setNumerationToEvaluations(item.requirementEvaluations.ToList(), item.numerationToShow);

        }

        private ControlEvaluationEntity getDefaultRequierementeEvaluation()
        {
            return new ControlEvaluationEntity
            {
                controlEvaluationId = 0,
                controlId = controlId,
                control = new ControlEntity
                {
                    controlId = controlId,
                    number = number,
                    name = name,
                },
            };
        }


    }
}

