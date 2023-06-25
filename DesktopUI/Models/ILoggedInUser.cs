using System;

namespace DesktopUI.Models
{
    public interface ILoggedInUser
    {
        DateTime CreatedDate { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        string Id { get; set; }
        string LastName { get; set; }
        string token { get; set; }
    }
}