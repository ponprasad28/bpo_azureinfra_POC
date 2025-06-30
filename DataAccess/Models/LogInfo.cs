using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class LogInfo : UserActionBase
    {
        public DateTime LoginTime { get; set; } = DateTime.UtcNow;

    }
}
