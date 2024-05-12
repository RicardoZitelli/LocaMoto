namespace LocaMoto.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public bool Active { get; set; }
        public required string UserEmail { get; set; }
        public required string Password { get; set; }
        public string? Role { get; set; }
        public DateTime CreationDate { get; set; }        
        public DateTime? LastLoginDate { get; set;}                    
        public bool UserEmailConfirmed { get; set;}        
    }
}
