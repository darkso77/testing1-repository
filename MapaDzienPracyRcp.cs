using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;

namespace Program_1
{
    public class MapaDzienPracyRcp : ClassMap<DzienPracyRcp>
    {
        public MapaDzienPracyRcp()
        {
            Map(m => m.Kod_pracownika).Name(nameof(DzienPracyRcp.Kod_pracownika));
            Map(m => m.data).Name(nameof(DzienPracyRcp.data));
            Map(m => m.godzina_wejscia).Name(nameof(DzienPracyRcp.godzina_wejscia));
            Map(m => m.godzina_wyjscia).Name(nameof(DzienPracyRcp.godzina_wyjscia));
        }
    }
}
