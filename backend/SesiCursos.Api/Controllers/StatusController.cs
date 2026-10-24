using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SesiCursos.Api.Data;
using SesiCursos.Api.DTOs;
using SesiCursos.Api.Models;

namespace SesiCursos.Api.Controllers;

[ApiController]
[Route("api/status")]
public class StatusController : ControllerBase
{
    private readonly AppDbContext _db;
    public StatusController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LookupResponse>>> Listar() =>
        Ok(await _db.Status.AsNoTracking().OrderBy(x => x.Nome)
            .Select(x => new LookupResponse(x.IdStatus, x.Nome)).ToListAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<LookupResponse>> Obter(int id)
    {
        var x = await _db.Status.AsNoTracking().FirstOrDefaultAsync(e => e.IdStatus == id);
        return x is null ? NotFound() : Ok(new LookupResponse(x.IdStatus, x.Nome));
    }

    [HttpPost]
    public async Task<ActionResult<LookupResponse>> Criar(LookupRequest req)
    {
        if (await _db.Status.AnyAsync(e => e.Nome == req.Nome))
            return Conflict(new { erro = "Já existe um registro com esse nome." });

        var x = new Status { Nome = req.Nome };
        _db.Status.Add(x);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Obter), new { id = x.IdStatus }, new LookupResponse(x.IdStatus, x.Nome));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, LookupRequest req)
    {
        var x = await _db.Status.FindAsync(id);
        if (x is null) return NotFound();
        if (await _db.Status.AnyAsync(e => e.Nome == req.Nome && e.IdStatus != id))
            return Conflict(new { erro = "Já existe um registro com esse nome." });

        x.Nome = req.Nome;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var x = await _db.Status.FindAsync(id);
        if (x is null) return NotFound();
        try
        {
            _db.Status.Remove(x);
            await _db.SaveChangesAsync();
            return NoContent();
        }
        catch (DbUpdateException)
        {
            return Conflict(new { erro = "Registro em uso por cursos; não pode ser excluído." });
        }
    }
}
