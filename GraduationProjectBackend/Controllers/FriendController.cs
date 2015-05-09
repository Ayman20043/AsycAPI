using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Data.Entity;

namespace GraduationProjectBackend.Controllers
{
    public class FriendController : ApiController
    {
        GPEntities db = new GPEntities();
        [HttpGet]
        [Route("api/Friend/GetAllFriends")]
        public async Task<HttpResponseMessage> GetAllFriends([FromUri]int UserId)
        {
            User value = await db.Users.Where(c => c.UserID ==UserId ).FirstOrDefaultAsync();
            if (value != null)
            {
              
                UserModel UserInfo = new UserModel() { Email = value.Email, Name = value.Name, Password = value.Password, Phone = value.Phone, ProfilePicture = value.ProfilePicture, UserID = value.UserID, LoggedIn = value.LoggedIn };
                return Request.CreateResponse(HttpStatusCode.OK, UserInfo);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

        }
    }
}
