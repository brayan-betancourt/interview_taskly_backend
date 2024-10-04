using System.Collections;

namespace InterviewAppTasklyWebApi.Entities;

public class TaskStateManagement
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
    public int TaskId { get; set; }
    public virtual TaskManagement TasksManagements { get; set; }
    public int StateId { get; set; }
    public virtual TaskState States { get; set; }
}