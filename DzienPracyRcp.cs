using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;

namespace Program_1
{
    public class DzienPracyRcp
    {
        public string Kod_pracownika { get; set; }
        public DateTime data { get; set; }
        public TimeSpan godzina_wejscia { get; set; }
        public TimeSpan godzina_wyjscia { get; set; }


        public void Pokaz(IEnumerable<DzienPracyRcp> dniPracyRcp)
        {
            foreach (var dzienPracyRcp in dniPracyRcp)
            {
                Console.WriteLine(dzienPracyRcp);
            }
        }

        public List<DzienPracyRcp> Pobierz(string csvRcp1)
        {
            using (var czytnik = new StreamReader(csvRcp1))
            using (var csv = new CsvReader(czytnik, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<MapaDzienPracyRcp>();
                var dokument = csv.GetRecords<DzienPracyRcp>().ToList();
                return dokument;
            }
        }

        public override string ToString()
        {
            return $"{Kod_pracownika,-7} | " +
                $"{data.ToShortDateString(),-12} | " +
                $"{godzina_wejscia,-10} | " +
                $"{godzina_wyjscia,-10} | ";
        }
    }
}
