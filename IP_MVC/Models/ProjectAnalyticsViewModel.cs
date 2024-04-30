namespace IP_MVC.Models
{
    public class ProjectAnalyticsViewModel
    {
        public int ProjectId { get; set; }
        public Dictionary<string, int> OpenQuestionData { get; set; }
        public Dictionary<string, int> MultipleChoiceQuestionData { get; set; }
        public Dictionary<string, int> SingleChoiceQuestionData { get; set; }
        public Dictionary<string, int> RangeQuestionData { get; set; }
    }
}