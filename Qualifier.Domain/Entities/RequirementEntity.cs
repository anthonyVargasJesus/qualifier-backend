using Qualifier.Common.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qualifier.Domain.Entities
{
    public class RequirementEntity : BaseEntity
    {
        public int requirementId { get; set; }
        public int numeration { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int standardId { get; set; }
        public int level { get; set; }
        public int parentId { get; set; }
        public bool isEvaluable { get; set; }
        public int companyId { get; set; }
        public RequirementEntity requirement { get; set; }
        public ICollection<RequirementEntity> requirements { get; set; }
        public ICollection<RequirementEvaluationEntity> requirementEvaluations { get; set; }


        [NotMapped]
        public string numerationToShow { get; set; }
        [NotMapped]
        public List<RequirementEntity> children { get; set; }

        [NotMapped]
        public List<MaturityLevelEntity> maturityLevels { get; set; }
        [NotMapped]
        public decimal value { get; set; }
        [NotMapped]
        public IndicatorEntity? indicator { get; set; }

        public void setIndicator(List<IndicatorEntity> indicators, decimal? value)
        {
            //const int ID_BASIC_INDICATOR = 3;
            //const int ID_MEDIUM_INDICATOR = 2;
            //const int ID_ADVANCED_INDICATOR = 1;
            const int ID_NOT_APPLICABLE_INDICATOR = 4;
            const int ID_NOT_IMPLEMENTED_INDICATOR = 5;

            if (value == null)
                indicator = indicators.FirstOrDefault(x => x.indicatorId == ID_NOT_APPLICABLE_INDICATOR);

            if (value == 0)
                indicator = indicators.FirstOrDefault(x => x.indicatorId == ID_NOT_IMPLEMENTED_INDICATOR);

            foreach (IndicatorEntity item in indicators)
                if (item.minimum != null && item.maximum != null)
                    if (value > item.minimum.Value && value <= item.maximum.Value)
                        indicator = item;

        }


        //public decimal CalculateIndicator()
        //{

        //}





    }
}

