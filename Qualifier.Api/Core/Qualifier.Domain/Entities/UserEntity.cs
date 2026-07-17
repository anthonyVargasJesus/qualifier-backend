using Qualifier.Common.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qualifier.Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        public int userId { get; set; }
        public string? name { get; set; }
        public string? middleName { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public string? uid { get; set; }
        // La tabla MAE_USER no tiene columna de password (la autenticación por
        // password está dada de baja a favor de Firebase/uid — ver LoginService,
        // que ya la tiene comentada); sin [NotMapped] EF intenta escribirla en
        // cada INSERT/UPDATE y revienta con "column password does not exist".
        [NotMapped]
        public string? password { get; set; }
        public int userStateId { get; set; }
        public string? image { get; set; }
        public string? userName { get; set; }
        public string? documentNumber { get; set; }
        public string? fcmToken { get; set; }
        public UserStateEntity userState { get; set; }
        public DateTime? lastAccess { get; set; }
        [NotMapped]
        public ICollection<RoleInUserEntity> roleInUsers { get; set; }
        [NotMapped]
        public ICollection<RoleEntity> roles { get; set; }
        public int standardId { get; set; }
        public StandardEntity standard { get; set; }
        public int companyId { get; set; }

        [NotMapped]
        public string? rolesText { get; set; }
    }
}

