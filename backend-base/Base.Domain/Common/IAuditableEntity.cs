namespace Base.Domain.Common
{
    public interface IAuditableEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
    }
}
