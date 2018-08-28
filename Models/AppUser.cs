using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ParityUI.Models
{
    public sealed class AppUser : IdentityUser
    {
        public List<AccountLink> LinkedAccounts { get; private set; }
    }
}
