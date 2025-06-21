using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeSharingPlatform.ViewModels;
using RecipeSharingPlatform.Web.Controllers;
using System.Diagnostics;

public class HomeController : BaseController
{
    [AllowAnonymous]
    [HttpGet]
    public IActionResult Index()
    {
        try 
        {
            if (this.IsUserAuthenticated())
            {
                return this.RedirectToAction(nameof(RecipeController.Index), "Recipe");
            }
            
            return View();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return RedirectToAction(nameof(Index));
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}