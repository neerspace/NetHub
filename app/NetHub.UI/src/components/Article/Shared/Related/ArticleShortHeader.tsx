import React, { FC } from 'react';
import cl from "../ArticleShared.module.sass";
import { Box, Text, useColorModeValue } from "@chakra-ui/react";
import { ISimpleArticle } from "../../../../types/api/ISimpleArticle";
import ArticleShortBadge from "./ArticleShortBadge";
import ArticleShortTime from "./ArticleShortTime";

interface IArticleShortHeaderProps {
  article: ISimpleArticle,
  time?: { before?: string, show?: 'default' | 'saved' }
  variant?: 'public' | 'private'
}


const ArticleShortHeader: FC<IArticleShortHeaderProps> = ({article, time, variant}) => {
  return (
    <Box className={cl.titleTime}>
      <Box display={'flex'} alignItems={'center'}>
        <Text
          as={'h2'}
          className={cl.publicTitle}
          color={useColorModeValue('#242D35', '#EFEFEF')}
        >
          {article.title}
        </Text>
        {
          (variant ?? 'public') === 'private'
            ? <ArticleShortBadge articleStatus={article.status}/>
            : <></>
        }
      </Box>
      <Text
        as={'p'}
        className={cl.timeAgo}
        color={useColorModeValue('#757575', '#EFEFEF')}
      >
        <ArticleShortTime article={article} time={time}/>
      </Text>
    </Box>
  );
};

export default ArticleShortHeader;