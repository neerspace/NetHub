import React, {FC} from 'react';
import classes from './ArticleCreating.module.sass'
import {useArticleCreatingContext} from "../../../pages/Articles/Create/ArticleCreatingSpace.Provider";

const ArticleImagesSettings: FC = () => {

  const {images} = useArticleCreatingContext();

  return (
    <div className={classes.images}>
      {images!.data!.map(src =>
        <img key={src} onClick={() => console.log(src)} src={src} alt={'damaged'}/>
      )}
    </div>
  );
};

export default ArticleImagesSettings;
