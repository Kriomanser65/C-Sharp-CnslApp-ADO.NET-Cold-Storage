using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.Remoting.Contexts;

namespace Cold_Storage
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection conn = new SqlConnection("Data Source = DESKTOP - 4SK39GD; Initial Catalog = ITacademy; Integrated Security = True; Connect Timeout = 30; Encrypt = False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            CreateTable(conn, "create table Products (Id int primary key identity, Name nvarchar(50), Price money)");
            CreateTable(conn, "create table Sellers (Id int primary key identity, Name nvarchar(50))");
            CreateTable(conn, "create table Customers (Id int primary key identity, Name nvarchar(50))");
            AddProduct(conn, "whirlpool W7X81OOX0", 554); /*https://www.foxtrot.com.ua/uk/shop/holodilniki_whirlpool_w7x_81o_ox_0.html*/
            AddProduct(conn, "bosch KGN36VL326", 682); /*https://www.foxtrot.com.ua/uk/shop/holodilniki_bosch_kgn36vl326.html*/
            AddSeller(conn, "Oleksandr Kuzmenko");
            AddSeller(conn, "Olga Volodko");
            AddCustomer(conn, "Maxim Rudenko");
            AddCustomer(conn, "Vlad Bezpalko");
            Purchase(conn, "Maxim Rudenko", "Oleksandr Kuzmenko", "whirlpool W7X81OOX0");
            SaveReceipt(new Receipt
            {
                Date = DateTime.Now,
                CustomerName = "Maxim Rudenko",
                SellerName = "Oleksandr Kuzmenko",
                Products = new[] { "whirlpool W7X81OOX0" }
            }, "receipts.xml");
        }

        static void CreateTable(SqlConnection conn, string query)
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        static void AddProduct(SqlConnection conn, string name, decimal price)
        {
            SqlCommand cmd = new SqlCommand("insert into Products (Name, Price) values (@name, @price)", conn);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@price", price);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        static void AddSeller(SqlConnection conn, string name)
        {
        }

        static void AddCustomer(SqlConnection conn, string name)
        {
        }

        static void Purchase(SqlConnection conn, string customerName, string sellerName, string productName)
        {
        }

        static void SaveReceipt(Receipt receipt, string fileName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Save(fileName);
        }
    }

    class Receipt
    {
        public DateTime Date { get; set; }
        public string CustomerName { get; set; }
        public string SellerName { get; set; }
        public string[] Products { get; set; }
    }
}