import {
  useCallback,
  useMemo,
  useRef,
  useState,
} from 'react';
import './App.css';
import 'devextreme/dist/css/dx.material.blue.light.compact.css';
import 'devexpress-gantt/dist/dx-gantt.css';
import Gantt, {
  Tasks,
  Dependencies,
  Resources,
  ResourceAssignments,
  Column,
  Toolbar,
  Item,
  Editing,
  type GanttTypes,
} from 'devextreme-react/gantt';
import Popup, { ToolbarItem } from 'devextreme-react/popup';
import Form, {
  Item as FormItem,
  Label,
  RequiredRule,
} from 'devextreme-react/form';
import {
  tasks,
  resources,
  resourceAssignments,
  dependencies,
} from './data';

function App(): JSX.Element {
  const ganttRef = useRef<any>(null);
  const formRef = useRef<any>(null);
  const [popupVisible, setPopupVisible] = useState(false);
  const [currentTaskData, setCurrentTaskData] = useState<any>({});

  const onTaskEditDialogShowing = useCallback(
    (e: GanttTypes.TaskEditDialogShowingEvent) => {
      e.cancel = true;
      const ganttInstance = ganttRef.current?.instance();
      if (ganttInstance) {
        const taskData = ganttInstance.getTaskData(e.key);
        setCurrentTaskData({ ...taskData });
        setPopupVisible(true);
      }
    },
    [],
  );

  const onPopupShown = useCallback(() => {
    const ganttInstance = ganttRef.current?.instance();
    if (ganttInstance && currentTaskData && currentTaskData.id) {
      const assignedResources: { id: number }[] = ganttInstance.getTaskResources(
        currentTaskData.id,
      );
      setCurrentTaskData((prev: typeof currentTaskData) => ({
        ...prev,
        resources: assignedResources.map((r) => r.id),
      }));
    }
  }, [currentTaskData]);

  const onConfirmClick = useCallback(() => {
    const formInstance = formRef.current?.instance();
    const ganttInstance = ganttRef.current?.instance();

    if (formInstance && ganttInstance) {
      const result = formInstance.validate();
      if (result.isValid) {
        const data = formInstance.option('formData');
        ganttInstance.updateTask(data.id, data);
        ganttInstance.unassignAllResourcesFromTask(data.id);
        if (data.resources && data.resources.length > 0) {
          data.resources.forEach((resourceId: number) => {
            ganttInstance.assignResourceToTask(resourceId, data.id);
          });
        }
        setPopupVisible(false);
      }
    }
  }, []);

  const onCancelClick = useCallback(() => {
    setPopupVisible(false);
  }, []);

  const onStartDateChanged = useCallback((e: any) => {
    const formInstance = formRef.current?.instance();
    if (formInstance) {
      const endEditor = formInstance.getEditor('end');
      if (endEditor) {
        endEditor.option('min', e.value);
      }
    }
  }, []);

  const onEndDateChanged = useCallback((e: any) => {
    const formInstance = formRef.current?.instance();
    if (formInstance) {
      const startEditor = formInstance.getEditor('start');
      if (startEditor) {
        startEditor.option('max', e.value);
      }
    }
  }, []);

  const formatLabel = useCallback((value: number) => `${value}%`, []);

  const onShowResourceManagerClick = useCallback(() => {
    const ganttInstance = ganttRef.current?.instance();
    if (ganttInstance) {
      ganttInstance.showResourceManagerDialog();
    }
  }, []);

  const confirmButtonOptions = useMemo(
    () => ({
      text: 'Confirm',
      type: 'success',
      onClick: onConfirmClick,
    }),
    [onConfirmClick],
  );

  const cancelButtonOptions = useMemo(
    () => ({
      text: 'Cancel',
      onClick: onCancelClick,
    }),
    [onCancelClick],
  );

  const startDateEditorOptions = useMemo(
    () => ({
      width: '100%',
      type: 'datetime',
      onValueChanged: onStartDateChanged,
    }),
    [onStartDateChanged],
  );

  const endDateEditorOptions = useMemo(
    () => ({
      width: '100%',
      type: 'datetime',
      onValueChanged: onEndDateChanged,
    }),
    [onEndDateChanged],
  );

  const progressEditorOptions = useMemo(
    () => ({
      min: 0,
      max: 100,
      width: '100%',
      label: {
        visible: true,
        position: 'top',
        format: formatLabel,
      },
      tooltip: {
        enabled: true,
        position: 'top',
        showMode: 'onHover',
        format: formatLabel,
      },
    }),
    [formatLabel],
  );

  const resourcesEditorOptions = useMemo(
    () => ({
      dataSource: resources,
      width: '100%',
      valueExpr: 'id',
      displayExpr: 'text',
    }),
    [],
  );

  const resourceManagerButtonOptions = useMemo(
    () => ({
      text: 'Resource Manager',
      onClick: onShowResourceManagerClick,
    }),
    [onShowResourceManagerClick],
  );

  return (
    <div className="demo-container">
      <Gantt
        ref={ganttRef}
        scaleType="weeks"
        taskListWidth={400}
        onTaskEditDialogShowing={onTaskEditDialogShowing}
      >
        <Tasks
          dataSource={tasks}
          keyExpr="id"
          parentIdExpr="parentId"
          titleExpr="title"
          startExpr="start"
          endExpr="end"
          progressExpr="progress"
        />
        <Dependencies
          dataSource={dependencies}
          keyExpr="id"
          predecessorIdExpr="predecessorId"
          successorIdExpr="successorId"
          typeExpr="type"
        />
        <Resources
          dataSource={resources}
          keyExpr="id"
          textExpr="text"
        />
        <ResourceAssignments
          dataSource={resourceAssignments}
          keyExpr="id"
          taskIdExpr="taskId"
          resourceIdExpr="resourceId"
        />
        <Column dataField="title" caption="Title" width={250} />
        <Column dataField="start" caption="Start Date" width={100} />
        <Column dataField="end" caption="End Date" width={100} />
        <Toolbar>
          <Item name="undo" />
          <Item name="redo" />
          <Item name="separator" />
          <Item name="collapseAll" />
          <Item name="expandAll" />
          <Item name="separator" />
          <Item name="addTask" />
          <Item name="deleteTask" />
          <Item name="separator" />
          <Item name="zoomIn" />
          <Item name="zoomOut" />
        </Toolbar>
        <Editing enabled={true} />
      </Gantt>

      <Popup
        visible={popupVisible}
        onHiding={onCancelClick}
        onShown={onPopupShown}
        maxWidth={800}
        maxHeight={500}
        title="Task Details"
      >
        <ToolbarItem
          widget="dxButton"
          location="after"
          toolbar="bottom"
          options={confirmButtonOptions}
        />
        <ToolbarItem
          widget="dxButton"
          location="after"
          toolbar="bottom"
          options={cancelButtonOptions}
        />
        <Form
          ref={formRef}
          formData={currentTaskData}
          labelLocation="top"
          showColonAfterLabel={true}
          colCount={2}
        >
          <FormItem dataField="title" colSpan={2}>
            <Label text="Title" />
            <RequiredRule />
          </FormItem>
          <FormItem
            dataField="start"
            editorType="dxDateBox"
            editorOptions={startDateEditorOptions}
          >
            <Label text="Start Date" />
          </FormItem>
          <FormItem
            dataField="end"
            editorType="dxDateBox"
            editorOptions={endDateEditorOptions}
          >
            <Label text="End Date" />
          </FormItem>
          <FormItem
            dataField="progress"
            editorType="dxSlider"
            editorOptions={progressEditorOptions}
          >
            <Label text="Progress" />
          </FormItem>
          <FormItem
            dataField="resources"
            editorType="dxTagBox"
            editorOptions={resourcesEditorOptions}
          >
            <Label text="Resources" />
          </FormItem>
          <FormItem
            itemType="button"
            colSpan={1}
            horizontalAlignment="left"
            buttonOptions={resourceManagerButtonOptions}
          />
        </Form>
      </Popup>
    </div>
  );
}

export default App;
