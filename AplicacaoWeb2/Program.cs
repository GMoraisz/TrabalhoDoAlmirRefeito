using AplicacaoWeb2.Data;
using Microsoft.EntityFrameworkCore;

namespace AplicacaoWeb2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuração do banco de dados
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Adicionar suporte ao HttpContext e sessões
            builder.Services.AddHttpContextAccessor(); // Necessário para acessar HttpContext nas views
            builder.Services.AddDistributedMemoryCache(); // Cache para sessões
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Sessões expiram após 30 minutos
                options.Cookie.HttpOnly = true; // Acesso apenas via HTTP
                options.Cookie.IsEssential = true; // Essencial para funcionamento
            });

            // Adicionar suporte ao MVC
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configuração do pipeline HTTP
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts(); // Configuração de segurança HTTP
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Configuração de autenticação e sessões
            app.UseSession(); // Habilita o uso de sessões
            app.UseAuthorization(); // Mantém suporte a autorização, caso seja necessário

            // Configuração de rotas
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
