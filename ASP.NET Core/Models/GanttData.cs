using System;
using System.Collections.Generic;

namespace ASP_NET_Core.Models;

public static class GanttData
{
    public static List<GanttTask> Tasks = new List<GanttTask>
    {
        new GanttTask
        {
            Id = 1,
            ParentId = 0,
            Title = "Software Development",
            Start = new DateTime(2019, 2, 21, 5, 0, 0, DateTimeKind.Utc),
            End = new DateTime(2019, 7, 4, 12, 0, 0, DateTimeKind.Utc),
            Progress = 31
        },
        new GanttTask
        {
            Id = 2,
            ParentId = 1,
            Title = "Scope",
            Start = new DateTime(2019, 2, 21, 5, 0, 0, DateTimeKind.Utc),
            End = new DateTime(2019, 2, 26, 9, 0, 0, DateTimeKind.Utc),
            Progress = 60
        },
        new GanttTask
        {
            Id = 3,
            ParentId = 2,
            Title = "Determine project scope",
            Start = new DateTime(2019, 2, 21, 5, 0, 0, DateTimeKind.Utc),
            End = new DateTime(2019, 2, 21, 9, 0, 0, DateTimeKind.Utc),
            Progress = 100
        },
        new GanttTask
        {
            Id = 4,
            ParentId = 2,
            Title = "Secure project sponsorship",
            Start = new DateTime(2019, 2, 21, 10, 0, 0, DateTimeKind.Utc),
            End = new DateTime(2019, 2, 22, 9, 0, 0, DateTimeKind.Utc),
            Progress = 100
        },
        new GanttTask
        {
            Id = 5,
            ParentId = 2,
            Title = "Define preliminary resources",
            Start = new DateTime(2019, 2, 22, 10, 0, 0, DateTimeKind.Utc),
            End = new DateTime(2019, 2, 25, 9, 0, 0, DateTimeKind.Utc),
            Progress = 60
        },
        new GanttTask
        {
            Id = 6,
            ParentId = 2,
            Title = "Secure core resources",
            Start = new DateTime(2019, 2, 25, 10, 0, 0, DateTimeKind.Utc),
            End = new DateTime(2019, 2, 26, 9, 0, 0, DateTimeKind.Utc),
            Progress = 0
        },
        new GanttTask
        {
            Id = 7,
            ParentId = 2,
            Title = "Scope complete",
            Start = new DateTime(2019, 2, 26, 9, 0, 0, DateTimeKind.Utc),
            End = new DateTime(2019, 2, 26, 9, 0, 0, DateTimeKind.Utc),
            Progress = 0
        }
    };

    public static List<GanttResource> Resources = new List<GanttResource>
    {
        new GanttResource { Id = 1, Text = "Management" },
        new GanttResource { Id = 2, Text = "Project Manager" },
        new GanttResource { Id = 3, Text = "Analyst" },
        new GanttResource { Id = 4, Text = "Developer" },
        new GanttResource { Id = 5, Text = "Testers" },
        new GanttResource { Id = 6, Text = "Trainers" },
        new GanttResource { Id = 7, Text = "Technical Communicators" },
        new GanttResource { Id = 8, Text = "Deployment Team" }
    };

    public static List<GanttResourceAssignment> ResourceAssignments = new List<GanttResourceAssignment>
    {
        new GanttResourceAssignment { Id = 0, TaskId = 3, ResourceId = 1 },
        new GanttResourceAssignment { Id = 1, TaskId = 4, ResourceId = 1 },
        new GanttResourceAssignment { Id = 2, TaskId = 5, ResourceId = 2 },
        new GanttResourceAssignment { Id = 3, TaskId = 6, ResourceId = 2 }
    };

    public static List<GanttDependency> Dependencies = new List<GanttDependency>
    {
        new GanttDependency { Id = 0, PredecessorId = 3, SuccessorId = 4, Type = 0 },
        new GanttDependency { Id = 1, PredecessorId = 4, SuccessorId = 5, Type = 0 },
        new GanttDependency { Id = 2, PredecessorId = 5, SuccessorId = 6, Type = 0 },
        new GanttDependency { Id = 3, PredecessorId = 6, SuccessorId = 7, Type = 0 }
    };
}
