using System.Web;
using System.Web.Mvc;

public class CustomAuthorizeAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        var session = filterContext.HttpContext.Session;
        if (session["username"] == null)
        {
            filterContext.Result = new RedirectToRouteResult(
                new System.Web.Routing.RouteValueDictionary(new { controller = "Account", action = "Login" })
            );
        }
        base.OnActionExecuting(filterContext);
    }
}
