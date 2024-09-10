using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library.WebAPI.Controllers
{
	public class ApiBaseController : ControllerBase
	{
		protected int UserId => int.Parse(FindClaim(ClaimTypes.NameIdentifier));
		string? FindClaim(string claimName)
		{
			var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
			var claim = claimsIdentity?.FindFirst(claimName);
			if (claim == null) return null;
			return claim.Value;
		}
	}
}
