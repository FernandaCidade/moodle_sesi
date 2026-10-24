using System.ComponentModel.DataAnnotations;

namespace SesiCursos.Api.DTOs;

public class CursoRequest
{
    [Required, StringLength(200)] public string NomeCompleto { get; set; } = null!;
    [Required, StringLength(50)] public string NomeBreve { get; set; } = null!;
    [Required] public string Descricao { get; set; } = null!;
    [Range(1, int.MaxValue)] public int CargaHoraria { get; set; }
    [Required, StringLength(255)] public string PublicoAlvo { get; set; } = null!;
    [Required, StringLength(255)] public string ImagemApresentacao { get; set; } = null!;
    public DateOnly? DataDisponibilizacao { get; set; }
    public int? StatusId { get; set; }
    public int? ModalidadeId { get; set; }
    public int? CategoriaId { get; set; }
}

public record CursoResponse(
    int Id, string NomeCompleto, string NomeBreve, string Descricao, int CargaHoraria,
    string PublicoAlvo, string ImagemApresentacao, DateOnly? DataDisponibilizacao,
    int? StatusId, string? Status,
    int? ModalidadeId, string? Modalidade,
    int? CategoriaId, string? Categoria);

public record PagedResult<T>(int Page, int PageSize, int Total, IEnumerable<T> Items);
