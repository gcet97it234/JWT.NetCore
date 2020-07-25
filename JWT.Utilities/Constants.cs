using System;
using System.Collections.Generic;
using System.Text;

namespace JWT.Utilities
{
    public class Constants
    {
        public static string SecretKey = "";//"asdv234234^&%&^%&^hjsdfb2%%%";
        public const string UserNameClaim = "UserName";
        public const string CompanyNameClaim = "CompanyName";
        public const string EmailClaim = "Email";
        public const string EmailConfirmedClaim = "EmailConfirmed";
        public const string TwoFactorCodeClaim = "TwoFactorCode";

        public static string Issuer = "";
        public static string Audience = "";
    }
}
