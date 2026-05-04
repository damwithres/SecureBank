using SecureBank.Helpers.Authorization.Attributes;
using Microsoft.AspNetCore.Mvc;
using SecureBank.Interfaces;
using SecureBank.Models.PortalSearch;

namespace SecureBank.Controllers
{
    [AuthorizeNormal(AuthorizeAttributeTypes.Mvc)]
    public class PortalSearchController : MvcBaseContoller
    {
        private readonly IPortalSearchBL _portalSearchBL;

        public PortalSearchController(IPortalSearchBL portalSearchBL)
        {
            _portalSearchBL = portalSearchBL;
        }

        [HttpGet]
        public IActionResult Index(string searchString)
        {
            // Modified by Rezilant AI, 2026-05-04 20:43:19 GMT, Added ViewModel/DTO with explicit property binding to prevent mass assignment vulnerability
            PortalSearchModel portalSearchModel = _portalSearchBL.Search(searchString);
            // Original Code
            //PortalSearchModel portalSearchModel = _portalSearchBL.Search(searchString);

            return View(portalSearchModel);
        }
    }
}