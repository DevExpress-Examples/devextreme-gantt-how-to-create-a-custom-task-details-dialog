using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DevExtremeMvcApp1.Models;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Net.Http.Formatting;
using Newtonsoft.Json;

namespace DevExtremeMvcApp1.Controllers {

    public class SampleDataController : ApiController {


        [HttpGet]
        public HttpResponseMessage GetTasks(DataSourceLoadOptions loadOptions) {
            return Request.CreateResponse(DataSourceLoader.Load(GanttDataProvider.Tasks, loadOptions));
        }
        [HttpPut]
        public HttpResponseMessage UpdateTask(FormDataCollection form) {
            var key = form.Get("Key");
            var values = form.Get("values");
            var task = GanttDataProvider.Tasks.First(t => t.Key == key);
            JsonConvert.PopulateObject(values, task);

            GanttDataProvider.UpdateTask(task);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage InsertTask(FormDataCollection form) {
            var values = form.Get("values");
            var newtask = new Task();
            JsonConvert.PopulateObject(values, newtask);
            GanttDataProvider.InsertTask(newtask);
            return Request.CreateResponse(HttpStatusCode.Created, newtask);
        }

        [HttpDelete]
        public void DeleteTask(FormDataCollection form) {
            var key = form.Get("Key");
            var task = GanttDataProvider.Tasks.First(t => t.Key == key);
            GanttDataProvider.DeleteTask(task);
        }

        [HttpGet]
        public HttpResponseMessage GetDependencies(DataSourceLoadOptions loadOptions) {
            return Request.CreateResponse(DataSourceLoader.Load(GanttDataProvider.Dependencies, loadOptions));
        }

        [HttpPost]
        public HttpResponseMessage InsertDependency(FormDataCollection form) {
            var values = form.Get("values");
            var newDependency = new Dependency();
            JsonConvert.PopulateObject(values, newDependency);
            GanttDataProvider.InsertDependency(newDependency);
            return Request.CreateResponse(HttpStatusCode.Created, newDependency);
        }

        [HttpDelete]
        public void DeleteDependency(FormDataCollection form) {
            var key = form.Get("Key");
            var dependency = GanttDataProvider.Dependencies.First(t => Guid.Equals(t.Key ,key));
            GanttDataProvider.DeleteDependency(dependency);
        }

        [HttpGet]
        public HttpResponseMessage GetResources(DataSourceLoadOptions loadOptions) {
            return Request.CreateResponse(DataSourceLoader.Load(GanttDataProvider.Resources, loadOptions));
        }

        [HttpGet]
        public HttpResponseMessage GetResourceAssignments(DataSourceLoadOptions loadOptions) {
            return Request.CreateResponse(DataSourceLoader.Load(GanttDataProvider.ResourceAssignments, loadOptions));
        }

        [HttpPost]
        public HttpResponseMessage InsertResourceAssignment(FormDataCollection form) {
            var values = form.Get("values");
            var newResourceAssignment = new ResourceAssignment();
            JsonConvert.PopulateObject(values, newResourceAssignment);
            GanttDataProvider.InsertResourceAssignment(newResourceAssignment);
            return Request.CreateResponse(HttpStatusCode.Created, newResourceAssignment);
        }

        [HttpDelete]
        public void DeleteResourceAssignment(FormDataCollection form) {
            var key = form.Get("Key");
            var resourceAssignment = GanttDataProvider.ResourceAssignments.First(t => Guid.Equals(t.Key, key));
            GanttDataProvider.DeleteResourceAssignment(resourceAssignment);
        }


        [HttpPost]
        public HttpResponseMessage InsertResource(FormDataCollection form) {
            var values = form.Get("values");
            var newResource = new Resource();
            JsonConvert.PopulateObject(values, newResource);
            GanttDataProvider.InsertResource(newResource);
            return Request.CreateResponse(HttpStatusCode.Created, newResource);
        }

        [HttpDelete]
        public void DeleteResource(FormDataCollection form) {
            var key = form.Get("Key");
            var resource = GanttDataProvider.Resources.First(t => t.Key == key);
            GanttDataProvider.DeleteResource(resource);
        }
    }
}