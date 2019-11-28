namespace ZipPay.UserAccountService.Infrastructure
{
    public static class Constants
    {
        public const string EitherUserOrAccountMissing = "Either account or user information is missing.";

        public const string UserNotFound = "User not found.";

        public const string UserNotEligible = "Unfortunately your application is unsuccessful at this time as per ZipPay responsible lending policies.";

        public const string AccountSuccess = "Account created successfully";

        public const string ErrorCreatingAccount = "An error occured while creating a ZipPay Account.";

        public const string UserInformationMissing = "Invalid user information";

        public const string UserSuccess = "User created successfully";

        public const string EmailAddressAlreadyExists = "Email address already in use.";

        public const string ErrorCreatingUser = "An error occured while creating a ZipPay User.";
    }
}
