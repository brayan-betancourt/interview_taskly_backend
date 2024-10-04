namespace InterviewAppTasklyWebApi.Entities;

public class TaskState
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
    public string Name { get; set; }
    
    public virtual ICollection<TaskManagement> TaskManagements { get; set; }
    public virtual ICollection<TaskStateManagement> TaskStateManagements { get; set; }

}