using CsvHelper;
using System.Globalization;

namespace CasePO.Services
{
    public class CsvReaderService
    {
        public List<Kunde> ReadFromCsv(string filePath)
        {
            var kunder = new List<Kunde>();
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found");
                return kunder;
            }

            try
            {
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
                {
                    csv.Read();
                    csv.ReadHeader();
                    while (csv.Read())
                    {
                        var orgNo = csv.GetField("OrgNo");
                        var name = csv.GetField("Name");
                        kunder.Add(new Kunde { OrganisasjonsNummer = orgNo, BrregNavn = name });
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            return kunder;
        }
    }
}
