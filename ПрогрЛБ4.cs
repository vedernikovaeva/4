//Варіант 2.
//Сформувати файл “Export.json”, що містить інформацію про дані з полями: код; найменування
//товару; країна, що експортує товар; об’єм товару в одиницях; ціна.
// Переглянути файл на консолі;
// За заданим кодом Х видати найменування товару та об’єм його поставок.
// Обчислити загальну вартість товару, який експортується в країну Y.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Product
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public int Volume { get; set; }
    public decimal Price { get; set; }
}

public class ExportData
{
    public List<Product> ProductList { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        var stringJsonData = @"
        {
            'ProductList': [
                {
                    'code': '01',
                    'name': 'Pr1',
                    'country': 'Ukraine',
                    'volume': 100,
                    'price': 500
                },
                {
                    'code': '02',
                    'name': 'Pr2',
                    'country': 'Austria',
                    'volume': 200,
                    'price': 800
                }
            ]
        }";

        var exportData = JsonConvert.DeserializeObject<ExportData>(stringJsonData);

        Console.WriteLine("Список товарів: ");
        foreach(var product in exportData.ProductList)
        {
            Console.WriteLine($"Код: {product.Code}, найменування: {product.Name}, краЇна: {product.Country}, кількість: {product.Volume}, ціна: {product.Price}");
        }

        Console.WriteLine("Введіть код товару: ");
        string searchCode = Console.ReadLine();
        var foundProduct = exportData.ProductList.FirstOrDefault(p => p.Code == searchCode);

        if(foundProduct != null)
        {
            Console.WriteLine($"Товар знайдено: {foundProduct.Name}, обсяг: {foundProduct.Volume}");
        }
        else
        {
            Console.WriteLine("Товар не знайдено");
        }

        Console.WriteLine("Введіть назву країни: ");
        string searchCountry = Console.ReadLine();
        var totalPrice = exportData.ProductList
            .Where(p => p.Country.Equals(searchCountry, StringComparison.OrdinalIgnoreCase))
            .Sum(p => p.Volume * p.Price);

        Console.WriteLine($"Загальна вартість товарів в {searchCountry}: {totalPrice}");

        var jsonOutput = JsonConvert.SerializeObject(exportData, Formatting.Indented);
        File.WriteAllText("Export.json", jsonOutput);
        Console.WriteLine("Дані збережені у файл Export.json");
    }
}