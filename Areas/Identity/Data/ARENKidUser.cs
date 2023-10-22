using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ProjectBackend.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ARENKidUser class
public class ARENKidUser : IdentityUser
{
    public int LevelAge {get; set;}
}

