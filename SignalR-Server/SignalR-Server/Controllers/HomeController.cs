using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR_Server.Business;
using System.Threading.Tasks;

namespace SignalR_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        readonly MyBusiness _myBusiness;
        public HomeController(MyBusiness myBusiness)
        {
            _myBusiness = myBusiness;
        }

        [HttpGet("{message}/{connectionId}")]
        public async Task<IActionResult> Index(string message, string connectionId)
        {
            await _myBusiness.SendMessageAsync(message, connectionId); // İş mantığınıza connectionId'yi de gönderiyoruz.
            return Ok();
        }
    }
}
