using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevExtremeMvcApp1.Models {
    public partial class SampleData {
        public static List<Task> DataGanttTasks {
            get { return CreateTasks(); }
        }


        public static Task CreateTask(string key, string parentkey, string title, string start, string end, string percent, string resources) {
            var task = new Task();
            task.Key = key;
            task.ParentKey = parentkey;
            task.Title = title;
            task.StartDate = DateTime.Parse(start, System.Globalization.CultureInfo.InvariantCulture);
            task.EndDate = DateTime.Parse(end, System.Globalization.CultureInfo.InvariantCulture);
            task.Progress = Convert.ToInt32(percent);
            task.Resources = resources;
            return task;
        }

        public static List<Task> CreateTasks() {
            var result = new List<Task>();

            result.Add(CreateTask("6", "", "Software Development", "02/21/2020 08:00:00", "07/04/2020 15:00:00", "31", ""));
            result.Add(CreateTask("1", "0", "Scope", "02/21/2020 08:00:00", "02/26/2020 12:00:00", "60", ""));
            result.Add(CreateTask("2", "1", "Determine project scope", "02/21/2020 08:00:00", "02/21/2020 12:00:00", "100", "1"));
            result.Add(CreateTask("3", "1", "Secure project sponsorship", "02/21/2020 13:00:00", "02/22/2020 12:00:00", "100", "1"));
            result.Add(CreateTask("4", "1", "Define preliminary resources", "02/22/2020 13:00:00", "02/25/2020 12:00:00", "60", "2"));
            result.Add(CreateTask("5", "1", "Secure core resources", "02/25/2020 13:00:00", "02/26/2020 12:00:00", "0", "2"));
 
            return result;
        }
    }
}
