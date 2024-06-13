namespace IP_MVC.Models;

public class AskPasswordPopUpModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string ActionName { get; set; }
    public string ButtonText { get; set; }
    public int? ProjectId { get; set; }
    public int? ParentFlowId { get; set; }
}