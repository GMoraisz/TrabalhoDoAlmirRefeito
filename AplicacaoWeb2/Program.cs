using AplicacaoWeb2.Data;
using Microsoft.EntityFrameworkCore;

namespace AplicacaoWeb2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configura��o do banco de dados
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Adicionar suporte ao HttpContext e sess�es
            builder.Services.AddHttpContextAccessor(); // Necess�rio para acessar HttpContext nas views
            builder.Services.AddDistributedMemoryCache(); // Cache para sess�es
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Sess�es expiram ap�s 30 minutos
                options.Cookie.HttpOnly = true; // Acesso apenas via HTTP
                options.Cookie.IsEssential = true; // Essencial para funcionamento
            });

            // Adicionar suporte ao MVC
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configura��o do pipeline HTTP
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts(); // Configura��o de seguran�a HTTP
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Configura��o de autentica��o e sess�es
            app.UseSession(); // Habilita o uso de sess�es
            app.UseAuthorization(); // Mant�m suporte a autoriza��o, caso seja necess�rio

            // Configura��o de rotas
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
