using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCapp.Model
{
    public class User
    {
        public string Name { get; set; }
        public string UserEmail { get; set; }

        public User(string displayName, string userEmail)
        {
            Name = displayName;
            UserEmail = userEmail;
        }

        public override string ToString()
        {
            return $"{Name} ({UserEmail})";
        }
    }
}
