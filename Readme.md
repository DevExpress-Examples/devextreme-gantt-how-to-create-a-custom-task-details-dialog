<!-- default badges list -->
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T949655)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# Gantt for DevExtreme ASP.NET MVC - How to implement a custom "Task details" dialog

This example demonstrates how display a custom "Task details" dialog instead of the default dialog. 

## Implementation Details

1. Add a popup edit form in your application.
   
	```csharp
	@(Html.DevExtreme().Popup()
	    .ID("taskDetailsPopup").MaxWidth(800).MaxHeight(500).Title("Task Details")
	    .ContentTemplate(new TemplateName("customPopupContentTemplate"))
	    .ToolbarItems(items => {
	        items.Add()
	            .Widget(editor => editor.Button()
	                .Text("Confirm")
	                .Type(ButtonType.Success)
	                .OnClick("onConfirmClick")
	            )
	            .Location(ToolbarItemLocation.After)
	            .Toolbar(Toolbar.Bottom);
	        items.Add()
	            .Widget(editor => editor.Button()
	                .Text("Cancel")
	                .Type(ButtonType.Success)
	                .OnClick("onCancelClick")
	            )
	            .Location(ToolbarItemLocation.After)
	            .Toolbar(Toolbar.Bottom);
	    })
	    .OnInitialized("onPopupInitialized").OnShown("onShown")
	)
 	```
	```jscript
    function onPopupInitialized(e) {
        popup = e.component;
    }
 	```

2. Handle the [taskEditDialogShowing](https://js.devexpress.com/jQuery/Documentation/ApiReference/UI_Components/dxGantt/Events/#taskEditDialogShowing) event to prevent the default dialog and display your custom dialog instead. Bind the form in the popup control to processed task data.

	```jscript
    function onTaskEditDialogShowing(e) {
        e.cancel = true;
        showTaskDetails(gantt.getTaskData(e.key))
    }
    function showTaskDetails(data) {
        popup.option("visible", true);
        if (form)
            form.option('formData', data);
    }
	```

3.  Call the [updateTask](https://js.devexpress.com/jQuery/Documentation/ApiReference/UI_Components/dxGantt/Methods/#updateTaskkey_data) and [assignResourceToTask](https://js.devexpress.com/jQuery/Documentation/ApiReference/UI_Components/dxGantt/Methods/#assignResourceToTaskresourceKey_taskKey)/[unassignResourceFromTask](https://js.devexpress.com/jQuery/Documentation/ApiReference/UI_Components/dxGantt/Methods/#unassignResourceFromTaskresourceKey_taskKey) methods to update Gantt data.

	```jscript
 	function onConfirmClick(e) {
        let result = form.validate();
        if (result.isValid) {
            var data = form.option("formData");
            gantt.updateTask(data.Key, data);
            gantt.unassignAllResourcesFromTask(data.Key);
            data.Resources.forEach(r => gantt.assignResourceToTask(r, data.Key));
            popup.hide();
        }
    }
 	```

## Files to Review

* [Index.cshtml](./CS/DevExtremeMvcApp1/Views/Home/Index.cshtml)
* [SampleDataController.cs](./CS/DevExtremeMvcApp1/Controllers/HomeController.cs)
* [SampleDataController.cs](./CS/DevExtremeMvcApp1/Controllers/SampleDataController.cs)
* [GanttDataProvider.cs](./CS/DevExtremeMvcApp1/Models/GanttDataProvider.cs)
