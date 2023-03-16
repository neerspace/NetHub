import React, { FC } from 'react';
import { ContentStatus } from "../../../../api/_api";
import { Badge } from "@chakra-ui/react";

interface IArticleShortBadgeProps {
  articleStatus: ContentStatus
}

const ArticleShortBadge: FC<IArticleShortBadgeProps> = ({articleStatus}) => {


  if (articleStatus === ContentStatus.Draft)
    return <Badge ml={2} variant='outline' colorScheme='yellow'>
      Draft
    </Badge>;

  if (articleStatus === ContentStatus.Pending)
    return <Badge ml={2} variant='outline' colorScheme={'orange'}>
      Pending
    </Badge>

  if (articleStatus === ContentStatus.Banned)
    return <Badge ml={2} variant={'outline'} colorScheme={'red'}>
      Banned
    </Badge>

  return <></>;
};

export default ArticleShortBadge;