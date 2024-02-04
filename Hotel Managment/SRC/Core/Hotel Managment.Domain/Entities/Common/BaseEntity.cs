

namespace Hotel_Managment.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string CreatedBy { get; set; } = null!;
        public BaseEntity()
        {
            CreatedBy = "movliyev.sharafaddin";
        }
    }
}
