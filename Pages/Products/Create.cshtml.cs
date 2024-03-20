using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Wera_Sentry.Pages.Products.IndexModel;
using System.Data.SqlClient;

namespace Wera_Sentry.Pages.Products
{
    public class CreateModel : PageModel
    {
		public ProductInfo productInfo = new ProductInfo();
		public String errorMessage = "";
		public String successMessage = "";
		public void OnGet()
		{
        }

		public void OnPost()
		{
			productInfo.BARCODE = Request.Form["BARCODE"];
			productInfo.CATEGORY_IDs = Request.Form["CATEGORY_IDs"];
			productInfo.PRODUCT_NAME = Request.Form["PRODUCT_NAME"];
			productInfo.unit_Of_measure = Request.Form["unit_Of_measure"];
			productInfo.PRODUCT_QUANTITY = Request.Form["PRODUCT_QUANTITY"];
			productInfo.PRODUCT_DESCRIPTION = Request.Form["PRODUCT_DESCRIPTION"];
			productInfo.SUPPLIER_IDs = Request.Form["SUPPLIER_IDs"];
			productInfo.SUPPLIER_NAME = Request.Form["SUPPLIER_NAME"];
			productInfo.re_order_point = Request.Form["re_order_point"];
			productInfo.costPrice = Request.Form["costPrice"];
			productInfo.sellingPrice = Request.Form["sellingPrice"];
			productInfo.profitPercentage = Request.Form["profitPercentage"];
			if (productInfo.BARCODE.Length == 0 || productInfo.CATEGORY_IDs.Length == 0 ||
				productInfo.PRODUCT_NAME.Length == 0 || productInfo.unit_Of_measure.Length == 0
				|| productInfo.PRODUCT_QUANTITY.Length == 0 || productInfo.PRODUCT_DESCRIPTION.Length == 0
				|| productInfo.SUPPLIER_IDs.Length == 0 || productInfo.SUPPLIER_NAME.Length == 0
				|| productInfo.re_order_point.Length == 0 || productInfo.costPrice.Length == 0
				|| productInfo.sellingPrice.Length == 0 || productInfo.profitPercentage.Length == 0)
			{
				errorMessage = "All the fields Must be filled!";
				return;
			}
			try
			{
				String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";
				using (SqlConnection sqlConnection = new SqlConnection(connectionString))
				{

					sqlConnection.Open();
					String sql = "INSERT INTO product " +
						"(BARCODE, CATEGORY_IDs, PRODUCT_NAME, unit_Of_measure ,PRODUCT_QUANTITY, PRODUCT_DESCRIPTION,SUPPLIER_IDs,SUPPLIER_NAME,re_order_point,costPrice,sellingPrice,profitPercentage) VALUES " +
						"(@name, @contactNumber,@email, @address,@PRODUCT_QUANTITY, @PRODUCT_DESCRIPTION,@SUPPLIER_IDs,@SUPPLIER_NAME,@re_order_point,@costPrice,@sellingPrice,@profitPercentage); ";
					using (SqlCommand command = new SqlCommand(sql, sqlConnection))
					{
						command.Parameters.AddWithValue("@BARCODE", productInfo.BARCODE);
						command.Parameters.AddWithValue("@CATEGORY_IDs", productInfo.CATEGORY_IDs);
						command.Parameters.AddWithValue("@PRODUCT_NAME", productInfo.PRODUCT_NAME);
						command.Parameters.AddWithValue("@unit_Of_measure", productInfo.unit_Of_measure);
						command.Parameters.AddWithValue("@PRODUCT_QUANTITY", productInfo.PRODUCT_QUANTITY);
						command.Parameters.AddWithValue("@PRODUCT_DESCRIPTION", productInfo.PRODUCT_DESCRIPTION);
						command.Parameters.AddWithValue("@SUPPLIER_IDs", productInfo.SUPPLIER_IDs);
						command.Parameters.AddWithValue("@SUPPLIER_NAME", productInfo.SUPPLIER_NAME);
						command.Parameters.AddWithValue("@re_order_point", productInfo.re_order_point);
						command.Parameters.AddWithValue("@costPrice", productInfo.costPrice);
						command.Parameters.AddWithValue("@sellingPrice", productInfo.sellingPrice);
						command.Parameters.AddWithValue("@profitPercentage", productInfo.profitPercentage);

						command.ExecuteNonQuery();

						Response.Redirect("/Products/Index");
					}
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
				return;
			}
			//Save the new Product into the database
			productInfo.BARCODE = ""; productInfo.CATEGORY_IDs = ""; productInfo.PRODUCT_NAME = ""; productInfo.unit_Of_measure = ""; productInfo.PRODUCT_QUANTITY = ""; productInfo.PRODUCT_DESCRIPTION = ""; productInfo.SUPPLIER_IDs = ""; productInfo.SUPPLIER_NAME = ""; productInfo.re_order_point = ""; productInfo.costPrice = ""; productInfo.sellingPrice = ""; productInfo.profitPercentage = "";
			successMessage = "New product Added ";
		}
	}
}
