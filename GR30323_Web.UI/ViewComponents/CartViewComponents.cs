using Microsoft.AspNetCore.Mvc;

namespace GR30323_Web.UI.ViewComponents
{
    public class CartViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
