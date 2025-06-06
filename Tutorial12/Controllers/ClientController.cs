using Microsoft.AspNetCore.Mvc;
using Tutorial12.Services;

namespace Tutorial12.Controllers;

public class ClientController
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly ITripServise service;

        public ClientsController(ITripServise service)
        {
            service = service;
        }

        [HttpDelete("{idClient}")]
        public async Task<IActionResult> DeleteClient(int idClient)
        {
            var deleted = await service.DeleteClientAsync(idClient);
            return deleted ? NoContent() : BadRequest("Cannot delete client.");
        }
    }
}