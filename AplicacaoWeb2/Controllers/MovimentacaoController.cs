using Microsoft.AspNetCore.Mvc;
using AplicacaoWeb2.Data;
using AplicacaoWeb2.Models;
using System;
using System.Linq;

namespace AplicacaoWeb2.Controllers
{
    public class MovimentacaoController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public MovimentacaoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Listar todas as movimentações
        public IActionResult Index()
        {
            var movimentacoes = _context.Movimentacoes.ToList();
            return View(movimentacoes);
        }

        // Criar movimentação
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Movimentacao movimentacao)
        {
            if (ModelState.IsValid)
            {
                movimentacao.DataMovimentacao = DateTime.Now; // Define a data como agora
                _context.Movimentacoes.Add(movimentacao);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(movimentacao);
        }

        // Programar movimentação futura
        [HttpGet]
        public IActionResult ProgramarMovimentacao()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProgramarMovimentacao(Movimentacao movimentacao)
        {
            if (ModelState.IsValid)
            {
                _context.Movimentacoes.Add(movimentacao);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(movimentacao);
        }

        // GET: Movimentacao/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            var movimentacao = _context.Movimentacoes.FirstOrDefault(m => m.Id == id);
            if (movimentacao == null) return NotFound();

            return View(movimentacao);
        }

        // GET: Movimentacao/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var movimentacao = _context.Movimentacoes.Find(id);
            if (movimentacao == null) return NotFound();

            return View(movimentacao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Movimentacao movimentacao)
        {
            if (id != movimentacao.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(movimentacao);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(movimentacao);
        }

        // GET: Movimentacao/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var movimentacao = _context.Movimentacoes.FirstOrDefault(m => m.Id == id);
            if (movimentacao == null) return NotFound();

            return View(movimentacao);
        }

        // POST: Movimentacao/DeleteConfirmed/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var movimentacao = _context.Movimentacoes.Find(id);
            if (movimentacao != null)
            {
                _context.Movimentacoes.Remove(movimentacao);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
