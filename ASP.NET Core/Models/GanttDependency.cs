namespace ASP_NET_Core.Models;

public class GanttDependency
{
    public int Id { get; set; }
    public int PredecessorId { get; set; }
    public int SuccessorId { get; set; }
    public int Type { get; set; }
}
