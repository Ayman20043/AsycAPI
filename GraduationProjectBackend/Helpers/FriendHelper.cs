using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Data.Entity;

namespace GraduationProjectBackend
{
    public class FriendHelper
    {
       static  GPEntities db = new GPEntities();
        public static async Task<List<FriendModel>> GetAllFriends(int UserID) 
        {
            User value = await db.Users.Where(c => c.UserID == UserID).FirstOrDefaultAsync();
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
            return all;
        }

        public static async Task<FriendModel> GetFriendByID(int UserID, int FriendID) 
        {
            var all = await GetAllFriends(UserID);
            FriendModel RequestedFriend = all.Where(f => f.UserID == FriendID).FirstOrDefault();
            return RequestedFriend;
        }
    }
}