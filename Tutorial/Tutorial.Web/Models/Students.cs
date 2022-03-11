using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tutorial.Web.Models
{
    public class Students
    {
        public int id { get; set; }
        public string username { get; set; }
        public DateTime birthDate { get; set; }
        public Gender gender { get; set; }
    }
}
