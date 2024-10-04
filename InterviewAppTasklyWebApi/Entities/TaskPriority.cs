using System.Collections;

namespace InterviewAppTasklyWebApi.Entities;

public class TaskPriority
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
    public string Name { get; set; }
    public virtual ICollection<TaskManagement> TaskManagements { get; set; }
}