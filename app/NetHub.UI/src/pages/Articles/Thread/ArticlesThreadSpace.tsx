import React from 'react';
import { useTranslation } from "react-i18next";
import ArticlesThreadTitle from "../../../components/Article/Thread/ArticlesThreadTitle";
import Currency from "../../../components/Currency/Currency";
import { Text } from "@chakra-ui/react";
import ArticlesThreadSpaceProvider, {
  useArticlesThreadContext
} from "./ArticlesThreadSpace.Provider";
import ArticlesThread from "../../../components/Article/Thread/ArticlesThread";
import Layout, { Page } from "../../../components/Layout/Layout";
import ErrorBlock from '../../../components/Layout/ErrorBlock';
import { ErrorsHandler } from "../../../utils/ErrorsHandler";
import ArticlesShortSkeleton from "../../../components/Article/Shared/ArticlesShortSkeleton";

const ArticlesThreadSpace: Page = () => {
  const {t} = useTranslation();


  const {
    languages,
    articlesLanguage,
    setArticlesLanguage,
    articlesAccessor,
    setArticles
  } = useArticlesThreadContext();

  const titles = {
    Center: <ArticlesThreadTitle
      title={'Стрічка'}
      articlesLanguage={articlesLanguage}
      setArticlesLanguage={setArticlesLanguage}
      options={languages}
    />,
    Right: <Text as={'h2'}>Курс</Text>
  }


  return <Layout Titles={titles}>
    {
      articlesAccessor.isError
        ? <ErrorBlock>{ErrorsHandler.default(articlesAccessor.error.statusCode)}</ErrorBlock>
        : !articlesAccessor.isSuccess
          ? <ArticlesShortSkeleton/>
          : <ArticlesThread
            articles={articlesAccessor.data!}
            setArticles={setArticles}
          />
    }
    <Currency/>
  </Layout>;
}

ArticlesThreadSpace.Provider = ArticlesThreadSpaceProvider;

export default ArticlesThreadSpace;

