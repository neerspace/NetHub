import { RoleModel } from 'app/api';
import { ColumnInfo, FilterType } from 'neercms/table/types';
import { deleteButton, editButton, formatAsText } from 'neercms/table/utilities';
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
          onClick: (model: RoleModel) => {
            context.showForm(model);
          },
        },
        {
          button: deleteButton(),
          onClick: (model: RoleModel) => {
            context.showForm(model);
          },
        },
      ],
    },
  ];
}
