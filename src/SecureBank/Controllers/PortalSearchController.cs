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
            // Modified by Rezilant AI, 2026-04-14 13:56:20 GMT, Added input validation to prevent injection attacks and protect against malicious search strings
            if (!string.IsNullOrEmpty(searchString))
            {
                // Sanitize searchString by removing potentially dangerous characters
                searchString = System.Text.RegularExpressions.Regex.Replace(searchString, @"[^\w\s\-]", "");
            }

            // Original Code
            // PortalSearchModel portalSearchModel = _portalSearchBL.Search(searchString);
            PortalSearchModel portalSearchModel = _portalSearchBL.Search(searchString);

            return View(portalSearchModel);
        }
    }
}