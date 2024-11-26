using Microsoft.AspNetCore.Mvc;
using AplicacaoWeb2.Data;
using AplicacaoWeb2.Models;
using System;
using System.Linq;

namespace AplicacaoWeb2.Controllers
{
    public class DoadorController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public DoadorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Doador
        public IActionResult Index(string search)
        {
            var doadores = _context.Doadores.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                if (int.TryParse(search, out int id))
                {
                    // Busca por Id
                    doadores = doadores.Where(d => d.Id == id);
                }
                else
                {
                    // Busca por Nome
                    doadores = doadores.Where(d => d.Nome.Contains(search));
                }
            }

            return View(doadores.ToList());
        }


        // Método para gerar dados aleatórios
        public IActionResult GerarDados(int quantidade = 10)
        {
            var random = new Random();

            for (int i = 0; i < quantidade; i++)
            {
                var doador = new Doador
                {
                    Nome = $"Doador {random.Next(1000, 9999)}",
                    Descricao = $"Descrição do doador {random.Next(1000, 9999)}",
                    CpfCnpj = random.Next(100000000, 999999999).ToString("D11")
                };

                _context.Doadores.Add(doador);
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: Doador/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            var doador = _context.Doadores.FirstOrDefault(m => m.Id == id);
            if (doador == null) return NotFound();

            return View(doador);
        }

        // GET: Doador/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Doador/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Nome,Descricao,CpfCnpj")] Doador doador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doador);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(doador);
        }

        // GET: Doador/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var doador = _context.Doadores.Find(id);
            if (doador == null) return NotFound();

            return View(doador);
        }

        // POST: Doador/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Nome,Descricao,CpfCnpj")] Doador doador)
        {
            if (id != doador.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doador);
                    _context.SaveChanges();
                }
                catch
                {
                    if (!_context.Doadores.Any(e => e.Id == doador.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(doador);
        }

        // GET: Doador/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var doador = _context.Doadores.FirstOrDefault(m => m.Id == id);
            if (doador == null) return NotFound();

            return View(doador);
        }

        // POST: Doador/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var doador = _context.Doadores.Find(id);
            if (doador != null)
            {
                _context.Doadores.Remove(doador);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
