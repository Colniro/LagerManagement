using _7_Lagerverwaltung.Application;
using _7_Lagerverwaltung.BuisnessLogic;
using _7_Lagerverwaltung.DataAccess;
using System.Collections;

while (true)
{
    Console.Clear();
    WriteTitle();
    WriteSelection();
    if (int.TryParse(Console.ReadLine(), out int input))
    {
        switch (input)
        {
            case 1:
                AddNewProduct();
                break;
            case 2:
                ShowAllProducts();
                break;
            case 3:
                AddStockToProduct();
                break;
            case 5:
                RemoveProductByNumber();
                break;

        }
    }
    else
    {
        Console.WriteLine("Error: Eingabe ist ungültig.");
    }
}

void WriteTitle()
{
    Console.WriteLine("================");
    Console.WriteLine("Lager-Management");
    Console.WriteLine("================\n\n");
}

void WriteSelection()
{
    Console.WriteLine("Was möchten Sie machen?");
    Console.WriteLine("-------------------------\n");
    Console.WriteLine("- Neues Produkt hinzufügen                                (1)" );
    Console.WriteLine("- Alle Produkte anzeigen                                  (2)");
    Console.WriteLine("- Bestand eines Produktes erhöhen oder verringern         (3)");
    Console.WriteLine("- Produkte mit unterschrittenem Mindestbestand anzeigen   (4)");
    Console.WriteLine("- Produkt entfernen                                       (5)\n");
    Console.Write("Auswahl: ");
}

void AddNewProduct()
{
    Console.Clear();
    WriteTitle();

    int productNumber = 0;
    string description = "asdf";
    int stock = 0;
    decimal price = 0;
    bool append = true;

    string? inputProductNumber;
    int parsedProductNumber;
    do
    {
        Console.Write("Produktnummer eingeben: ");
        inputProductNumber = Console.ReadLine();
        if (!int.TryParse(inputProductNumber, out parsedProductNumber) || parsedProductNumber < 0)
        {
            Console.WriteLine("Error: Ungültige Eingabe.");
        }
    }
    while (!int.TryParse(inputProductNumber, out parsedProductNumber) || parsedProductNumber < 0);
    productNumber = parsedProductNumber;


    string? inputDescription;
    Console.Write("Namen eingeben: ");
    inputDescription = Console.ReadLine();
    while (inputDescription == null || inputDescription.Contains(';'))
    {
        Console.Write("Error: Ungültige eingabe.");
        Console.Write("\nNamen eingeben: ");
        inputDescription = Console.ReadLine();
    }
    description = inputDescription;

    string? inputStock;
    int parsedStock;
    do
    {
        Console.Write("Bestand eingeben: ");
        inputStock = Console.ReadLine();
        if (!int.TryParse(inputStock, out parsedStock) || parsedStock < 0)
        {
            Console.WriteLine("Error: Ungültige Eingabe.");
        }
    }
    while (!int.TryParse(inputStock, out parsedStock) || parsedStock < 0);
    stock = parsedStock;

    string? inputPrice;
    decimal parsedPrice;
    do
    {
        Console.Write("Preis eingeben: ");
        inputPrice = Console.ReadLine();
        if (!decimal.TryParse(inputPrice, out parsedPrice) || parsedPrice < 0)
        {
            Console.WriteLine("Error: Ungültige Eingabe.");
        }
    }
    while (!decimal.TryParse(inputPrice, out parsedPrice) || parsedPrice < 0);
    price = parsedPrice;

    Console.Write("\nAn die Liste anhängen? (sonst wird alles überschrieben) (y/n)");
    string apnd = Console.ReadLine();
    if (apnd.ToLower() == "y")
    {
        append = true;
    }
    else
    {
        append = false;
    }

    try
    {
        Product currentProduct = new Product(productNumber, description, stock, price);
        ProductService.AddProduct(currentProduct);
        ProductFileManager.SaveProducts(ProductService.products, append);
        Console.WriteLine("Erfolgreich. Enter zum fortfahren...");
        Console.ReadLine();
    }
    catch(Exception ex)
    {
        Console.WriteLine(ex.Message);
        Console.WriteLine("Enter zum fortfahren...");
        Console.ReadLine();
    }
}

