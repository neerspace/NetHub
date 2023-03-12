import React, { useRef } from 'react';
import ArticleSettings from '../../../components/Article/Create/ArticleSettings';
import Layout, { Page } from "../../../components/Layout/Layout";
import CreateArticleForm from "../../../components/Article/Create/CreateArticleForm";
import { useTranslation } from "react-i18next";
import ArticleCreatingSpaceProvider from "./ArticleCreatingSpace.Provider";
import { useArticleCreatingSpace } from "./ArticleCreatingSpace.functions";

type CreateArticleFormRef = React.ElementRef<typeof CreateArticleForm>

const ArticleCreatingSpace: Page = () => {
  const {t} = useTranslation();

  const articleCreationRef = useRef<CreateArticleFormRef>(null);
  const {createArticle} = useArticleCreatingSpace(articleCreationRef);

  const titles = {
    // Center: <h2>{t('article.create.mainSettings')}</h2>,
    Center: <h2>Створення статті</h2>,
    Right: <h2>Налаштування</h2>
  };

  return (
    <Layout Titles={titles}>
      <CreateArticleForm ref={articleCreationRef}/>
      <ArticleSettings createArticle={createArticle.mutateAsync}/>
    </Layout>
  );
}

ArticleCreatingSpace.Provider = ArticleCreatingSpaceProvider;

export default ArticleCreatingSpace;
