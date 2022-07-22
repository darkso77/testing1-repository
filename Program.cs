using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Program_1
{
    class Program
    {
        static void Main(string[] args)
        {
            string csvRcp1 = @"C:\Test1\rcp1a.csv";
            DzienPracyRcp dzienPracyRcp = new DzienPracyRcp();
            var dniPracyRcp = dzienPracyRcp.Pobierz(csvRcp1);
            //dzienPracyRcp.Pokaz(dniPracyRcp);

            var objektDniPracy = dniPracyRcp.Select(c => new DzienPracy()
            {
                KodPracownika = c.Kod_pracownika,
                Data = c.data,
                GodzinaWejscia = c.godzina_wejscia,
                GodzinaWyjscia = c.godzina_wyjscia
            }
            );

            DzienPracy dzienPracy = new DzienPracy();
            //dzienPracy.Pokaz(objektDniPracy);

            Console.WriteLine("Ap-a1 Dzień Pracy");
            Console.WriteLine("1 - Wyświetl wszystkie numery kodów pracowników");
            Console.WriteLine("2 - Wyświetl numery kodów pracowników wg wybranej frazy");
            Console.WriteLine("3 - Wyświetl podstawowe informacje na podstawie wybranego nr kodu pracownika");
            Console.WriteLine("4 - Wyświetl zakres dni pracy podając datę rozpoczęcia i datę zakończenia szukania");
            Console.WriteLine("x - Wyjście z programu");

            var daneOdUzytkownika = Console.ReadLine();

            while (true)
            {
                switch (daneOdUzytkownika)
                {
                    case "1":
                        dzienPracy.SortowanieDanych(objektDniPracy);
                        break;
                    case "2":
                        dzienPracy.SortowanieWgFrazy(objektDniPracy);
                        break;
                    case "3":
                        dzienPracy.WeryfikacjaNrKodu(objektDniPracy);
                        break;
                    case "4":
                        dzienPracy.WeryfikacjaZakresDaty(objektDniPracy);
                        break;

                    case "x":
                        return;
                    default:
                        {
                            Console.WriteLine("Niepoprawna operacja");
                            break;
                        }
                }
                Console.WriteLine("Wybierz operację");
                daneOdUzytkownika = Console.ReadLine();
            }
        }
    }
}
