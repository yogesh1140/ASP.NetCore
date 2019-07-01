using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Services
{
    public class LocalMailService: IMailService
    {
        private string _mailTo = Startup._config["mailSettings:mailToAddress"];
        private string _mailFrom = Startup._config["mailSettings:mailFromAddress"];

        public void Send(string subject, string meesage)
        {
            Debug.WriteLine($"Mail from {_mailFrom} to {_mailTo}, with LocalMailService");
            Debug.WriteLine($"Subject: {subject} ");
            Debug.WriteLine($"Message: {meesage} ");


        }

    }
}
