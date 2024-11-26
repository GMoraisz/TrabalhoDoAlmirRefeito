using Microsoft.AspNetCore.Mvc;
using AplicacaoWeb2.Data;
using AplicacaoWeb2.Models;
using System.Linq;
using Org.BouncyCastle.Crypto.Generators;

namespace AplicacaoWeb2.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: User/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: User/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                // Verificar se o nome de usuário já existe
                if (_context.Users.Any(u => u.Username == user.Username))
                {
                    ModelState.AddModelError("", "O nome de usuário já está em uso.");
                    return View(user);
                }

                // Salvar usuário
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password); // Armazena o hash da senha
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);
        }

        // GET: User/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: User/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                // Salva sessão
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Usuário ou senha inválidos.");
            return View();
        }

        // GET: User/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
