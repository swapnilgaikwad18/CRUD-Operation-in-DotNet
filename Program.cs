using Microsoft.Data.SqlClient;
using System.Data;

namespace CRUD_Operation_in_DotNet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Customer customer = new Customer();

            int val;
            Console.WriteLine("Select options from 0-4: ");
            Console.WriteLine("1: Insert a Customer");
            Console.WriteLine("2: Display all Customer's Details.");
            Console.WriteLine("3: Update a Customer's Details.");
            Console.WriteLine("4: Delete a Customer.");
            Console.WriteLine("0: End");
            val = Convert.ToInt32(Console.ReadLine());

            switch (val)
            {
                //1: Insert a Customer.
                case 1:
                    Console.WriteLine("Enter Customer's details to be inserted: (CustID | Name | Address | Email | Mobile)");
                    Customer customer1 = new Customer();

                    Console.Write("Enter CustID: ");
                    customer1.CustID = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter Name: ");
                    customer1.Name = Console.ReadLine();
                    Console.Write("Enter Address: ");
                    customer1.Address = Console.ReadLine();
                    Console.Write("Enter Email: ");
                    customer1.Email = Console.ReadLine();
                    Console.Write("Enter Mobile: ");
                    customer1.Mobile = Convert.ToDouble(Console.ReadLine());

                    InsertData(customer1);
                    break;

                case 0:
                    Console.WriteLine("Ended!");
                    break;

                default:
                    Console.WriteLine("Option not Available. Please enter options between 0-4.");
                    break;

            }
        }

        static void connect()
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=KTjune23;Integrated Security=True";
            cn.Open();

            Console.WriteLine("Open");

            cn.Close();
        }

        static void InsertData(Customer customer)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=KTjune23;Integrated Security=True";
            cn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "InsertCustomer";

                cmd.Parameters.AddWithValue("@CustID", customer.CustID);
                cmd.Parameters.AddWithValue("@Name", customer.Name);
                cmd.Parameters.AddWithValue("@Address", customer.Address);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@Mobile", customer.Mobile);

                cmd.ExecuteNonQuery();

                Console.WriteLine("Customers Details Inserted Successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        static Customer DisplayCustomers()
        {


            return null;
        }

    }

    class Customer
    {
        public int CustID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public double Mobile { get; set; }
    }
}