import { formatAsText } from '../../../components/table/formatters';
import { ColumnInfo, FilterType } from '../../../components/table/types';

export const languageColumns: ColumnInfo[] = [
  {
    key: 'code',
    title: 'Two Letter Language ISO Code',
    sortable: true,
    filter: FilterType.text,
    filterMaxLength: 2,
  },
  {
    key: 'name',
    title: 'English Name',
    filter: FilterType.optText,
    filterMaxLength: 50,
    formatter: val => formatAsText(val.en),
  },
  {
    key: 'name',
    title: 'Localized Name',
    filter: FilterType.optText,
    filterMaxLength: 50,
    formatter: (val, obj) =>
      val && obj.code ? formatAsText(val[obj.code]) : formatAsText(undefined),
  },
];
