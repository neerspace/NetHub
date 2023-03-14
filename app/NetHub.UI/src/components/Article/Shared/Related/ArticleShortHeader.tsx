import React, { FC, useCallback } from 'react';
import cl from "../ArticleShort.module.sass";
import { Text, useColorModeValue } from "@chakra-ui/react";
import { ISimpleArticle } from "../../../../types/api/ISimpleArticle";

interface IArticleShortHeaderProps {
  article: ISimpleArticle,
  time?: { before?: string, show?: 'default' | 'saved' }
}


const ArticleShortHeader: FC<IArticleShortHeaderProps> = ({article, time}) => {

  const getTimeAgo = useCallback(() => {

    if ((time?.show ?? 'default') === 'saved')
      return article.savedDate!.toRelativeCalendar();

    switch (article.status) {
      case 'Published':
        return article.published?.toRelativeCalendar();
      case 'Banned':
        return article.banned!.toRelativeCalendar();
      case 'Draft' || 'Pending':
        return article.created.toRelativeCalendar();
    }
  }, [article, time])

  return (
    <div className={cl.titleTime}>
      <Text
        as={'h2'}
        className={cl.publicTitle}
        color={useColorModeValue('#242D35', '#EFEFEF')}
      >
        {article.title}
      </Text>
      <Text
        as={'p'}
        className={cl.timeAgo}
        color={useColorModeValue('#757575', '#EFEFEF')}
      >
        {time?.before ? `${time.before}: ${getTimeAgo()}` : getTimeAgo()}
      </Text>
    </div>
  );
};

export default ArticleShortHeader;