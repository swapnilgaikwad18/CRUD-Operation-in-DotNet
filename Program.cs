using Microsoft.Data.SqlClient;
using System.Data;
using System.Numerics;

namespace CRUD_Operation_in_DotNet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Customer customer = new Customer();
            bool order = true;

            while (order)
            {
                int val;
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("1: Insert a Customer");
                Console.WriteLine("2: Display all Customer's Details.");
                Console.WriteLine("3: Update a Customer's Details.");
                Console.WriteLine("4: Delete a Customer.");
                Console.WriteLine("0: Exit");
                Console.Write("Select options from 0-4: ");
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

                    case 2:
                        List<Customer> allCust = DisplayCustomers();
                        Console.WriteLine("All Customers Details: ");
                        
                        if(allCust.Count > 0)
                        {
                            foreach (Customer cst in allCust)
                            {
                                Console.WriteLine("(CustID | Name | Address | Email | Mobile)");
                                Console.Write($"CustID: {cst.CustID} | ");
                                Console.Write($"Name: {cst.Name} | ");
                                Console.Write($"Address: {cst.Address} | ");
                                Console.Write($"Email: {cst.Email} | ");
                                Console.WriteLine($"Mobile: {cst.Mobile}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No data available.");
                        }
                        break;

                    case 3:
                        Console.Write("Enter CustID to be updated: ");
                        int CustID = Convert.ToInt32(Console.ReadLine());

                        Customer customer2 = new Customer();

                        Console.Write("Enter CustID: ");
                        customer2.CustID = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Name: ");
                        customer2.Name = Console.ReadLine();
                        Console.Write("Enter Address: ");
                        customer2.Address = Console.ReadLine();
                        Console.Write("Enter Email: ");
                        customer2.Email = Console.ReadLine();
                        Console.Write("Enter Mobile: ");
                        customer2.Mobile = Convert.ToDouble(Console.ReadLine());

                        updateCustomer(customer2);
                        break; 
                    
                    case 4:
                        Console.Write("Enter CustID to be deleted: ");
                        int Cid = Convert.ToInt32(Console.ReadLine());

                        DeleteCustomer(Cid);
                        break;

                    case 0:
                        order = false;
                        Console.WriteLine("Ended!");
                        break;

                    default:
                        Console.WriteLine("Option not Available. Please enter options between 0-4.");
                        break;

                }
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

        static List<Customer> DisplayCustomers()
        {
            List<Customer> lstCust = new List<Customer>();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=KTjune23;Integrated Security=True";
            cn.Open();

            try
            {             
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Customers";

                SqlDataReader dr = cmd.ExecuteReader();

                

                while (dr.Read())
                {
                    Customer customer = new Customer();
                    customer.CustID = dr.GetInt32("CustID");
                    customer.Name = dr.GetString("Name");
                    customer.Address = dr.GetString("Address");
                    customer.Email = dr.GetString("Email");
                    customer.Mobile = dr.GetInt64("Mobile");
                    lstCust.Add(customer);
                }
                dr.Close(); 
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally 
            { 
                cn.Close(); 
            }

            return lstCust;
        }

        static void updateCustomer(Customer customer)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=KTjune23;Integrated Security=True";
            cn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE Customers SET Name = @Name, Address = @Address, Email = @Email, Mobile = @Mobile WHERE CustID = @CustID";

                cmd.Parameters.AddWithValue("@CustID", customer.CustID);
                cmd.Parameters.AddWithValue("@Name", customer.Name);
                cmd.Parameters.AddWithValue("@Address", customer.Address);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@Mobile", customer.Mobile);

                int check = cmd.ExecuteNonQuery();
                if (check > 0)
                {
                    Console.WriteLine("Customer updated successfully...");
                }
                else
                {
                    Console.WriteLine("Customer not found.");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally 
            { 
                cn.Close(); 
            }
        }

        static void DeleteCustomer(int CustID)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=KTjune23;Integrated Security=True";
            cn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM Customers WHERE CustID = @CustID";

                cmd.Parameters.AddWithValue("@CustID", CustID);

                int check = cmd.ExecuteNonQuery();
                if (check > 0)
                {
                    Console.WriteLine("Customer Deleted Successfully...");
                }
                else
                {
                    Console.WriteLine("Customer not found.");
                }
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