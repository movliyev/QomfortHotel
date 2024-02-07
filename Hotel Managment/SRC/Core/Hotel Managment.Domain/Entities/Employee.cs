using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Managment.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string EmployeeImage { get; set; }
        public List<Setting> Settings { get; set; }
        public string Name { get; set; }
        public int EmployeeTypeId { get; set; }
        public EmployeeType EmployeeType { get; set; }
    }
}
