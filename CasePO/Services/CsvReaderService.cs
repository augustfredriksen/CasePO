using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace CasePO.Services
{
    /// <summary>
    /// CsvReaderService class is used to read data from the provided CSV file and create a list of Kunde objects from the data.
    /// </summary>
    public class CsvReaderService
    {
        /// <summary>
        /// Reads data from a CSV file located at the specified file path and returns a list of Kunde objects.
        /// </summary>
        /// <param name="filePath">
        /// The file path of the CSV file to be read.
        /// </param>
        /// <returns>
        /// A list of Kunde objects created from the data in the CSV file.
        /// </returns>
        public List<Kunde> ReadFromCsv(string filePath)
        {
            var kunder = new List<Kunde>();
            if (!File.Exists(filePath))
            {
                //If the file does not exist, log error message and return empty list.
                var error = $"{DateTime.Now} File does not exist: {filePath}";
                File.AppendAllText("error_log.txt", error + Environment.NewLine);
                return kunder;
            }
            try
            {
                // Configure the culture and delimiter used when reading the CSV file. The provided CSV file is a semicolon separated list.
                var culture = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ";",
                    HeaderValidated = null,
                    MissingFieldFound = null
                };
                // Use the CsvReader to read the data in the file and create a list of Kunde objects.
                using var reader = new StreamReader(filePath);
                using var csv = new CsvReader(reader, culture);
                kunder = csv.GetRecords<Kunde>().ToList();
            }
            catch (Exception e)
            {
                // Log unexpected errors while reading the CSV file.
                var error = $"{DateTime.Now} Error reading from .csv: {e}";
                File.AppendAllText("error_log.txt", error + Environment.NewLine);
            }
            return kunder;
        }
    }
}
