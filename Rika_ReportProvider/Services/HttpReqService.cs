using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;

namespace Rika_ReportProvider.Services;

public class HttpReqService
{
    public void MapParametersToSqlCommand(HttpRequest req, SqlCommand command)
    {

        //Develop this to a generic mapping in the future pls.

        var id = req.Query["Id"].ToString();
        var productName = req.Query["ProductName"].ToString();
        var productDescription = req.Query["ProductDescription"].ToString();
        var productColor = req.Query["ProductColor"].ToString();
        var productSize = req.Query["ProductSize"].ToString();
        var productPrice = req.Query["ProductPrice"];
        var productSalePrice = req.Query["ProductSalePrice"];

        command.Parameters.AddWithValue("@Id", string.IsNullOrEmpty(id) ? null : id);
        command.Parameters.AddWithValue("@ProductName", string.IsNullOrEmpty(productName) ? null : productName);
        command.Parameters.AddWithValue("@ProductDescription", string.IsNullOrEmpty(productDescription) ? null : productDescription);
        command.Parameters.AddWithValue("@ProductColor", string.IsNullOrEmpty(productColor) ? null : productColor);
        command.Parameters.AddWithValue("@ProductSize", string.IsNullOrEmpty(productSize) ? null : productSize);
        command.Parameters.AddWithValue("@ProductPrice", DBNull.Value);
        command.Parameters.AddWithValue("@ProductSalePrice", DBNull.Value);

        return;
    }
}