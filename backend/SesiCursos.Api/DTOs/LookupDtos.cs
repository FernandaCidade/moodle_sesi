using System.ComponentModel.DataAnnotations;

namespace SesiCursos.Api.DTOs;

public class LookupRequest
{
    [Required, StringLength(80)] public string Nome { get; set; } = null!;
}

public record LookupResponse(int Id, string Nome);
