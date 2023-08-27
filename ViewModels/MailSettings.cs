namespace YoutubeBlog.ViewModels
{
    public class MailSettings
    {
        //So we can configure and use a SMTP Server from Google
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }

    }
}
