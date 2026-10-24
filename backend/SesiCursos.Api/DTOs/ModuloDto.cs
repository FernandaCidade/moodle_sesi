using System.ComponentModel.DataAnnotations;

namespace SesiCursos.Api.DTOs;

// ================= MÓDULO =================

public class ModuloRequest
{
    [Required, StringLength(200)] public string Titulo { get; set; } = null!;
    [Range(1, int.MaxValue)] public int Ordem { get; set; }
}

public record ModuloResponse(int Id, string Titulo, int Ordem, List<string> Topicos);

// ================= TÓPICO =================

public class TopicoRequest
{
    [Required, StringLength(500)] public string Descricao { get; set; } = null!;
    [Range(1, int.MaxValue)] public int Ordem { get; set; }
}

public record TopicoResponse(int Id, string Descricao, int Ordem);