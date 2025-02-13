using System;

namespace Azure.TimerTrigger.ViewModels
{
    public class UserViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime LastLoginDtTm { get; set; }
    }
}
