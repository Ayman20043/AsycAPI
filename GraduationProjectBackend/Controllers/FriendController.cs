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
            User value = await db.Users.Where(c => c.UserID == UserId).FirstOrDefaultAsync();
            if (value != null)
            {
                // var allfriend=value.FriendsIntiated.Join(value.FriendsRecieved,
                List<FriendModel> all = new List<FriendModel>();
                if (value.FriendsIntiated != null)
                    foreach (var item in value.FriendsIntiated)
                    {
                        var f=new FriendModel()
                        {
                            Name = item.RecieverUID.Name,
                            Email = item.RecieverUID.Email,
                            LoggedIn = item.RecieverUID.LoggedIn,
                            Phone = item.RecieverUID.Phone,
                            ProfilePicture = item.RecieverUID.ProfilePicture,
                            UserID = item.RecieverUID.UserID,
                        };

                        //all.Add(new FriendModel()
                        //{
                        //    Name = item.RecieverUID.Name,
                        //    Email = item.RecieverUID.Email,
                        //    LoggedIn = item.RecieverUID.LoggedIn,
                        //    Phone = item.RecieverUID.Phone,
                        //    ProfilePicture = item.RecieverUID.ProfilePicture,
                        //    UserID = item.RecieverUID.UserID,
                        //    RealTimeTrack=item.RealTimeTrack
                        //});
                    }
                if (value.FriendsRecieved != null)
                    foreach (var item in value.FriendsRecieved)
                    {
                        all.Add(new FriendModel()
                        {
                            Name = item.InitiatorUID.Name,
                            Email = item.InitiatorUID.Email,
                            LoggedIn = item.InitiatorUID.LoggedIn,
                            Phone = item.InitiatorUID.Phone,
                            ProfilePicture = item.InitiatorUID.ProfilePicture,
                            UserID = item.InitiatorUID.UserID,
                            RealTimeTrack = item.RealTimeTrack
                        });
                    }

                return Request.CreateResponse(HttpStatusCode.OK, all);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

        }

        [HttpGet]
        [Route("api/Friend/GetFriendByID")]
        public async Task<HttpResponseMessage> GetFriendByID([FromUri]int UserId, [FromUri] int FriendID)
        {
            User value = await db.Users.Where(c => c.UserID == UserId).FirstOrDefaultAsync();
            if (value != null)
            {
                // var allfriend=value.FriendsIntiated.Join(value.FriendsRecieved,
                List<FriendModel> all = new List<FriendModel>();
                if (value.FriendsIntiated != null)
                    foreach (var item in value.FriendsIntiated)
                    {
                        all.Add(new FriendModel()
                        {
                            Name = item.RecieverUID.Name,
                            Email = item.RecieverUID.Email,
                            LoggedIn = item.RecieverUID.LoggedIn,
                            Phone = item.RecieverUID.Phone,
                            ProfilePicture = item.RecieverUID.ProfilePicture,
                            UserID = item.RecieverUID.UserID,
                            RealTimeTrack = item.RealTimeTrack
                        });
                    }
                if (value.FriendsRecieved != null)
                    foreach (var item in value.FriendsRecieved)
                    {
                        all.Add(new FriendModel()
                        {
                            Name = item.InitiatorUID.Name,
                            Email = item.InitiatorUID.Email,
                            LoggedIn = item.InitiatorUID.LoggedIn,
                            Phone = item.InitiatorUID.Phone,
                            ProfilePicture = item.InitiatorUID.ProfilePicture,
                            UserID = item.InitiatorUID.UserID,
                            RealTimeTrack = item.RealTimeTrack
                        });
                    }
                var friend = all.Where(f => f.UserID ==FriendID).FirstOrDefault();
                return Request.CreateResponse(HttpStatusCode.OK, friend);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
        [HttpGet]
        [Route("api/Friend/GetFriendByName")]
        public async Task<HttpResponseMessage> GetFriendByName([FromUri]int UserId, [FromUri] string FriendName)
        {
            User value = await db.Users.Where(c => c.UserID == UserId).FirstOrDefaultAsync();
            if (value != null)
            {
                // var allfriend=value.FriendsIntiated.Join(value.FriendsRecieved,
                List<FriendModel> all = new List<FriendModel>();
                if (value.FriendsIntiated != null)
                    foreach (var item in value.FriendsIntiated)
                    {
                        all.Add(new FriendModel()
                        {
                            Name = item.RecieverUID.Name,
                            Email = item.RecieverUID.Email,
                            LoggedIn = item.RecieverUID.LoggedIn,
                            Phone = item.RecieverUID.Phone,
                            ProfilePicture = item.RecieverUID.ProfilePicture,
                            UserID = item.RecieverUID.UserID,
                            RealTimeTrack = item.RealTimeTrack
                        });
                    }
                if (value.FriendsRecieved != null)
                    foreach (var item in value.FriendsRecieved)
                    {
                        all.Add(new FriendModel()
                        {
                            Name = item.InitiatorUID.Name,
                            Email = item.InitiatorUID.Email,
                            LoggedIn = item.InitiatorUID.LoggedIn,
                            Phone = item.InitiatorUID.Phone,
                            ProfilePicture = item.InitiatorUID.ProfilePicture,
                            UserID = item.InitiatorUID.UserID,
                            RealTimeTrack = item.RealTimeTrack
                        });
                    }
                var friend = all.Where(f => f.Name == FriendName).FirstOrDefault();
                return Request.CreateResponse(HttpStatusCode.OK, friend);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
        [HttpGet]
        [Route("api/Friend/GetSuggestionsByEmails")]
        public async Task<HttpResponseMessage> GetSuggestionsByEmails([FromUri] string[] Emails)
        {
            try 
	        {
                var suggestions = await db.Users.Where(s => Emails.Contains(s.Email)).ToListAsync();
                if (suggestions!=null)
                {
                    List<FriendModel> finalList = new List<FriendModel>();
                    foreach (var item in suggestions)
                    {
                        finalList.Add(new FriendModel()
                        {
                            Name = item.Name,
                            Email = item.Email,
                            LoggedIn = item.LoggedIn,
                            Phone = item.Phone,
                            ProfilePicture = item.ProfilePicture,
                            UserID = item.UserID
                        });
                    }
                       return Request.CreateResponse(HttpStatusCode.OK, finalList);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.NoContent);
	
              
                 
	     }
	        catch (Exception)
	        {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);
	     }
       }
        [HttpGet]
        [Route("api/Friend/GetSuggestionsByNumbers")]
        public async Task<HttpResponseMessage> GetSuggestionsByNumbers([FromUri] string[] Numbers)
        {
            try
            {
                var suggestions = await db.Users.Where(s => Numbers.Contains(s.Phone)).ToListAsync();
                if (suggestions != null)
                {
                    List<FriendModel> finalList = new List<FriendModel>();
                    foreach (var item in suggestions)
                    {
                        finalList.Add(new FriendModel()
                        {
                            Name = item.Name,
                            Email = item.Email,
                            LoggedIn = item.LoggedIn,
                            Phone = item.Phone,
                            ProfilePicture = item.ProfilePicture,
                            UserID = item.UserID
                        });
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, finalList);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.NoContent);



            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("api/Friend/SendFriendRequest")]
        public async Task<HttpResponseMessage> SendFriendRequest([FromUri] int UserID,[FromBody] int FriendID)
        {
            var user = await db.Users.Where(u => u.UserID == UserID).FirstAsync();            
            var Friend = await db.Users.Where(u => u.UserID == FriendID).FirstAsync();
            if (user!=null)
            {
                try
                {
                    user.FriendRequestsSent.Add(new FriendRequest() { SenderID = UserID, Sender=user,State = null, RequestDate = DateTime.Now, RecieverID = FriendID, Reciever = Friend });
                  await  db.SaveChangesAsync();
                    return Request.CreateResponse(HttpStatusCode.OK);
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
        [Route("api/Friend/AcceptFriendRequest")]
        public async Task<HttpResponseMessage> AcceptFriendRequest([FromUri] int UserID, [FromBody] int FriendRequestID)
        {
            var user = await db.Users.Where(u => u.UserID == UserID).FirstAsync();
            var FriendRequest = await db.FriendRequests.Where(u => u.RequestId == FriendRequestID).FirstAsync();
            var Friend= FriendRequest.Sender;
            if (user != null&&FriendRequest!=null&&Friend!=null&&FriendRequest.RecieverID==UserID)
            {
                try
                {
                    FriendRequest.State = true;
                    user.FriendsRecieved.Add(new Friend() { InitatiorUserID = Friend.UserID, InitiatorUID = Friend, RecieverUserID = UserID, RecieverUID = user,RealTimeTrack=null });
                    await db.SaveChangesAsync();
                    return Request.CreateResponse(HttpStatusCode.OK);
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
        [Route("api/Friend/RejectFriendRequest")]
        public async Task<HttpResponseMessage> RejectFriendRequest([FromUri] int UserID, [FromBody] int FriendRequestID)
        {
            var user = await db.Users.Where(u => u.UserID == UserID).FirstAsync();
            var FriendRequest = await db.FriendRequests.Where(u => u.RequestId == FriendRequestID).FirstAsync();
            var Friend = FriendRequest.Sender;
            if (user != null && FriendRequest != null && Friend != null && FriendRequest.RecieverID == UserID)
            {
                try
                {
                    FriendRequest.State = false;
                    await db.SaveChangesAsync();
                    return Request.CreateResponse(HttpStatusCode.OK);
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

        [HttpGet]
        [Route("api/Friend/GetAllFriendRequests")]
        public async Task<HttpResponseMessage> GetAllFriendRequests([FromUri] int UserID)
        {
            var user = await db.Users.Where(u => u.UserID == UserID).FirstAsync();    
            if (user != null)
            {
                try
                {
                    List<FriendRequsetModel> final = new List<FriendRequsetModel>();
                    var reqs = await db.FriendRequests.Where(r => r.RecieverID == user.UserID&&r.State==null).ToListAsync();
                    foreach (var item in reqs)
                    {
                        final.Add(new FriendRequsetModel() {SenderID=item.SenderID,RequestDate=item.RequestDate,RequestId=item.RequestId,State=item.State });
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,final);
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
        [Route("api/Friend/UnFriend")]
        public async Task<HttpResponseMessage> UnFriend([FromUri]int UserId, [FromBody] int FriendID)
        {
            User value = await db.Users.Where(c => c.UserID == UserId).FirstOrDefaultAsync();
            if (value != null)
            {
                // var allfriend=value.FriendsIntiated.Join(value.FriendsRecieved,
                List<FriendModel> all = new List<FriendModel>();
                if (value.FriendsIntiated != null)
                    foreach (var item in value.FriendsIntiated)
                    {
                        all.Add(new FriendModel()
                        {
                            Name = item.RecieverUID.Name,
                            Email = item.RecieverUID.Email,
                            LoggedIn = item.RecieverUID.LoggedIn,
                            Phone = item.RecieverUID.Phone,
                            ProfilePicture = item.RecieverUID.ProfilePicture,
                            UserID = item.RecieverUID.UserID
                        });
                    }
                if (value.FriendsRecieved != null)
                    foreach (var item in value.FriendsRecieved)
                    {
                        all.Add(new FriendModel()
                        {
                            Name = item.InitiatorUID.Name,
                            Email = item.InitiatorUID.Email,
                            LoggedIn = item.InitiatorUID.LoggedIn,
                            Phone = item.InitiatorUID.Phone,
                            ProfilePicture = item.InitiatorUID.ProfilePicture,
                            UserID = item.InitiatorUID.UserID
                        });
                    }
                var friend = all.Where(f => f.UserID == FriendID).SingleOrDefault();
                var friend2 = await db.Users.Where(s => s.UserID == FriendID).SingleOrDefaultAsync();
                if(value.FriendsIntiated.Where(i=>i.RecieverUserID==FriendID).FirstOrDefault()!=null)
                {
                  //  value.FriendsIntiated.Remove((value.FriendsIntiated.Where(i=>i.RecieverUserID==FriendID).FirstOrDefault()));
                    db.Friends.Remove(value.FriendsIntiated.Where(i => i.RecieverUserID == FriendID).FirstOrDefault());
                }
                else if(value.FriendsRecieved.Where(i=>i.InitatiorUserID==FriendID).FirstOrDefault()!=null)
                {
                   // value.FriendsRecieved.Remove(new Friend() { InitatiorUserID = FriendID });
                    db.Friends.Remove(value.FriendsRecieved.Where(i => i.InitatiorUserID == FriendID).FirstOrDefault());
                }
                await db.SaveChangesAsync();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [Route("api/Friend/SendRealTimeRequest")]
        public async Task<HttpResponseMessage> SendRealTimeRequest([FromUri] int UserID, [FromBody] int FriendID)
        {
            var user = await db.Users.Where(u => u.UserID == UserID).FirstAsync();
            var Friend = await db.Users.Where(u => u.UserID == FriendID).FirstAsync();
            if (user != null)
            {
                try
                {
                    user.RealTimeTrackRequestSent.Add(new RealTimeTrackRequest() { UserID = UserID, User = user, State = null, RequestDate = DateTime.Now, FriendID = FriendID, Friend = Friend });
                    await db.SaveChangesAsync();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                catch (Exception e)
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
        [Route("api/Friend/AcceptRealTimeRequest")]
        public async Task<HttpResponseMessage> AcceptRealTimeRequest([FromUri] int UserID, [FromBody] int RealTimeRequestID)
        {
            var user = await db.Users.Where(u => u.UserID == UserID).FirstAsync();
            var RealTimeRequest = await db.RealTimeTrackRequests.Where(u => u.RequestID == RealTimeRequestID).FirstAsync();
            var Friend = (RealTimeRequest.Friend.UserID == UserID) ? RealTimeRequest.User : RealTimeRequest.Friend;
            if (user != null && RealTimeRequest != null && Friend != null )
            {
                try
                {
                   
                    if (user.FriendsIntiated.Where(f => f.RecieverUserID == Friend.UserID).SingleOrDefault() != null)
                    {
                        RealTimeRequest.State = true;
                        user.FriendsIntiated.Where(f => f.RecieverUserID == Friend.UserID).SingleOrDefault().RealTimeTrack = true;
                    }
                    else
                        if (user.FriendsRecieved.Where(f => f.InitatiorUserID == Friend.UserID).SingleOrDefault() != null)
                        {
                            RealTimeRequest.State = true;
                            user.FriendsRecieved.Where(f => f.InitatiorUserID == Friend.UserID).SingleOrDefault().RealTimeTrack = true;
                        }
                    //if (user.FriendsIntiated != null)
                    //{
                    //    foreach (var item in user.FriendsIntiated)
                    //    {
                    //        if (item.InitatiorUserID == Friend.UserID)
                    //            item.RealTimeTrack = true;

                    //    }
                    //}
                    //if (user.FriendsRecieved != null)
                    //{
                    //    foreach (var item in user.FriendsRecieved)
                    //    {
                    //        if (item.RecieverUserID == Friend.UserID)
                    //            item.RealTimeTrack = true;
                    //    }
                    //}
                    await db.SaveChangesAsync();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                catch (Exception e)
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
        [Route("api/Friend/RejectRealTimeRequest")]
        public async Task<HttpResponseMessage> RejectRealTimeRequest([FromUri] int UserID, [FromBody] int RealTimeRequestID)
        {
            var user = await db.Users.Where(u => u.UserID == UserID).FirstAsync();
            var RealTimeRequest = await db.RealTimeTrackRequests.Where(u => u.RequestID == RealTimeRequestID).FirstAsync();
            var Friend = RealTimeRequest.Friend;
            if (user != null && RealTimeRequest != null && Friend != null && RealTimeRequest.FriendID == UserID)
            {
                try
                {
                    RealTimeRequest.State = false;

                    await db.SaveChangesAsync();
                    return Request.CreateResponse(HttpStatusCode.OK);
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
        [Route("api/Friend/RemoveRealTime")]
        public async Task<HttpResponseMessage> RemoveRealTimeTrack([FromUri] int UserID,[FromBody] int friendID) 
        {
            var user = await db.Users.Where(u => u.UserID == UserID).FirstAsync();
            FriendModel friend = await FriendHelper.GetFriendByID(UserID,friendID);
            if (user != null &&friend!=null )
            {
                try
                {
                   
                    if (user.FriendsIntiated.Where(f => f.RecieverUserID ==friend.UserID ).SingleOrDefault() != null)
                    {
                        user.FriendsIntiated.Where(f => f.RecieverUserID == friend.UserID).SingleOrDefault().RealTimeTrack = false;
                    }
                    else
                        if (user.FriendsRecieved.Where(f => f.InitatiorUserID == friend.UserID).SingleOrDefault() != null)
                        {
                            user.FriendsRecieved.Where(f => f.InitatiorUserID == friend.UserID).SingleOrDefault().RealTimeTrack = false;
                        }

                    await db.SaveChangesAsync();
                    return Request.CreateResponse(HttpStatusCode.OK);
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

        [HttpGet]
        [Route("api/Friend/GetAllRealTimeRequests")]
        public async Task<HttpResponseMessage> GetAllRealTimeRequests([FromUri] int UserID)
        {
            var user = await db.Users.Where(u => u.UserID == UserID).FirstAsync();
            if (user != null)
            {
                try
                {
                    List<FriendRequsetModel> final = new List<FriendRequsetModel>();
                 //   var reqs = await db.FriendRequests.Where(r => r.RecieverID == user.UserID && r.State == null).ToListAsync();
                    var reqs = await db.RealTimeTrackRequests.Where(r => r.FriendID == UserID && r.State == null).ToListAsync();
                    foreach (var item in reqs)
                    {
                        final.Add(new FriendRequsetModel() { SenderID = item.UserID, RequestDate = item.RequestDate, RequestId = item.RequestID, State = item.State });
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, final);
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
    }
}
