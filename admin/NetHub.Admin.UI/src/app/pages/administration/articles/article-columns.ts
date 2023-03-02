import { ArticleModel } from '../../../api';
import { deleteButton } from '../../../components/table/buttons';
import {
  formatAsDate,
  formatAsText,
  formatCounter,
  formatLink,
} from '../../../components/table/formatters';
import { ColumnInfo, FilterType } from '../../../components/table/types';
import { ArticlesTableComponent } from './articles-table/articles-table.component';

export function articleColumns(context: ArticlesTableComponent): ColumnInfo[] {
  return [
    {
      actions: [
        {
          button: {
            class: 'details-button',
            text: 'Details',
          },
          onClick: model => {
            context.onLocalizationClick(model);
          },
        },
      ],
    },
    {
      key: 'id',
      title: 'Id',
      sortable: true,
      filter: FilterType.number,
      numberPattern: 'integer',
    },
    {
      key: 'name',
      title: 'Name',
      sortable: true,
      filter: FilterType.optText,
      formatter: formatAsText,
    },
    {
      key: 'created',
      title: 'Created',
      sortable: true,
      hideable: true,
      filter: FilterType.dateRange,
      formatter: formatAsDate,
    },
    {
      key: 'updated',
      title: 'Updated',
      sortable: true,
      hideable: true,
      hidden: true,
      filter: FilterType.dateRange,
      formatter: formatAsDate,
    },
    {
      key: 'published',
      title: 'Published',
      sortable: true,
      hideable: true,
      filter: FilterType.dateRange,
      formatter: formatAsDate,
    },
    {
      key: 'banned',
      title: 'Banned',
      sortable: true,
      hideable: true,
      filter: FilterType.dateRange,
      formatter: formatAsDate,
    },
    {
      key: 'originalArticleLink',
      title: 'Original Article',
      sortable: false,
      hideable: true,
      filter: FilterType.optText,
      formatter: formatLink,
    },
    {
      key: 'rate',
      title: 'Rate',
      sortable: true,
      hideable: true,
      filter: FilterType.optNumber,
      numberPattern: 'integer',
      formatter: formatCounter,
    },
    {
      actions: [
        {
          button: deleteButton(),
          onClick: (model: ArticleModel) => {
            context.fetchDelete(model);
          },
        },
      ],
    },
    // {
    //   key: 'tags',
    //   title: 'Tags',
    //   sortable: false,
    //   filter: FilterType.text,
    //   formatter: value => {
    //     const data = value ?? null;
    //     if (data === null)
    //       return formatAsText(data);
    //
    //     console.log('data', data);
    //
    //     return formatAsText(data.map((t: string) => `#${t}`).join(', ') ?? null)
    //   }
    // }
  ];
}
