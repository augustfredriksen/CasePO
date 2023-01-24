
using Newtonsoft.Json;

namespace CasePO.Services
{
    public class JsonDeserializerService
    {
        public List<Kunde> DeserializeBatchJson(string json, List<Kunde> orgNosAndNames)
        {
            var kunder = new List<Kunde>();
            var jsonObject = JsonConvert.DeserializeObject<dynamic>(json);
            if (jsonObject._embedded == null)
            {
                Console.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
            }
            else
            {
                var enheter = jsonObject._embedded.enheter;
                foreach (var enhet in enheter)
                {
                    if (orgNosAndNames.Any(x => x.BrregNavn.ToUpper() == enhet.navn.ToString().ToUpper()))
                    {
                        var kunde = new Kunde()
                        {
                            OrganisasjonsNummer = enhet.organisasjonsnummer.ToString(),
                            BrregNavn = orgNosAndNames.FirstOrDefault(i => i.OrganisasjonsNummer == enhet.organisasjonsnummer.ToString()).BrregNavn,
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

        public Kunde DeserializeIndividualJson(string? json)
        {
            var kunde = JsonConvert.DeserializeObject<Kunde>(json);
            return kunde;
        }
    }
}