void ShowAllProducts()
{
    Console.Clear();
    WriteTitle();
    foreach (var item in ProductFileManager.GetProducts())
    {
        Console.Write($"Nummer: {item.ProductNumber}");
        for (int i = 0; i < 9 - item.ProductNumber.ToString().Length; i++)
        {
            Console.Write(" ");
        }
        Console.Write("| ");

        Console.Write($"Name: {item.Description}");
        for (int i = 0; i < 20 - item.Description.ToString().Length; i++)
        {
            Console.Write(" ");
        }
        Console.Write("| ");

        Console.Write($"Anzahl: {item.Stock}");
        for (int i = 0; i < 9 - item.Stock.ToString().Length; i++)
        {
            Console.Write(" ");
        }
        Console.Write("| ");

        Console.Write($"Preis: {item.Price}");
        for (int i = 0; i < 6 - item.Price.ToString().Length; i++)
        {
            Console.Write(" ");
        }
        Console.WriteLine("\n");
    }
    Console.WriteLine("\nEnter um zu schliessen...");
    Console.ReadLine();
}


void AddStockToProduct()
{
    Console.Clear();
    WriteTitle();
    Console.Write("Gib den Namen vom Produkt ein: ");
    string? descInput = Console.ReadLine();
    while (string.IsNullOrWhiteSpace(descInput))
    {
        Console.Write("Error: Ungültige Eingabe.");
        Console.Write("\nGib den Namen vom Produkt ein: ");
        descInput = Console.ReadLine();
    }
    descInput = descInput.Trim();

    string? increaseInput;
    do
    {
        Console.Write("Bestand erhöhen? (sonst verringern) (y/n): ");
        increaseInput = Console.ReadLine();
        if (increaseInput == null || (increaseInput.ToLower() != "y" && increaseInput.ToLower() != "n"))
        {
            Console.WriteLine("Error: Ungültige Eingabe.");
        }
    }
    while (increaseInput == null || (increaseInput.ToLower() != "y" && increaseInput.ToLower() != "n"));

    bool increase = increaseInput.ToLower() == "y";

    int amount = 0;
    string? inputAmount;
    int parsedAmount;
    do
    {
        Console.Write("Um wieviel: ");
        inputAmount = Console.ReadLine();
        if (!int.TryParse(inputAmount, out parsedAmount) || parsedAmount < 0)
        {
            Console.WriteLine("Error: Ungültige Eingabe.");
        }
    }
    while (!int.TryParse(inputAmount, out parsedAmount) || parsedAmount < 0);
    amount = parsedAmount;
    try
    {
        ProductService.AddStock(descInput, amount, increase);
        Console.WriteLine("Erfolgreich. Enter zum fortfahren...");
        Console.ReadLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        Console.WriteLine("Enter zum fortfahren...");
        Console.ReadLine();
    }
}

void RemoveProductByNumber()
{
    Console.Clear();
    WriteTitle();

    string? inputProductNumber;
    int parsedProductNumber;
    do
    {
        Console.Write("Produktnummer eingeben: ");
        inputProductNumber = Console.ReadLine();
        if (!int.TryParse(inputProductNumber, out parsedProductNumber) || parsedProductNumber < 0)
        {
            Console.WriteLine("Error: Ungültige Eingabe.");
        }
    }
    while (!int.TryParse(inputProductNumber, out parsedProductNumber) || parsedProductNumber < 0);

    try
    {
        ProductService.RemoveProduct(parsedProductNumber);
        Console.WriteLine("Erfolgreich");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    Console.WriteLine("Enter zum fortfahren...");
    Console.ReadLine();
}