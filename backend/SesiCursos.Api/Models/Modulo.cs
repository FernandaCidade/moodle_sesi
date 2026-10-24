namespace SesiCursos.Api.Models;

public class Modulo
{
    public int IdModulos { get; set; }
    public string Titulo { get; set; } = null!;
    public int Ordem { get; set; }
    public int FkIdCursos { get; set; }

    public Curso? Curso { get; set; }
    public List<Topico> Topicos { get; set; } = new();
}