using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.V1;

namespace Presentation.Controllers.V1
{
    public class EntryController : ControllerBase
    {

        [Route(ApiRoutes.Entry.GetAll)]
        public async Task<IActionResult> GetEntries()
        {

            return Ok();
        }
        
        [Route(ApiRoutes.Entry.Get)]
        public async Task<IActionResult> GetEntry()
        {
            
            return Ok();
        }

        [Route(ApiRoutes.Entry.Create)]
        public async Task<IActionResult> CreateEntry()
        {
            
            return Ok();
        }

        [Route(ApiRoutes.Entry.Update)]
        public async Task<IActionResult> UpdateEntry()
        {
            
            return Ok();
        }

        [Route(ApiRoutes.Entry.Delete)]
        public async Task<IActionResult> DeleteEntry()
        {
            
            return Ok();
        }
        
    }
}