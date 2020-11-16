using DevExtremeMvcApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevExtremeMvcApp1.Controllers {
    public class InMemoryGanttDataContext {
        const string SessionKey = "3fd387f5-610a-4060-9204-619ef6e6f300";

        public ICollection<Task> Tasks {
            get {
                var session = HttpContext.Current.Session;
                if (session[SessionKey] == null)
                    session[SessionKey] = SampleData.DataGanttTasks;

                return (ICollection<Task>)session[SessionKey];
            }
        }

        
    }

     
}