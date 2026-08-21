using System;
using System.Collections.Generic;
using System.Text;

namespace _7_Lagerverwaltung.BuisnessLogic;

internal class Product
{
    public static int _MinStock = 5;
    private int _stock;
    public int ProductNumber { get; private set; }
    public string Description { get; set; }
    public int Stock
    {
        get => _stock;
        set
        {
            if (value < 0)
            {
                throw new Exception("Error: Anzahl unterschreitet 0.");
            }
            _stock = value;
        }
    }
    public decimal Price { get; set; }
    public Product(int productNumber, string description, int stock, decimal price)
    {
        ProductNumber = productNumber;
        Description = description;
        Stock = stock;
        Price = price;
    }
}
