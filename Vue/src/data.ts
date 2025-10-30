export interface Task {
  id: number;
  parentId?: number;
  title: string;
  start: Date;
  end: Date;
  progress: number;
}

export interface Resource {
  id: number;
  text: string;
}

export interface ResourceAssignment {
  id: number;
  taskId: number;
  resourceId: number;
}

export interface Dependency {
  id: number;
  predecessorId: number;
  successorId: number;
  type: number;
}

export const tasks: Task[] = [
  {
    id: 1,
    parentId: 0,
    title: 'Software Development',
    start: new Date('2019-02-21T05:00:00.000Z'),
    end: new Date('2019-07-04T12:00:00.000Z'),
    progress: 31,
  },
  {
    id: 2,
    parentId: 1,
    title: 'Scope',
    start: new Date('2019-02-21T05:00:00.000Z'),
    end: new Date('2019-02-26T09:00:00.000Z'),
    progress: 60,
  },
  {
    id: 3,
    parentId: 2,
    title: 'Determine project scope',
    start: new Date('2019-02-21T05:00:00.000Z'),
    end: new Date('2019-02-21T09:00:00.000Z'),
    progress: 100,
  },
  {
    id: 4,
    parentId: 2,
    title: 'Secure project sponsorship',
    start: new Date('2019-02-21T10:00:00.000Z'),
    end: new Date('2019-02-22T09:00:00.000Z'),
    progress: 100,
  },
  {
    id: 5,
    parentId: 2,
    title: 'Define preliminary resources',
    start: new Date('2019-02-22T10:00:00.000Z'),
    end: new Date('2019-02-25T09:00:00.000Z'),
    progress: 60,
  },
  {
    id: 6,
    parentId: 2,
    title: 'Secure core resources',
    start: new Date('2019-02-25T10:00:00.000Z'),
    end: new Date('2019-02-26T09:00:00.000Z'),
    progress: 0,
  },
  {
    id: 7,
    parentId: 2,
    title: 'Scope complete',
    start: new Date('2019-02-26T09:00:00.000Z'),
    end: new Date('2019-02-26T09:00:00.000Z'),
    progress: 0,
  },
];

export const resources: Resource[] = [
  { id: 1, text: 'Management' },
  { id: 2, text: 'Project Manager' },
  { id: 3, text: 'Analyst' },
  { id: 4, text: 'Developer' },
  { id: 5, text: 'Testers' },
  { id: 6, text: 'Trainers' },
  { id: 7, text: 'Technical Communicators' },
  { id: 8, text: 'Deployment Team' },
];

export const resourceAssignments: ResourceAssignment[] = [
  { id: 0, taskId: 3, resourceId: 1 },
  { id: 1, taskId: 4, resourceId: 1 },
  { id: 2, taskId: 5, resourceId: 2 },
  { id: 3, taskId: 6, resourceId: 2 },
];

export const dependencies: Dependency[] = [
  { id: 0, predecessorId: 3, successorId: 4, type: 0 },
  { id: 1, predecessorId: 4, successorId: 5, type: 0 },
  { id: 2, predecessorId: 5, successorId: 6, type: 0 },
  { id: 3, predecessorId: 6, successorId: 7, type: 0 },
];
