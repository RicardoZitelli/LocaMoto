namespace LocaMotoDomain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public required string Login { get; set; }
        public required string Senha { get; set; }
        public string? Role { get; set; }
    }
}
