

using Hotel_Managment.Domain.Entities.Common;

namespace Hotel_Managment.Domain.Entities
{
    public class Servicee : BaseNameable
    {

        public int Id { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }
}
