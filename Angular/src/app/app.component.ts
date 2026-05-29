import { Component, ViewChild } from '@angular/core';
import { DxGanttModule, DxPopupModule, DxFormModule, DxGanttComponent, DxPopupComponent, DxFormComponent } from 'devextreme-angular';
import { tasks, resources, resourceAssignments, dependencies, Task, } from './data';

@Component({
  imports: [DxGanttModule, DxPopupModule, DxFormModule],
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  @ViewChild(DxGanttComponent, { static: false })
    gantt!: DxGanttComponent;

  @ViewChild('taskDetailsPopup', { static: false })
    popup!: DxPopupComponent;

  @ViewChild('taskForm', { static: false }) form!: DxFormComponent;

  tasks = tasks;

  resources = resources;

  resourceAssignments = resourceAssignments;

  dependencies = dependencies;

  popupVisible = false;

  currentTaskData: any = {};

  onTaskEditDialogShowing(e: any): void {
    e.cancel = true;
    this.showTaskDetails(this.gantt.instance.getTaskData(e.key));
  }

  showTaskDetails(data: any): void {
    this.currentTaskData = { ...data };
    this.popupVisible = true;
  }

  onPopupShown(): void {
    if (this.currentTaskData?.id) {
      const assignedResources = this.gantt.instance.getTaskResources(
        this.currentTaskData.id,
      );
      this.currentTaskData.resources = assignedResources.map(
        (r: any) => r.id as number,
      );
    }
  }

  onConfirmClick(): void {
    const result = this.form.instance.validate();
    if (result.isValid) {
      const data = this.form.instance.option('formData');
      this.gantt.instance.updateTask(data.id, data);
      this.gantt.instance.unassignAllResourcesFromTask(data.id);
      if (data.resources && data.resources.length > 0) {
        data.resources.forEach((resourceId: number) => {
          this.gantt.instance.assignResourceToTask(resourceId, data.id);
        });
      }
      this.popupVisible = false;
    }
  }

  onCancelClick(): void {
    this.popupVisible = false;
  }

  onStartDateChanged(e: any): void {
    const endEditor = this.form.instance.getEditor('end');
    if (endEditor) {
      endEditor.option('min', e.value);
    }
  }

  onEndDateChanged(e: any): void {
    const startEditor = this.form.instance.getEditor('start');
    if (startEditor) {
      startEditor.option('max', e.value);
    }
  }

  formatLabel(value: number): string {
    return `${value}%`;
  }

  onShowResourceManagerClick(): void {
    this.gantt.instance.showResourceManagerDialog();
  }
}
