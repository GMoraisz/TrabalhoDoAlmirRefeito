using Microsoft.AspNetCore.Mvc;
using AplicacaoWeb2.Data;
using AplicacaoWeb2.Models;
using System;
using System.Linq;

namespace AplicacaoWeb2.Controllers
{
    public class ResponsavelController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public ResponsavelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Responsavel
        public IActionResult Index(string search)
        {
            var responsaveis = _context.Responsaveis.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                if (int.TryParse(search, out int id))
                {
                    // Busca por Id
                    responsaveis = responsaveis.Where(r => r.Id == id);
                }
                else
                {
                    // Busca por Nome
                    responsaveis = responsaveis.Where(r => r.Nome.Contains(search));
                }
            }

            return View(responsaveis.ToList());
        }


        // Método para gerar dados aleatórios
        public IActionResult GerarDados(int quantidade = 10)
        {
            var random = new Random();

            for (int i = 0; i < quantidade; i++)
            {
                var responsavel = new Responsavel
                {
                    Nome = $"Responsável {random.Next(1000, 9999)}",
                    Telefone = $"(11) 9{random.Next(10000000, 99999999)}",
                    Endereco = $"Rua {random.Next(1, 200)}, Número {random.Next(10, 500)}"
                };

                _context.Responsaveis.Add(responsavel);
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: Responsavel/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            var responsavel = _context.Responsaveis.FirstOrDefault(m => m.Id == id);
            if (responsavel == null) return NotFound();

            return View(responsavel);
        }

        // GET: Responsavel/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Responsavel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Nome,Telefone,Endereco")] Responsavel responsavel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(responsavel);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(responsavel);
        }

        // GET: Responsavel/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var responsavel = _context.Responsaveis.Find(id);
            if (responsavel == null) return NotFound();

            return View(responsavel);
        }

        // POST: Responsavel/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Nome,Telefone,Endereco")] Responsavel responsavel)
        {
            if (id != responsavel.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(responsavel);
                    _context.SaveChanges();
                }
                catch
                {
                    if (!_context.Responsaveis.Any(e => e.Id == responsavel.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(responsavel);
        }

        // GET: Responsavel/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var responsavel = _context.Responsaveis.FirstOrDefault(m => m.Id == id);
            if (responsavel == null) return NotFound();

            return View(responsavel);
        }

        // POST: Responsavel/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var responsavel = _context.Responsaveis.Find(id);
            if (responsavel != null)
            {
                _context.Responsaveis.Remove(responsavel);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
