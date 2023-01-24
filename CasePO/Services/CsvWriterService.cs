
using System.Globalization;
using CsvHelper;

namespace CasePO.Services
{
    public class CsvWriterService
    {
        public void WriteToCsv(string filePath, List<Kunde> kunder)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
            {
                csv.WriteRecords(kunder);
            }
        }
    }
}
