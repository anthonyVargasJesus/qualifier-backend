using Qualifier.Common.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qualifier.Domain.Entities
{
    public class ControlEntity : BaseEntity
    {
        public int controlId { get; set; }
        public int number { get; set; }
        public string name { get; set; } = string.Empty;
        public string? description { get; set; }
        public int controlGroupId { get; set; }
        public int standardId { get; set; }
        public int companyId { get; set; }
        // Responsable sugerido para precargar ControlEvaluation.responsibleId cuando se evalúa
        // este control por primera vez en un ciclo nuevo (no reemplaza el responsable guardado
        // por evaluación, solo lo precarga).
        public int? defaultResponsibleId { get; set; }
        public ResponsibleEntity? defaultResponsible { get; set; }
        public ControlGroupEntity? controlGroup { get; set; }
        public StandardEntity? standard { get; set; }
        public ICollection<ControlEvaluationEntity>? controlEvaluations { get; set; }
        [NotMapped]
        public string numerationToShow { get; set; } = string.Empty;
        public void setEvaluations(List<ControlEvaluationEntity> evaluations)
        {
            // No debería haber más de una fila de ControlEvaluation por (controlId,
            // evaluationId), pero no hay constraint único que lo garantice — si llegan a
            // existir duplicados, nos quedamos con la más reciente (mismo criterio que usan
            // GetActionPlansByUserIdQuery/CreateControlEvaluationCommand/GapItemsBuilder) para
            // que todas las pantallas coincidan en cuál fila es "la" evaluación del control.
            controlEvaluations = evaluations
                .Where(x => x.controlId == controlId)
                .OrderByDescending(x => x.controlEvaluationId)
                .Take(1)
                .ToList();

            if (controlEvaluations.Count == 0)
                controlEvaluations.Add(getDefaultRequierementeEvaluation());

        }

        private ControlEvaluationEntity getDefaultRequierementeEvaluation()
        {
            return new ControlEvaluationEntity
            {
                controlEvaluationId = 0,
                controlId = controlId,
                // Precarga el responsable por defecto del control (si se configuró uno); sigue
                // siendo editable y solo se guarda de verdad en el primer POST de esta evaluación.
                responsibleId = defaultResponsibleId ?? 0,
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

