using System.ComponentModel.DataAnnotations;

namespace UniversidadAPI.Models;

public class Asignacion
{
    [Key]
    public int AsignacionId { get; set; }

    public int EstudianteId { get; set; }

    public int CursoId { get; set; }

    public int DocenteId { get; set; }

    [ForeingKey("EstudianteId")]
    public Estudiante? estudiante { get; set; }
    public Curso? curso { get; set; }
    public Docente? Docente { get; set; }

}
