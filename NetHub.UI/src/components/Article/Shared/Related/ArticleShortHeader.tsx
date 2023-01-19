import React, {FC, useCallback, useMemo} from 'react';
import cl from "../ArticleShort.module.sass";
import {Box, Text, useColorModeValue} from "@chakra-ui/react";
import IExtendedArticle from "../../../../types/IExtendedArticle";
import {DateTime} from "luxon";

interface IArticleShortHeaderProps {
  localization: IExtendedArticle,
  time?: { before?: string, show?: 'default' | 'saved' }
}


const ArticleShortHeader: FC<IArticleShortHeaderProps> = ({localization, time}) => {

  const getTimeAgo = useCallback(() => {

    if ((time?.show ?? 'default') === 'saved')
      return DateTime.fromISO(localization.savedDate!).toRelativeCalendar();

    switch (localization.status) {
      case 'Published':
        return DateTime.fromISO(localization.published!).toRelativeCalendar();
      case 'Banned':
        return DateTime.fromISO(localization.banned!).toRelativeCalendar();
      case 'Draft' || 'Pending':
        return DateTime.fromISO(localization.created).toRelativeCalendar();
    }
  }, [localization, time])

  return (
    <div className={cl.titleTime}>
      <Text
        as={'h2'}
        className={cl.publicTitle}
        color={useColorModeValue('#242D35', '#EFEFEF')}
      >
        {localization.title}
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