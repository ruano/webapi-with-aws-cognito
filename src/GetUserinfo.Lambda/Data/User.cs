namespace GetUserinfo.Lambda.Data
{
    public class User
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public IReadOnlyCollection<Permission> Permissions { get; set; }
    }
}
