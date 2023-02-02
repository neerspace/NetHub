import {ColumnInfo, FilterType} from "../../../components/table/types";
import {
  formatAsDate,
  formatAsText,
  formatCounter,
  formatLink
} from "../../../components/table/formatters";
import {Log} from "oidc-client";
import {getDomain} from "../../../shared/utilities";

export const articleColumns: ColumnInfo[] = [
  {
    key: 'id',
    title: 'Id',
    sortable: true,
    filter: FilterType.number,
    numberPattern: "integer"
  },
  {
    key: 'name',
    title: 'Name',
    sortable: true,
    filter: FilterType.text,
  },
  {
    key: 'created',
    title: 'Created',
    sortable: true,
    filter: FilterType.dateRange,
    formatter: formatAsDate
  },
  {
    key: 'updated',
    title: 'Updated',
    sortable: true,
    filter: FilterType.dateRange,
    formatter: formatAsDate
  },
  {
    key: 'published',
    title: 'Published',
    sortable: true,
    filter: FilterType.dateRange,
    formatter: formatAsDate
  },
  {
    key: 'banned',
    title: 'Banned',
    sortable: true,
    filter: FilterType.dateRange,
    formatter: formatAsDate
  },
  {
    key: 'originalArticleLink',
    title: 'Original Article Link',
    sortable: false,
    filter: FilterType.text,
    formatter: formatLink
  },
  {
    key: 'rate',
    title: 'Rate',
    sortable: true,
    filter: FilterType.number,
    numberPattern: "integer",
    formatter: formatCounter
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