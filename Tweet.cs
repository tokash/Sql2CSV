using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlCE2CSV
{
    public class TweetRow
    {
        public string TweetID { get; set; }
        public string UserID { get; set; }
        public string Tweet { get; set; }
        public string TimeOfTweet { get; set; }
    }
}
