import React from 'react';
import ArticlesList from "../../../components/Article/Thread/ArticlesList";
import Currency from "../../../components/Currency/Currency";
import { Skeleton, Text } from "@chakra-ui/react";
import ArticlesListTitle from "../../../components/Article/Thread/ArticlesListTitle";
import CreatorArticlesSpaceProvider, {
  useContributorArticlesContext
} from "./CreatorArticlesSpace.Provider";
import ErrorBlock from "../../../components/UI/Error/ErrorBlock";
import { ErrorsHandler } from "../../../utils/ErrorsHandler";
import ArticlesShortSkeleton from "../../../components/Article/Shared/ArticlesShortSkeleton";
import Dynamic, { IPage } from "../../../components/Dynamic/Dynamic";

const CreatorArticlesSpace: IPage = () => {
  const {
    languages,
    articlesLanguage,
    setArticlesLanguage,
    contributorAccessor,
    contributorArticlesAccessor,
    setContributorArticles
  } = useContributorArticlesContext()


  const title = contributorAccessor.data?.userName
    ? `Статті, написані ${contributorAccessor.data.userName}`
    : <Skeleton height={'auto'}>height</Skeleton>

  const titles = {
    Center: <ArticlesListTitle
      title={title}
      articlesLanguage={articlesLanguage}
      setArticlesLanguage={setArticlesLanguage}
      options={languages}
    />,
    Right: <Text as={'h2'}>Курс</Text>
  }

  return <Dynamic Titles={titles}>
    {
      contributorArticlesAccessor.isError
        ?
        <ErrorBlock>{ErrorsHandler.default(contributorArticlesAccessor.error.statusCode)}</ErrorBlock>
        : !contributorArticlesAccessor.isSuccess
          ? <ArticlesShortSkeleton/>
          : <ArticlesList
            articles={contributorArticlesAccessor.data!}
            setArticles={setContributorArticles}
            byUser={true}
          />
    }
    <Currency/>
  </Dynamic>
}

CreatorArticlesSpace.Provider = CreatorArticlesSpaceProvider;

export default CreatorArticlesSpace;
