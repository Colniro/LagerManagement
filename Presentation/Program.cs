while (true)
{
    WriteTitle();
    WriteSelection();

}

void WriteTitle()
{
    Console.WriteLine("================");
    Console.WriteLine("Lager-Management");
    Console.WriteLine("================");
}

void WriteSelection()
{
    Console.WriteLine("Was möchten Sie machen?\n");
    Console.WriteLine("- Neues Produkt hinzufügen");
    Console.WriteLine("- Alle Produkte anzeigen");
    Console.WriteLine("- Bestand eines Produktes erhöhen oder verringern");
    Console.WriteLine("- Produkte mit unterschrittenem Mindestbestand anzeigen");
}