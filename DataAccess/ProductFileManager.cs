using _7_Lagerverwaltung.BuisnessLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace _7_Lagerverwaltung.DataAccess;

internal class ProductFileManager
{
    public static void SaveProducts(Dictionary<Product, int> items, bool append)
    {
        using (StreamWriter sw = new StreamWriter("products.csv", append))
        {
            foreach(var key in items.Keys)
            {
                sw.WriteLine($"{key.ProductNumber}; {key.Description}; {key.Stock}; {key.Price}; {items[key]}");
            }
        }
    }

    public static Dictionary<Product, int> GetProducts()
    {
        Dictionary<Product, int> output = new Dictionary<Product, int>();

        using (StreamReader sr = new StreamReader("products.csv"))
        {
            string? line = null;
            while ((line = sr.ReadLine()) != null)
            {
                string[] current = line.Split(";");
                Product currentProduct = new Product(int.Parse(current[0]), current[1], int.Parse(current[3]), decimal.Parse(current[4]));
                output.Add(currentProduct, int.Parse(current[^1]));
            }
        }

        return output;
    }

    public static Dictionary<Product, int> GetProductsBelowMinStock()
    {
        Dictionary<Product, int> output = new Dictionary<Product, int>();

        using (StreamReader sr = new StreamReader("products.csv"))
        {
            string? line = null;
            while ((line = sr.ReadLine()) != null)
            {
                string[] current = line.Split(";");
                if (int.Parse(current[^1]) < Product._MinStock)
                {
                    Product currentProduct = new Product(int.Parse(current[0]), current[1], int.Parse(current[3]), decimal.Parse(current[4]));
                    output.Add(currentProduct, int.Parse(current[^1]));
                }
            }
        }

        return output;
    }
}
