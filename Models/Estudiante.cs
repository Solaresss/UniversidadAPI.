using System.ComponentModel.DataAnnotations;

namespace UniversidadAPI.Models;

public class Estudiante
{
    [Key]
    public int EstudianteId { get; set; }

    [Required(ErrorMessage = "Este campo es requerido")]
    [StringLength(50)]
    public string Nombres { get; set; }

    [Required(ErrorMessage = "Este campo es requerido")]
    [StringLength(50)]
    public string Apellidos { get; set; }

    [Required(ErrorMessage = "Este campo es requerido")]
    [StringLength(100)]
    public string Direccion { get; set; }
}
