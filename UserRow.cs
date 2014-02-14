using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlCE2CSV
{
    public class UserRow
    {
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Follows { get; set; }
        public string Description { get; set; }
        public string Screen_Name { get; set; }
        public string Followers_Count { get; set; }
        public string Friends_Count { get; set; }
        public string Favourites_Count { get; set; }
        public string Tweet_Count { get; set; }
        public string Location { get; set; }
        public string Twitter_Name { get; set; }
        public string Time_Zone { get; set; }
        public string Created_At { get; set; }
        public string Time_Stamp { get; set; }
    }
}
