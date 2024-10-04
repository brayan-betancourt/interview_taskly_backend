namespace InterviewAppTasklyWebApi.Models;

public class TaskManagementModel
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Priority { get; set; } = null!;
    public string Status { get; set; } = null!;
    public string CreatedBy { get; set; } = null!;
    public DateTime DueDate { get; set; }
}