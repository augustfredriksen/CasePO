
using System.Globalization;
using CsvHelper;

namespace CasePO.Services
{

    /// <summary>
    /// The CsvWriterService is used to write data to a CSV file from a list of Kunde objects.
    /// </summary>
    public class CsvWriterService
    {
        /// <summary>
        /// Writes data from a list of Kunde objects to a CSV file located at the specified file path.
        /// </summary>
        /// <param name="filePath">The file path of the CSV file to be written.</param>
        /// <param name="kunder">A list of Kunde objects to be written to the CSV file.</param>
        public void WriteToCsv(string filePath, List<Kunde> kunder)
        {
            // Use the CsvWriter to write the data to the file.
            using var writer = new StreamWriter(filePath);
            using var csv = new CsvWriter(writer, CultureInfo.CurrentCulture);
            csv.WriteRecords(kunder);
        }
    }
}
