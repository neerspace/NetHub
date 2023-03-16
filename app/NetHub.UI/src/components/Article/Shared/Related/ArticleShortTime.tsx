import React, { FC } from 'react';
import { ISimpleArticle } from "../../../../types/api/ISimpleArticle";
import { ContentStatus } from "../../../../api/_api";

interface IArticleShortTime {
  article: ISimpleArticle,
  time?: { before?: string, show?: 'default' | 'saved' }
}

const ArticleShortTime: FC<IArticleShortTime> = ({article, time}) => {
  const textBeforeTime = time?.before ? `${time.before}: ` : '';

  if ((time?.show ?? 'default') === 'saved')
    return <>{textBeforeTime}{article.savedDate!.toRelativeCalendar()}</>;

  if (article.status === ContentStatus.Published)
    return <>{textBeforeTime}{article.published?.toRelativeCalendar()}</>;

  if (article.status === ContentStatus.Draft || ContentStatus.Pending)
    return <>{textBeforeTime}{article.created.toRelativeCalendar()}</>;

  if (article.status === ContentStatus.Banned)
    return <>{textBeforeTime}{article.banned!.toRelativeCalendar()}</>;


  return <></>
};

export default ArticleShortTime;