using _7_Lagerverwaltung.BuisnessLogic;
using _7_Lagerverwaltung.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace _7_Lagerverwaltung.Application;

internal class ProductService
{
    public static Dictionary<Product, int> products = new Dictionary<Product, int>();

    public static bool AddProduct(Product prod, int amount)
    {
        if (ProductFileManager.GetProducts().ContainsKey(prod))
        {
            throw new Exception("Error: Produkt existiert schon.");
        }
        else
        {
            products.Add(prod, amount);
            return true;
        }
    }

    public static bool AddStock(Product prod, int amount)
    {
        if (ProductFileManager.GetProducts().ContainsKey(prod))
        {
            int previousAmount = ProductFileManager.GetProducts()[prod];
            products.Remove(prod);
            products.Add(prod, previousAmount + amount);
            return true;
        }
        else
        {
            throw new Exception("Error: Produkt existiert schon.");
        }
    }
}
