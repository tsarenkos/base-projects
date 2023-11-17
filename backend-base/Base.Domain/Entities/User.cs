using Base.Domain.Common;
using Base.Enums;

namespace Base.Domain.Entities
{
    public class User: IAuditableEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public string Password { get; set; }
        public UserRole UserRole { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
    }
}
