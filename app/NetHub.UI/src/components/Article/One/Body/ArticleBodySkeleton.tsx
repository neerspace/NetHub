import {Box, Skeleton} from '@chakra-ui/react';
import React from 'react';
import cl from './ArticleBody.module.sass';

const ArticleBodySkeleton = () => {
  return (
    <Box className={cl.articleBodySkeleton}>
      <Skeleton height={300}/>
      <Skeleton height={300}/>
    </Box>
  );
};

export default ArticleBodySkeleton;
