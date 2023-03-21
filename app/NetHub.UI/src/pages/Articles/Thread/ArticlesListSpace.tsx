import React from 'react';
import { useTranslation } from "react-i18next";
import ArticlesListTitle from "../../../components/Article/Thread/ArticlesListTitle";
import Currency from "../../../components/Currency/Currency";
import { Box, Text } from "@chakra-ui/react";
import ArticlesListSpaceProvider, {
  useArticlesThreadContext
} from "./ArticlesListSpace.Provider";
import ArticlesList from "../../../components/Article/Thread/ArticlesList";
import ErrorBlock from '../../../components/UI/Error/ErrorBlock';
import { ErrorsHandler } from "../../../utils/ErrorsHandler";
import ArticlesShortSkeleton from "../../../components/Article/Shared/ArticlesShortSkeleton";
import Dynamic, { IPage } from "../../../components/Dynamic/Dynamic";
import Feedback from "../../../components/Feedback/Feedback";

const ArticlesListSpace: IPage = () => {
  const {t} = useTranslation();


  const {
    languages,
    articlesLanguage,
    setArticlesLanguage,
    articlesAccessor,
    setArticles
  } = useArticlesThreadContext();

  const titles = {
    Center: <ArticlesListTitle
      title={'Стрічка'}
      articlesLanguage={articlesLanguage}
      setArticlesLanguage={setArticlesLanguage}
      options={languages}
    />,
    Right: <Text as={'h2'}>Курс</Text>
  }


  return <Dynamic Titles={titles}>
    {
      articlesAccessor.isError
        ? <ErrorBlock>{ErrorsHandler.default(articlesAccessor.error.statusCode)}</ErrorBlock>
        : !articlesAccessor.isSuccess
          ? <ArticlesShortSkeleton/>
          : <ArticlesList
            articles={articlesAccessor.data!}
            setArticles={setArticles}
          />
    }
    <Box display={'flex'} flexDirection={'column'}>
      <Currency/>
      <Feedback/>
    </Box>
  </Dynamic>;
}

ArticlesListSpace.Provider = ArticlesListSpaceProvider;

export default ArticlesListSpace;

