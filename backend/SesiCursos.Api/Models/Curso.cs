namespace SesiCursos.Api.Models;

public class Curso
{
    public int IdCursos { get; set; }
    public string NomeCompleto { get; set; } = null!;
    public string NomeBreve { get; set; } = null!;
    public string Descricao { get; set; } = null!;
    public int CargaHoraria { get; set; }
    public string PublicoAlvo { get; set; } = null!;
    public string ImagemApresentacao { get; set; } = null!;
    public DateOnly? DataDisponibilizacao { get; set; }

    public int? FkIdStatus { get; set; }
    public int? FkIdModalidade { get; set; }
    public int? FkIdCategoria { get; set; }

    public Status? Status { get; set; }
    public Modalidade? Modalidade { get; set; }
    public Categoria? Categoria { get; set; }
}
