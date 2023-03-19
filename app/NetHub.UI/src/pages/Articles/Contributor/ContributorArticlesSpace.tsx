import React from 'react';
import ArticlesThread from "../../../components/Article/Thread/ArticlesThread";
import Currency from "../../../components/Currency/Currency";
import { Skeleton, Text } from "@chakra-ui/react";
import ArticlesThreadTitle from "../../../components/Article/Thread/ArticlesThreadTitle";
import ContributorArticlesSpaceProvider, {
  useContributorArticlesContext
} from "./ContributorArticlesSpace.Provider";
import ErrorBlock from "../../../components/Layout/ErrorBlock";
import { ErrorsHandler } from "../../../utils/ErrorsHandler";
import ArticlesShortSkeleton from "../../../components/Article/Shared/ArticlesShortSkeleton";
import Dynamic, { IPage } from "../../../components/Layout/Dynamic";

const ContributorArticlesSpace: IPage = () => {
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
    Center: <ArticlesThreadTitle
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
          : <ArticlesThread
            articles={contributorArticlesAccessor.data!}
            setArticles={setContributorArticles}
            byUser={true}
          />
    }
    <Currency/>
  </Dynamic>
}

ContributorArticlesSpace.Provider = ContributorArticlesSpaceProvider;

export default ContributorArticlesSpace;
