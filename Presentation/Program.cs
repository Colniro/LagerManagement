using _7_Lagerverwaltung.BuisnessLogic;

while (true)
{
    WriteTitle();
    WriteSelection();
    if (int.TryParse(Console.ReadLine(), out int input))
    {
        switch (input)
        {
            case 1:
                AddNewProduct();
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
    Console.WriteLine("- Produkte mit unterschrittenem Mindestbestand anzeigen   (4)\n");
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
    Console.Write("\nBeschreibung eingeben: ");
    inputDescription = Console.ReadLine();
    while (inputDescription == null || inputDescription.Contains(';'))
    {
        Console.Write("Error: Ungültige eingabe.");
        Console.Write("\nBeschreibung eingeben: ");
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

    Product currentProduct = new Product(productNumber, description, stock, price);
}