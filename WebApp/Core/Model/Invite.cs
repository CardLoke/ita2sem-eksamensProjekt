using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Model
{
    public class Invite
    {
        public Studio Studio { get; set; } = new();
        public string Username { get; set; } = "";
        public int Id { get; set; }
    }
}
