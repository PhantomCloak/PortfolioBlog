using System.Collections.Generic;
using AutoMapper;
using BusinessLogic.Services;
using Shared.Contracts.V1.Commands;
using Shared.Models;

namespace Presentation
{
    public class StartupTasks
    {
        private readonly IContentService _contentService;
        private readonly IMapper _mapper;

        public StartupTasks(IContentService contentService,IMapper mapper)
        {
            _contentService = contentService;
            _mapper = mapper;
        }
        
        public void GenerateTaskContents()
        {
            var firstContent = new Content
            {
                ContentName = "Yazi:OrnekYazi",
                ContentFields = new Dictionary<string, string>()
                {
                    {"Yazar","Unal Ozyurt"},
                    {"Yazi","Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod"},
                    {"Puanlama","4.8"},
                    {"Katagori","Tarih"}
                }
            };

            var secondContent = new Content
            {
                ContentName = "Katagori:Tarih",
                ContentFields = new Dictionary<string, string>()
                {
                    {"Toplam Yazi","15903"},
                    {"Abone","120"}
                }
            };

            if (!_contentService.ContentExistAsync(firstContent.ContentName).Result)
            {
                var contentMap = _mapper.Map<ContentCommand>(firstContent);
                _contentService.CreateContentAsync(contentMap).Wait();
            }
            
            if (!_contentService.ContentExistAsync(secondContent.ContentName).Result)
            {
                var contentMap = _mapper.Map<ContentCommand>(secondContent);
                _contentService.CreateContentAsync(contentMap).Wait();
            }
        }
        
    }
}