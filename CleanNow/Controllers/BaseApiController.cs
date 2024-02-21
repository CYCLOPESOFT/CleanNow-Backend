using Microsoft.AspNetCore.Mvc;

namespace CleanNow.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseApiController : Controller
    {
    }
}
