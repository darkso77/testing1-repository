using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program_1
{
    public class DzienPracy
    {
        private static TimeSpan czasPracy1 = default;
        private static TimeSpan czasPracy2 = default;
        private static TimeSpan czasPracy3 = default;

        public string KodPracownika { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan GodzinaWejscia { get; set; }
        public TimeSpan GodzinaWyjscia { get; set; }

        public void SortowanieDanych(IEnumerable<DzienPracy> dniPracy)
        {
            var intDniPracy = dniPracy.Select(a => new IntDzienPracy()
            {
                KodPracownika = int.Parse(a.KodPracownika)
            }
            );
            var wynikSortowania = intDniPracy.OrderBy(a => a.KodPracownika);
            var kody = wynikSortowania.Select(a => a.KodPracownika).Distinct();

            Console.WriteLine($"Nr kodu: {string.Join(", ", kody)}");
            Console.WriteLine($"Liczba pracowników: {kody.Count()}");
        }

        public void SortowanieWgFrazy(IEnumerable<DzienPracy> dniPracy)
        {
            Console.Write("Podaj frazę numeru kodu pracownika: ");
            var fraza = Console.ReadLine();
            var frazaKod = dniPracy.Where(a => a.KodPracownika.Contains(fraza))
                .OrderBy(a => a.KodPracownika);
            var frazaKodSort = frazaKod.Select(a => a.KodPracownika).Distinct();
            Console.WriteLine($"Nr kodu: {string.Join(", ", frazaKodSort)}");
            Console.WriteLine($"Liczba znalezionych nr kodów: {frazaKodSort.Count()}");
        }

        public void WeryfikacjaNrKodu(IEnumerable<DzienPracy> dniPracy)
        {
            Console.Write("Podaj nr kodu pracownika: ");
            var kod = Console.ReadLine();
            var nrKodu = dniPracy.Where(a => a.KodPracownika == kod);
            //Pokaz(nrKodu);
            var pierwszyDzien = nrKodu.FirstOrDefault().Data;
            var ostatniDzien = nrKodu.LastOrDefault().Data;
            Console.WriteLine($"Ilość dni w pracy: {nrKodu.Count()}, od {pierwszyDzien.ToShortDateString()} do {ostatniDzien.ToShortDateString()}");

            var pierwszyDzienPracy = pierwszyDzien.DayOfWeek;
            if (pierwszyDzienPracy == DayOfWeek.Monday)
            {
                var dataWyjscia1 = pierwszyDzien.AddDays(6);
                TygodniePracy(pierwszyDzien, nrKodu, dataWyjscia1);
            }
            else if (pierwszyDzienPracy == DayOfWeek.Tuesday)
            {
                var dataWyjscia1 = pierwszyDzien.AddDays(5);
                TygodniePracy(pierwszyDzien, nrKodu, dataWyjscia1);
            }
            else if (pierwszyDzienPracy == DayOfWeek.Wednesday)
            {
                var dataWyjscia1 = pierwszyDzien.AddDays(4);
                TygodniePracy(pierwszyDzien, nrKodu, dataWyjscia1);
            }
            else if (pierwszyDzienPracy == DayOfWeek.Thursday)
            {
                var dataWyjscia1 = pierwszyDzien.AddDays(3);
                TygodniePracy(pierwszyDzien, nrKodu, dataWyjscia1);
            }
            else if (pierwszyDzienPracy == DayOfWeek.Friday)
            {
                var dataWyjscia1 = pierwszyDzien.AddDays(2);
                TygodniePracy(pierwszyDzien, nrKodu, dataWyjscia1);
            }
            else if (pierwszyDzienPracy == DayOfWeek.Saturday)
            {
                var dataWyjscia1 = pierwszyDzien.AddDays(1);
                TygodniePracy(pierwszyDzien, nrKodu, dataWyjscia1);
            }
        }

        public void ObliczCzasPracyTygodnia(IEnumerable<DzienPracy> dataWyjsciaTydzien)
        {
            var godzWy22 = dataWyjsciaTydzien.Where(a => a.GodzinaWyjscia > new TimeSpan(21, 0, 0));
            //Pokaz(godzWy22);
            CzasPracy1(godzWy22);
            //czasPracy1 = czasPracy;
            //Console.WriteLine($"czas Pracy1: {czasPracy1}");

            var godzWy14 = dataWyjsciaTydzien.Where(a => a.GodzinaWyjscia > new TimeSpan(13, 0, 0) && a.GodzinaWyjscia < new TimeSpan(21, 0, 0)).ToList();
            //Pokaz(godzWy14);
            CzasPracy1(godzWy14);
            //czasPracy2 = czasPracy;
            //Console.WriteLine($"czas Pracy2: {czasPracy2}");

            var godzWy2 = dataWyjsciaTydzien.Where(a => a.GodzinaWyjscia > new TimeSpan(1, 0, 0) && a.GodzinaWyjscia < new TimeSpan(4, 0, 0)).ToList();
            //Pokaz(godzWy2);
            CzasPracy2(godzWy2);
            //czasPracy3 = czasPracy;
            //Console.WriteLine($"czas Pracy3: {czasPracy3}");

            var godzWy6 = dataWyjsciaTydzien.Where(a => a.GodzinaWyjscia > new TimeSpan(4, 0, 0) && a.GodzinaWyjscia < new TimeSpan(12, 0, 0)).ToList();
            //Pokaz(godzWy6);
            CzasPracy3(godzWy6);
            //czasPracy4 = czasPracy;
            //Console.WriteLine($"czas Pracy4: {czasPracy4}");
        }

        List<TimeSpan> czasPracy = new List<TimeSpan>();


        public void CzasPracy1(IEnumerable<DzienPracy> gW)
        {
            var godzWy = gW.Select(a => a.GodzinaWyjscia).ToList();
            var godzWe = gW.Select(a => a.GodzinaWejscia).ToList();
            var data = gW.Select(a => a.Data).ToList();

            czasPracy1 = new TimeSpan(0, 0, 0);
            for (int i = 0; i < godzWy.Count; i++)
            {
                czasPracy1 += godzWy[i] - godzWe[i];
                Console.WriteLine(godzWy[i] - godzWe[i] + $" godz.    - dnia {data[i].ToShortDateString()}");
            }
            if (czasPracy1 != new TimeSpan(0, 0, 0))
            {
                Console.WriteLine($"Suma godzin: {czasPracy1.TotalHours}");
            }
            //Console.WriteLine(czasPracy1.TotalHours);
            czasPracy.Add(czasPracy1);
            //czasPracy1 = czasPracy;
            //return czasPracy1;
        }

        public void CzasPracy2(IEnumerable<DzienPracy> gW)
        {
            var godzWy = gW.Select(a => a.GodzinaWyjscia).ToList();
            var godzWe = gW.Select(a => a.GodzinaWejscia).ToList();
            var data = gW.Select(a => a.Data).ToList();

            czasPracy2 = new TimeSpan(0, 0, 0);
            for (int i = 0; i < godzWy.Count; i++)
            {
                czasPracy2 += godzWe[i] - godzWy[i];
                Console.WriteLine(godzWe[i] - godzWy[i] + $" godz.    - dnia {data[i].ToShortDateString()}");
            }
            if (czasPracy2 != new TimeSpan(0, 0, 0))
            {
                Console.WriteLine($"Suma godzin: {czasPracy2.TotalHours}");
            }
            //Console.WriteLine(czasPracy2.TotalHours);
            czasPracy.Add(czasPracy2);
            //czasPracy2 = czasPracy;
            //return czasPracy2;
        }

        public void CzasPracy3(IEnumerable<DzienPracy> gW)
        {
            var godzWy = gW.Select(a => a.GodzinaWyjscia).ToList();
            var godzWe = gW.Select(a => a.GodzinaWejscia).ToList();
            var data = gW.Select(a => a.Data).ToList();

            czasPracy3 = new TimeSpan(0, 0, 0);
            for (int i = 0; i < godzWy.Count; i++)
            {
                czasPracy3 += (godzWe[i] - godzWy[i]).Add(new TimeSpan(-8, 0, 0));
                Console.WriteLine((godzWe[i] - godzWy[i]).Add(new TimeSpan(-8, 0, 0)) + $" godz.    - dnia {data[i].ToShortDateString()}");
            }
            if (czasPracy3 != new TimeSpan(0, 0, 0))
            {
                Console.WriteLine($"Suma godzin: {czasPracy3.TotalHours}");
            }
            //Console.WriteLine(czasPracy3.TotalHours);
            czasPracy.Add(czasPracy3);
            //czasPracy3 = czasPracy;
            //return czasPracy3;
        }

        public void TygodniePracy(DateTime pierwszyDzien, IEnumerable<DzienPracy> nrKodu, DateTime dataWyjscia1)
        {
            dataWyjscia1 = pierwszyDzien.AddDays(6);
            var dataWyjscia1Tydzien = nrKodu.Where(a => a.Data < dataWyjscia1);
            ObliczCzasPracyTygodnia(dataWyjscia1Tydzien);

            var dataWyjscia2 = dataWyjscia1.AddDays(7);
            //Console.WriteLine(dataWyjscia2);
            var dataWyjscia2Tydzien = nrKodu.Where(a => a.Data > dataWyjscia1 && a.Data < dataWyjscia2);
            //Pokaz(dataWyjscia2Tydzien);
            ObliczCzasPracyTygodnia(dataWyjscia2Tydzien);

            var dataWyjscia3 = dataWyjscia2.AddDays(7);
            var dataWyjscia3Tydzien = nrKodu.Where(a => a.Data > dataWyjscia2 && a.Data < dataWyjscia3);
            //Pokaz(dataWyjscia3Tydzien);
            ObliczCzasPracyTygodnia(dataWyjscia3Tydzien);

            var dataWyjscia4 = dataWyjscia3.AddDays(7);
            var dataWyjscia4Tydzien = nrKodu.Where(a => a.Data > dataWyjscia3 && a.Data < dataWyjscia4);
            //Pokaz(dataWyjscia4Tydzien);
            ObliczCzasPracyTygodnia(dataWyjscia4Tydzien);

            var ostatniDzien = nrKodu.LastOrDefault().Data;
            if (ostatniDzien < dataWyjscia4)
            {
                var dataWyjscia5Tydzien = nrKodu.Where(a => a.Data > dataWyjscia4 && a.Data < ostatniDzien);
                ObliczCzasPracyTygodnia(dataWyjscia5Tydzien);
            }


            TimeSpan suma = new TimeSpan(0, 0, 0);
            for (int i = 0; i < czasPracy.Count; i++)
            {
                suma += czasPracy[i];
            }

            Console.WriteLine($"W {nrKodu.Count()} dniach przepracowano " +
                $"{suma.TotalHours} godzin");
        }

        public void WeryfikacjaZakresDaty(IEnumerable<DzienPracy> dniPracy)
        {
            Console.Write("Podaj nr kodu pracownika: ");
            var kod = Console.ReadLine();
            var nrKodu = dniPracy.Where(a => a.KodPracownika == kod);

            var minData = nrKodu.Min(d => d.Data).ToShortDateString();
            Console.Write($"Podaj dzień w formacie: \"rrrr.mm.dd\" od dnia {minData}, aby rozpocząć szukanie: ");
            string dzienString1 = Console.ReadLine();
            var dzienRozpoczecia = DateTime.Parse(dzienString1);
            var maxData = nrKodu.Max(d => d.Data).ToShortDateString();
            Console.Write($"Podaj dzień w formacie: \"rrrr.mm.dd\" do dnia {maxData}), aby zakączyć szukanie: ");
            string dzienString2 = Console.ReadLine();
            var dzienZakonczenia = DateTime.Parse(dzienString2);
            var zakresDni = nrKodu.Where(d => d.Data >= dzienRozpoczecia && d.Data <= dzienZakonczenia);
            Pokaz(zakresDni);
        }

        public void Pokaz(DzienPracy dzienPracy)
        {
            Console.WriteLine(dzienPracy);
        }

        public void Pokaz(IEnumerable<DzienPracy> dniPracy)
        {
            foreach (var dzienPracy in dniPracy)
            {
                Console.WriteLine(dzienPracy);
            }
        }

        public override string ToString()
        {
            return $"{KodPracownika,-7} | " +
                $"{Data.ToShortDateString(),-12} | " +
                $"{GodzinaWejscia,-10} | " +
                $"{GodzinaWyjscia,-10}";
        }
    }
}
