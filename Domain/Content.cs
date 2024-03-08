namespace BL.Domain;

public class Content
{
    //TODO: finish Content class
    
    public int ContentId { get; set; }
    public ContentType Type { get; set; }

    public Content()
    {
    }

    public Content(ContentType type)
    {
        Type = type;
    }
}