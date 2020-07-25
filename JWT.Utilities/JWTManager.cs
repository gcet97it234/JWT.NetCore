using Microsoft.AspNet.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JWT.Utilities;
using JWT.Utilities.Models;

namespace JWT.NetCore
{
    public class JWTManager
    {

        public static string GenerateToken(UserInformation userInformation,CompanyInfo companyInfo)
        {


            var mySecret = Utilities.Constants.SecretKey; // "asdv234234^&%&^%&^hjsdfb2%%%";
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

            var myIssuer = Utilities.Constants.Issuer;// "http://mysite.com";
            var myAudience = Utilities.Constants.Audience;// "http://myaudience.com";

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(Utilities.Constants.UserNameClaim, userInformation.UserName),
                    new Claim(Utilities.Constants.CompanyNameClaim,companyInfo.Name),
                    new Claim(Utilities.Constants.EmailClaim,userInformation.Email),
                    new Claim(Utilities.Constants.EmailConfirmedClaim,userInformation.EmailConfirmed.ToString()),
                    new Claim(Utilities.Constants.TwoFactorCodeClaim,"123"),
                    
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = myIssuer,
                Audience = myAudience,
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature),
                
                 
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var t1 = tokenHandler.WriteToken(token);

            return t1;
        }

        public static bool ValidateCurrentToken(string token)
        {
            //var mySecret = "asdv234234^&%&^%&^hjsdfb2%%%";
            //var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

            //var myIssuer = "http://mysite.com";
            //var myAudience = "http://myaudience.com";

            var mySecret = Utilities.Constants.SecretKey; // "asdv234234^&%&^%&^hjsdfb2%%%";
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

            var myIssuer = Utilities.Constants.Issuer;// "http://mysite.com";
            var myAudience = Utilities.Constants.Audience;// "http://myaudience.com";

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = myIssuer,
                    ValidAudience = myAudience,
                    IssuerSigningKey = mySecurityKey
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static string GetClaim(string token, string claimType)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            var stringClaimValue = securityToken.Claims.First(claim => claim.Type == claimType).Value;
            return stringClaimValue;
        }

    }

}
