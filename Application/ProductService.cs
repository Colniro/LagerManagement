using _7_Lagerverwaltung.BuisnessLogic;
using _7_Lagerverwaltung.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace _7_Lagerverwaltung.Application;

internal class ProductService
{
    public static List<Product> products = new List<Product>();

    public static bool ProductExists(string description)
    {
        return ProductFileManager.GetProducts()
            .Exists(prod => string.Equals(prod.Description.ToLower(), description.ToLower()));
    }

    public static bool AddProduct(Product prod)
    {
        if (ProductExists(prod.Description))
        {
            throw new Exception("Error: Produkt existiert schon.");
        }
        else
        {
            products.Add(prod);
            return true;
        }
    }

    public static bool AddStock(string desc, int amount)
    {
        List<Product> products = ProductFileManager.GetProducts();
        Product? product = products.Find(prod => string.Equals(prod.Description.ToLower() desc.ToLower()));

        if (product == null)
        {
            throw new Exception("Error: Produkt existiert noch nicht.");
        }

        product.Stock += amount;
        ProductFileManager.SaveProducts(products, false);
        return true;
    }
}
