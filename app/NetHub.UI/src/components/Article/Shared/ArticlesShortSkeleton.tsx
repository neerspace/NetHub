import {Skeleton} from '@chakra-ui/react';
import React from 'react';

const ArticlesShortSkeleton = () => {
  return (
    <>
      <Skeleton height={150}/>
      <Skeleton height={150} mt={'15px'}/>
      <Skeleton height={150} mt={'15px'}/>
      <Skeleton height={150} mt={'15px'}/>
    </>
  );
};

export default ArticlesShortSkeleton;
