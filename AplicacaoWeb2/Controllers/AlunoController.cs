using Microsoft.AspNetCore.Mvc;
using AplicacaoWeb2.Data;
using AplicacaoWeb2.Models;
using System;
using System.Linq;

namespace AplicacaoWeb2.Controllers
{
    public class AlunoController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public AlunoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Aluno
        public IActionResult Index(string search)
        {
            var alunos = _context.Alunos.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                if (int.TryParse(search, out int id))
                {
                    // Busca por Id
                    alunos = alunos.Where(a => a.Id == id);
                }
                else
                {
                    // Busca por Nome
                    alunos = alunos.Where(a => a.Nome.Contains(search));
                }
            }

            return View(alunos.ToList());
        }


        // Método para gerar dados aleatórios
        public IActionResult GerarDados(int quantidade = 10)
        {
            var random = new Random();

            for (int i = 0; i < quantidade; i++)
            {
                var aluno = new Aluno
                {
                    Nome = $"Aluno {random.Next(1000, 9999)}",
                    Nascimento = DateTime.Now.AddYears(-random.Next(18, 40)).AddDays(-random.Next(0, 365)),
                    Endereco = $"Rua {random.Next(1, 200)}, Número {random.Next(10, 500)}",
                    CPF = random.Next(100000000, 999999999).ToString("D11")
                };

                _context.Alunos.Add(aluno);
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: Aluno/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            var aluno = _context.Alunos.FirstOrDefault(m => m.Id == id);
            if (aluno == null) return NotFound();

            return View(aluno);
        }

        // GET: Aluno/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Aluno/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Nome,Nascimento,Endereco,CPF")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aluno);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(aluno);
        }

        // GET: Aluno/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var aluno = _context.Alunos.Find(id);
            if (aluno == null) return NotFound();

            return View(aluno);
        }

        // POST: Aluno/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Nome,Nascimento,Endereco,CPF")] Aluno aluno)
        {
            if (id != aluno.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aluno);
                    _context.SaveChanges();
                }
                catch
                {
                    if (!_context.Alunos.Any(e => e.Id == aluno.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(aluno);
        }

        // GET: Aluno/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var aluno = _context.Alunos.FirstOrDefault(m => m.Id == id);
            if (aluno == null) return NotFound();

            return View(aluno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var aluno = _context.Alunos.Find(id);
            if (aluno != null)
            {
                _context.Alunos.Remove(aluno);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
