import { Skeleton } from '@chakra-ui/react';
import React from 'react';
import cl from './Article.module.sass'

const ContributorsSkeleton = () => {
  return (
    <div className={cl.contributorsSkeleton}>
      <Skeleton width={150} height={35}/>
      <Skeleton width={150} height={35}/>
    </div>
  );
};

export default ContributorsSkeleton;
