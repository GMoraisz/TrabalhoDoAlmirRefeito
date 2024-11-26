using AplicacaoWeb2.Data;
using AplicacaoWeb2.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AplicacaoWeb2.Controllers
{
    public class RelatorioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RelatorioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Método para gerar dados aleatórios
        public IActionResult GerarDadosAleatorios()
        {
            var random = new Random();

            // Criar 5 agendamentos aleatórios
            for (int i = 0; i < 5; i++)
            {
                var agendamento = new Agendamento
                {
                    Descricao = $"Agendamento {random.Next(1000, 9999)}"
                };

                _context.Agendamentos.Add(agendamento);
                _context.SaveChanges();

                // Criar de 1 a 5 visitas para cada agendamento
                int totalVisitas = random.Next(1, 6);
                for (int j = 0; j < totalVisitas; j++)
                {
                    var visita = new Visita
                    {
                        IdAgendamento = agendamento.IdAgendamento,
                        DataVisita = DateTime.Now.AddDays(-random.Next(0, 30))
                    };

                    _context.Visitas.Add(visita);
                }
            }

            _context.SaveChanges();
            return RedirectToAction("RelatorioVisitas");
        }

        // Relatório: Total de Visitas por Agendamento
        public IActionResult RelatorioVisitas()
        {
            var resultados = _context.Agendamentos
                .GroupJoin(
                    _context.Visitas,
                    agendamento => agendamento.IdAgendamento,
                    visita => visita.IdAgendamento,
                    (agendamento, visitas) => new
                    {
                        AgendamentoId = agendamento.IdAgendamento,
                        AgendamentoDescricao = agendamento.Descricao,
                        TotalVisitas = visitas.Count()
                    }
                )
                .ToList();

            return View(resultados);
        }
    }
}
