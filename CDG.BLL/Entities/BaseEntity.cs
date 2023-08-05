using System.ComponentModel.DataAnnotations;

namespace CDG.BLL.Entities;

public abstract class BaseEntity
{
    [Key]
    public virtual int Id { get; protected set; }
}
