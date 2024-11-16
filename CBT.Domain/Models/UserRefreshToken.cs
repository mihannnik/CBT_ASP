namespace CBT.Domain.Models
{
    public class UserRefreshToken
    {
        public required string RefreshToken { get; set; }
        public required Guid UserId { get; set; }
        public required DateTime ExpireAt { get; set; }
    }
}
