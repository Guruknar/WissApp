using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WissAppWebApi.Attributes
{
    public class ClaimsAuthorizeAttribute :AuthorizationFilterAttribute
    {
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        public override Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var principal = actionContext.RequestContext.Principal as ClaimsPrincipal;
            if(!principal.Identity.IsAuthenticated)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return Task.FromResult<object>(null);
            }
            if(!(principal.HasClaim(e => e.Type.ToLower().Equals(ClaimType.ToLower()) && ClaimValue.ToLower().Equals(e.Value.ToLower()))))
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return Task.FromResult<object>(null);
            }
            return Task.FromResult<object>(null);
        }


    }
}