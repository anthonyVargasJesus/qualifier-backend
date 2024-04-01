using Qualifier.Common.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qualifier.Domain.Entities
{
    public class MaturityLevelEntity: BaseEntity
    {
        public int maturityLevelId { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public string? abbreviation { get; set; }
        public decimal value { get; set; }
        public string? color { get; set; }
        public decimal? factor { get; set; }
        public int companyId { get; set; }
        public ICollection<RequirementEvaluationEntity> requirementEvaluations { get; set; }
        public ICollection<ControlEvaluationEntity> controlEvaluations { get; set; }
        [NotMapped]
        public decimal percent { get; set; }
    }

}
