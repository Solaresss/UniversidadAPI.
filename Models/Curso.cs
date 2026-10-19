using System.ComponentModel.DataAnnotations;

namespace UniversidadAPI.Models;

public class Curso
{
    [Key]
    public int CursoId { get; set; }

    [Required]
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;
}
