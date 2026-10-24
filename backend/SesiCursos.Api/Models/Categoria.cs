namespace SesiCursos.Api.Models;

public class Categoria
{
    public int IdCategoria { get; set; }
    public string Nome { get; set; } = null!;   // coluna "categoria"
    public ICollection<Curso> Cursos { get; set; } = new List<Curso>();
}
