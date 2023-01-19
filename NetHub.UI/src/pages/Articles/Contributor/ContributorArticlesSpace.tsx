import React from 'react';
import ArticlesThread from "../../../components/Article/Thread/ArticlesThread";
import ArticlesThreadSpaceSkeleton from "../Thread/ArticlesThreadSpaceSkeleton";
import Currency from "../../../components/Currency/Currency";
import {Skeleton, Text} from "@chakra-ui/react";
import ArticlesThreadTitle from "../../../components/Article/Thread/ArticlesThreadTitle";
import ContributorArticlesSpaceProvider, {useContributorArticlesContext} from "./ContributorArticlesSpace.Provider";
import Layout, {Page} from "../../../components/Layout/Layout";
import ErrorBlock from "../../../components/Layout/ErrorBlock";
import {ErrorsHandler} from "../../../utils/ErrorsHandler";

const ContributorArticlesSpace: Page = () => {
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

  return <Layout Titles={titles}>
    {
      contributorArticlesAccessor.isError
        ? <ErrorBlock>{ErrorsHandler.default(contributorArticlesAccessor.error.statusCode)}</ErrorBlock>
        : !contributorArticlesAccessor.isSuccess
          ? <ArticlesThreadSpaceSkeleton/>
          : <ArticlesThread
            articles={contributorArticlesAccessor.data!}
            setArticles={setContributorArticles}
            byUser={true}
          />
    }
    <Currency/>
  </Layout>
}

ContributorArticlesSpace.Provider = ContributorArticlesSpaceProvider;

export default ContributorArticlesSpace;
