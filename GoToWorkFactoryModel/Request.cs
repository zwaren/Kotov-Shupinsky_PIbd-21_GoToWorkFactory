using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoToWorkFactoryModel
{
    public class Request
    {
        public int Id { get; set; }

        public int Count { get; set; }

        public int MaterialId { get; set; }

        public DateTime ImplementDate { get; set; }

        public virtual Material Material { get; set; }
    }
}
