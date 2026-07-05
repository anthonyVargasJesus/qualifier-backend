using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    // Asigna una familia de cláusula (fila de nivel 1 de MAE_REQUIREMENT, ej. "4. Contexto de la
    // organización") a un usuario, para que /gap/panel le muestre solo las familias que le
    // corresponden. Muchos a muchos: un usuario puede tener varias familias asignadas.
    public class UserRequirementFamilyEntity : BaseEntity
    {
        public int userRequirementFamilyId { get; set; }
        public int userId { get; set; }
        public int requirementId { get; set; }
        public int standardId { get; set; }
        public int companyId { get; set; }
        public UserEntity? user { get; set; }
        public RequirementEntity? requirement { get; set; }
    }
}
