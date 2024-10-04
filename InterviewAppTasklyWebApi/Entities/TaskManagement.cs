namespace InterviewAppTasklyWebApi.Entities;

public class TaskManagement
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public int PriorityId { get; set; }
    public virtual TaskPriority Priorities { get; set; }
    public int StateId { get; set; }
    public virtual TaskState States { get; set; }
    public string UserId { get; set; }
    public virtual ApplicationUser Users { get; set; }
    public virtual ICollection<TaskStateManagement> TaskStates { get; set; }
}