using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RegisterNewUser
{
    public class TenMinEmail
    {
        
        private const string indexUrl = "https://10minutemail.com/10MinuteMail/resources/session/address";


        public string ObtainEmailBox(bool renew = false)
        {
            var webClient = new WebClient();
            var result = webClient.DownloadString(new Uri(indexUrl));
            return result;
        }
    }
}
