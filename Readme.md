
## Gantt - How to implement a custom "Task details" dialog

This example demonstrates how to cancel the default dialog showing and create a custom dialog using [Popup MVC Wrapper](https://js.devexpress.com/Documentation/ApiReference/UI_Widgets/dxPopup/). 
1. Handle the [taskEditDialogShowing](https://js.devexpress.com/Documentation/ApiReference/UI_Widgets/dxGantt/Events/#taskEditDialogShowing) event and cancel the default dialog showing:
   function onTaskEditDialogShowing(e) {
	   	e.cancel = true; 
		... }

2. Create an edit form using Popup. Display it and bind it to an edited task data.
 
        function showTaskDetails(data) { 
			popup.option("visible", true);
        	if (form)
           	 form.option('formData', data);  } 
3.  Use the [updateTask](https://js.devexpress.com/Documentation/ApiReference/UI_Widgets/dxGantt/Methods/#updateTaskkey_data) and [assignResourceToTask](https://js.devexpress.com/Documentation/ApiReference/UI_Widgets/dxGantt/Methods/#assignResourceToTaskresourceKey_taskKey)/[unassignResourceFromTask](https://js.devexpress.com/Documentation/ApiReference/UI_Widgets/dxGantt/Methods/#unassignResourceFromTaskresourceKey_taskKey) method to update Gantt data. 