export interface Task {
  id: number;
  parentId: number;
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
    start: new Date('2020-02-21T08:00:00.000Z'),
    end: new Date('2020-07-04T15:00:00.000Z'),
    progress: 31,
  },
  {
    id: 2,
    parentId: 1,
    title: 'Scope',
    start: new Date('2020-02-21T08:00:00.000Z'),
    end: new Date('2020-02-26T12:00:00.000Z'),
    progress: 60,
  },
  {
    id: 3,
    parentId: 2,
    title: 'Determine project scope',
    start: new Date('2020-02-21T08:00:00.000Z'),
    end: new Date('2020-02-21T12:00:00.000Z'),
    progress: 100,
  },
  {
    id: 4,
    parentId: 2,
    title: 'Secure project sponsorship',
    start: new Date('2020-02-21T13:00:00.000Z'),
    end: new Date('2020-02-22T12:00:00.000Z'),
    progress: 100,
  },
  {
    id: 5,
    parentId: 2,
    title: 'Define preliminary resources',
    start: new Date('2020-02-22T13:00:00.000Z'),
    end: new Date('2020-02-25T12:00:00.000Z'),
    progress: 60,
  },
  {
    id: 6,
    parentId: 2,
    title: 'Secure core resources',
    start: new Date('2020-02-25T13:00:00.000Z'),
    end: new Date('2020-02-26T12:00:00.000Z'),
    progress: 0,
  },
  {
    id: 7,
    parentId: 2,
    title: 'Scope complete',
    start: new Date('2020-02-26T12:00:00.000Z'),
    end: new Date('2020-02-26T12:00:00.000Z'),
    progress: 0,
  },
];

export const resources: Resource[] = [
  {
    id: 1,
    text: 'Management',
  },
  {
    id: 2,
    text: 'Project Manager',
  },
  {
    id: 3,
    text: 'Analyst',
  },
  {
    id: 4,
    text: 'Developer',
  },
  {
    id: 5,
    text: 'Tester',
  },
];

export const resourceAssignments: ResourceAssignment[] = [
  {
    id: 1,
    taskId: 3,
    resourceId: 1,
  },
  {
    id: 2,
    taskId: 4,
    resourceId: 1,
  },
  {
    id: 3,
    taskId: 5,
    resourceId: 2,
  },
  {
    id: 4,
    taskId: 6,
    resourceId: 2,
  },
];

export const dependencies: Dependency[] = [
  {
    id: 1,
    predecessorId: 3,
    successorId: 4,
    type: 0,
  },
  {
    id: 2,
    predecessorId: 4,
    successorId: 5,
    type: 0,
  },
  {
    id: 3,
    predecessorId: 5,
    successorId: 6,
    type: 0,
  },
  {
    id: 4,
    predecessorId: 6,
    successorId: 7,
    type: 0,
  },
];
