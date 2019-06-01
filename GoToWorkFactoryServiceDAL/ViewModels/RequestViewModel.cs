using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoToWorkFactoryServiceDAL.ViewModels
{
    public class RequestViewModel
    {
        public int Id { get; set; }

        public int Count { get; set; }

        public int MaterialId { get; set; }

        public string MaterialName { get; set; }

        public string ImplementDate { get; set; }
    }
}
