using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using static Wera_Sentry.Pages.Customers.IndexModel;

namespace Wera_Sentry.Pages.Customers
{
    public class EditModel : PageModel
    {
		public CustomerInfo customerInfo = new CustomerInfo();
		public String errorMessage = "";
		public String successMessage = "";
		public void OnGet()
		{
			//String customerId = Request.Query["customerId"];
			string customerId = Request.Query["id"];


			try
			{
				String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM customer WHERE customerId=@customerId";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{

						command.Parameters.AddWithValue("@customerId",customerId);
						using (SqlDataReader reader = command.ExecuteReader())
						{
							if (reader.Read())
							{
								customerInfo.customerId = "" + reader.GetInt32(0);
								customerInfo.name = reader.GetString(1);
								customerInfo.contactNumber = reader.GetString(2);
								customerInfo.email = reader.GetString(3);
								customerInfo.address = reader.GetString(4);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
						return;
			}
		}


        public void OnPost()
        {
			customerInfo.customerId = Request.Form["customerId"];
			customerInfo.name = Request.Form["name"];
			customerInfo.contactNumber = Request.Form["contactNumber"];
			customerInfo.email = Request.Form["email"];
			customerInfo.address = Request.Form["address"];


			if (customerInfo.name.Length == 0 || customerInfo.contactNumber.Length == 0 ||
				customerInfo.email.Length == 0 || customerInfo.address.Length == 0)
			{
				errorMessage = "All the fields Must be filled!";
				return;
			}

			try
			{
				String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{

					connection.Open();
					String sql = "UPDATE customer " +
						"SET name=@name, contactNumber=@contactNumber,email=@email,address=@address,updatedAt = GETDATE() " +
						"WHERE customerId=@customerId";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@name", customerInfo.name);
						command.Parameters.AddWithValue("@contactNumber", customerInfo.contactNumber);
						command.Parameters.AddWithValue("@email", customerInfo.email);
						command.Parameters.AddWithValue("@address", customerInfo.address);

						//should be here 
						command.Parameters.AddWithValue("@customerId", customerInfo.customerId);

						command.ExecuteNonQuery();

						Response.Redirect("/Customers/Index");
					}
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
				return;
			}
		}
    }
}
