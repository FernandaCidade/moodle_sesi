namespace SesiCursos.Api.Models;

public class Topico
{
    public int IdTopicos { get; set; }
    public string Descricao { get; set; } = null!;
    public int Ordem { get; set; }
    public int FkIdModulos { get; set; }

    public Modulo? Modulo { get; set; }
}