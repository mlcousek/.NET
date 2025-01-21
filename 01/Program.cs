using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text.Json;  
using System.Net.Mail;
using System.Text.Json.Serialization;

class Flag
{
    public string Name { get; set; }       // Jméno země
    [JsonIgnore]
    public int Language { get; set; }      // Jazyk
    [JsonIgnore]
    public int HaveRed {  get; set; }       // Má na sobě červenou
    [JsonIgnore]
    public int HaveWhite {  get; set; }     // Má na sobě bílou
    [JsonIgnore]
    public int Bars { get; set; }          // Počet pruhů na vlajce
    [JsonIgnore]
    public int Stripes { get; set; }       // Počet pruhů na vlajce
    public int Colors { get; set; }         //Pocet barev
    public int Population { get; set; }     //Populace v milionech
    public int StripesAndBars
    {
        get
        {
            return Stripes + Bars;
        }
    }
    
    public override string ToString()
    {
        return $"Name: {Name}, Stripes + Bars: {StripesAndBars}, Colors: {Colors}, Population: {Population}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        string url = "https://archive.ics.uci.edu/static/public/40/flags.zip";
        string zipPath = "flags.zip";
        string extractPath = "flags_dataset";

        // Stažení ZIP souboru
        using (WebClient client = new WebClient())
        {
            client.DownloadFile(url, zipPath);
        }

        // Rozbalení ZIP souboru
        ZipFile.ExtractToDirectory(zipPath, extractPath);

        // Načtení CSV souboru
        string dataFile = Path.Combine(extractPath, "flag.data");

        List<Flag> flags = new List<Flag>();

        using (StreamReader reader = new StreamReader(dataFile))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] values = line.Split(',');

                Flag flag = new Flag
                {
                    Name = values[0],
                    Language = int.Parse(values[5]),
                    HaveRed = int.Parse(values[10]),
                    HaveWhite = int.Parse(values[14]),
                    Bars = int.Parse(values[7]),
                    Stripes = int.Parse(values[8]),
                    Colors = int.Parse(values[9]),
                    Population = int.Parse(values[4]),
                };

                flags.Add(flag);
            }
        }

        // Filtrování co mají horizontální pruhy a nemají červenou
        List<Flag> hasStripesWithoutWhite = flags.FindAll(flag => flag.Stripes > 0 && flag.HaveWhite == 0);
        
        // Filtrování Španělsky mluvící země bez červené barvy
        List<Flag> spanishFlagsWithoutRed = flags.FindAll(flag => flag.Language == 2 && flag.HaveRed == 0);

        //Console.WriteLine("Španělsky mluvící země bez červené barvy na vlajce:");
        //foreach (var flag in spanishFlagsWithoutRed)
        //{
        //    Console.WriteLine(flag.Name);
        //}
        //Console.WriteLine();
        //Console.WriteLine("Setřízeno podle počtu všech pruhů:");

        List<Flag> spanishFlagsWithoutRedOrdered = spanishFlagsWithoutRed.OrderByDescending(flag => flag.StripesAndBars).ToList(); //vím, že nemusím vytvářet nový list, ale přišlo mi to vhodné, protože bych třeba chtěl pracovat zvlášť s nesetřízeným, ale klidně bych mohl jen setřídit ten už vytvořený
        //foreach (var flag in spanishFlagsWithoutRedOrdered)
        //{
        //    Console.WriteLine($"{flag.Name} - Pruhy: {flag.StripesAndBars}");
        //}

        // Serializace do JSON
        var json1 = JsonSerializer.Serialize(spanishFlagsWithoutRedOrdered, new JsonSerializerOptions { WriteIndented = true });
        //Console.WriteLine();
        //Console.WriteLine("Vygenerovaný JSON:");
        //Console.WriteLine(json1);

        //Console.WriteLine("_________________________________________________");

        //Console.WriteLine("Mající horizontální pruhy bez bílé barvy na vlajce:");
        //foreach (var flag in hasStripesWithoutWhite)
        //{
        //    Console.WriteLine(flag.Name);
        //}
        //Console.WriteLine();
        //Console.WriteLine("Setřízeno podle počtu všech pruhů:");
        List<Flag> hasStripesWithoutWhiteOrdered = hasStripesWithoutWhite.OrderByDescending(flag => flag.StripesAndBars).ToList();
        //foreach (var flag in hasStripesWithoutWhiteOrdered)
        //{
        //    Console.WriteLine($"{flag.Name} - Pruhy: {flag.StripesAndBars}");
        //}

        // Serializace do JSON
        var json2 = JsonSerializer.Serialize(hasStripesWithoutWhiteOrdered, new JsonSerializerOptions { WriteIndented = true });
        //Console.WriteLine();
        //Console.WriteLine("Vygenerovaný JSON:");
        //Console.WriteLine(json2);


        //Console.WriteLine("_________________________________________________");
        //// Odeslání e-mailu
        SendEmail(json1, json2);

    }
    static void SendEmail(string jsonData1, string jsonData2)
    {
        // Nastavení SMTP klienta 
        SmtpClient client = new SmtpClient("smtp.seznam.cz", 587)
        {
            Credentials = new NetworkCredential("jirka.mlcousek.net@seznam.cz", "Jirka123456"), // Zadej své přihlašovací údaje
            EnableSsl = true
        };

        // Vytvoření e-mailové zprávy
        MailMessage mailMessage = new MailMessage
        {
            From = new MailAddress("jirka.mlcousek.net@seznam.cz"), // Odesílatel
            Subject = "PNE -- výsledky -- Mlčoušek",      // Předmět
            Body = "Dobrý den, v příloze Vám posílám výsledky v JSON formátu. flags1 jsou: Španělsky mluvící země bez červené barvy na vlajce seřazeny sestupně podle počtu pruhů a flags2 jsou: Vlajky mající horizontální pruhy bez bílé barvy na vlajce seřazeny sestupně podle počtu pruhů.",     // Text zprávy
            IsBodyHtml = false                              
        };

        // Adresa příjemce
        mailMessage.To.Add("radek.janostik@upol.cz");

        // Přidání JSON jako přílohy
        Attachment attachment1 = new Attachment(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(jsonData1)), "flags1.json");
        mailMessage.Attachments.Add(attachment1);

        Attachment attachment2 = new Attachment(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(jsonData2)), "flags2.json");
        mailMessage.Attachments.Add(attachment2);

        // Odeslání e-mailu
        try
        {
            client.Send(mailMessage);
            Console.WriteLine("E-mail byl úspěšně odeslán.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Chyba při odesílání e-mailu: {ex.Message}");
        }
    }
}
