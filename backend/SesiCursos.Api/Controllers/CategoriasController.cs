using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SesiCursos.Api.Data;
using SesiCursos.Api.DTOs;
using SesiCursos.Api.Models;

namespace SesiCursos.Api.Controllers;

[ApiController]
[Route("api/categorias")]
public class CategoriasController : ControllerBase
{
    private readonly AppDbContext _db;
    public CategoriasController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LookupResponse>>> Listar() =>
        Ok(await _db.Categorias.AsNoTracking().OrderBy(x => x.Nome)
            .Select(x => new LookupResponse(x.IdCategoria, x.Nome)).ToListAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<LookupResponse>> Obter(int id)
    {
        var x = await _db.Categorias.AsNoTracking().FirstOrDefaultAsync(e => e.IdCategoria == id);
        return x is null ? NotFound() : Ok(new LookupResponse(x.IdCategoria, x.Nome));
    }

    [HttpPost]
    public async Task<ActionResult<LookupResponse>> Criar(LookupRequest req)
    {
        if (await _db.Categorias.AnyAsync(e => e.Nome == req.Nome))
            return Conflict(new { erro = "Já existe um registro com esse nome." });

        var x = new Categoria { Nome = req.Nome };
        _db.Categorias.Add(x);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Obter), new { id = x.IdCategoria }, new LookupResponse(x.IdCategoria, x.Nome));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, LookupRequest req)
    {
        var x = await _db.Categorias.FindAsync(id);
        if (x is null) return NotFound();
        if (await _db.Categorias.AnyAsync(e => e.Nome == req.Nome && e.IdCategoria != id))
            return Conflict(new { erro = "Já existe um registro com esse nome." });

        x.Nome = req.Nome;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var x = await _db.Categorias.FindAsync(id);
        if (x is null) return NotFound();
        try
        {
            _db.Categorias.Remove(x);
            await _db.SaveChangesAsync();
            return NoContent();
        }
        catch (DbUpdateException)
        {
            return Conflict(new { erro = "Registro em uso por cursos; não pode ser excluído." });
        }
    }
}
