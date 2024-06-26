using Qualifier.Common.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qualifier.Domain.Entities
{
    public class ControlGroupEntity : BaseEntity
    {
        public int controlGroupId { get; set; }
        public int number { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int? standardId { get; set; }
        public int? companyId { get; set; }

        [NotMapped]
        public List<ControlEntity> controls { get; set; }

        [NotMapped]
        public List<MaturityLevelEntity> maturityLevels { get; set; }
        [NotMapped]
        public decimal value { get; set; }
        [NotMapped]
        public IndicatorEntity? indicator { get; set; }

        public void setIndicator(List<IndicatorEntity> indicators, decimal? value)
        {
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

    }
}

