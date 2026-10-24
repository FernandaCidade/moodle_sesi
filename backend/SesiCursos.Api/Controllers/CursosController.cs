using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SesiCursos.Api.Data;
using SesiCursos.Api.DTOs;
using SesiCursos.Api.Models;

namespace SesiCursos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CursosController : ControllerBase
{
    private readonly AppDbContext _db;
    public CursosController(AppDbContext db) => _db = db;

    private static CursoResponse ToResponse(Curso c) => new(
        c.IdCursos, c.NomeCompleto, c.NomeBreve, c.Descricao, c.CargaHoraria,
        c.PublicoAlvo, c.ImagemApresentacao, c.DataDisponibilizacao,
        c.FkIdStatus, c.Status?.Nome,
        c.FkIdModalidade, c.Modalidade?.Nome,
        c.FkIdCategoria, c.Categoria?.Nome);

    // GET api/cursos?busca=&statusId=&modalidadeId=&categoriaId=&page=1&pageSize=10
    [HttpGet]
    public async Task<ActionResult<PagedResult<CursoResponse>>> Listar(
        string? busca, int? statusId, int? modalidadeId, int? categoriaId,
        int page = 1, int pageSize = 10)
    {
        page = Math.Max(page, 1);
        pageSize = Math.Clamp(pageSize, 1, 100);

        var q = _db.Cursos.AsNoTracking()
            .Include(c => c.Status).Include(c => c.Modalidade).Include(c => c.Categoria)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(busca))
            q = q.Where(c => c.NomeCompleto.Contains(busca) || c.NomeBreve.Contains(busca));
        if (statusId.HasValue) q = q.Where(c => c.FkIdStatus == statusId);
        if (modalidadeId.HasValue) q = q.Where(c => c.FkIdModalidade == modalidadeId);
        if (categoriaId.HasValue) q = q.Where(c => c.FkIdCategoria == categoriaId);

        var total = await q.CountAsync();
        var itens = await q.OrderBy(c => c.NomeCompleto)
            .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return Ok(new PagedResult<CursoResponse>(page, pageSize, total, itens.Select(ToResponse)));
    }

    // GET api/cursos/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CursoResponse>> Obter(int id)
    {
        var c = await _db.Cursos.AsNoTracking()
            .Include(x => x.Status).Include(x => x.Modalidade).Include(x => x.Categoria)
            .FirstOrDefaultAsync(x => x.IdCursos == id);
        return c is null ? NotFound() : Ok(ToResponse(c));
    }

    // POST api/cursos
    [HttpPost]
    public async Task<ActionResult<CursoResponse>> Criar(CursoRequest req)
    {
        if (await ValidarFks(req) is { } erro) return erro;

        var c = new Curso();
        Mapear(req, c);
        _db.Cursos.Add(c);
        await _db.SaveChangesAsync();

        await _db.Entry(c).Reference(x => x.Status).LoadAsync();
        await _db.Entry(c).Reference(x => x.Modalidade).LoadAsync();
        await _db.Entry(c).Reference(x => x.Categoria).LoadAsync();
        return CreatedAtAction(nameof(Obter), new { id = c.IdCursos }, ToResponse(c));
    }

    // PUT api/cursos/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, CursoRequest req)
    {
        var c = await _db.Cursos.FindAsync(id);
        if (c is null) return NotFound();
        if (await ValidarFks(req) is { } erro) return erro;

        Mapear(req, c);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    // DELETE api/cursos/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var c = await _db.Cursos.FindAsync(id);
        if (c is null) return NotFound();
        _db.Cursos.Remove(c);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    private static void Mapear(CursoRequest r, Curso c)
    {
        c.NomeCompleto = r.NomeCompleto;
        c.NomeBreve = r.NomeBreve;
        c.Descricao = r.Descricao;
        c.CargaHoraria = r.CargaHoraria;
        c.PublicoAlvo = r.PublicoAlvo;
        c.ImagemApresentacao = r.ImagemApresentacao;
        c.DataDisponibilizacao = r.DataDisponibilizacao;
        c.FkIdStatus = r.StatusId;
        c.FkIdModalidade = r.ModalidadeId;
        c.FkIdCategoria = r.CategoriaId;
    }

    private async Task<ActionResult?> ValidarFks(CursoRequest r)
    {
        if (r.StatusId.HasValue && !await _db.Status.AnyAsync(s => s.IdStatus == r.StatusId))
            return BadRequest(new { erro = "StatusId inexistente." });
        if (r.ModalidadeId.HasValue && !await _db.Modalidades.AnyAsync(m => m.IdModalidade == r.ModalidadeId))
            return BadRequest(new { erro = "ModalidadeId inexistente." });
        if (r.CategoriaId.HasValue && !await _db.Categorias.AnyAsync(c => c.IdCategoria == r.CategoriaId))
            return BadRequest(new { erro = "CategoriaId inexistente." });
        return null;
    }
}
