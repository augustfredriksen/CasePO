
using Newtonsoft.Json;

namespace CasePO.Services
{
    /// <summary>
    /// The JsonDeserializerService class is used to deserialize JSON data into Kunde objects.
    /// </summary>
    public class JsonDeserializerService
    {
        /// <summary>
        /// Deserialize a batch of json data into a list of Kunde objects. If the name from the json data matches the name from input file, keep the original name.
        /// </summary>
        /// <param name="json">A json string from the ApiClientService to be deserialized</param>
        /// <param name="orgNosAndNames">A list of Kunde objects that only contain orgNos and Names created by the input file and CsvReader.
        /// Used to match the json data.
        /// </param>
        /// <returns>A list of kunde objects created from the json data</returns>
        public List<Kunde> DeserializeBatchJson(string json, List<Kunde> orgNosAndNames)
        {
            var kunder = new List<Kunde>();
            var jsonObject = JsonConvert.DeserializeObject<dynamic>(json);
            if (jsonObject?._embedded == null)
            {
                // If the _embedded field in the json object is null, log error message and return an empty list.
                var error = $"{DateTime.Now} Json _embedded is null";
                File.AppendAllText("error_log.txt", error + Environment.NewLine);
            }
            else
            {
                var enheter = jsonObject._embedded.enheter;
                foreach (var enhet in enheter)
                {
                    // Check if the name from original file matches the name in the json.
                    if (orgNosAndNames.Any(x => x.BrregNavn.ToUpper() == enhet.navn.ToString().ToUpper()))
                    {
                        var kunde = new Kunde()
                        {
                            OrganisasjonsNummer = enhet.organisasjonsnummer.ToString(),
                            BrregNavn = orgNosAndNames.FirstOrDefault(orgNoName => orgNoName.OrganisasjonsNummer == enhet.organisasjonsnummer.ToString()).BrregNavn,
                            AntallAnsatte = enhet.antallAnsatte,
                            Organisasjonsform = new OrganisasjonsForm
                            { 
                                Kode = enhet.organisasjonsform?.kode.ToString() ?? "N/A"
                            },
                            Næringskode = new Næringskode()
                            {
                                Kode = enhet.naeringskode1?.kode.ToString() ?? "N/A"
                            },
                            Konkurs = enhet.konkurs
                        };
                        kunder.Add(kunde);
                    }
                    else
                    {
                        kunder.Add(JsonConvert.DeserializeObject<Kunde>(enhet.ToString()));
                    }
                }
            }
            return kunder;
        }

        /// <summary>
        /// Deserialize individual json data into a single Kunde object.
        /// </summary>
        /// <param name="json"> json data from ApiClientService to be deserialized</param>
        /// <returns>A Kunde object created from the json data</returns>
        public Kunde DeserializeIndividualJson(string? json)
        {
            var kunde = JsonConvert.DeserializeObject<Kunde>(json);
            return kunde;
        }
    }
}
