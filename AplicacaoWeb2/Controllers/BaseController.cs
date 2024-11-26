using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AplicacaoWeb2.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (HttpContext.Session.GetString("UserId") == null)
            {
                context.Result = RedirectToAction("Login", "User");
            }

            base.OnActionExecuting(context);
        }
    }
}
