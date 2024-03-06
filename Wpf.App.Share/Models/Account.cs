namespace Wpf.App.Share.Models
{
    public class Account
    {
        public string UsertName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Email { get; set; } = null!;

        public DateTime LoginTime { get; set; } = DateTime.Now;
    }
}
