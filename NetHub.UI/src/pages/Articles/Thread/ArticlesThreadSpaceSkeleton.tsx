import {Skeleton} from '@chakra-ui/react';
import React from 'react';

const ArticlesThreadSpaceSkeleton = () => {

  return (
    <>
      <Skeleton height={160} mb={'15px'}/>
      <Skeleton height={160} mb={'15px'}/>
      <Skeleton height={160} mb={'15px'}/>
      <Skeleton height={160} mb={'15px'}/>
    </>
  );
};

export default ArticlesThreadSpaceSkeleton;
