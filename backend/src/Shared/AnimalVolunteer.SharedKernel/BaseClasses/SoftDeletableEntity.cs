namespace AnimalVolunteer.SharedKernel.BaseClasses;
public abstract class SoftDeletableEntity<TId> : Entity<TId> where TId : notnull
{
    protected SoftDeletableEntity(TId id) : base(id) { }
    public bool IsDeleted { get; protected set; }
    public DateTime? DeletionDateTime { get; protected set; }
    public virtual void SoftDelete()
    {
        IsDeleted = true;
        DeletionDateTime = DateTime.Now;
    }
    public virtual void Restore()
    {
        IsDeleted = false;
        DeletionDateTime = null;
    }
}
