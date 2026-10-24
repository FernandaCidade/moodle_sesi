# SesiCursos.Api (.NET 8 + EF Core + SQL Server)

## Executar
1. Rode o script `sesi_cursos.sql` (e opcionalmente `seed_status.sql`).
2. Ajuste a connection string em `appsettings.json`.
3. `dotnet restore && dotnet run` -> Swagger em `/swagger`.

## Rotas
| Método | Rota | Descrição |
|---|---|---|
| GET | /api/cursos?busca&statusId&modalidadeId&categoriaId&page&pageSize | Lista paginada com filtros |
| GET | /api/cursos/{id} | Detalhe |
| POST | /api/cursos | Cria |
| PUT | /api/cursos/{id} | Atualiza |
| DELETE | /api/cursos/{id} | Exclui |
| GET/POST | /api/categorias, /api/modalidades, /api/status | Lista / cria |
| GET/PUT/DELETE | /api/{categorias,modalidades,status}/{id} | Detalhe / atualiza / exclui |

### Exemplo POST /api/cursos
{
  "nomeCompleto": "Matemática Básica para o Ensino Médio",
  "nomeBreve": "Mat Básica",
  "descricao": "Revisão de conteúdos fundamentais.",
  "cargaHoraria": 40,
  "publicoAlvo": "Estudantes do ensino médio",
  "imagemApresentacao": "img/mat.png",
  "dataDisponibilizacao": "2026-11-01",
  "statusId": 1, "modalidadeId": 3, "categoriaId": 1
}
