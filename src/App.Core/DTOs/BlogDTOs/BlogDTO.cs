using App.Core.DTOs.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.DTOs.BlogDTOs
{
    public class BlogDTO : BaseEntityDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
