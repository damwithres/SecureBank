using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace SecureBank.Controllers
{
    // Modified by Rezilant AI, 2026-05-01 18:14:29 GMT, Added [Authorize] attribute to enforce authentication by default for all derived controllers
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize]
    public class MvcBaseContoller : Controller
    {
    }
    // Original Code
    // [ApiExplorerSettings(IgnoreApi = true)]
    // public class MvcBaseContoller : Controller
    // {
    // }
}