using BL.Domain;
using BL.Domain.Questions;
using Microsoft.AspNetCore.Identity;

namespace DAL.EF;

public class DataSeeder
{
    private readonly UserManager<IdentityUser> _userManager;

    public DataSeeder(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task Seed(PhygitalDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        var verkiezingen = new Project("Verkiezingen", "Verkiezingen vormen een fundamenteel onderdeel van een " +
                                                       "democratisch systeem, waarbij burgers hun stem kunnen " +
                                                       "uitbrengen om vertegenwoordigers te kiezen die hen in " +
                                                       "de regering zullen vertegenwoordigen. Dit proces stelt " +
                                                       "de bevolking in staat om invloed uit te oefenen op" +
                                                       " beleidsbeslissingen en de richting van het land. " +
                                                       "Verkiezingen vinden meestal plaats op lokaal, regionaal " +
                                                       "en nationaal niveau en kunnen verschillende vormen " +
                                                       "aannemen, zoals parlementaire verkiezingen, `" +
                                                       "presidentsverkiezingen, en referenda. Het doel is om " +
                                                       "een eerlijke en transparante manier te bieden voor het kiezen van leiders en het " +
                                                       "vormgeven van de toekomst van de samenleving.");

        var audi = new Project("Audi",
            "Audi is een Duits automerk dat wereldwijd bekend staat om zijn luxueuze en hoogwaardige voertuigen. Het merk, opgericht in 1909 door August Horch, is tegenwoordig onderdeel van de Volkswagen Group en heeft zijn hoofdkantoor in Ingolstadt, Duitsland. Audi combineert geavanceerde technologie, innovatief ontwerp en uitstekende prestaties om voertuigen te creëren die zowel elegant als krachtig zijn.");
        context.Add(verkiezingen);
        context.Add(audi);

        var adminUser = await _userManager.FindByNameAsync("admin@kdg.be");
        verkiezingen.AdminId = adminUser?.Id;
        audi.AdminId = adminUser?.Id;
        await context.SaveChangesAsync();

        var verkiezingenFlows = new List<Flow>
        {
            new(
                1,
                "Betrokkenheid bij het lokale beleid",
                "Betrokkenheid bij het lokale beleid verwijst naar de mate waarin burgers actief deelnemen aan en invloed uitoefenen op de besluitvorming en uitvoering van beleid in hun lokale gemeenschap. Dit is een cruciaal aspect van een goed functionerende democratie, omdat het zorgt voor een betere afstemming tussen de behoeften en wensen van de burgers en de acties van lokale overheden. ",
                verkiezingen.Id,
                FlowType.LINEAR,
                null,
                [
                    new Flow(
                        1,
                        "Betrokkenheid en participatie",
                        "Betrokkenheid verwijst naar de interesse en het bewustzijn van burgers met betrekking tot politieke en maatschappelijke zaken. Het gaat om de mate waarin mensen zich geïnformeerd voelen en zich bekommeren om wat er gebeurt in hun samenleving.",
                        verkiezingen.Id,
                        FlowType.LINEAR,
                        [
                            new SingleChoiceQuestion(
                                1,
                                "Als jij de begroting van je stad of gemeente zou opmaken, waar zou je dan in de komende jaren vooral op inzetten?",
                                [
                                    new Option("Natuur en ecologie", null),
                                    new Option("Vrije tijd, sport, cultuur", null),
                                    new Option("Huisvesting", null),
                                    new Option("Onderwijs en kinderopvang", null),
                                    new Option("Gezondheidszorg en welzijn", null),
                                    new Option("Verkeersveiligheid en mobiliteit", null),
                                    new Option("Ondersteunen van lokale handel", null)
                                ],
                                new Media()
                                {
                                    description = "Dit is een video over hoe de bevolking naar de politiek kijkt",
                                    url =
                                        "https://storage.googleapis.com/phygital-public/Questions/informatie_stemming.mp4",
                                    type = MediaType.VIDEO
                                }
                            ),
                            new MultipleChoiceQuestion(
                                2,
                                "Wat zou jou helpen om een keuze te maken tussen de verschillende partijen?",
                                [
                                    new Option()
                                    {
                                        Text = "Meer lessen op school rond de partijprogramma’s", NextQuestionId = null
                                    },
                                    new Option()
                                    {
                                        Text = "Activiteiten in mijn jeugdclub, sportclub… rond de verkiezingen",
                                        NextQuestionId = null
                                    },
                                    new Option()
                                    {
                                        Text = "Een bezoek van de partijen aan mijn school, jeugd/sportclub, …",
                                        NextQuestionId = null
                                    },
                                    new Option()
                                    {
                                        Text = "Een gesprek met mijn ouders rond de gemeentepolitiek",
                                        NextQuestionId = null
                                    },
                                    new Option()
                                    {
                                        Text =
                                            "Een debat georganiseerd door een jeugdhuis met de verschillende partijen",
                                        NextQuestionId = null
                                    }
                                ],
                                new Media()
                                {
                                    description = "Afbeelding van alle partijen",
                                    url =
                                        "https://storage.googleapis.com/phygital-public/Questions/afbeelding_partijen.jpg",
                                    type = MediaType.IMAGE
                                }
                            ),
                            new RangeQuestion(
                                3,
                                "Voel jij je betrokken bij het beleid dat wordt uitgestippeld door je gemeente of stad?",
                                1,
                                5,
                                new Media()
                                {
                                    description =
                                        "Dit is een afbeelding over de betrokkenheid van de bevolking bij de politiek",
                                    url =
                                        "https://storage.googleapis.com/phygital-public/Questions/bevolking_antwerpen.jpg",
                                    type = MediaType.IMAGE
                                })
                        ],
                        null,
                        new Media()
                        {
                            description = "Dit is een afbeelding over de betrokkenheid van jongeren",
                            url =
                                "https://storage.googleapis.com/phygital-public/Questions/OVM-Jongeren-betrekken-bij-de-politiek-hoe-doe-je-dat.jpg",
                            type = MediaType.IMAGE
                        },
                        new DateTime(2024, 1, 1).ToUniversalTime(),
                        new DateTime(2024, 7, 1).ToUniversalTime()
                    ),
                    new Flow(
                        2,
                        "Kiesintenties en participatie aan verkiezingen",
                        "Kiesintenties verwijzen naar de bereidheid van kiezers om op een bepaalde partij of kandidaat te stemmen bij een verkiezing. Deze intenties worden vaak gemeten door middel van opiniepeilingen, waarin respondenten wordt gevraagd op welke partij of kandidaat ze van plan zijn te stemmen. Kiesintenties kunnen worden beïnvloed door verschillende factoren",
                        verkiezingen.Id,
                        FlowType.LINEAR,
                        [
                            new SingleChoiceQuestion(
                                1,
                                "Waarop wil jij dat de focus wordt gelegd in het nieuwe stadspark?",
                                [
                                    new Option() { Text = "Sportinfrastructuur", NextQuestionId = null },
                                    new Option() { Text = "Speeltuin voor kinderen", NextQuestionId = null },
                                    new Option() { Text = "Zitbanken en picknickplaatsen", NextQuestionId = null },
                                    new Option() { Text = "Ruimte voor kleine evenementen", NextQuestionId = null },
                                    new Option() { Text = "Drank- en eetmogelijkheden", NextQuestionId = null }
                                ],
                                new Media()
                                {
                                    description = "Video over het nieuwe stadspark in Mechelen",
                                    url = "https://storage.googleapis.com/phygital-public/Questions/video_park.mp4",
                                    type = MediaType.VIDEO
                                }
                            ),
                            new MultipleChoiceQuestion(
                                2,
                                "Jij gaf aan dat je waarschijnlijk niet zal gaan stemmen. Om welke reden(en) zeg je dit?",
                                [
                                    new Option()
                                        { Text = "Ik ben niet geïnteresseerd in politiek", NextQuestionId = null },
                                    new Option()
                                        { Text = "Ik weet niet waar ik moet gaan stemmen", NextQuestionId = null },
                                    new Option()
                                        { Text = "Ik weet niet waarover de verkiezingen gaan", NextQuestionId = null },
                                    new Option()
                                    {
                                        Text = "Ik weet niet wat de verschillende partijen willen doen",
                                        NextQuestionId = null
                                    },
                                    new Option()
                                    {
                                        Text = "Ik weet niet wat de verschillende partijen willen doen",
                                        NextQuestionId = null
                                    }
                                ],
                                new Media()
                                {
                                    description = "Video over hoe het stemmen werkt",
                                    url =
                                        "https://storage.googleapis.com/phygital-public/Questions/hoeWerkthetStemmen.mp3",
                                    type = MediaType.AUDIO
                                }
                            ),
                            new RangeQuestion(
                                3,
                                "Hoe sta jij tegenover deze stelling? “Mijn stad moet meer investeren in fietspaden",
                                1,
                                2,
                                new Media()
                                {
                                    description = "Fietser op een fietspad",
                                    url = "https://storage.googleapis.com/phygital-public/Questions/fietser.jpg",
                                    type = MediaType.IMAGE
                                }
                            ),
                            new OpenQuestion(
                                4,
                                "Je bent schepen van onderwijs voor een dag: waar zet je dan vooral op in?",
                                new Media()
                                {
                                    description = "Afbeelding van de schepen van onderwijs",
                                    url =
                                        "https://storage.googleapis.com/phygital-public/Questions/schepenVanOnderwijs.jpeg",
                                    type = MediaType.IMAGE
                                }
                            )
                        ],
                        null,
                        new Media()
                        {
                            description = "Afbeelding over de kiesintenties en participatie aan verkiezingen",
                            url =
                                "https://storage.googleapis.com/phygital-public/Questions/shutterstock_1937926147_1.jpg",
                            type = MediaType.IMAGE
                        },
                        new DateTime(2024, 1, 1).ToUniversalTime(),
                        new DateTime(2024, 7, 1).ToUniversalTime()
                    )
                ],
                new Media()
                {
                    description = "Afbeelding over de betrokkenheid bij het lokale beleid",
                    url = "https://storage.googleapis.com/phygital-public/Questions/betrekkingBevolking.jpg",
                    type = MediaType.IMAGE
                },
                new DateTime(2024, 1, 1).ToUniversalTime(),
                new DateTime(2024, 7, 1).ToUniversalTime()
            )
        };
        var audiExperienceFlows = new List<Flow>
        {
            new(
                1,
                "Customer Experience Optimization",
                "Het project richt zich op het verbeteren van de algehele klantbeleving van Audi door middel van een gedetailleerde enquête.",
                audi.Id,
                FlowType.LINEAR,
                null,
                new List<Flow>
                {
                    new Flow(
                        1,
                        "Vehicle Performance and Quality",
                        "Dit deel van de enquête verzamelt feedback over de prestaties en kwaliteit van Audi-voertuigen.",
                        audi.Id,
                        FlowType.LINEAR,
                        new List<Question>
                        {
                            new RangeQuestion(
                                1,
                                "Op een schaal van 1 tot 10, hoe zou u de prestaties van uw Audi-voertuig beoordelen?",
                                1,
                                10,
                                new Media()
                                {
                                    description = "Afbeelding van Audi's prestaties",
                                    url = "https://storage.googleapis.com/phygital-public/Questions/audi_prestaties.png",
                                    type = MediaType.IMAGE
                                }
                            ),
                            new OpenQuestion(
                                2,
                                "Wat vindt u het meest indrukwekkend aan uw Audi-voertuig?",
                                new Media()
                                {
                                    description = "Video van Audi's indrukwekkende kenmerken",
                                    url = "https://storage.googleapis.com/phygital-public/Questions/audi_kenmerken.mp4",
                                    type = MediaType.VIDEO
                                }
                            ),
                            new MultipleChoiceQuestion(
                                3,
                                "Welke van de volgende aspecten van uw Audi-voertuig vindt u het belangrijkst? (Kies er maximaal 3)",
                                new List<Option>
                                {
                                    new Option("Rijcomfort", null),
                                    new Option("Brandstofefficiëntie", null),
                                    new Option("Veiligheidsvoorzieningen", null),
                                    new Option("Design", null)
                                },
                                new Media()
                                {
                                    description = "Afbeelding van Audi-interieur",
                                    url = "https://storage.googleapis.com/phygital-public/Questions/audi_kenmerken.mp4",
                                    type = MediaType.IMAGE
                                }
                            )
                        },
                        null,
                        null,
                        new DateTime(2024, 1, 1).ToUniversalTime(),
                        new DateTime(2024, 7, 1).ToUniversalTime()
                    ),
                    new Flow(
                        2,
                        "Dealership and Purchase Experience",
                        "Dit deel van de enquête verzamelt feedback over de aankoopervaring bij Audi-dealers.",
                        audi.Id,
                        FlowType.LINEAR,
                        new List<Question>
                        {
                            new RangeQuestion(
                                1,
                                "Op een schaal van 1 tot 10, hoe zou u uw aankoopervaring bij de Audi-dealer beoordelen?",
                                1,
                                10,
                                new Media()
                                {
                                    description = "Video van de Audi-dealerervaring",
                                    url = "https://storage.googleapis.com/phygital-public/Questions/audi_vlotteverkoop.webp",
                                    type = MediaType.VIDEO
                                }
                            ),
                            new OpenQuestion(
                                2,
                                "Wat kan volgens u worden verbeterd aan de dienstverlening van de Audi-dealer?",
                                new Media()
                                {
                                    description = "Afbeelding van Audi's klantendienst",
                                    url = "https://storage.googleapis.com/phygital-public/Questions/audi_klantendienst.webp",
                                    type = MediaType.IMAGE
                                }
                            ),
                            new SingleChoiceQuestion(
                                3,
                                "Vond u het aankoopproces bij de Audi-dealer soepel en probleemloos?",
                                new List<Option>
                                {
                                    new Option("Ja", null),
                                    new Option("Nee", null)
                                },
                                new Media()
                                {
                                    description = "Afbeelding van een vlotte Audi-aankoop",
                                    url = "https://storage.googleapis.com/phygital-public/Questions/audi_vlotteverkoop.webp",
                                    type = MediaType.IMAGE
                                }
                            )
                        },
                        null,
                        null,
                        new DateTime(2024, 1, 1).ToUniversalTime(),
                        new DateTime(2024, 7, 1).ToUniversalTime()
                    ),
                    new Flow(
                        3,
                        "Service and Maintenance",
                        "Dit deel van de enquête verzamelt feedback over de service en het onderhoud bij Audi-dealers.",
                        audi.Id,
                        FlowType.LINEAR,
                        new List<Question>
                        {
                            new RangeQuestion(
                                1,
                                "Hoe zou u de kwaliteit van de service en onderhoud bij uw Audi-dealer beoordelen op een schaal van 1 tot 10?",
                                1,
                                10,
                                new Media()
                                {
                                    description = "Video van Audi-onderhoudsservice",
                                    url = "https://storage.googleapis.com/phygital-public/Questions/audi_onderhoud.mp4",
                                    type = MediaType.VIDEO
                                }
                            ),
                            new OpenQuestion(
                                2,
                                "Kunt u een specifiek voorbeeld geven van een positieve of negatieve ervaring met de serviceafdeling van Audi?",
                                new Media()
                                {
                                    description = "Afbeelding van Audi-service",
                                    url = "https://storage.googleapis.com/phygital-public/Questions/audi_klantendienst.webp",
                                    type = MediaType.IMAGE
                                }
                            ),
                            new MultipleChoiceQuestion(
                                3,
                                "Hoe heeft u contact opgenomen met de serviceafdeling van uw Audi-dealer? (Meerdere opties mogelijk)",
                                new List<Option>
                                {
                                    new Option("Telefonisch", null),
                                    new Option("E-mail", null),
                                    new Option("Chat", null),
                                    new Option("Persoonlijk bezoek", null)
                                },
                                new Media()
                                {
                                    description = "Afbeelding van verschillende contactmethoden",
                                    url = "https://storage.googleapis.com/phygital-public/Questions/audi_contact.jpeg",
                                    type = MediaType.IMAGE
                                }
                            )
                        },
                        null,
                        null,
                        new DateTime(2024, 1, 1).ToUniversalTime(),
                        new DateTime(2024, 7, 1).ToUniversalTime()
                    ),
                    new Flow(
                        4,
                        "Overall Audi Experience",
                        "Dit deel van de enquête verzamelt feedback over de algehele ervaring met Audi.",
                        audi.Id,
                        FlowType.LINEAR,
                        new List<Question>
                        {
                            new RangeQuestion(
                                1,
                                "Hoe tevreden bent u in het algemeen over uw ervaring met Audi op een schaal van 1 tot 10?",
                                1,
                                10,
                                new Media()
                                {
                                    description = "Video van een algemene Audi-ervaring",
                                    url = "https://example.com/audi_overall_experience.mp4",
                                    type = MediaType.VIDEO
                                }
                            ),
                            new OpenQuestion(
                                2,
                                "Wat is volgens u het belangrijkste dat Audi kan doen om uw algehele ervaring te verbeteren?",
                                new Media()
                                {
                                    description = "Afbeelding van een Audi-verbetering",
                                    url = "https://storage.googleapis.com/phygital-public/Questions/audi_vlotteverkoop.webp",
                                    type = MediaType.IMAGE
                                }
                            ),
                            new SingleChoiceQuestion(
                                3,
                                "Zou u Audi aanbevelen aan anderen?",
                                new List<Option>
                                {
                                    new Option("Ja", null),
                                    new Option("Nee", null)
                                },
                                new Media()
                                {
                                    description = "Afbeelding van een aanbeveling",
                                    url = "https://storage.googleapis.com/phygital-public/Questions/audi_interieur.webp",
                                    type = MediaType.IMAGE
                                }
                            )
                        },
                        null,
                        null,
                        new DateTime(2024, 1, 1).ToUniversalTime(),
                        new DateTime(2024, 7, 1).ToUniversalTime()
                    )
                },
                new Media()
                {
                    description = "Afbeelding van een Audi-voertuig",
                    url = "https://storage.googleapis.com/phygital-public/Questions/audi_interieur.webp",
                    type = MediaType.IMAGE
                },
                new DateTime(2024, 1, 1).ToUniversalTime(),
                new DateTime(2024, 7, 1).ToUniversalTime()
            )
        };


        verkiezingen.Flows = verkiezingenFlows;
        audi.Flows = audiExperienceFlows;
        await context.SaveChangesAsync();
    }
}