import { PrimeIcons } from 'primeng/api';

export enum TableActionsEnum {
  EDIT = 'edit',
  DELETE = 'delete',
  VIEW = 'view',
  NOTIFY = 'notify',
  ACTIVATE = 'activate',
  DEACTIVATE = 'deactivate',
  DUPLICATE = 'duplicate',
  APPROVE = 'approve',
  REJECT = 'reject',
}

export type TableActions = {
  action: TableActionsEnum;
  label: string;
  icon?: string;
  severity?:
    | 'primary'
    | 'secondary'
    | 'success'
    | 'info'
    | 'warn'
    | 'danger'
    | 'help'
    | 'contrast';
  onClick: (row: any) => void;
};
