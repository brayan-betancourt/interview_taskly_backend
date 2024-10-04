using System.ComponentModel.DataAnnotations;

namespace InterviewAppTasklyWebApi.Models;

public class LoginModel
{
    [Required(ErrorMessage = "El Email es requerido.")]
    [EmailAddress(ErrorMessage = "El Email no es válido.")]
    public string? Email { get; set; }
    
    [Required(ErrorMessage = "La Contraseña es requerida")]
    [DataType(DataType.Password, ErrorMessage = "La Contraseña no es valida, debe contener letras, numeros y caracteres especiales.")]
    [MinLength(8, ErrorMessage = "La Contraseña debe contener minimo 8 caracteres.")]
    [MaxLength(12, ErrorMessage = "La Contraseña debe contener maximo de 12 caracteres.")]
    public string? Password { get; set; }
}