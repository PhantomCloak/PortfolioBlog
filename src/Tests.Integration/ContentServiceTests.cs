using System.Collections.Generic;
using AutoMapper;
using BusinessLogic.Maps;
using BusinessLogic.Services;
using Dapper;
using Dapper.FluentMap;
using DataAccess.Maps;
using DataAccess.Repositories;
using KellermanSoftware.CompareNetObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Shared.Contracts.V1.Commands;
using Shared.Contracts.V1.Queries;
using Shared.DTOs;

namespace Tests.Integration
{
    public class ContentServiceTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            SqlMapper.ResetTypeHandlers();
            FluentMapper.EntityMaps.Clear();
            FluentMapper.TypeConventions.Clear();
            
            SqlMapper.AddTypeHandler(new ContentParseMap());
            FluentMapper.Initialize(config => { config.AddMap(new ContentMap()); });
        }

        [Test]
        public void Add_Content_Return_Same_Content()
        {
            //Arrange
            var createCommand = new ContentCommand
            {
                ContentName = "Foo:Posts",
                ContentFields = new Dictionary<string, string>
                {
                    {"Article", "Hello World"},
                    {"Body", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor"}
                }
            };
            var readQuery = new ContentQuery
            {
                ContentName = "Foo:Posts"
            };

            var expectedContent = new ContentDto
            {
                ContentName = "Foo:Posts",
                ContentFields = new Dictionary<string, string>
                {
                    {"Article", "Hello World"},
                    {"Body", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor"}
                }
            };
            
            var contentService = GetContentService();
            var compareLogic = new CompareLogic();
            //Act
            contentService.CreateContentAsync(createCommand).Wait();

            var retrievedContent = contentService.GetContentAsync(readQuery).Result;
            
            //Assert
            var compareResult = compareLogic.Compare(expectedContent, retrievedContent);

            Assert.IsTrue(compareResult.AreEqual,
                $"Added and retrieved roles aren't identical differences are {string.Join(",", compareResult.Differences)}");
        }

        [TearDown]
        public void Cleanup()
        {
            var contentService = GetContentService();
            var allContents = contentService.GetAllContentAsync().Result;

            foreach (var content in allContents)
            {
                contentService.DeleteContentAsync(new ContentQuery() {ContentName = content.ContentName});
            }
        }
        
        private static IContentService GetContentService()
        {
            IContentService service = new ContentService(GetRepository(), new Mock<ILogger<ContentService>>().Object, GetMapper());

            return service;
        }

        private static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(typeof(DtoMapper)));
            var mapper = config.CreateMapper();
            return mapper;
        }

        private static IContentRepository GetRepository()
        {
            var configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.test.json", optional: false);
            var configurationRoot = configBuilder.Build();

            var contentRepository = new ContentRepository(configurationRoot,new Mock<ILogger<ContentRepository>>().Object);

            return contentRepository;
        }
    }
}