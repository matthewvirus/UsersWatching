using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace UsersWebApp.Areas.Identity.Data;

public class User : IdentityUser
{
    [PersonalData]
    public string? Name { get; set; }

    public DateTime RegistrationDate { get; set; }

    public DateTime LastLoginDate { get; set; }

    public UserStatus Status { get; set; }

    public bool IsChecked { get; set; }
}

public enum UserStatus
{
    Online, Offline, Blocked, Unblocked
}