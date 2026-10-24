using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SesiCursos.Api.Data;
using SesiCursos.Api.DTOs;
using SesiCursos.Api.Models;

namespace SesiCursos.Api.Controllers;

[ApiController]
[Route("api/modalidades")]
public class ModalidadesController : ControllerBase
{
    private readonly AppDbContext _db;
    public ModalidadesController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LookupResponse>>> Listar() =>
        Ok(await _db.Modalidades.AsNoTracking().OrderBy(x => x.Nome)
            .Select(x => new LookupResponse(x.IdModalidade, x.Nome)).ToListAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<LookupResponse>> Obter(int id)
    {
        var x = await _db.Modalidades.AsNoTracking().FirstOrDefaultAsync(e => e.IdModalidade == id);
        return x is null ? NotFound() : Ok(new LookupResponse(x.IdModalidade, x.Nome));
    }

    [HttpPost]
    public async Task<ActionResult<LookupResponse>> Criar(LookupRequest req)
    {
        if (await _db.Modalidades.AnyAsync(e => e.Nome == req.Nome))
            return Conflict(new { erro = "Já existe um registro com esse nome." });

        var x = new Modalidade { Nome = req.Nome };
        _db.Modalidades.Add(x);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Obter), new { id = x.IdModalidade }, new LookupResponse(x.IdModalidade, x.Nome));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, LookupRequest req)
    {
        var x = await _db.Modalidades.FindAsync(id);
        if (x is null) return NotFound();
        if (await _db.Modalidades.AnyAsync(e => e.Nome == req.Nome && e.IdModalidade != id))
            return Conflict(new { erro = "Já existe um registro com esse nome." });

        x.Nome = req.Nome;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var x = await _db.Modalidades.FindAsync(id);
        if (x is null) return NotFound();
        try
        {
            _db.Modalidades.Remove(x);
            await _db.SaveChangesAsync();
            return NoContent();
        }
        catch (DbUpdateException)
        {
            return Conflict(new { erro = "Registro em uso por cursos; não pode ser excluído." });
        }
    }
}
