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
    public class DangerListController : ApiController
    {
        GPEntities db = new GPEntities();
        [HttpGet]
        [Route("api/DangerList/GetAllDangerListMembers")]
        public async Task<HttpResponseMessage> GetAllDangerListMembers([FromUri]int UserId)
        {
            var user = await db.Users.Where(u => u.UserID == UserId).SingleOrDefaultAsync();
            var allFriends=await FriendHelper.GetAllFriends(UserId);
            if (user != null)
            {
                try
                {
                    List<DangerListMemberModel> all = new List<DangerListMemberModel>();
                    foreach (var item in user.DangerList)
                    {
                        if (item.MemberUserID != null)
                        {
                            all.Add(new DangerListMemberModel() {MemberID=item.MemberID,UserID=item.UserID,MemberUserID=item.MemberUserID, Name = item.Name, Phone = item.Phone, Picture = allFriends.Where(f => f.UserID == item.MemberUserID).SingleOrDefault().ProfilePicture });
                        }
                        else
                        {
                            all.Add(new DangerListMemberModel() {MemberID=item.MemberID,UserID=item.UserID, Name = item.Name, Phone = item.Phone, Picture = null });
                        }

                    }
                   return Request.CreateResponse(HttpStatusCode.OK,all);
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
        [Route("api/DangerList/AddMember")]
        public async Task<HttpResponseMessage> AddDangerListMember([FromUri] int UserID,[FromBody] DangerListMemberModel DangerListMember) 
        {
               var user = await db.Users.Where(u => u.UserID == UserID).SingleOrDefaultAsync();
               if (user != null)
               {
                   try
                   {
                       if (DangerListMember.MemberUserID!=null)
                       {
                            user.DangerList.Add(new DangerList() {UserID=UserID,Name=DangerListMember.Name,Phone=DangerListMember.Phone,MemberUserID=DangerListMember.MemberUserID });
                       }
                       else
                       user.DangerList.Add(new DangerList() {UserID=UserID,Name=DangerListMember.Name,Phone=DangerListMember.Phone});
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
        [Route("api/DangerList/DeleteMembers")]
        public async Task<HttpResponseMessage> DeleteMembers([FromUri] int UserID, [FromBody] int[] DangerListMemberIDs)
        {
            var user = await db.Users.Where(u => u.UserID == UserID).SingleOrDefaultAsync();
            var all=user.DangerList;
            if (user != null)
            {
                try
                {
                    foreach (var item in DangerListMemberIDs)
                    {
                        DangerList temp = all.Where(u => u.MemberID == item).SingleOrDefault();
                         user.DangerList.Remove(temp);
                         await db.SaveChangesAsync();
                    }
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
        [Route("api/DangerList/EditMember")]
        public async Task<HttpResponseMessage> EditMember([FromUri] int UserID, [FromBody] DangerListMemberModel DangerListMember)
        {
            var user = await db.Users.Where(u => u.UserID == UserID).SingleOrDefaultAsync();
            var member = await db.DangerLists.Where(m => m.MemberID == DangerListMember.MemberID).SingleOrDefaultAsync();
            if (user != null)
            {
                try
                {
                    //user.DangerList.Add(new DangerList() { UserID = UserID, Name = DangerListMember.Name, Phone = DangerListMember.Phone, MemberUserID = DangerListMember.MemberUserID });
                
                    member.Phone = DangerListMember.Phone;
                    member.Name = DangerListMember.Name;
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
        [Route("api/DangerList/SendRequest")]
        public async Task<HttpResponseMessage> SendRequest([FromUri] int UserID)
        {
            var user = await db.Users.Where(u => u.UserID == UserID).SingleOrDefaultAsync();
           
            if (user != null)
            {
                try
                {
                    //user.DangerList.Add(new DangerList() { UserID = UserID, Name = DangerListMember.Name, Phone = DangerListMember.Phone, MemberUserID = DangerListMember.MemberUserID });

                  
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
    }
}
