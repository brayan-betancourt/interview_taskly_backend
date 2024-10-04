using Microsoft.AspNetCore.Identity;

namespace InterviewAppTasklyWebApi.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime? DisableDate { get; set; }
    public bool DisableUser { get; set; }
    public ICollection<TaskManagement> TaskManagements { get; set; }

}