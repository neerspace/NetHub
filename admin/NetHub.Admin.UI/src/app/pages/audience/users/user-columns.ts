import { UserModel } from 'app/api';
import { deleteButton, editButton } from 'neercms/table/buttons';
import { formatAsDate, formatAsText, formatCheckmark } from 'neercms/table/formatters';
import { ColumnInfo, FilterType } from 'neercms/table/types';
import { UsersTableComponent } from './users-table/users-table.component';

export function userColumns(context: UsersTableComponent): ColumnInfo[] {
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
      key: 'userName',
      title: 'UserName',
      sortable: true,
      filter: FilterType.optText,
      filterMaxLength: 50,
      formatter: formatAsText,
    },
    {
      key: 'firstName',
      title: 'First Name',
      sortable: true,
      filter: FilterType.optText,
      filterMaxLength: 50,
      formatter: formatAsText,
    },
    {
      key: 'middleName',
      title: 'Middle Name',
      sortable: true,
      filter: FilterType.optText,
      filterMaxLength: 50,
      formatter: formatAsText,
    },
    {
      key: 'emailConfirmed',
      title: 'EmailConf',
      filter: FilterType.boolDropdown,
      formatter: formatCheckmark,
    },
    {
      key: 'registered',
      title: 'Registration Date',
      sortable: true,
      filter: FilterType.dateRange,
      formatter: formatAsDate,
    },
    {
      actions: [
        {
          button: editButton(),
          onClick: (model: UserModel) => {
            context.showForm(model);
          },
        },
        {
          button: deleteButton(),
          onClick: (model: UserModel) => {
            context.fetchDelete(model);
          },
        },
      ],
    },
  ];
}
