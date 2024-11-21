namespace api.Models
{
    public class MisCursosViewModel
    {
        public int CursoId { get; set; }
        public string TituloCurso { get; set; } = null!;
        public string DescripcionCurso { get; set; } = null!;
        public DateTime? FechaInscripcion { get; set; }  // Fecha de inscripción (nullable)
        public int? Calificacion { get; set; }  // Calificación (nullable)
    }
}
