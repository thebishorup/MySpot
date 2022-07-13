using MySpot.Core.ValueObjects;

namespace MySpot.Core.Entities
{
    public class User
    {
        public UserId Id { get; set; }
        public Email Email { get; set; }
        public UserName UserName { get; set; }
        public Password Password { get; set; }
        public FullName FullName { get; set; }
        public Role Role { get; set; }
        public DateTime CreatedAt { get; set; }

        public User(UserId id, Email email, UserName userName, 
            Password password, FullName fullName, Role role, 
            DateTime createdAt)
        {
            Id = id;
            Email = email;
            UserName = userName;
            Password = password;
            FullName = fullName;
            Role = role;
            CreatedAt = createdAt;
        }
    }
}