using System;
using DTOs.Enum;

namespace DTOs
{
    public class LogInfoDTO
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public DateTime LoginTime { get; set; } = DateTime.UtcNow;
        public ClientAppDTO LoginFrom { get; set; } = ClientAppDTO.Revit;
        
    }
}
