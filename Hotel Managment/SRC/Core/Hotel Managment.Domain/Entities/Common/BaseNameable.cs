using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Managment.Domain.Entities.Common
{
    public abstract class BaseNameable : BaseEntity
    {
        public string Name { get; set; } = null!;
    }
}
