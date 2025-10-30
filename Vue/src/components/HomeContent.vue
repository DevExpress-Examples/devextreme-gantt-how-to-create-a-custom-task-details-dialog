<script setup lang="ts">
import { ref } from 'vue';

import 'devextreme/dist/css/dx.material.blue.light.compact.css';
import 'devexpress-gantt/dist/dx-gantt.css';

// Import editors for the form
import 'devextreme-vue/date-box';
import 'devextreme-vue/slider';
import 'devextreme-vue/tag-box';

import DxGantt, {
  DxTasks,
  DxDependencies,
  DxResources,
  DxResourceAssignments,
  DxColumn,
  DxEditing,
} from 'devextreme-vue/gantt';
import DxPopup from 'devextreme-vue/popup';
import { DxForm } from 'devextreme-vue/form';
import type Gantt from 'devextreme/ui/gantt';
import type Popup from 'devextreme/ui/popup';
import type Form from 'devextreme/ui/form';
import type { SimpleItem } from 'devextreme/ui/form';

import { tasks, dependencies, resources, resourceAssignments } from '../data';

const ganttRef = ref<any>();
const popupRef = ref<any>();
const formRef = ref<any>();

const tasksData = ref(tasks);
const dependenciesData = ref(dependencies);
const resourcesData = ref(resources);
const resourceAssignmentsData = ref(resourceAssignments);

const formItems: SimpleItem[] = [
  {
    dataField: 'title',
    label: { text: 'Title' },
    validationRules: [{ type: 'required', message: 'Title is required' }],
  },
  {
    dataField: 'start',
    editorType: 'dxDateBox' as any,
    label: { text: 'Start Date' },
    editorOptions: {
      type: 'datetime',
      onValueChanged: (e: any) => {
        const startDate = e.value;
        const formInstance = formRef.value?.instance;
        const endDate = formInstance?.option('formData').end;
        if (startDate && endDate && startDate > endDate) {
          formInstance?.getEditor('end')?.option('value', startDate);
        }
      },
    },
  },
  {
    dataField: 'end',
    editorType: 'dxDateBox' as any,
    label: { text: 'End Date' },
    editorOptions: {
      type: 'datetime',
      onValueChanged: (e: any) => {
        const endDate = e.value;
        const formInstance = formRef.value?.instance;
        const startDate = formInstance?.option('formData').start;
        if (startDate && endDate && endDate < startDate) {
          formInstance?.getEditor('start')?.option('value', endDate);
        }
      },
    },
  },
  {
    dataField: 'progress',
    editorType: 'dxSlider' as any,
    label: { text: 'Progress' },
    editorOptions: {
      min: 0,
      max: 100,
      tooltip: {
        enabled: true,
        format: (value: number) => `${value}%`,
        showMode: 'always',
        position: 'bottom',
      },
    },
  },
  {
    dataField: 'resourceIds',
    editorType: 'dxTagBox' as any,
    label: { text: 'Resources' },
    editorOptions: {
      items: resources,
      displayExpr: 'text',
      valueExpr: 'id',
      showSelectionControls: true,
      applyValueMode: 'useButtons',
    },
  },
];

function onTaskEditDialogShowing(e: any) {
  e.cancel = true;
  const ganttInstance = ganttRef.value?.instance;
  const taskData = ganttInstance?.getTaskData(e.key);
  if (!taskData) {
    return;
  }

  const taskResources = ganttInstance?.getTaskResources(e.key);
  const resourceIds = taskResources?.map((r: any) => r.id) || [];

  popupRef.value?.instance.show();
  formRef.value?.instance.option('formData', {
    ...taskData,
    resourceIds,
  });
}

function onConfirmClick() {
  const formInstance = formRef.value?.instance;
  const validationResult = formInstance?.validate();
  if (!validationResult?.isValid) {
    return;
  }

  const formData = formInstance?.option('formData');
  if (!formData) {
    return;
  }

  const { resourceIds, ...taskData } = formData;

  const ganttInstance = ganttRef.value?.instance;
  ganttInstance?.updateTask(taskData.id, taskData);

  const currentResourceIds =
    ganttInstance?.getTaskResources(taskData.id)?.map((r: any) => r.id) || [];

  const resourcesToRemove = currentResourceIds.filter(
    (id: number) => !resourceIds.includes(id)
  );
  resourcesToRemove.forEach((resourceId: number) => {
    ganttInstance?.unassignResourceFromTask(resourceId, taskData.id);
  });

  const resourcesToAdd = resourceIds.filter((id: number) => !currentResourceIds.includes(id));
  resourcesToAdd.forEach((resourceId: number) => {
    ganttInstance?.assignResourceToTask(resourceId, taskData.id);
  });

  popupRef.value?.instance.hide();
}

function onCancelClick() {
  popupRef.value?.instance.hide();
}

</script>
<template>
  <div>
    <DxGantt
      ref="ganttRef"
      height="700px"
      @task-edit-dialog-showing="onTaskEditDialogShowing"
    >
      <DxTasks :data-source="tasksData"/>
      <DxDependencies :data-source="dependenciesData"/>
      <DxResources :data-source="resourcesData"/>
      <DxResourceAssignments :data-source="resourceAssignmentsData"/>
      <DxColumn
        data-field="title"
        caption="Subject"
        :width="300"
      />
      <DxColumn
        data-field="start"
        caption="Start Date"
      />
      <DxColumn
        data-field="end"
        caption="End Date"
      />
      <DxEditing :enabled="true"/>
    </DxGantt>

    <DxPopup
      ref="popupRef"
      title="Task Details"
      :width="400"
      :height="450"
      :show-title="true"
      :drag-enabled="false"
      :visible="false"
      container="body"
    >
      <DxForm
        ref="formRef"
        :items="formItems"
      />
      <div class="popup-buttons">
        <button
          class="btn btn-primary"
          @click="onConfirmClick"
        >
          OK
        </button>
        <button
          class="btn btn-secondary"
          @click="onCancelClick"
        >
          Cancel
        </button>
      </div>
    </DxPopup>
  </div>
</template>

<style scoped>
.popup-buttons {
  display: flex;
  justify-content: flex-end;
  gap: 10px;
  margin-top: 20px;
  padding: 10px 0;
}

.btn {
  padding: 8px 20px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-size: 14px;
}

.btn-primary {
  background-color: #337ab7;
  color: white;
}

.btn-primary:hover {
  background-color: #286090;
}

.btn-secondary {
  background-color: #6c757d;
  color: white;
}

.btn-secondary:hover {
  background-color: #5a6268;
}
</style>
