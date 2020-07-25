using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWT.NetCore.Data;
using JWT.Utilities.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace JWT.NetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        static bool RowsAdded = false;
        static AccountController()
        {

        }

        IConfiguration configuration;
        ApplicationDBContext context;
        public AccountController(IConfiguration configuration, ApplicationDBContext dBContext)
        {
            this.configuration = configuration;
            context = dBContext;

            if(!RowsAdded)
            {

                context.CompanyInfo.Add(new CompanyInfo
                {
                    ID = 1,
                    CompanyKey = "ABC",
                    Name = "ABC LLC",
                    LocationKey = "123"
                });

                context.SaveChanges();

                context.Users.AddRange(
                    new UserInformation
                    {
                        Id = Guid.NewGuid(),
                        Email = "1.test@test.com",
                        UserName = "1test",
                        CompanyId = 1,
                        EmailConfirmed = true,
                        PasswordHash = "123456",
                        PhoneNumber = "12345678990"
                    },
                    new UserInformation
                    {
                        Id = Guid.NewGuid(),
                        Email = "3.test@test.com",
                        UserName = "3test",
                        CompanyId = 1,
                        EmailConfirmed = true,
                        PasswordHash = "123456",
                        PhoneNumber = "12345678990"
                    }
                );
                context.SaveChanges();

                context.Users.Add(
                   new UserInformation
                   {
                       Id = Guid.NewGuid(),
                       Email = "2.test@test.com",
                       UserName = "2test",
                       CompanyId = 1,
                       EmailConfirmed = true,
                       PasswordHash = "123456",
                       PhoneNumber = "12345678990"
                   }
               );
                context.SaveChanges();

                RowsAdded = true;
            }
        }

        [HttpPost]
        [Route("Token")]

        public string GetToken(LoginModel loginModel)
        {

            var userInfo = context.Users.FirstOrDefault(f => f.UserName == loginModel.UserName);
            if (userInfo == null)
                throw new UnauthorizedAccessException();

            if (userInfo.PasswordHash != loginModel.Password)
                throw new UnauthorizedAccessException();

            var companyInfo = context.CompanyInfo.FirstOrDefault(f => f.ID == userInfo.CompanyId);




            var token = JWTManager.GenerateToken(userInfo, companyInfo);
            return token;

        }


    }
}