using App.Core.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Entities
{
    public class Blog : AuditableEntity
    {
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
