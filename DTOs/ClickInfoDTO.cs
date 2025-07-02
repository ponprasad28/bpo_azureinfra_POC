using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs
{
    public class ClickInfoDTO
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public DateTime ClickTime { get; set; } = DateTime.UtcNow;

    }
}
