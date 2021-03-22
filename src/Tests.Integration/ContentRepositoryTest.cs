using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Dapper;
using Dapper.FluentMap;
using DataAccess.Maps;
using DataAccess.Repositories;
using KellermanSoftware.CompareNetObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Shared.Models;

namespace Tests.Integration
{
    public class ContentRepositoryTest
    {
        [OneTimeSetUp]
        public void Setup()
        {
            //Clear mappings from last tests
            SqlMapper.ResetTypeHandlers();
            FluentMapper.EntityMaps.Clear();
            FluentMapper.TypeConventions.Clear();
            
            SqlMapper.ResetTypeHandlers();
            SqlMapper.AddTypeHandler(new ContentParseMap());
            FluentMapper.Initialize(config => { config.AddMap(new ContentMap()); });
        }

        [Test]
        public void Add_Content_Return_Same_Content()
        {
            //Arrange
            var contentRepository = GetRepository();

            var myContent = new Content()
            {
                ContentName = "Category:Science",
                ContentFields = new Dictionary<string, string>()
                {
                    {"Rating", "5"},
                    {"NumberOfView", "100000"},
                    {"ReleaseDate", new DateTime(2000, 12, 12, 13, 15, 22).ToString(CultureInfo.InvariantCulture)}
                }
            };

            var compareLogic = new CompareLogic();
            compareLogic.Config.IgnoreProperty<Content>(x => x.ContentId);

            //Act
            var createResult = contentRepository.CreateContentAsync(myContent).Result;

            if (!createResult)
            {
                Assert.Fail("Content couldn't write to database.");
            }

            var retrievedContent = contentRepository.GetContentByKeyAsync("Category:Science").Result;
            
            //Assert
            var compareResult = compareLogic.Compare(myContent, retrievedContent);

            Assert.IsTrue(compareResult.AreEqual,
                $"Added and retrieved roles aren't identical differences are {string.Join(",", compareResult.Differences)}");
        }

        [Test]
        public void Add_Multiple_Content_Return_Same_Content()
        {
            //Arrange
            var contentList = new List<Content>
            {
                new Content()
                {
                    ContentName = $"Numbers:{new Random().Next(0,int.MaxValue)}",
                    ContentFields = new Dictionary<string, string>
                    {
                        {"Rating", "5"},
                        {"NumberOfView", "100000"},
                        {
                            "ReleaseDate",
                            new DateTime(2000, 12, 12, 13, 15, 22).ToString(CultureInfo.InvariantCulture)
                        }
                    }
                },
                new Content()
                {
                    ContentName = $"Numbers:{new Random().Next(0,int.MaxValue)}",
                    ContentFields = new Dictionary<string, string>
                    {
                        {"Rating", "5"},
                        {"NumberOfView", "100000"},
                        {
                            "ReleaseDate",
                            new DateTime(2000, 12, 12, 13, 15, 22).ToString(CultureInfo.InvariantCulture)
                        }
                    }
                },
                new Content()
                {
                    ContentName = $"Numbers:{new Random().Next(0,int.MaxValue)}",
                    ContentFields = new Dictionary<string, string>
                    {
                        {"Rating", "5"},
                        {"NumberOfView", "100000"},
                        {
                            "ReleaseDate",
                            new DateTime(2000, 12, 12, 13, 15, 22).ToString(CultureInfo.InvariantCulture)
                        }
                    }
                },
                new Content()
                {
                    ContentName = $"Numbers:{new Random().Next(0,int.MaxValue)}",
                    ContentFields = new Dictionary<string, string>
                    {
                        {"Rating", "5"},
                        {"NumberOfView", "100000"},
                        {
                            "ReleaseDate",
                            new DateTime(2000, 12, 12, 13, 15, 22).ToString(CultureInfo.InvariantCulture)
                        }
                    }
                }
            };
            var contentRepository = GetRepository();
            var compareLogic = new CompareLogic();
            compareLogic.Config.IgnoreProperty<Content>(x => x.ContentId);

            //Act
            foreach (var content in contentList)
            {
                contentRepository.CreateContentAsync(content).Wait();
            }
            
            var retrievedContents = contentRepository.GetAllContentsAsync().Result.OrderBy(x=>x.ContentId).ToList();
            contentList = contentList.OrderBy(x => x.ContentId).ToList();
    
            //Assert
            var compareResult = compareLogic.Compare(contentList, retrievedContents);

            Assert.IsTrue(compareResult.AreEqual,
                $"Added and retrieved roles aren't identical differences are {string.Join(",", compareResult.Differences)}");
        }

        [Test]
        public void Update_Content_Return_Same_Content()
        {
            //Arrange
            var repository = GetRepository();
            
            var myContent = new Content
            {
                ContentName = "Category:Science",
                ContentFields = new Dictionary<string, string>
                {
                    {"Rating", "5"},
                    {"NumberOfView", "100000"},
                    {"ReleaseDate", new DateTime(2000, 12, 12, 13, 15, 22).ToString(CultureInfo.InvariantCulture)}
                }
            };

            //Act
            var compareLogic = new CompareLogic();
            compareLogic.Config.IgnoreProperty<Content>(x => x.ContentId);
            
            var result = repository.CreateContentAsync(myContent).Result;
            if (!result)
            {
                Assert.Fail();
            }

            myContent.ContentFields.Remove("Rating");

            repository.UpdateContentAsync(myContent).Wait();

            var retVal = repository.GetContentByKeyAsync(myContent.ContentName).Result;
            
            //Assert
            var compareResult = compareLogic.Compare(myContent, retVal);

            Assert.IsTrue(compareResult.AreEqual,
                $"Added and retrieved roles aren't identical differences are {string.Join(",", compareResult.Differences)}");
        }

        [Test]
        public void Get_Non_Exist_Content_Returns_Null()
        {
            //Arrange
            var contentRepository = GetRepository();

            //Act
            var createResult = contentRepository.GetContentByKeyAsync("Noo:Exist").Result;

            //Assert
            if (createResult != null)
            {
                Assert.Fail();
            }
            
            Assert.Pass();
        }
        
        [TearDown]
        public void Cleanup()
        {
            var contentRepository = GetRepository();

            var contents = contentRepository.GetAllContentsAsync().Result;

            foreach (var content in contents)
            {
                contentRepository.DeleteContentAsync(content.ContentName).Wait();
            }
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