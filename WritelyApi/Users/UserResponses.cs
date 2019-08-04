using WritelyApi.Helpers;

namespace WritelyApi.Users
{
    public static class UserResponses
    {
        public const string AccountCreated = "Account created";
        public const string AccountCreationFailed = "Unable to create account";
        public const string LoginSucceeded = "Logged in";
        public const string LoginFailed = "Invalid email or password";
        public const string LoggedOut = "Logged out";
        public const string EmailUpdated = "Email updated";
        public const string PasswordUpdated = "Password updated";
        public const string AccountDeleted = "Account deleted";
        public const string AccountDeletionFailed = "Unable to delete account";
    }
}