﻿namespace Server.Services.EmailSending
{
    public class MailSettings
    {
        public string Mail { get; set; }
        public int Port { get; set; }
        public string Host { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
    }
}
