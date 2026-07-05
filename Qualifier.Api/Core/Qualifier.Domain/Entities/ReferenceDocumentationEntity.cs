using Qualifier.Common.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qualifier.Domain.Entities
{
    public class ReferenceDocumentationEntity : BaseEntity
    {
        public long referenceDocumentationId { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string description { get; set; }
        // Nullable porque el alta rápida de evidencia (botones "Adjuntar archivo"/"Agregar
        // enlace") ya no obliga a elegir un tipo de documento del catálogo.
        public int? documentationId { get; set; }
        public long? requirementEvaluationId { get; set; }
        public long? controlEvaluationId { get; set; }
        public int evaluationId { get; set; }
        public int companyId { get; set; }
        // Distingue si la evidencia es un archivo subido (ARCHIVO) o un enlace externo (ENLACE);
        // determina qué metadato mostrar (tamaño vs. solo fecha) en la tarjeta compacta.
        public string? evidenceType { get; set; }
        // Tamaño en bytes del archivo subido a Firebase Storage; null para evidencias de tipo enlace.
        public long? fileSizeBytes { get; set; }
        // Marca manual (no automática por fecha): el responsable la marca cuando la evidencia
        // ya no aplica (ej. una política reemplazada), sin necesidad de borrarla.
        public bool isObsolete { get; set; }
        public DocumentationEntity documentation { get; set; }
        public RequirementEvaluationEntity requirementEvaluation { get; set; }
        [NotMapped]
        public RequirementEntity? requirement { get; set; }
        [NotMapped]
        public ControlEntity? control { get; set; }
        // Email de quien registró la evidencia, resuelto vía join a User en las consultas de
        // listado (no es una relación EF real, solo se llena para mostrarlo en la tarjeta).
        [NotMapped]
        public string? creationUserEmail { get; set; }
    }
}

