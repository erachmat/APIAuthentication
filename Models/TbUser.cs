using System;
using System.Collections.Generic;

#nullable disable

namespace APIAuthentication.Models
{
    public partial class TbUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}
