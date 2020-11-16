using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace DevExtremeMvcApp1.Models {
    public static class GanttDataProvider {
        const string
            TasksSessionKey = "Tasks",
            DependenciesSessionKey = "Dependencies",
            ResourcesSessionKey = "Resources",
            ResourceAssignmentsSessionKey = "ResourceAssignments";

        static HttpSessionState Session { get { return HttpContext.Current.Session; } }

        public static object GetTasks() { return Tasks; }
        public static object GetDependencies() { return Dependencies; }
        public static object GetResources() { return Resources; }
        public static object GetResourceAssignments() { return ResourceAssignments; }

        public static List<Task> Tasks {
            get {
                if (Session[TasksSessionKey] == null)
                    Session[TasksSessionKey] = CreateTasks();
                return (List<Task>)Session[TasksSessionKey];
            }
        }
        public static List<Dependency> Dependencies {
            get {
                if (Session[DependenciesSessionKey] == null)
                    Session[DependenciesSessionKey] = CreateDependencies();
                return (List<Dependency>)Session[DependenciesSessionKey];
            }
        }
        public static List<Resource> Resources {
            get {
                if (Session[ResourcesSessionKey] == null)
                    Session[ResourcesSessionKey] = CreateResources();
                return (List<Resource>)Session[ResourcesSessionKey];
            }
        }
        public static List<ResourceAssignment> ResourceAssignments {
            get {
                if (Session[ResourceAssignmentsSessionKey] == null)
                    Session[ResourceAssignmentsSessionKey] = CreateResourceAssignments();
                return (List<ResourceAssignment>)Session[ResourceAssignmentsSessionKey];
            }
        }

        static List<Task> CreateTasks() {
            var result = new List<Task>();

            result.Add(CreateTask("0", "-1", "Software Development", "02/21/2020 08:00:00", "07/04/2020 15:00:00", "31", "1", "0", "", ""));
            result.Add(CreateTask("1", "0", "Scope", "02/21/2020 08:00:00", "02/26/2020 12:00:00", "60", "1", "1", "", ""));
            result.Add(CreateTask("2", "1", "Determine project scope", "02/21/2020 08:00:00", "02/21/2020 12:00:00", "100", "1", "2", "1", "Important"));
            result.Add(CreateTask("3", "1", "Secure project sponsorship", "02/21/2020 13:00:00", "02/22/2020 12:00:00", "100", "1", "2", "1", ""));
            result.Add(CreateTask("4", "1", "Define preliminary resources", "02/22/2020 13:00:00", "02/25/2020 12:00:00", "60", "1", "2", "2", ""));
            result.Add(CreateTask("5", "1", "Secure core resources", "02/25/2020 13:00:00", "02/26/2020 12:00:00", "0", "1", "2", "2", ""));
            result.Add(CreateTask("6", "1", "Scope complete", "02/26/2020 12:00:00", "02/26/2020 12:00:00", "0", "0", "2", "", "Important"));
            result.Add(CreateTask("7", "0", "Analysis/Software Requirements", "02/26/2020 13:00:00", "03/18/2020 12:00:00", "80", "1", "1", "", "Important"));
            result.Add(CreateTask("8", "7", "Conduct needs analysis", "02/26/2020 13:00:00", "03/05/2020 12:00:00", "100", "1", "2", "3", ""));
            result.Add(CreateTask("9", "7", "Draft preliminary software specifications", "03/05/2020 13:00:00", "03/08/2020 12:00:00", "100", "1", "2", "3", ""));
            result.Add(CreateTask("10", "7", "Develop preliminary budget", "03/08/2020 13:00:00", "03/12/2020 12:00:00", "100", "1", "2", "2", ""));
            result.Add(CreateTask("11", "7", "Review software specifications/budget with team", "03/12/2020 13:00:00", "03/12/2020 17:00:00", "100", "1", "2", "2,3", ""));
            result.Add(CreateTask("12", "7", "Incorporate feedback on software specifications", "03/13/2020 08:00:00", "03/13/2020 17:00:00", "70", "1", "2", "3", ""));
            result.Add(CreateTask("13", "7", "Develop delivery timeline", "03/14/2020 08:00:00", "03/14/2020 17:00:00", "0", "1", "2", "2", ""));
            result.Add(CreateTask("14", "7", "Obtain approvals to proceed (concept, timeline, budget)", "03/15/2020 08:00:00", "03/15/2020 12:00:00", "0", "1", "2", "1,2", ""));
            result.Add(CreateTask("15", "7", "Secure required resources", "03/15/2020 13:00:00", "03/18/2020 12:00:00", "0", "1", "2", "2", ""));
            result.Add(CreateTask("16", "7", "Analysis complete", "03/18/2020 12:00:00", "03/18/2020 12:00:00", "0", "0", "2", "", ""));
            result.Add(CreateTask("17", "0", "Design", "03/18/2020 13:00:00", "04/05/2020 17:00:00", "80", "1", "1", "", ""));
            result.Add(CreateTask("18", "17", "Review preliminary software specifications", "03/18/2020 13:00:00", "03/20/2020 12:00:00", "100", "1", "2", "3", ""));
            result.Add(CreateTask("19", "17", "Develop functional specifications", "03/20/2020 13:00:00", "03/27/2020 12:00:00", "100", "1", "2", "3", ""));
            result.Add(CreateTask("20", "17", "Develop prototype based on functional specifications", "03/27/2020 13:00:00", "04/02/2020 12:00:00", "100", "1", "2", "3", ""));
            result.Add(CreateTask("21", "17", "Review functional specifications", "04/02/2020 13:00:00", "04/04/2020 12:00:00", "30", "1", "2", "1", ""));
            result.Add(CreateTask("22", "17", "Incorporate feedback into functional specifications", "04/04/2020 13:00:00", "04/05/2020 12:00:00", "0", "1", "2", "1", ""));
            result.Add(CreateTask("23", "17", "Obtain approval to proceed", "04/05/2020 13:00:00", "04/05/2020 17:00:00", "0", "1", "2", "1,2", ""));
            result.Add(CreateTask("24", "17", "Design complete", "04/05/2020 17:00:00", "04/05/2020 17:00:00", "0", "0", "2", "", ""));
            result.Add(CreateTask("25", "0", "Development", "04/08/2020 08:00:00", "05/07/2020 15:00:00", "42", "1", "1", "", ""));
            result.Add(CreateTask("26", "25", "Review functional specifications", "04/08/2020 08:00:00", "04/08/2020 17:00:00", "100", "1", "2", "4", ""));
            result.Add(CreateTask("27", "25", "Identify modular/tiered design parameters", "04/09/2020 08:00:00", "04/09/2020 17:00:00", "100", "1", "2", "4", ""));
            result.Add(CreateTask("28", "25", "Assign development staff", "04/10/2020 08:00:00", "04/10/2020 17:00:00", "100", "1", "2", "4", ""));
            result.Add(CreateTask("29", "25", "Develop code", "04/11/2020 08:00:00", "05/01/2020 17:00:00", "49", "1", "2", "4", ""));
            result.Add(CreateTask("30", "25", "Developer testing (primary debugging)", "04/16/2020 15:00:00", "05/07/2020 15:00:00", "24", "1", "2", "4", ""));
            result.Add(CreateTask("31", "25", "Development complete", "05/07/2020 15:00:00", "05/07/2020 15:00:00", "0", "0", "2", "", ""));
            result.Add(CreateTask("32", "0", "Testing", "04/08/2020 08:00:00", "06/13/2020 15:00:00", "23", "1", "1", "", ""));
            result.Add(CreateTask("33", "32", "Develop unit test plans using product specifications", "04/08/2020 08:00:00", "04/11/2020 17:00:00", "100", "1", "2", "5", ""));
            result.Add(CreateTask("34", "32", "Develop integration test plans using product specifications", "04/08/2020 08:00:00", "04/11/2020 17:00:00", "100", "1", "2", "5", ""));
            result.Add(CreateTask("35", "32", "Unit Testing", "05/07/2020 15:00:00", "05/28/2020 15:00:00", "0", "1", "2", "", ""));
            result.Add(CreateTask("36", "35", "Review modular code", "05/07/2020 15:00:00", "05/14/2020 15:00:00", "0", "1", "3", "5", ""));
            result.Add(CreateTask("37", "35", "Test component modules to product specifications", "05/14/2020 15:00:00", "05/16/2020 15:00:00", "0", "1", "3", "5", ""));
            result.Add(CreateTask("38", "35", "Identify anomalies to product specifications", "05/16/2020 15:00:00", "05/21/2020 15:00:00", "0", "1", "3", "5", ""));
            result.Add(CreateTask("39", "35", "Modify code", "05/21/2020 15:00:00", "05/24/2020 15:00:00", "0", "1", "3", "5", ""));
            result.Add(CreateTask("40", "35", "Re-test modified code", "05/24/2020 15:00:00", "05/28/2020 15:00:00", "0", "1", "3", "5", ""));
            result.Add(CreateTask("41", "35", "Unit testing complete", "05/28/2020 15:00:00", "05/28/2020 15:00:00", "0", "0", "3", "", ""));
            result.Add(CreateTask("42", "32", "Integration Testing", "05/28/2020 15:00:00", "06/13/2020 15:00:00", "0", "1", "2", "", ""));
            result.Add(CreateTask("43", "42", "Test module integration", "05/28/2020 15:00:00", "06/04/2020 15:00:00", "0", "1", "3", "5", ""));
            result.Add(CreateTask("44", "42", "Identify anomalies to specifications", "06/04/2020 15:00:00", "06/06/2020 15:00:00", "0", "1", "3", "5", ""));
            result.Add(CreateTask("45", "42", "Modify code", "06/06/2020 15:00:00", "06/11/2020 15:00:00", "0", "1", "3", "5", ""));
            result.Add(CreateTask("46", "42", "Re-test modified code", "06/11/2020 15:00:00", "06/13/2020 15:00:00", "0", "1", "3", "5", ""));
            result.Add(CreateTask("47", "42", "Integration testing complete", "06/13/2020 15:00:00", "06/13/2020 15:00:00", "0", "0", "3", "", ""));
            result.Add(CreateTask("48", "0", "Training", "04/08/2020 08:00:00", "06/10/2020 15:00:00", "25", "1", "1", "", ""));
            result.Add(CreateTask("49", "48", "Develop training specifications for end users", "04/08/2020 08:00:00", "04/10/2020 17:00:00", "100", "1", "2", "6", ""));
            result.Add(CreateTask("50", "48", "Develop training specifications for helpdesk support staff", "04/08/2020 08:00:00", "04/10/2020 17:00:00", "100", "1", "2", "6", ""));
            result.Add(CreateTask("51", "48", "Identify training delivery methodology (computer based training, classroom, etc.)", "04/08/2020 08:00:00", "04/09/2020 17:00:00", "100", "1", "2", "6", ""));
            result.Add(CreateTask("52", "48", "Develop training materials", "05/07/2020 15:00:00", "05/28/2020 15:00:00", "0", "1", "2", "6", ""));
            result.Add(CreateTask("53", "48", "Conduct training usability study", "05/28/2020 15:00:00", "06/03/2020 15:00:00", "0", "1", "2", "6", ""));
            result.Add(CreateTask("54", "48", "Finalize training materials", "06/03/2020 15:00:00", "06/06/2020 15:00:00", "0", "1", "2", "6", ""));
            result.Add(CreateTask("55", "48", "Develop training delivery mechanism", "06/06/2020 15:00:00", "06/10/2020 15:00:00", "0", "1", "2", "6", ""));
            result.Add(CreateTask("56", "48", "Training materials complete", "06/10/2020 15:00:00", "06/10/2020 15:00:00", "0", "0", "2", "", ""));
            result.Add(CreateTask("57", "0", "Documentation", "04/08/2020 08:00:00", "05/20/2020 12:00:00", "0", "1", "1", "", ""));
            result.Add(CreateTask("58", "57", "Develop Help specification", "04/08/2020 08:00:00", "04/08/2020 17:00:00", "80", "1", "2", "7", ""));
            result.Add(CreateTask("59", "57", "Develop Help system", "04/22/2020 13:00:00", "05/13/2020 12:00:00", "0", "1", "2", "7", ""));
            result.Add(CreateTask("60", "57", "Review Help documentation", "05/13/2020 13:00:00", "05/16/2020 12:00:00", "0", "1", "2", "7", ""));
            result.Add(CreateTask("61", "57", "Incorporate Help documentation feedback", "05/16/2020 13:00:00", "05/20/2020 12:00:00", "0", "1", "2", "7", ""));
            result.Add(CreateTask("62", "57", "Develop user manuals specifications", "04/08/2020 08:00:00", "04/09/2020 17:00:00", "65", "1", "2", "7", ""));
            result.Add(CreateTask("63", "57", "Develop user manuals", "04/22/2020 13:00:00", "05/13/2020 12:00:00", "0", "1", "2", "7", ""));
            result.Add(CreateTask("64", "57", "Review all user documentation", "05/13/2020 13:00:00", "05/15/2020 12:00:00", "0", "1", "2", "7", ""));
            result.Add(CreateTask("65", "57", "Incorporate user documentation feedback", "05/15/2020 13:00:00", "05/17/2020 12:00:00", "0", "1", "2", "7", ""));
            result.Add(CreateTask("66", "57", "Documentation complete", "05/20/2020 12:00:00", "05/20/2020 12:00:00", "0", "0", "2", "", ""));
            result.Add(CreateTask("67", "0", "Pilot", "03/18/2020 13:00:00", "06/24/2020 15:00:00", "22", "1", "1", "", ""));
            result.Add(CreateTask("68", "67", "Identify test group", "03/18/2020 13:00:00", "03/19/2020 12:00:00", "100", "1", "2", "2", ""));
            result.Add(CreateTask("69", "67", "Develop software delivery mechanism", "03/19/2020 13:00:00", "03/20/2020 12:00:00", "100", "1", "2", "", ""));
            result.Add(CreateTask("70", "67", "Install/deploy software", "06/13/2020 15:00:00", "06/14/2020 15:00:00", "0", "1", "2", "8", ""));
            result.Add(CreateTask("71", "67", "Obtain user feedback", "06/14/2020 15:00:00", "06/21/2020 15:00:00", "0", "1", "2", "8", ""));
            result.Add(CreateTask("72", "67", "Evaluate testing information", "06/21/2020 15:00:00", "06/24/2020 15:00:00", "0", "1", "2", "8", ""));
            result.Add(CreateTask("73", "67", "Pilot complete", "06/24/2020 15:00:00", "06/24/2020 15:00:00", "0", "0", "2", "", ""));
            result.Add(CreateTask("74", "0", "Deployment", "06/24/2020 15:00:00", "07/01/2020 15:00:00", "0", "1", "1", "", ""));
            result.Add(CreateTask("75", "74", "Determine final deployment strategy", "06/24/2020 15:00:00", "06/25/2020 15:00:00", "0", "1", "2", "8", ""));
            result.Add(CreateTask("76", "74", "Develop deployment methodology", "06/25/2020 15:00:00", "06/26/2020 15:00:00", "0", "1", "2", "8", ""));
            result.Add(CreateTask("77", "74", "Secure deployment resources", "06/26/2020 15:00:00", "06/27/2020 15:00:00", "0", "1", "2", "8", ""));
            result.Add(CreateTask("78", "74", "Train support staff", "06/27/2020 15:00:00", "06/28/2020 15:00:00", "0", "1", "2", "8", ""));
            result.Add(CreateTask("79", "74", "Deploy software", "06/28/2020 15:00:00", "07/01/2020 15:00:00", "0", "1", "2", "8", ""));
            result.Add(CreateTask("80", "74", "Deployment complete", "07/01/2020 15:00:00", "07/01/2020 15:00:00", "0", "0", "2", "", ""));
            result.Add(CreateTask("81", "0", "Post Implementation Review", "07/01/2020 15:00:00", "07/04/2020 15:00:00", "0", "1", "1", "", ""));
            result.Add(CreateTask("82", "81", "Document lessons learned", "07/01/2020 15:00:00", "07/02/2020 15:00:00", "0", "1", "2", "2", ""));
            result.Add(CreateTask("83", "81", "Distribute to team members", "07/02/2020 15:00:00", "07/03/2020 15:00:00", "0", "1", "2", "2", ""));
            result.Add(CreateTask("84", "81", "Create software maintenance team", "07/03/2020 15:00:00", "07/04/2020 15:00:00", "0", "1", "2", "2", ""));
            result.Add(CreateTask("85", "81", "Post implementation review complete", "07/04/2020 15:00:00", "07/04/2020 15:00:00", "0", "0", "2", "", ""));
            result.Add(CreateTask("86", "0", "Software development template complete", "07/04/2020 15:00:00", "07/04/2020 15:00:00", "0", "0", "1", "", ""));

            return result;
        }
        public static Task CreateTask(string key, string parentkey, string title, string start, string end, string percent, string type, string status, string resources, string description) {
            var task = new Task();
            task.Key = key;
            task.ParentKey = parentkey;
            task.Title = title;
            task.StartDate = DateTime.Parse(start, System.Globalization.CultureInfo.InvariantCulture);
            task.EndDate = DateTime.Parse(end, System.Globalization.CultureInfo.InvariantCulture);
            task.Progress = Convert.ToInt32(percent);
            task.Resources = resources;
            task.Description = description;
            return task;
        }
        static List<Dependency> CreateDependencies() {
            
            var result = new List<Dependency>();
            for (int i = 0; i < Tasks.Count; i++) {
                Task task = Tasks[i];
                if (!string.IsNullOrEmpty(task.ParentKey) && (task.ParentKey !="-1")) {
                    result.Add(new Dependency() {  Key = CreateUniqueId(), PredecessorKey = Tasks[i - 1].Key, SuccessorKey = task.Key });
                }
            }
            return result;
        }
        static List<Resource> CreateResources() {
           
            var result = new List<Resource>();
            result.Add(new Resource() { Key = "1", Name = "Management" });
            result.Add(new Resource() { Key = "2", Name = "Project Manager" });
            result.Add(new Resource() { Key = "3", Name = "Analyst" });
            result.Add(new Resource() { Key = "4", Name = "Developer" });
            result.Add(new Resource() { Key = "5", Name = "Testers" });
            result.Add(new Resource() { Key = "6", Name = "Trainers" });
            result.Add(new Resource() { Key = "7", Name = "Technical Communicators" });
            result.Add(new Resource() { Key = "8", Name = "Deployment Team" });
            return result;
        }
        static List<ResourceAssignment> CreateResourceAssignments() {
            
            var result = new List<ResourceAssignment>();
            foreach (Task task in Tasks) {
                if (!string.IsNullOrEmpty(task.Resources)) {
                    string[] resourcesID = task.Resources.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < resourcesID.Length; i++)
                        result.Add(new ResourceAssignment() {  Key = CreateUniqueId(),  TaskKey = task.Key,  ResourceKey = resourcesID[i] });
                }
            }
            return result;
        }

        public static void UpdateTask(Task task) {
            Task item = Tasks.FirstOrDefault(c => c.Key.Equals(task.Key));
            item.ParentKey = task.ParentKey;
            item.Progress = task.Progress;
            item.StartDate = task.StartDate;
            item.EndDate = task.EndDate;
            item.Title = task.Title;
            item.Description = task.Description;
        }

        public static string InsertTask(Task task) {
            task.Key = CreateUniqueId();
            Tasks.Add(task);
            return task.Key;
        }
        public static void DeleteTask(Task task) {
            var taskToDelete = Tasks.FirstOrDefault(t => t.Key.Equals(task.Key));
            if (taskToDelete != null)
                Tasks.Remove(taskToDelete);
        }
        public static void DeleteTaskByKey(string key) {
            var taskToDelete = Tasks.FirstOrDefault(t => t.Key.Equals(key));
            if (taskToDelete != null)
                Tasks.Remove(taskToDelete);
        }

        public static string InsertDependency(Dependency dependency) {
            dependency.Key = CreateUniqueId();
            Dependencies.Add(dependency);
            return dependency.Key;
        }
        public static void DeleteDependency(Dependency dependency) {
            var dependencyToDelete = Dependencies.FirstOrDefault(t => t.Key.Equals(dependency.Key));
            if (dependencyToDelete != null)
                Dependencies.Remove(dependencyToDelete);
        }
        public static void DeleteDependencyByKey(string key) {
            var dependencyToDelete = Dependencies.FirstOrDefault(t => t.Key.Equals(key));
            if (dependencyToDelete != null)
                Dependencies.Remove(dependencyToDelete);
        }

        public static void UpdateResource(Resource resource) {
            Resource item = Resources.FirstOrDefault(c => c.Key.Equals(resource.Key));
            item.Name = resource.Name;
        }

        public static string InsertResource(Resource resource) {
            resource.Key = CreateUniqueId();
            Resources.Add(resource);
            return resource.Key;
        }
        public static void DeleteResource(Resource resource) {
            var resourceToDelete = Resources.FirstOrDefault(t => t.Key.Equals(resource.Key));
            if (resourceToDelete != null)
                Resources.Remove(resourceToDelete);
        }

        public static void DeleteResourceByKey(string key) {
            var resourceToDelete = Resources.FirstOrDefault(t => t.Key.Equals(key));
            if (resourceToDelete != null)
                Resources.Remove(resourceToDelete);
        }

        public static string InsertResourceAssignment(ResourceAssignment resourceAssignment) {
            resourceAssignment.Key = CreateUniqueId();
            ResourceAssignments.Add(resourceAssignment);
            return resourceAssignment.Key;
        }
        public static void DeleteResourceAssignment(ResourceAssignment resourceAssignment) {
            var itemToDelete = ResourceAssignments.FirstOrDefault(t => t.Key.Equals(resourceAssignment.Key));
            if (itemToDelete != null)
                ResourceAssignments.Remove(itemToDelete);
        }

        public static void DeleteResourceAssignmentByKey(string key) {
            var itemToDelete = ResourceAssignments.FirstOrDefault(t => t.Key.Equals(key));
            if (itemToDelete != null)
                ResourceAssignments.Remove(itemToDelete);
        }

        static string CreateUniqueId() { return Guid.NewGuid().ToString(); }
    }


    public class Task {
        public string Key { get; set; }
        public string ParentKey { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Progress { get; set; }
        public string Resources { get; set; }
    }
    public class Dependency {
        public string Key { get; set; }
        public string PredecessorKey { get; set; }
        public string SuccessorKey { get; set; } 
    }
    public class Resource {
        public string Key { get; set; }
        public string Name { get; set; }
    }
    public class ResourceAssignment {
        public string Key { get; set; }
        public string TaskKey { get; set; }
        public string ResourceKey { get; set; }

    }
}