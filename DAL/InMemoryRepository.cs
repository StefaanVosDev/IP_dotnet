using System.Linq.Expressions;
using BL.Domain;
using BL.Domain.Questions;
using DAL.Interfaces;

namespace DAL;

public class InMemoryRepository : IRepository
{
    private readonly Dictionary<Type, object> _store = new Dictionary<Type, object>();

    public Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : class
    {
        var type = typeof(TEntity);
        return _store.TryGetValue(type, out object value) ? Task.FromResult((IEnumerable<TEntity>)value) : Task.FromResult(Enumerable.Empty<TEntity>());
    }

    public Task<TEntity> GetAsync<TEntity>(int id) where TEntity : class
    {
        var entities = GetAllAsync<TEntity>().Result;
        var entity = entities.FirstOrDefault(e => ((dynamic)e).Id == id);
        return Task.FromResult(entity);
    }

    public Task AddAsync<TEntity>(TEntity entity) where TEntity : class
    {
        var type = typeof(TEntity);
        if (!_store.ContainsKey(type))
        {
            _store[type] = new List<TEntity>();
        }
        ((List<TEntity>)_store[type]).Add(entity);
        return Task.CompletedTask;
    }

    public Task UpdateAsync<TEntity>(TEntity entityToUpdate, TEntity entity) where TEntity : class
    {
        throw new NotImplementedException("This method is not implemented for the InMemoryRepository.");
    }

    public Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class
    {
        var type = typeof(TEntity);
        if (_store.ContainsKey(type))
        {
            ((List<TEntity>)_store[type]).Remove(entity);
        }
        return Task.CompletedTask;
    }

    public Task<IEnumerable<TEntity>> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
    {
        throw new NotImplementedException("This method is not implemented for the InMemoryRepository.");
    }
    
    public static void Seed(PhygitalDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        context.Add(new Project("Phygital",
            [
                new Flow(
                    "Betrokkenheid bij het lokale beleid",
                    FlowType.LINEAR,
                    null,
                    [
                        new Flow(
                            "Betrokkenheid en participatie",
                            FlowType.LINEAR,
                            [
                                new SingleChoiceQuestion(
                                    "Als jij de begroting van je stad of gemeente zou opmaken, waar zou je dan in de komende jaren vooral op inzetten?",
                                    [
                                        "Natuur en ecologie",
                                        "Vrije tijd, sport, cultuur",
                                        "Huisvesting",
                                        "Onderwijs en kinderopvang",
                                        "Gezondheidszorg en welzijn",
                                        "Verkeersveiligheid en mobiliteit",
                                        "Ondersteunen van lokale handel"
                                    ]
                                ),
                                new MultipleChoiceQuestion(
                                    "Wat zou jou helpen om een keuze te maken tussen de verschillende partijen?",
                                    [
                                        "Meer lessen op school rond de partijprogramma’s",
                                        "Activiteiten in mijn jeugdclub, sportclub… rond de verkiezingen",
                                        "Een bezoek van de partijen aan mijn school, jeugd/sportclub, …",
                                        "Een gesprek met mijn ouders rond de gemeentepolitiek",
                                        "Een debat georganiseerd door een jeugdhuis met de verschillende partijen"
                                    ]
                                ),
                                new ScaleQuestion(
                                    "Voel jij je betrokken bij het beleid dat wordt uitgestippeld door je gemeente of stad?",
                                    1,
                                    5)
                            ],
                            null
                        ),
                        new Flow(
                            "Kiesintenties en participatie aan verkiezingen",
                            FlowType.LINEAR,
                            [
                                new SingleChoiceQuestion(
                                    "Waarop wil jij dat de focus wordt gelegd in het nieuwe stadspark?",
                                    [
                                        "Sportinfrastructuur",
                                        "Speeltuin voor kinderen",
                                        "Zitbanken en picknickplaatsen",
                                        "Ruimte voor kleine evenementen",
                                        "Drank- en eetmogelijkheden"
                                    ]
                                ),
                                new MultipleChoiceQuestion(
                                    "Jij gaf aan dat je waarschijnlijk niet zal gaan stemmen. Om welke reden(en) zeg je dit?",
                                    [
                                        "Ik ben niet geïnteresseerd in politiek",
                                        "Ik weet niet waar ik moet gaan stemmen",
                                        "Ik weet niet waarover de verkiezingen gaan",
                                        "Ik weet niet wat de verschillende partijen willen doen",
                                        "Ik weet niet wat de verschillende partijen willen doen"
                                    ]
                                ),
                                new ScaleQuestion(
                                    "Hoe sta jij tegenover deze stelling? “Mijn stad moet meer investeren in fietspaden",
                                    1,
                                    2
                                ),
                                new OpenQuestion(
                                    "Je bent schepen van onderwijs voor een dag: waar zet je dan vooral op in?"
                                )
                            ],
                            null
                        )
                    ]
                )
            ]
        ));

        context.SaveChanges();
    }
}
