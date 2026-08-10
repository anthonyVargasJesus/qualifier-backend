using Qualifier.Common.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace Qualifier.Domain.Entities
{
    public class ControlGroupEntity : BaseEntity
    {
        public int controlGroupId { get; set; }
        // decimal (no int): el Anexo A de algunas normas (ej. NTP-ISO/IEC 42001) tiene
        // sub-grupos anidados que el documento numera "6.1"/"6.2" — un simple int solo
        // alcanzaba para grupos "planos" tipo ISO 27001 (5, 6, 7...).
        public decimal number { get; set; }
        public string name { get; set; } = string.Empty;
        public string? description { get; set; }
        public int? standardId { get; set; }
        public int? companyId { get; set; }

        [NotMapped]
        public List<ControlEntity>? controls { get; set; }

        [NotMapped]
        public List<MaturityLevelEntity>? maturityLevels { get; set; }
        [NotMapped]
        public decimal value { get; set; }
        [NotMapped]
        public IndicatorEntity? indicator { get; set; }
        [NotMapped]
        public decimal indicatorValue { get; set; }

        // "6" para grupos enteros (ISO 27001), "6.1"/"6.2" para sub-grupos (NTP 42001) — sin
        // ceros de más (6.10 no) ni separador decimal dependiente de la cultura del servidor
        // (6,1 no). Único punto de formateo: los 3 lugares que arman el numerationToShow de un
        // control (StandardEntity.setControlsWithChildren, GapItemsBuilder,
        // GetControlEvaluationByProcessQuery.setNumeration) deben usar esto en vez de
        // concatenar `number` crudo.
        [NotMapped]
        public string numberToShow => number.ToString("0.##", CultureInfo.InvariantCulture);

        public void setIndicator(List<IndicatorEntity> indicators, decimal? value)
        {
            if (value.HasValue)
                indicatorValue = value.Value;

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

