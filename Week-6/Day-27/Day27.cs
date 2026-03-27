using System;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data;

namespace ConsoleApp8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connStr = config.GetConnectionString("DefaultConnection");
            using SqlConnection con = new SqlConnection(connStr);
            while (true)
            {
                Console.WriteLine("\nOperations:");
                Console.WriteLine("1. Insert Product");
                Console.WriteLine("2. View All Products");
                Console.WriteLine("3. Update Product");
                Console.WriteLine("4. Delete Product");
                Console.WriteLine("5. Get Product by ID");
                Console.WriteLine("6. Exit");
                Console.Write("Enter option: ");
                int option = int.Parse(Console.ReadLine()!);
                switch (option)
                {
                    case 1: InsertProduct(con); break;
                    case 2: ViewProducts(con); break;
                    case 3: UpdateProduct(con); break;
                    case 4: DeleteProduct(con); break;
                    case 5: GetProductById(con); break;
                    case 6: return;
                    default: Console.WriteLine("Invalid option!"); break;
                }
            }
        }

        static DataSet LoadData(SqlDataAdapter da)
        {
            DataSet ds = new DataSet();
            da.Fill(ds, "Products");
            return ds;
        }

        static void InsertProduct(SqlConnection con)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = new SqlCommand("sp_GetAllProducts", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.InsertCommand = new SqlCommand("sp_InsertProduct", con);
                da.InsertCommand.CommandType = CommandType.StoredProcedure;

                da.InsertCommand.Parameters.Add("@Productid", SqlDbType.Int, 0, "Productid");
                da.InsertCommand.Parameters.Add("@Productname", SqlDbType.VarChar, 50, "Productname");
                da.InsertCommand.Parameters.Add("@Category", SqlDbType.VarChar, 50, "Category");
                da.InsertCommand.Parameters.Add("@Price", SqlDbType.Decimal, 0, "Price");

                DataSet ds = LoadData(da);
                DataTable dt = ds.Tables["Products"]!;

                DataRow row = dt.NewRow();

                Console.Write("Enter Product ID: ");
                row["Productid"] = int.Parse(Console.ReadLine()!);

                Console.Write("Enter Name: ");
                row["Productname"] = Console.ReadLine()!;

                Console.Write("Enter Category: ");
                row["Category"] = Console.ReadLine()!;

                Console.Write("Enter Price: ");
                row["Price"] = decimal.Parse(Console.ReadLine()!);

                dt.Rows.Add(row);

                da.Update(ds, "Products");

                Console.WriteLine("Product inserted successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static void ViewProducts(SqlConnection con)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = new SqlCommand("sp_GetAllProducts", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                DataSet ds = LoadData(da);
                DataTable dt = ds.Tables["Products"]!;

                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine($"ID: {row["Productid"]}, Name: {row["Productname"]}, Category: {row["Category"]}, Price: {row["Price"]}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static void UpdateProduct(SqlConnection con)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = new SqlCommand("sp_GetAllProducts", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.UpdateCommand = new SqlCommand("sp_UpdateProduct", con);
                da.UpdateCommand.CommandType = CommandType.StoredProcedure;

                da.UpdateCommand.Parameters.Add("@Productid", SqlDbType.Int, 0, "Productid");
                da.UpdateCommand.Parameters.Add("@Productname", SqlDbType.VarChar, 50, "Productname");
                da.UpdateCommand.Parameters.Add("@Category", SqlDbType.VarChar, 50, "Category");
                da.UpdateCommand.Parameters.Add("@Price", SqlDbType.Decimal, 0, "Price");

                DataSet ds = LoadData(da);
                DataTable dt = ds.Tables["Products"]!;

                Console.Write("Enter Product ID: ");
                int id = int.Parse(Console.ReadLine()!);

                bool found = false;

                foreach (DataRow row in dt.Rows)
                {
                    if ((int)row["Productid"] == id)
                    {
                        found = true;

                        Console.Write("New Name: ");
                        row["Productname"] = Console.ReadLine()!;

                        Console.Write("New Category: ");
                        row["Category"] = Console.ReadLine()!;

                        Console.Write("New Price: ");
                        row["Price"] = decimal.Parse(Console.ReadLine()!);
                    }
                }

                da.Update(ds, "Products");

                Console.WriteLine(found ? "Product updated successfully!" : "Product not found!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static void DeleteProduct(SqlConnection con)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = new SqlCommand("sp_GetAllProducts", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.DeleteCommand = new SqlCommand("sp_DeleteProduct", con);
                da.DeleteCommand.CommandType = CommandType.StoredProcedure;

                da.DeleteCommand.Parameters.Add("@Productid", SqlDbType.Int, 0, "Productid");

                DataSet ds = LoadData(da);
                DataTable dt = ds.Tables["Products"]!;

                Console.Write("Enter Product ID: ");
                int id = int.Parse(Console.ReadLine()!);

                bool found = false;

                foreach (DataRow row in dt.Rows)
                {
                    if ((int)row["Productid"] == id)
                    {
                        found = true;
                        row.Delete();
                    }
                }

                da.Update(ds, "Products");

                Console.WriteLine(found ? "Product deleted successfully!" : "Product not found!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        static void GetProductById(SqlConnection con)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = new SqlCommand("sp_getProductsbyid", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                Console.Write("Enter Product ID: ");
                int id = int.Parse(Console.ReadLine()!);

                da.SelectCommand.Parameters.AddWithValue("@Productid", id);

                DataSet ds = new DataSet();
                da.Fill(ds, "Products");

                DataTable dt = ds.Tables["Products"]!;

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    Console.WriteLine($"ID: {row["Productid"]}, Name: {row["Productname"]}, Category: {row["Category"]}, Price: {row["Price"]}");
                }
                else
                {
                    Console.WriteLine("Product not found!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}