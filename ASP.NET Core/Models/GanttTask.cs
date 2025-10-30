using System;

namespace ASP_NET_Core.Models;

public class GanttTask
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public string Title { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int Progress { get; set; }
}
