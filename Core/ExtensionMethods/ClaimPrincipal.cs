using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace ProjectFinal101.Core.ExtensionMethods
{
    public static class ControllerBaseExtension
    {
        public static string GetUserId(this ControllerBase controller)
        {
            return controller.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
