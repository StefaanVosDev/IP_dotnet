namespace BL.Domain;

//TODO: change Class name and add new classes when needed
public class Template
{
    //TODO: change to needed vars
    //TODO: add restrictions when needed to vars
    #region Vars
    
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }

    #endregion

    #region Constructors

    //TODO: if adding other constructors, be sure to leave the default constructor in the code for the DB

    public Template()
    {
    }

    #endregion
}