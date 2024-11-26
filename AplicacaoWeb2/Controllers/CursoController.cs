using Microsoft.AspNetCore.Mvc;
using AplicacaoWeb2.Data;
using AplicacaoWeb2.Models;
using System;
using System.Linq;

namespace AplicacaoWeb2.Controllers
{
    public class CursoController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public CursoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Curso
        public IActionResult Index(string search)
        {
            var cursos = _context.Cursos.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                if (int.TryParse(search, out int id))
                {
                    // Busca por Id
                    cursos = cursos.Where(c => c.Id == id);
                }
                else
                {
                    // Busca por Nome
                    cursos = cursos.Where(c => c.Nome.Contains(search));
                }
            }

            return View(cursos.ToList());
        }


        // Método para gerar dados aleatórios
        public IActionResult GerarDados(int quantidade = 10)
        {
            var random = new Random();

            for (int i = 0; i < quantidade; i++)
            {
                var curso = new Curso
                {
                    Nome = $"Curso {random.Next(1000, 9999)}",
                    Descricao = $"Descrição do curso {random.Next(1000, 9999)}",
                    DataInicio = DateTime.Now.AddDays(-random.Next(0, 365)),
                    DataFim = DateTime.Now.AddDays(random.Next(1, 365))
                };

                _context.Cursos.Add(curso);
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: Curso/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            var curso = _context.Cursos.FirstOrDefault(m => m.Id == id);
            if (curso == null) return NotFound();

            return View(curso);
        }

        // GET: Curso/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Curso/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Nome,Descricao,DataInicio,DataFim")] Curso curso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(curso);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(curso);
        }

        // GET: Curso/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var curso = _context.Cursos.Find(id);
            if (curso == null) return NotFound();

            return View(curso);
        }

        // POST: Curso/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Nome,Descricao,DataInicio,DataFim")] Curso curso)
        {
            if (id != curso.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(curso);
                    _context.SaveChanges();
                }
                catch
                {
                    if (!_context.Cursos.Any(e => e.Id == curso.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(curso);
        }

        // GET: Curso/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var curso = _context.Cursos.FirstOrDefault(m => m.Id == id);
            if (curso == null) return NotFound();

            return View(curso);
        }

        // POST: Curso/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var curso = _context.Cursos.Find(id);
            if (curso != null)
            {
                _context.Cursos.Remove(curso);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
