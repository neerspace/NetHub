import { formatAsText } from '../../../components/table/formatters';
import { ColumnInfo, FilterType } from '../../../components/table/types';

export const roleColumns: ColumnInfo[] = [
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
];
