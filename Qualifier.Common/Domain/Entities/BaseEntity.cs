using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qualifier.Common.Domain.Entities
{
    public class BaseEntity
    {
        public DateTime? creationDate { get; set; }
        public DateTime? updateDate { get; set; }
        public int? creationUserId { get; set; }
        public int? updateUserId { get; set; }
        public bool? isDeleted { get; set; }
    }
}
