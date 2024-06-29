using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioAppBackend.Errors;


namespace PortfolioAppBackend.Controllers
{
    [Route("errores/{codigo}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        public IActionResult Error (int codigo)
        {
            return new ObjectResult(new ApiErrorResponse(codigo));
        }
    }
}
