using System.ComponentModel.DataAnnotations;

namespace InterviewAppTasklyWebApi.Models;

public class RegisterModel
{
    [Required(ErrorMessage = "User First Name is required")]
    public string UserFirstName { get; set; } = null!;
    
    [Required(ErrorMessage = "User Last Name is required")]
    public string UserLastName { get; set; } = null!;

    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = null!;
    
    [Required(ErrorMessage = "User Rol is required")]
    public string UserRol { get; set; } = null!;
    
}