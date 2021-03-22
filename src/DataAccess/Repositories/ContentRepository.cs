using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shared.Models;

namespace DataAccess.Repositories
{
    public class ContentRepository : IContentRepository
    {
        private readonly ILogger<ContentRepository> _logger;
        private readonly string _connectionString;

        public ContentRepository(IConfiguration configuration, ILogger<ContentRepository> _logger)
        {
            this._logger = _logger;
            _connectionString = configuration["ConnectionStrings:Database"];
        }

        public async Task<IEnumerable<Content>> GetAllContentsAsync()
        {
            await using var connection = new SqlConnection(_connectionString);

            IEnumerable<Content> contents;
            try
            {
                await connection.OpenAsync();

                contents = await connection.QueryAsync<Content>("SELECT * FROM contents");

                await connection.CloseAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(
                    $"An error occured during the select operation message: {e.Message} trace: {e.StackTrace}");
                return Enumerable.Empty<Content>();
            }

            return contents;
        }

        public async Task<bool> CreateContentAsync(Content content)
        {
            await using var connection = new SqlConnection(_connectionString);

            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@contentKey", content.ContentName);
            dynamicParameters.Add("@contentFields", JsonConvert.SerializeObject(content.ContentFields));

            try
            {
                await connection.OpenAsync();

                await connection.ExecuteAsync("INSERT INTO contents VALUES(@contentKey,@contentFields)",
                    dynamicParameters);

                await connection.CloseAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(
                    $"An error occured during the insert operation message: {e.Message} trace: {e.StackTrace}");
                return false;
            }

            return true;
        }

        public async Task<Content> GetContentByKeyAsync(string key)
        {
            await using var connection = new SqlConnection(_connectionString);

            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@contentKey", key);

            Content content;
            try
            {
                await connection.OpenAsync();

                content = (await connection.QueryAsync<Content>(
                        "SELECT TOP 1 * FROM contents WHERE content_name = @contentKey", dynamicParameters))
                    .FirstOrDefault();

                await connection.CloseAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(
                    $"An error occured during the select operation message: {e.Message} trace: {e.StackTrace}");
                return null;
            }

            return content;
        }

        public async Task<bool> UpdateContentAsync(Content contentToUpdate)
        {
            await using var connection = new SqlConnection(_connectionString);

            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@contentName", contentToUpdate.ContentName);
            //BUG seems like registered handler doesn't apply to this query 
            dynamicParameters.Add("@contentFields", JsonConvert.SerializeObject(contentToUpdate.ContentFields),DbType.String);

            try
            {
                await connection.OpenAsync();

                await connection.ExecuteAsync(
                    "UPDATE contents SET content_name = @contentName, content_fields = @contentFields WHERE content_name = @contentName",
                    dynamicParameters);

                await connection.CloseAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(
                    $"An error occured during the update operation message: {e.Message} trace: {e.StackTrace}");
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteContentAsync(string contentKey)
        {
            await using var connection = new SqlConnection(_connectionString);

            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@contentKey", contentKey);

            try
            {
                await connection.OpenAsync();

                await connection.ExecuteAsync("DELETE FROM contents WHERE content_name = @contentKey", dynamicParameters);

                await connection.CloseAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(
                    $"An error occured during the delete operation message: {e.Message} trace: {e.StackTrace}");
                return false;
            }

            return true;
        }
    }
}