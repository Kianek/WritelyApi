namespace WritelyApi.Users
{
    public class UserPasswordChangeDto
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}