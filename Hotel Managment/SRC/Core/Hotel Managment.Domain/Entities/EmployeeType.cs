using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Managment.Domain.Entities
{
    public class EmployeeType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Employee>? Employes { get; set; }
    }
}
