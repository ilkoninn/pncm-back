using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Entities.Commons
{
    public abstract class AuditableEntity : BaseEntity
    {
        public DateTimeOffset CreatedOn { get; set; }
        public string CreatedById { get; set; }

        public DateTimeOffset LastModifiedOn { get; set; }
        public string LastModifiedById { get; set; }

        public string? DeletedById { get; set; }
        public DateTimeOffset? DeletedOn { get; set; }

        public bool IsDeleted { get; set; } 
        public bool IsActive { get; set; } 
    }
}
