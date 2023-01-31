import {Skeleton} from '@chakra-ui/react';
import React from 'react';
import cl from './ArticleBody.module.sass';

const ArticleBodySkeleton = () => {
  return (
    <div className={cl.articleBodySkeleton}>
      <Skeleton height={300}/>
      <Skeleton height={300}/>
    </div>
  );
};

export default ArticleBodySkeleton;
