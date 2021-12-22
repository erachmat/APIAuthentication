using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIAuthentication.Models;
using APIAuthentication.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APIAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : Controller
    {
        dbXamarinContext dbXamarinContext = new dbXamarinContext();

        [HttpPost]
        public string Post([FromBody]TbUser value) {
            if (!dbXamarinContext.TbUsers.Any(user => user.Username.Equals(value.Username))) {

                TbUser user = new TbUser();
                user.Username = value.Username;
                user.Salt = Convert.ToBase64String(Common.GetRandomSalt(16));
                user.Password = Convert.ToBase64String(Common.SaltHashPassword(
                    Encoding.ASCII.GetBytes(value.Password),
                    Convert.FromBase64String(user.Salt)));

                try
                {
                    dbXamarinContext.Add(user);
                    dbXamarinContext.SaveChanges();
                    return JsonConvert.SerializeObject("Register successfully");
                }
                catch (Exception ex) {
                    return JsonConvert.SerializeObject(ex.Message);
                }
            }
            else {
                return JsonConvert.SerializeObject("User is existing in Database");
            }
        }
    }
}
