using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SesiCursos.Api.Data;
using SesiCursos.Api.DTOs;
using SesiCursos.Api.Models;

namespace SesiCursos.Api.Controllers;

[ApiController]
public class ModulosController : ControllerBase
{
    private readonly AppDbContext _db;
    public ModulosController(AppDbContext db) => _db = db;

    // ================= MÓDULOS =================

    // GET api/cursos/5/modulos  (usado pela página do curso)
    [HttpGet("api/cursos/{cursoId:int}/modulos")]
    public async Task<ActionResult<IEnumerable<ModuloResponse>>> ListarPorCurso(int cursoId)
    {
        if (!await _db.Cursos.AnyAsync(c => c.IdCursos == cursoId))
            return NotFound(new { erro = "Curso inexistente." });

        var modulos = await _db.Modulos.AsNoTracking()
            .Where(m => m.FkIdCursos == cursoId)
            .OrderBy(m => m.Ordem)
            .Select(m => new ModuloResponse(
                m.IdModulos,
                m.Titulo,
                m.Ordem,
                m.Topicos.OrderBy(t => t.Ordem).Select(t => t.Descricao).ToList()))
            .ToListAsync();

        return Ok(modulos);
    }

    // GET api/modulos/3
    [HttpGet("api/modulos/{id:int}")]
    public async Task<ActionResult<ModuloResponse>> ObterModulo(int id)
    {
        var modulo = await _db.Modulos.AsNoTracking()
            .Where(m => m.IdModulos == id)
            .Select(m => new ModuloResponse(
                m.IdModulos,
                m.Titulo,
                m.Ordem,
                m.Topicos.OrderBy(t => t.Ordem).Select(t => t.Descricao).ToList()))
            .FirstOrDefaultAsync();

        return modulo is null ? NotFound() : Ok(modulo);
    }

    // POST api/cursos/5/modulos
    [HttpPost("api/cursos/{cursoId:int}/modulos")]
    public async Task<ActionResult<ModuloResponse>> CriarModulo(int cursoId, ModuloRequest req)
    {
        if (!await _db.Cursos.AnyAsync(c => c.IdCursos == cursoId))
            return NotFound(new { erro = "Curso inexistente." });

        var modulo = new Modulo { Titulo = req.Titulo, Ordem = req.Ordem, FkIdCursos = cursoId };
        _db.Modulos.Add(modulo);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(ObterModulo), new { id = modulo.IdModulos },
            new ModuloResponse(modulo.IdModulos, modulo.Titulo, modulo.Ordem, new List<string>()));
    }

    // PUT api/modulos/3
    [HttpPut("api/modulos/{id:int}")]
    public async Task<IActionResult> AtualizarModulo(int id, ModuloRequest req)
    {
        var modulo = await _db.Modulos.FindAsync(id);
        if (modulo is null) return NotFound();

        modulo.Titulo = req.Titulo;
        modulo.Ordem = req.Ordem;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    // DELETE api/modulos/3  (apaga os tópicos junto, por causa do cascade)
    [HttpDelete("api/modulos/{id:int}")]
    public async Task<IActionResult> ExcluirModulo(int id)
    {
        var modulo = await _db.Modulos.FindAsync(id);
        if (modulo is null) return NotFound();

        _db.Modulos.Remove(modulo);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    // ================= TÓPICOS =================

    // GET api/modulos/3/topicos
    [HttpGet("api/modulos/{moduloId:int}/topicos")]
    public async Task<ActionResult<IEnumerable<TopicoResponse>>> ListarTopicos(int moduloId)
    {
        if (!await _db.Modulos.AnyAsync(m => m.IdModulos == moduloId))
            return NotFound(new { erro = "Módulo inexistente." });

        var topicos = await _db.Topicos.AsNoTracking()
            .Where(t => t.FkIdModulos == moduloId)
            .OrderBy(t => t.Ordem)
            .Select(t => new TopicoResponse(t.IdTopicos, t.Descricao, t.Ordem))
            .ToListAsync();

        return Ok(topicos);
    }

    // POST api/modulos/3/topicos
    [HttpPost("api/modulos/{moduloId:int}/topicos")]
    public async Task<ActionResult<TopicoResponse>> CriarTopico(int moduloId, TopicoRequest req)
    {
        if (!await _db.Modulos.AnyAsync(m => m.IdModulos == moduloId))
            return NotFound(new { erro = "Módulo inexistente." });

        var topico = new Topico { Descricao = req.Descricao, Ordem = req.Ordem, FkIdModulos = moduloId };
        _db.Topicos.Add(topico);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(ListarTopicos), new { moduloId },
            new TopicoResponse(topico.IdTopicos, topico.Descricao, topico.Ordem));
    }

    // PUT api/topicos/7
    [HttpPut("api/topicos/{id:int}")]
    public async Task<IActionResult> AtualizarTopico(int id, TopicoRequest req)
    {
        var topico = await _db.Topicos.FindAsync(id);
        if (topico is null) return NotFound();

        topico.Descricao = req.Descricao;
        topico.Ordem = req.Ordem;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    // DELETE api/topicos/7
    [HttpDelete("api/topicos/{id:int}")]
    public async Task<IActionResult> ExcluirTopico(int id)
    {
        var topico = await _db.Topicos.FindAsync(id);
        if (topico is null) return NotFound();

        _db.Topicos.Remove(topico);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}