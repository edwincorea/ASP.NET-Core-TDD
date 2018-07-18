using Microsoft.AspNetCore.Identity;
using System;

namespace LandonHotel.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime Birthdate { get; set; }
    }
}
