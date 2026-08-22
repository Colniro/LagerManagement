using _7_Lagerverwaltung.BuisnessLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace _7_Lagerverwaltung.DataAccess;

internal class ProductFileManager
{
    public static void SaveProducts(List<Product> items, bool append)
    {
        using (StreamWriter sw = new StreamWriter("products.csv", append))
        {
            foreach (Product product in items)
            {
                sw.WriteLine($"{product.ProductNumber}; {product.Description}; {product.Stock}; {product.Price}");
            }
        }
    }

    public static List<Product> GetProducts()
    {
        List<Product> output = new List<Product>();

        if (!File.Exists("products.csv"))
        {
            return output;
        }

        using (StreamReader sr = new StreamReader("products.csv"))
        {
            string? line = null;
            while ((line = sr.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] current = line.Split(";");
                Product currentProduct = new Product(int.Parse(current[0]), current[1].Trim(), int.Parse(current[2]), decimal.Parse(current[3]));
                output.Add(currentProduct);
            }
        }

        return output;
    }

    public static List<Product> GetProductsBelowMinStock()
    {
        return GetProducts().FindAll(p => p.Stock < Product._MinStock);
    }
}
