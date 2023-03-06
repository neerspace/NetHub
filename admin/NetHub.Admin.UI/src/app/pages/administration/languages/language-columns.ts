import { LanguageModel } from '../../../api';
import { deleteButton, editButton } from '../../../components/table/buttons';
import { emptyStringPlaceholder, formatAsText } from '../../../components/table/formatters';
import { ColumnInfo, FilterType } from '../../../components/table/types';
import { LangsTableComponent } from './langs-table/langs-table.component';

export function languageColumns(context: LangsTableComponent): ColumnInfo[] {
  return [
    {
      key: 'flagUrl',
      title: '',
      sortable: false,
      filter: FilterType.none,
      formatter: val => {
        if (!val) {
          return `<img class="flag-img" src="/assets/default-flag.svg" alt="" />`;
        }

        return `<img class="flag-img" src="${val}" alt="" />`;
      },
    },
    {
      key: 'code',
      title: 'Two Letter ISO Code',
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
    {
      actions: [
        {
          button: editButton(),
          onClick: (model: LanguageModel) => {
            context.showForm(model);
          },
        },
        {
          button: deleteButton(),
          onClick: (model: LanguageModel) => {
            context.fetchDelete(model);
          },
        },
      ],
    },
  ];
}
