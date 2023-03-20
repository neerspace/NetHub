import React, {useRef} from 'react';
import ArticleCreateInfo
  from '../../../components/Article/Create/Info/ArticleCreateInfo';
import ArticleCreate from "../../../components/Article/Create/ArticleCreate";
import {useTranslation} from "react-i18next";
import ArticleCreatingSpaceProvider from "./ArticleCreateSpace.Provider";
import {useArticleCreatingSpace} from "./ArticleCreateSpace.functions";
import Dynamic, {IPage} from "../../../components/Dynamic/Dynamic";

type CreateArticleFormRef = React.ElementRef<typeof ArticleCreate>

const ArticleCreateSpace: IPage = () => {
  const {t} = useTranslation();

  const articleCreationRef = useRef<CreateArticleFormRef>(null);
  const {createArticle} = useArticleCreatingSpace(articleCreationRef);

  const titles = {
    // Center: <h2>{t('article.create.mainSettings')}</h2>,
    Center: <h2>Створення статті</h2>,
    Right: <h2>Налаштування</h2>
  };

  return (
    <Dynamic Titles={titles}>
      <ArticleCreate ref={articleCreationRef}/>
      <ArticleCreateInfo createArticle={createArticle.mutateAsync}/>
    </Dynamic>
  );
}

ArticleCreateSpace.Provider = ArticleCreatingSpaceProvider;

export default ArticleCreateSpace;
