using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Working.Services
{
    public class DebugMailService: IMailService
    {
        public void SendMail(string to, string from, string subject, string body)
        {
            Debug.WriteLine("Sending Mail to :{0} from: {1} Subject: {2}", to, from, subject);
        }
    }
}
