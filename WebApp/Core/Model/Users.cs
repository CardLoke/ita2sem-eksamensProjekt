using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Model
{
    public class User
    {
        public string Username { get; set; }
        public string Mail { get; set; }

        public string Password { get; set; }
        public int Id { get; set; }

        public string Producer { get; set; }
    }
}
