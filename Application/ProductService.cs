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
            .Exists(prod => string.Equals(prod.Description.Trim().ToLower(), description.Trim().ToLower()));
    }

    public static bool ProductNumberExists(int productNumber)
    {
        return ProductFileManager.GetProducts().Exists(prod => prod.ProductNumber == productNumber);
    }

    public static bool AddProduct(Product prod)
    {
        if (ProductExists(prod.Description) || ProductNumberExists(prod.ProductNumber))
        {
            throw new Exception("Error: Produkt oder Produktnummer existiert schon.");
        }
        else
        {
            products.Add(prod);
            return true;
        }
    }

    public static bool RemoveProduct(int productNumber)
    {
        List<Product> products = ProductFileManager.GetProducts();
        Product? product = products.Find(prod => prod.ProductNumber == productNumber);

        if (product == null)
        {
            throw new Exception("Error: Produkt existiert nicht.");
        }

        products.Remove(product);
        ProductFileManager.SaveProducts(products, false);
        return true;
    }

    public static bool AddStock(string desc, int amount, bool add)
    {
        List<Product> products = ProductFileManager.GetProducts();
        Product? product = products.Find(prod => string.Equals(prod.Description.Trim().ToLower(), desc.Trim().ToLower()));

        if (product == null)
        {
            throw new Exception("Error: Produkt existiert noch nicht.");
        }

        if (add)
        {
            product.Stock += amount;
        }
        else
        {
            product.Stock -= amount;
        }
        ProductFileManager.SaveProducts(products, false);
        return true;
    }
}
