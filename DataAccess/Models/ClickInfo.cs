using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class ClickInfo : UserActionBase
    {
        public DateTime ClickTime { get; set; } = DateTime.UtcNow;

    }
}
