using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    // Asigna un grupo de control (Organizacional/Personas/Físico/Tecnológico) a un usuario, para
    // que /gap/panel le muestre solo los grupos que le corresponden. Muchos a muchos: un usuario
    // puede tener varios grupos asignados.
    public class UserControlGroupEntity : BaseEntity
    {
        public int userControlGroupId { get; set; }
        public int userId { get; set; }
        public int controlGroupId { get; set; }
        public int standardId { get; set; }
        public int companyId { get; set; }
        public UserEntity? user { get; set; }
        public ControlGroupEntity? controlGroup { get; set; }
    }
}
