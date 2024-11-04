using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Rika_ReportProvider.Models;
using Rika_ReportProvider.Services;
using System.Data;
using System.Data.SqlClient;

namespace Rika_ReportProvider.Functions;

public class ProductsReport
{
    private readonly HttpReqService _httpReqService;
    private readonly IConfiguration _config;
    private readonly ILogger<ProductsReport> _logger;

    public ProductsReport(ILogger<ProductsReport> logger, IConfiguration config, HttpReqService httpReqService)
    {
        _logger = logger;
        _config = config;
        _httpReqService = httpReqService;
    }

    [Function("ProductsReport")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
    {
        try
        {
            var products = new List<Product>();

            var sqlConnString = _config.GetConnectionString("ProductsSql");

            using (SqlConnection connection = new SqlConnection(sqlConnString))
            {
                using (var command = new SqlCommand("SP_GET_ALL_PRODUCTS", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    _httpReqService.MapParametersToSqlCommand(req, command);

                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var product = new Product
                            {
                                Id = reader.GetString(reader.GetOrdinal("Id")),
                                ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                                ProductPrice = reader.GetDecimal(reader.GetOrdinal("ProductPrice")),
                                ProductSalePrice = reader.GetDecimal(reader.GetOrdinal("ProductSalePrice")),
                                ProductDescription = reader.GetString(reader.GetOrdinal("ProductDescription")),
                                ProductCategoryId = reader.GetInt32(reader.GetOrdinal("ProductCategoryId")),
                                ProductSize = reader.GetString(reader.GetOrdinal("ProductSize")),
                                ProductColor = reader.GetString(reader.GetOrdinal("ProductColor")),
                            };

                            products.Add(product);
                        }
                    }
                }
            }
            return new OkObjectResult(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return new BadRequestObjectResult(ex.Message);
        }
    }
}