using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;

namespace CasePO.Services
{
    public class Kunde
    {
        [JsonProperty("organisasjonsnummer")]
        public string OrganisasjonsNummer { get; set; }

        [JsonProperty("navn")]
        public string BrregNavn { get; set; }
        [JsonProperty("antallAnsatte")]
        public int? AntallAnsatte { get; set; }
        [JsonProperty("naeringskode1")]
        public Næringskode? Næringskode { get; set; }

        [JsonProperty("organisasjonsform")]
        public OrganisasjonsForm? Organisasjonsform { get; set; }
        [JsonProperty("slettedato")]
        public string? Slettedato { get; set; }
        [JsonProperty("konkurs")] 
        public bool Konkurs { get; set; }
    }

    public class OrganisasjonsForm
    {
        [JsonProperty("kode")]
        public string? Kode { get; set; }
    }

    public class Næringskode
    {
        [JsonProperty("kode")]
        public string? Kode { get; set; }
    }
}
