import React, { FC } from 'react';
import classes from './ArticleCreating.module.sass'
import {
  useArticleCreatingContext
} from "../../../pages/Articles/Create/ArticleCreatingSpace.Provider";
import useCustomSnackbar from "../../../hooks/useCustomSnackbar";

const ArticleImagesSettings: FC = () => {

  const {images} = useArticleCreatingContext();
  const {enqueueSuccess, enqueueError} = useCustomSnackbar();

  const onClickHandle = (link: string) => {
    navigator.clipboard.writeText(link)
      .then(() => enqueueSuccess('Посилання скопійовано'))
      .catch(() => enqueueError('Помилка копіювання'))
  }

  return (
    <div className={classes.images}>
      {images!.data!.map(src =>
        <img key={src}
             onClick={() => onClickHandle(src)}
             src={src} alt={'damaged'}
             style={{cursor: 'pointer'}}/>
      )}
    </div>
  );
};

export default ArticleImagesSettings;
