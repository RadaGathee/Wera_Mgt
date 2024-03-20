using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace Wera_Sentry.Pages.Customers
{
    public class IndexModel : PageModel
    {
        public List<CustomerInfo> listCustomers = new List<CustomerInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM customer";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CustomerInfo customerInfo = new CustomerInfo();
                                customerInfo.customerId = "" + reader.GetInt32(0);
                                customerInfo.name = reader.GetString(1);
                                customerInfo.contactNumber = reader.GetString(2);
                                customerInfo.email = reader.GetString(3);
                                customerInfo.address = reader.GetString(4);
                                customerInfo.createdAt = reader.GetDateTime(5).ToString();
                                customerInfo.updatedAt = reader.GetDateTime(6).ToString();

                                listCustomers.Add(customerInfo);
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

        public class CustomerInfo
        {
            public String customerId;
            public String name;
            public String contactNumber;
            public String email;
            public String address;
            public String createdAt;
            public String updatedAt;
        }
    }
}
