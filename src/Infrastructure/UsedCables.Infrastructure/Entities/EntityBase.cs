using System.ComponentModel.DataAnnotations;

namespace UsedCables.Infrastructure.Entities
{
    public abstract class EntityBase
    {
        [Key]
        public Guid Id { get; set; }
        public int? State { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int ProcessedBy { get; set; }

        public void Activate() => State = (int)Enums.State.Active;

        public void Passivated() => State = (int)Enums.State.Passive;

        public void Delete() => State = (int)Enums.State.Deleted;
    }
}