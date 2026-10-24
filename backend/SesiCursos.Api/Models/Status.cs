namespace SesiCursos.Api.Models;

public class Status
{
    public int IdStatus { get; set; }
    public string Nome { get; set; } = null!;   // coluna "status"
    public ICollection<Curso> Cursos { get; set; } = new List<Curso>();
}
