using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static Wera_Sentry.Pages.Customers.IndexModel;

namespace Wera_Sentry.Pages.Customers
{
    public class CreateModel : PageModel
    {
        public CustomerInfo customerInfo = new CustomerInfo();
        public String errorMessage = "";
        public String successMessage = "";
		public void OnGet()
        {
        }
        public void OnPost() { 
            customerInfo.name = Request.Form["name"];
            customerInfo.contactNumber = Request.Form["contactNumber"];
            customerInfo.email = Request.Form["email"];
            customerInfo.address = Request.Form["address"];

            if(customerInfo.name.Length == 0 || customerInfo.contactNumber.Length == 0 ||
                customerInfo.email.Length == 0 || customerInfo.address.Length == 0 )
            {
                errorMessage = "All the fields Must be filled!";
                return;
            }
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";
                using(SqlConnection sqlConnection = new SqlConnection(connectionString)) { 
                    
                    sqlConnection.Open();
                    String sql = "INSERT INTO customer " +
						"(name, contactNumber, email, address) VALUES " +
						"(@name, @contactNumber,@email, @address); ";
                    using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@name", customerInfo.name);
                        command.Parameters.AddWithValue("@contactNumber", customerInfo.contactNumber);
                        command.Parameters.AddWithValue("@email", customerInfo.email);
                        command.Parameters.AddWithValue("@address", customerInfo.address);

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
            //Save the new Customer into the database
            customerInfo.name = ""; customerInfo.contactNumber = ""; customerInfo.email = ""; customerInfo.address = "";
            successMessage = "New customer Added ";
		}
    }
}
