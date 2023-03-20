import React, { FC } from 'react';
import cl from '../ArticleCreate.module.sass'
import {
  useArticleCreatingContext
} from "../../../../pages/Articles/Create/ArticleCreateSpace.Provider";
import useCustomSnackbar from "../../../../hooks/useCustomSnackbar";

const ArticleImages: FC = () => {

  const {articleSet} = useArticleCreatingContext();
  const {enqueueSuccess, enqueueError} = useCustomSnackbar();

  const onClickHandle = (link: string) => {
    navigator.clipboard.writeText(link)
      .then(() => enqueueSuccess('Посилання скопійовано'))
      .catch(() => enqueueError('Помилка копіювання'))
  }

  return (
    <div className={cl.images}>
      {articleSet!.data!.imagesLinks!.map(src =>
        <img key={src}
             onClick={() => onClickHandle(src)}
             src={src} alt={'damaged'}
             style={{cursor: 'pointer'}}/>
      )}
    </div>
  );
};

export default ArticleImages;
