import { RoleModel } from '../../../api';
import { deleteButton, editButton } from '../../../components/table/buttons';
import { formatAsText } from '../../../components/table/formatters';
import { ColumnInfo, FilterType } from '../../../components/table/types';
import { RolesTableComponent } from './roles-table/roles-table.component';

export function roleColumns(context: RolesTableComponent): ColumnInfo[] {
  return [
    {
      key: 'id',
      title: '#',
      sortable: true,
      filter: FilterType.number,
      filterValueRange: { from: 0 },
      numberPattern: 'integer',
    },
    {
      key: 'name',
      title: 'Role Name',
      sortable: true,
      filter: FilterType.optText,
      filterMaxLength: 50,
      formatter: formatAsText,
    },
    {
      actions: [
        {
          button: editButton('Permissions'),
          onClick: (model: RoleModel | 'create') => {
            context.showForm(model);
          },
        },
        {
          button: deleteButton(),
          onClick: (model: RoleModel | 'create') => {
            context.showForm(model);
          },
        },
      ],
    },
  ];
}
