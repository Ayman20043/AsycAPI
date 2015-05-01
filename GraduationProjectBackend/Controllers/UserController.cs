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
    public class UserController : ApiController
    {
        GPEntities db = new GPEntities();
        [HttpPost]
        [Route("api/User/Login")]
        public async Task<HttpResponseMessage> Login([FromBody]UserModel user)
        {
            User value = await db.Users.Where(c => c.Email == user.Email && c.Password == user.Password).FirstOrDefaultAsync();
            if (value != null)
            {
                value.LoggedIn = true;
                await db.SaveChangesAsync();
                UserModel UserInfo=new UserModel(){ Email=value.Email,Name=value.Name,Password=value.Password,Phone=value.Phone,ProfilePicture=value.ProfilePicture,UserID=value.UserID,LoggedIn=value.LoggedIn};
             return Request.CreateResponse(HttpStatusCode.OK,UserInfo);
            }
            else
            {
              return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
    
        }

        [HttpPost]
        [Route("api/User/Register")]
        public async Task<HttpResponseMessage> Register([FromBody] UserModel Value)
        {
            if (Value.Email != null && Value.Password != null)
            {
                try
                {
                    User user = new User() { Email = Value.Email, Password = Value.Password, Phone = Value.Phone, Name = Value.Name,ProfilePicture=Value.ProfilePicture,LoggedIn=true };
                    db.Users.Add(user);
                    await db.SaveChangesAsync();
                    return Request.CreateResponse(HttpStatusCode.OK, user);
                }
                catch (Exception)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }


            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
        [HttpPost]
        [Route("api/User/LogOut")]
        public async Task<HttpResponseMessage> LogOut(int Id)
        {
            try
            {
                User user = await db.Users.Where(u => u.UserID == Id).SingleOrDefaultAsync();
                if (user.LoggedIn==true)
                {
                    user.LoggedIn = false;
                  await  db.SaveChangesAsync();
                  return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

                }
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
           

        }
        [HttpPost]
        [Route("api/User/ChangePicture")]
        public async Task<HttpResponseMessage> ChangePicture(int Id,[FromBody] string picureString)
        {
            try
            {
                User user = await db.Users.Where(u => u.UserID == Id).SingleOrDefaultAsync();
                if (user!=null)
                {
                    user.ProfilePicture = picureString;
                    await db.SaveChangesAsync();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

                }
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }


        }
        [HttpPost]
        [Route("api/User/ChangeName")]
        public async Task<HttpResponseMessage> ChangeName(int Id, [FromBody] string Name)
        {
            try
            {
                User user = await db.Users.Where(u => u.UserID == Id).SingleOrDefaultAsync();
                if (user != null)
                {
                    user.Name = Name;
                    await db.SaveChangesAsync();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

                }
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }            
        }
        [HttpPost]
        [Route("api/User/ChangePassword")]
        public async Task<HttpResponseMessage> ChangePassword(int Id, [FromBody] string[] Passwords)
        {
            try
            {
                User user = await db.Users.Where(u => u.UserID == Id).SingleOrDefaultAsync();
                if (user != null)
                {
                    if(user.Password==Passwords[0])
                    {
                        user.Password = Passwords[1];
                        await db.SaveChangesAsync();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.Conflict);

                    }
                    
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

                }
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        [Route("api/User/ChangeEmail")]
        public async Task<HttpResponseMessage> ChangeEmail(int Id, [FromBody] string Email)
        {
            try
            {
                User user = await db.Users.Where(u => u.UserID == Id).SingleOrDefaultAsync();
                if (user != null)
                {
                    user.Email = Email;
                    await db.SaveChangesAsync();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

                }
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        [Route("api/User/ChangeNumber")]
        public async Task<HttpResponseMessage> ChangeNumber(int Id, [FromBody] string Number)
        {
            try
            {
                User user = await db.Users.Where(u => u.UserID == Id).SingleOrDefaultAsync();
                if (user != null)
                {
                    user.Phone = Number;
                    await db.SaveChangesAsync();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

                }
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        [Route("api/User/ForgetPassword")]
        public async Task<HttpResponseMessage> ForgetPassword([FromBody] string Email)
        {
            try
            {
                User user = await db.Users.Where(u => u.Email == Email).SingleOrDefaultAsync();
                if (user != null)
                {
                    //user.Name = Name;
                    //await db.SaveChangesAsync();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

                }
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        // POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}