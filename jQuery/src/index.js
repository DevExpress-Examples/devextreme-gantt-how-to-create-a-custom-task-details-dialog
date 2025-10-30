import {
  tasks, resources, resourceAssignments, dependencies,
} from './data.js';

let ganttInstance;
let popupInstance;
let formInstance;

$(() => {
  // Initialize Gantt
  ganttInstance = $('#gantt')
    .dxGantt({
      tasks: {
        dataSource: tasks,
        keyExpr: 'id',
        parentIdExpr: 'parentId',
        titleExpr: 'title',
        startExpr: 'start',
        endExpr: 'end',
        progressExpr: 'progress',
      },
      dependencies: {
        dataSource: dependencies,
        keyExpr: 'id',
        predecessorIdExpr: 'predecessorId',
        successorIdExpr: 'successorId',
        typeExpr: 'type',
      },
      resources: {
        dataSource: resources,
        keyExpr: 'id',
        textExpr: 'text',
      },
      resourceAssignments: {
        dataSource: resourceAssignments,
        keyExpr: 'id',
        taskIdExpr: 'taskId',
        resourceIdExpr: 'resourceId',
      },
      columns: [
        {
          dataField: 'title',
          caption: 'Title',
          width: 250,
        },
        {
          dataField: 'start',
          caption: 'Start Date',
          width: 100,
        },
        {
          dataField: 'end',
          caption: 'End Date',
          width: 100,
        },
      ],
      toolbar: {
        items: [
          'undo',
          'redo',
          'separator',
          'collapseAll',
          'expandAll',
          'separator',
          'addTask',
          'deleteTask',
          'separator',
          'zoomIn',
          'zoomOut',
        ],
      },
      editing: {
        enabled: true,
      },
      scaleType: 'weeks',
      taskListWidth: 400,
      onTaskEditDialogShowing(e) {
        e.cancel = true;
        showTaskDetails(ganttInstance.getTaskData(e.key));
      },
    })
    .dxGantt('instance');

  // Initialize Popup with custom task details form
  popupInstance = $('#taskDetailsPopup')
    .dxPopup({
      maxWidth: 800,
      maxHeight: 500,
      title: 'Task Details',
      visible: false,
      contentTemplate(container) {
        const formContainer = $('<div>');
        
        formInstance = formContainer
          .dxForm({
            labelLocation: 'top',
            showColonAfterLabel: true,
            colCount: 2,
            items: [
              {
                dataField: 'title',
                label: { text: 'Title' },
                colSpan: 2,
                validationRules: [{ type: 'required' }],
              },
              {
                dataField: 'start',
                label: { text: 'Start Date' },
                editorType: 'dxDateBox',
                editorOptions: {
                  width: '100%',
                  type: 'datetime',
                  onValueChanged(e) {
                    const endEditor = formInstance.getEditor('end');
                    if (endEditor) {
                      endEditor.option('min', e.value);
                    }
                  },
                },
              },
              {
                dataField: 'end',
                label: { text: 'End Date' },
                editorType: 'dxDateBox',
                editorOptions: {
                  width: '100%',
                  type: 'datetime',
                  onValueChanged(e) {
                    const startEditor = formInstance.getEditor('start');
                    if (startEditor) {
                      startEditor.option('max', e.value);
                    }
                  },
                },
              },
              {
                dataField: 'progress',
                label: { text: 'Progress' },
                editorType: 'dxSlider',
                editorOptions: {
                  min: 0,
                  max: 100,
                  width: '100%',
                  label: {
                    visible: true,
                    position: 'top',
                    format(value) {
                      return `${value}%`;
                    },
                  },
                  tooltip: {
                    enabled: true,
                    position: 'top',
                    showMode: 'onHover',
                    format(value) {
                      return `${value}%`;
                    },
                  },
                },
              },
              {
                dataField: 'resources',
                label: { text: 'Resources' },
                editorType: 'dxTagBox',
                editorOptions: {
                  dataSource: resources,
                  width: '100%',
                  valueExpr: 'id',
                  displayExpr: 'text',
                },
              },
              {
                itemType: 'button',
                colSpan: 1,
                horizontalAlignment: 'left',
                buttonOptions: {
                  text: 'Resource Manager',
                  onClick() {
                    ganttInstance.showResourceManagerDialog();
                  },
                },
              },
            ],
          })
          .dxForm('instance');

        container.append(formContainer);
      },
      toolbarItems: [
        {
          widget: 'dxButton',
          location: 'after',
          toolbar: 'bottom',
          options: {
            text: 'Confirm',
            type: 'success',
            onClick: onConfirmClick,
          },
        },
        {
          widget: 'dxButton',
          location: 'after',
          toolbar: 'bottom',
          options: {
            text: 'Cancel',
            onClick: onCancelClick,
          },
        },
      ],
      onShown() {
        const data = formInstance.option('formData');
        if (data && data.id) {
          const assignedResources = ganttInstance.getTaskResources(data.id);
          formInstance.updateData('resources', assignedResources.map((r) => r.id));
        }
      },
    })
    .dxPopup('instance');

  function showTaskDetails(data) {
    popupInstance.show();
    if (formInstance) {
      formInstance.option('formData', data);
    }
  }

  function onConfirmClick() {
    const result = formInstance.validate();
    if (result.isValid) {
      const data = formInstance.option('formData');
      ganttInstance.updateTask(data.id, data);
      ganttInstance.unassignAllResourcesFromTask(data.id);
      if (data.resources && data.resources.length > 0) {
        data.resources.forEach((resourceId) => {
          ganttInstance.assignResourceToTask(resourceId, data.id);
        });
      }
      popupInstance.hide();
    }
  }

  function onCancelClick() {
    popupInstance.hide();
  }
});
