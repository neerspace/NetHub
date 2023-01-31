import { formatAsDate, formatAsText, formatCheckmark } from '../../../components/table/formatters';
import { ColumnInfo, FilterType } from '../../../components/table/types';

export const userColumns: ColumnInfo[] = [
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
];
