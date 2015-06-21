using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraduationProjectBackend
{
    public class DangerListMemberModel
    {
        public int MemberID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Nullable<int> MemberUserID { get; set; }//User ID if the member is a system User 
        public string Picture { get; set; }
    }
}