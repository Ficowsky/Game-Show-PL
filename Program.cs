using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        string sciezka = @"ścieżka\do\pliku\pytania.csv"; // Zmień ścieżkę na odpowiednią dla Twojego środowiska

        List<Pytanie> pytania = WczytajPytania(sciezka);
        Random rand = new Random();
        int liczbaGraczy;
        int punkty = 0;

        // Pobieranie liczby graczy
        do
        {
            Console.Write("Podaj liczbę graczy: ");
        }
        while (!int.TryParse(Console.ReadLine(), out liczbaGraczy) || liczbaGraczy <= 0);

        // Pętla dla każdego gracza
        for (int i = 0; i < liczbaGraczy; i++)
        {
            Console.Write("Gracz " + (i + 1) + ", podaj swoje imię: ");
            string imie = Console.ReadLine();

            // Losowanie pytania
            int numerPytania = rand.Next(pytania.Count);
            Pytanie wylosowanePytanie = pytania[numerPytania];
            Console.WriteLine("Pytanie " + (i + 1) + ": " + wylosowanePytanie.Tresc);

            // Wybór odpowiedzi
            Console.WriteLine("1. " + wylosowanePytanie.Odpowiedz1);
            Console.WriteLine("2. " + wylosowanePytanie.Odpowiedz2);
            Console.WriteLine("3. " + wylosowanePytanie.Odpowiedz3);
            Console.Write("Podaj numer odpowiedzi: ");
            int odpowiedz;
            if (int.TryParse(Console.ReadLine(), out odpowiedz) && odpowiedz >= 1 && odpowiedz <= 3)
            {
                // Sprawdzenie poprawności odpowiedzi
                if (odpowiedz == wylosowanePytanie.PrawidlowaOdpowiedz)
                {
                    Console.WriteLine("Poprawna odpowiedź!");
                    punkty++;
                }
                else
                {
                    Console.WriteLine("Błędna odpowiedź.");
                }
            }
            else
            {
                Console.WriteLine("Nieprawidłowy numer odpowiedzi.");
            }
        }

        // Wyświetlanie wyniku
        Console.WriteLine("Koniec gry!");
        Console.WriteLine("Liczba zdobytych punktów: " + punkty);
    }

    static List<Pytanie> WczytajPytania(string sciezka)
    {
        List<Pytanie> pytania = new List<Pytanie>();

        using (StreamReader reader = new StreamReader(sciezka))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                pytania.Add(new Pytanie
                {
                    Tresc = parts[0],
                    Odpowiedz1 = parts[1],
                    Odpowiedz2 = parts[2],
                    Odpowiedz3 = parts[3],
                    PrawidlowaOdpowiedz = int.Parse(parts[4])
                });
            }
        }

        return pytania;
    }
}

class Pytanie
{
    public string Tresc { get; set; }
    public string Odpowiedz1 { get; set; }
    public string Odpowiedz2 { get; set; }
    public string Odpowiedz3 { get; set; }
    public int PrawidlowaOdpowiedz { get; set; }
}

