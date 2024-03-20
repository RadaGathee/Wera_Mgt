using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace Wera_Sentry.Pages.Products
{
	public class IndexModel : PageModel
	{
		public List<ProductInfo> listProducts = new List<ProductInfo>();
		public void OnGet()
		{
			try
			{
				String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM products";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						using(SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								/*
								 * 
								ProductInfo productInfo = new ProductInfo();
								productInfo.barcode = "" + reader.GetInt32(0);
								productInfo.categoryIds = "" + reader.GetInt32(1);
								productInfo.productName = reader.GetString(2);
								productInfo.unitOfmeasure = "" + reader.GetInt32(3);
								productInfo.productQuantity = "" + reader.GetInt32(4);
								productInfo.productDescription = reader.GetString(5);
								productInfo.supplierIds = "" + reader.GetInt32(6);
								productInfo.supplierName = reader.GetString(7);
								productInfo.reOrderpoint = "" + reader.GetInt32(8);
								productInfo.costPrice = "" + reader.GetInt32(9);
								productInfo.sellingPrice = "" + reader.GetInt32(10);
								productInfo.profitPercentage = "" + reader.GetInt32(11);

								listProducts.Add(productInfo);

								*/

								ProductInfo productInfo = new ProductInfo();
								productInfo.BARCODE = "" + reader.GetInt32(0);
								productInfo.CATEGORY_IDs = "" + reader.GetInt32(1);
								productInfo.PRODUCT_NAME = reader.GetString(2);
								productInfo.unit_Of_measure = "" + reader.GetInt32(3);
								productInfo.PRODUCT_QUANTITY = "" + reader.GetInt32(4);
								productInfo.PRODUCT_DESCRIPTION = reader.GetString(5);
								productInfo.SUPPLIER_IDs = "" + reader.GetInt32(6);
								productInfo.SUPPLIER_NAME = reader.GetString(7);
								productInfo.re_order_point = "" + reader.GetInt32(8);
								productInfo.costPrice = "" + reader.GetInt32(9);
								productInfo.sellingPrice = "" + reader.GetInt32(10);
								productInfo.profitPercentage = "" + reader.GetInt32(11);

								listProducts.Add(productInfo);

							}
						}
					}
				}

			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception: " + ex.ToString());

			}
		}

		public class ProductInfo
		{

			/*
			public String barcode;
			public String categoryIds;
			public String productName;
			public String unitOfmeasure;
			public String productQuantity;
			public String productDescription;
			public String supplierIds;
			public String supplierName;
			public String reOrderpoint;
			public String costPrice;
			public String sellingPrice;
			public String profitPercentage;

			*/

			public String BARCODE;
			public String CATEGORY_IDs;
			public String PRODUCT_NAME;
			public String unit_Of_measure;
			public String PRODUCT_QUANTITY;
			public String PRODUCT_DESCRIPTION;
			public String SUPPLIER_IDs;
			public String SUPPLIER_NAME;
			public String re_order_point;
			public String costPrice;
			public String sellingPrice;
			public String profitPercentage;
		}
	}
}
