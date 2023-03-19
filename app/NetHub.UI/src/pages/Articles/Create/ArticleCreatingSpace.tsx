import React, { useRef } from 'react';
import ArticleSettings from '../../../components/Article/Create/ArticleSettings';
import CreateArticleForm from "../../../components/Article/Create/CreateArticleForm";
import { useTranslation } from "react-i18next";
import ArticleCreatingSpaceProvider from "./ArticleCreatingSpace.Provider";
import { useArticleCreatingSpace } from "./ArticleCreatingSpace.functions";
import Dynamic, { IPage } from "../../../components/Layout/Dynamic";

type CreateArticleFormRef = React.ElementRef<typeof CreateArticleForm>

const ArticleCreatingSpace: IPage = () => {
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
      <CreateArticleForm ref={articleCreationRef}/>
      <ArticleSettings createArticle={createArticle.mutateAsync}/>
    </Dynamic>
  );
}

ArticleCreatingSpace.Provider = ArticleCreatingSpaceProvider;

export default ArticleCreatingSpace;
