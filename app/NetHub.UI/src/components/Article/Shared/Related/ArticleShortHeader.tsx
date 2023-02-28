import React, { FC, useCallback } from 'react';
import cl from "../ArticleShort.module.sass";
import { Text, useColorModeValue } from "@chakra-ui/react";
import { ISimpleLocalization } from "../../../../types/api/ISimpleLocalization";

interface IArticleShortHeaderProps {
  localization: ISimpleLocalization,
  time?: { before?: string, show?: 'default' | 'saved' }
}


const ArticleShortHeader: FC<IArticleShortHeaderProps> = ({localization, time}) => {

  const getTimeAgo = useCallback(() => {

    if ((time?.show ?? 'default') === 'saved')
      return localization.savedDate!.toRelativeCalendar();

    switch (localization.status) {
      case 'Published':
        return localization.published?.toRelativeCalendar();
      case 'Banned':
        return localization.banned!.toRelativeCalendar();
      case 'Draft' || 'Pending':
        return localization.created.toRelativeCalendar();
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