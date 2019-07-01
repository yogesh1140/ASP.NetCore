using Microsoft.AspNetCore.Identity;
using System;

namespace Working.Models
{
    public class WorkingUser: IdentityUser
    {
        public DateTime FirstTrip { get; set; }
    }
}