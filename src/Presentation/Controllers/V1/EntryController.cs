using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.V1.Commands;
using Shared.Contracts.V1.Queries;
using Shared.Contracts.V1.Routes;
using Shared.DTOs;

namespace Presentation.Controllers.V1
{
    [ApiController]
    public class EntryController : ControllerBase
    {
        private readonly IContentService _contentService;
        private readonly IUriService _uriService;

        public EntryController(IContentService contentService, IUriService uriService)
        {
            _contentService = contentService;
            _uriService = uriService;
        }
        
        [HttpGet(ApiRoutes.Entry.GetAll)]
        public async Task<IActionResult> GetEntries()
        {
            var entries = await _contentService.GetAllContentAsync();

            if (!entries.Any())
            {
                return NoContent();
            }
            
            return Ok(entries);
        }
        
        [HttpGet(ApiRoutes.Entry.Get)]
        public async Task<IActionResult> GetEntry(string contentKey)
        {
            var content = await _contentService.GetContentAsync(new ContentQuery{ContentName = contentKey});
            
            return Ok(content);
        }

        [HttpPost(ApiRoutes.Entry.Create)]
        public async Task<IActionResult> CreateEntry([FromBody]ContentCommand command)
        {
            var existingContent = await _contentService.GetContentAsync(new ContentQuery {ContentName = command.ContentName});

            if (existingContent != null)
            {
                return BadRequest("There is already a content with a same key");
            }

            var createdObject = await _contentService.CreateContentAsync(command);
            
            if (createdObject == null)
            {
                return BadRequest("Resource failed to create.");
            }

            var uri = _uriService.GetContentUri(createdObject.ContentName);
            return Created(uri, createdObject);
        }

        [HttpPut(ApiRoutes.Entry.Update)]
        public async Task<IActionResult> UpdateEntry([FromBody]ContentDto contentToUpdate)
        {
            var existingContent = await _contentService.GetContentAsync(new ContentQuery {ContentName = contentToUpdate.ContentName});

            if (existingContent == null)
            {
                return NotFound("Requested resource not found.");
            }
            
            var updateResult = await _contentService.UpdateContentAsync(contentToUpdate);

            if (!updateResult)
            {
                return BadRequest("An error occured during the updating resource.");
            }

            return Ok();
        }

        [HttpDelete(ApiRoutes.Entry.Delete)]
        public async Task<IActionResult> DeleteEntry(string contentKey)
        {
            var existingContent = await _contentService.GetContentAsync(new ContentQuery {ContentName = contentKey});

            if (existingContent == null)
            {
                return NotFound("Requested resource not found.");
            }
            
            var deletionResult = await _contentService.DeleteContentAsync(new ContentQuery{ContentName = existingContent.ContentName});

            if (!deletionResult)
            {
                return BadRequest("Deletion failed.");
            }

            return Ok();
        }
        
    }
}