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
    colSpan: 2,
    validationRules: [{ type: 'required', message: 'Title is required' }],
  },
  {
    dataField: 'start',
    editorType: 'dxDateBox' as any,
    label: { text: 'Start Date' },
    editorOptions: {
      type: 'datetime',
      width: '100%',
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
      width: '100%',
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
      width: '100%',
      label: {
        visible: true,
        position: 'top',
        format: (value: number) => `${value}%`,
      },
      tooltip: {
        enabled: true,
        position: 'top',
        showMode: 'onHover',
        format: (value: number) => `${value}%`,
      },
    },
  },
  {
    dataField: 'resourceIds',
    editorType: 'dxTagBox' as any,
    label: { text: 'Resources' },
    editorOptions: {
      dataSource: resources,
      width: '100%',
      displayExpr: 'text',
      valueExpr: 'id',
    },
  },
  {
    itemType: 'button',
    colSpan: 1,
    horizontalAlignment: 'left',
    buttonOptions: {
      text: 'Resource Manager',
      onClick: () => {
        const ganttInstance = ganttRef.value?.instance;
        ganttInstance?.showResourceManagerDialog();
      },
    },
  } as any,
];

function onTaskEditDialogShowing(e: any) {
  e.cancel = true;

  const ganttInstance = ganttRef.value?.instance;
  if (!ganttInstance) {
    return;
  }

  const taskData = ganttInstance.getTaskData(e.key);
  if (!taskData) {
    return;
  }

  const taskResources = ganttInstance.getTaskResources(e.key);
  const resourceIds = taskResources?.map((r: any) => r.id) || [];

  const popupInstance = popupRef.value?.instance;
  if (popupInstance) {
    popupInstance.show();
  }

  const formInstance = formRef.value?.instance;
  if (formInstance) {
    formInstance.option('formData', {
      ...taskData,
      resourceIds,
    });
  }
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
      scale-type="weeks"
      :task-list-width="400"
      :on-task-edit-dialog-showing="onTaskEditDialogShowing"
    >
      <DxTasks
        :data-source="tasksData"
        key-expr="id"
        parent-id-expr="parentId"
        title-expr="title"
        start-expr="start"
        end-expr="end"
        progress-expr="progress"
      />
      <DxDependencies
        :data-source="dependenciesData"
        key-expr="id"
        predecessor-id-expr="predecessorId"
        successor-id-expr="successorId"
        type-expr="type"
      />
      <DxResources
        :data-source="resourcesData"
        key-expr="id"
        text-expr="text"
      />
      <DxResourceAssignments
        :data-source="resourceAssignmentsData"
        key-expr="id"
        task-id-expr="taskId"
        resource-id-expr="resourceId"
      />
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
      :max-width="800"
      :max-height="500"
      :show-title="true"
      :drag-enabled="false"
      :visible="false"
      container="body"
    >
      <DxForm
        ref="formRef"
        :items="formItems"
        :col-count="2"
        label-location="top"
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
