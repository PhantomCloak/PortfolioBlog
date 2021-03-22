using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Repositories;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shared.Contracts.V1.Commands;
using Shared.Contracts.V1.Queries;
using Shared.DTOs;
using Shared.Models;

namespace BusinessLogic.Services
{
    public class ContentService : IContentService
    {
        private readonly IContentRepository _contentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ContentService> _logger;

        public ContentService(IContentRepository contentRepository, ILogger<ContentService> logger, IMapper mapper)
        {
            _contentRepository = contentRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContentDto>> GetAllContentAsync()
        {
            var contents = await _contentRepository.GetAllContentsAsync();

            return _mapper.Map<IEnumerable<ContentDto>>(contents);
        }

        public async Task<ContentDto> GetContentAsync(ContentQuery contentQuery)
        {
            var content = await _contentRepository.GetContentByKeyAsync(contentQuery.ContentName);

            if (content == null)
            {
                _logger.LogError($"Content {contentQuery.ContentName} not found.");
                return null;
            }

            return _mapper.Map<ContentDto>(content);
        }

        public async Task<ContentDto> CreateContentAsync(ContentCommand contentCommand)
        {
            var contentToCreate = _mapper.Map<Content>(contentCommand);

            var createResult = await _contentRepository.CreateContentAsync(contentToCreate);

            if (!createResult)
            {
                _logger.LogError($"Content {JsonConvert.SerializeObject(contentToCreate)} failed to create.");
                return null;
            }

            return _mapper.Map<ContentDto>(contentToCreate);
        }

        public async Task<bool> UpdateContentAsync(ContentDto contentDto)
        {
            var content = _mapper.Map<Content>(contentDto);

            var updateResult = await _contentRepository.UpdateContentAsync(content);

            if (!updateResult)
            {
                _logger.LogError($"Content {contentDto.ContentName} failed to update.");
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteContentAsync(ContentQuery contentQuery)
        {
            var deleteResult = await _contentRepository.DeleteContentAsync(contentQuery.ContentName);

            if (!deleteResult)
            {
                _logger.LogError($"Content {contentQuery.ContentName} failed to delete.");
                return false;
            }

            return true;
        }
    }
}