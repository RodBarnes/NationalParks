namespace NationalParks.Models;

public class LessonPlan
{
    public List<CommonCore> CommonCore { get; set; }
    public string Duration { get; set; }
    public string GradeLevel { get; set; }
    public int Id { get; set; }
    public List<string> Parks { get; set; }
    public string QuestionObjective { get; set; }
    public string Subject { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
}
