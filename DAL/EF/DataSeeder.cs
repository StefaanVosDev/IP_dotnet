using BL.Domain;
using BL.Domain.Answers;
using BL.Domain.Questions;
using Microsoft.AspNetCore.Identity;

namespace DAL.EF;

public class DataSeeder
{

    public DataSeeder()
    {
    }

    public static void Seed(PhygitalDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        
        /*var adminUser = _userManager.FindByNameAsync("admin@kdg.be").Result;
        if (adminUser == null)
        {
            throw new Exception("Admin user not found");
        }*/

        var project = new Project("Phygital", "dit is de interesante beschrijving van de flow");
        context.Add(project);
        context.SaveChanges();

        var flows = new List<Flow>
        {
            new(
                1,
                "Betrokkenheid bij het lokale beleid",
                "dit is de interesante beschrijving van de flow",
                project.Id,
                FlowType.LINEAR,
                null,
                [
                    new Flow(
                        1,
                        "Betrokkenheid en participatie",
                        "dit is de interesante beschrijving van de flow",
                        project.Id,
                        FlowType.LINEAR,
                        [
                            new SingleChoiceQuestion(
                                1,
                                "Als jij de begroting van je stad of gemeente zou opmaken, waar zou je dan in de komende jaren vooral op inzetten?",
                                [
                                    "Natuur en ecologie",
                                    "Vrije tijd, sport, cultuur",
                                    "Huisvesting",
                                    "Onderwijs en kinderopvang",
                                    "Gezondheidszorg en welzijn",
                                    "Verkeersveiligheid en mobiliteit",
                                    "Ondersteunen van lokale handel"
                                ],
                                new Media()
                                {
                                    description = "Dit is een video over hoe de bevolking naar de politiek kijkt",
                                    url = "https://www.youtube.com/watch?v=JGfXiIz5nqQ",
                                    type = MediaType.VIDEO
                                }
                            ),
                            new MultipleChoiceQuestion(
                                2,
                                "Wat zou jou helpen om een keuze te maken tussen de verschillende partijen?",
                                [
                                    "Meer lessen op school rond de partijprogramma’s",
                                    "Activiteiten in mijn jeugdclub, sportclub… rond de verkiezingen",
                                    "Een bezoek van de partijen aan mijn school, jeugd/sportclub, …",
                                    "Een gesprek met mijn ouders rond de gemeentepolitiek",
                                    "Een debat georganiseerd door een jeugdhuis met de verschillende partijen"
                                ],
                                new Media()
                                {
                                    description = "Afbeelding van alle partijen",
                                    url = "https://images.vrt.be/width960/2019/04/26/10341807-6837-11e9-abcc-02b7b76bf47f.png",
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
                                    description = "Dit is een afbeelding over de betrokkenheid van de bevolking bij de politiek",
                                    url="https://assets.antwerpen.be/srv/assets/api/image/1a6473b3-e416-4112-90e6-5fd7b26a9c60/Illustratie_00_ALL_02_website.jpg/contentimage_full",
                                    type = MediaType.IMAGE
                                })
                        ],
                        null,
                        new Media()
                        {
                            description = "Dit is een afbeelding over de betrokkenheid van jongeren",
                            url="https://www.onderwijsvanmorgen.nl/wp-content/uploads/2022/12/OVM-Jongeren-betrekken-bij-de-politiek-hoe-doe-je-dat.jpg",
                            type = MediaType.IMAGE
                        }
                    ),
                    new Flow(
                        2,
                        "Kiesintenties en participatie aan verkiezingen",
                        "dit is de interesante beschrijving van de flow",
                        project.Id,
                        FlowType.LINEAR,
                        [
                            new SingleChoiceQuestion(
                                1,
                                "Waarop wil jij dat de focus wordt gelegd in het nieuwe stadspark?",
                                [
                                    "Sportinfrastructuur",
                                    "Speeltuin voor kinderen",
                                    "Zitbanken en picknickplaatsen",
                                    "Ruimte voor kleine evenementen",
                                    "Drank- en eetmogelijkheden"
                                ],
                                new Media()
                                {
                                    description = "Video over het nieuwe stadspark in Mechelen",
                                    url = "https://www.google.com/search?q=nieuw+stadspark+video&sca_esv=114751fe4ad266cf&sca_upv=1&rlz=1C1QPHC_nlBE1075BE1075&ei=K-IuZoPCLeWYkdUP_7GCyA0&ved=0ahUKEwjDg-CUjuaFAxVlTKQEHf-YANkQ4dUDCBA&uact=5&oq=nieuw+stadspark+video&gs_lp=Egxnd3Mtd2l6LXNlcnAiFW5pZXV3IHN0YWRzcGFyayB2aWRlbzIFECEYoAEyBRAhGKABMgUQIRigAUjmDFCSAljRC3ABeACQAQCYAWCgAe0EqgEBObgBA8gBAPgBAZgCCaAC1ATCAgsQABiABBiwAxiiBMICCxAAGLADGKIEGIkFwgIFEAAYgATCAggQABiABBiiBMICCBAAGKIEGIkFwgIGEAAYFhgewgIFECEYnwWYAwCIBgGQBgOSBwM4LjGgB78c&sclient=gws-wiz-serp#fpstate=ive&vld=cid:b5ff1d6c,vid:4kfe77stM-s,st:0",
                                    type = MediaType.VIDEO
                                }
                            ),
                            new MultipleChoiceQuestion(
                                2,
                                "Jij gaf aan dat je waarschijnlijk niet zal gaan stemmen. Om welke reden(en) zeg je dit?",
                                [
                                    "Ik ben niet geïnteresseerd in politiek",
                                    "Ik weet niet waar ik moet gaan stemmen",
                                    "Ik weet niet waarover de verkiezingen gaan",
                                    "Ik weet niet wat de verschillende partijen willen doen",
                                    "Ik weet niet wat de verschillende partijen willen doen"
                                ],
                                new Media()
                                {
                                    description = "Video over hoe het stemmen werkt",
                                    url = "/audio/hoeWerkthetStemmen.mp3",
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
                                    url = "/images/fietser.jpg",
                                    type = MediaType.IMAGE
                                }
                            ),
                            new OpenQuestion(
                                4,
                                "Je bent schepen van onderwijs voor een dag: waar zet je dan vooral op in?",
                                new Media()
                                {
                                    description = "Afbeelding van de schepen van onderwijs",
                                    url = "/images/schepenOnderwijs.jpg",
                                    type = MediaType.IMAGE
                                }
                            )
                        ],
                        null,
                        new Media()
                        {
                            description = "Afbeelding over de kiesintenties en participatie aan verkiezingen",
                            url = "https://www.vlaamsparlement.be/sites/default/files/styles/social_media_image/public/2023-06/shutterstock_1937926147_1.jpg?h=55c51ed5&itok=jGJL2iNw",
                            type = MediaType.IMAGE
                        }
                    )
                ],
                new Media()
                {
                    description = "Afbeelding over de betrokkenheid bij het lokale beleid",
                    url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS36v1EQLdgUmzyTsY8tDOJCKA2ZH3_-seFCZYmWEGq8w&s",
                    type = MediaType.IMAGE
                }
            )
        };
        
        project.Flows = flows;
        project.AdminId = "1";
        context.Update(project);
        context.SaveChanges();
    }
}