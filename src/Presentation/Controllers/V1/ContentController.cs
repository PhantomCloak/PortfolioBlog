using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.V1.Commands;
using Shared.Contracts.V1.Queries;
using Shared.Contracts.V1.Routes;
using Shared.DTOs;

namespace Presentation.Controllers.V1
{
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly IContentService _contentService;
        private readonly IUriService _uriService;
        private readonly bool _initializedFirstRunTask;

        public ContentController(IContentService contentService, IUriService uriService, IMapper mapper)
        {
            _contentService = contentService;
            _uriService = uriService;

            if (!_initializedFirstRunTask)
            {
                new StartupTasks(contentService, mapper).GenerateTaskContents();
                _initializedFirstRunTask = true;
            }
        }

        [HttpGet(ApiRoutes.Content.GetAll)]
        public async Task<IActionResult> GetContents()
        {
            var contents = await _contentService.GetAllContentAsync();

            if (!contents.Any())
            {
                return NoContent();
            }

            return Ok(contents);
        }

        [HttpGet(ApiRoutes.Content.Get)]
        public async Task<IActionResult> GetContent(string contentKey)
        {
            var content = await _contentService.GetContentAsync(new ContentQuery {ContentName = contentKey});

            return Ok(content);
        }

        [HttpPost(ApiRoutes.Content.Create)]
        public async Task<IActionResult> CreateContent([FromBody] ContentCommand command)
        {
            var existingContent = await _contentService.ContentExistAsync(command.ContentName);

            if (existingContent)
            {
                return BadRequest("There is already a content with a same key");
            }

            var createdContent = await _contentService.CreateContentAsync(command);

            if (createdContent)
            {
                return BadRequest("Resource failed to create.");
            }

            var resourceUri = _uriService.GetContentUri(createdContent.ContentName);
            return Created(resourceUri, createdContent);
        }

        [HttpPut(ApiRoutes.Content.Update)]
        public async Task<IActionResult> UpdateContent([FromBody] ContentDto contentToUpdate)
        {
            var existingContent = await _contentService.ContentExistAsync(contentToUpdate.ContentName);

            if (existingContent)
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

        [HttpDelete(ApiRoutes.Content.Delete)]
        public async Task<IActionResult> DeleteContent(string contentKey)
        {
            var existingContent = await _contentService.ContentExistAsync(contentKey);

            if (existingContent)
            {
                return NotFound("Requested resource not found.");
            }

            var deletionResult = await _contentService.DeleteContentAsync(new ContentQuery {ContentName = contentKey});

            if (!deletionResult)
            {
                return BadRequest("Deletion failed.");
            }

            return Ok();
        }
    }
}
