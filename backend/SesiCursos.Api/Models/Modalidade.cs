namespace SesiCursos.Api.Models;

public class Modalidade
{
    public int IdModalidade { get; set; }
    public string Nome { get; set; } = null!;   // coluna "modalidade"
    public ICollection<Curso> Cursos { get; set; } = new List<Curso>();
}
