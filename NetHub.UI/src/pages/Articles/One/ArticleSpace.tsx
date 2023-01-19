import React from 'react';
import ArticleBody from "../../../components/Article/One/Body/ArticleBody";
import CommentsWidget from "../../../components/Shared/CommentsWidget";
import ArticleInfo from "../../../components/Article/One/ArticleInfo";
import {useParams} from "react-router-dom";
import ArticleBodySkeleton from "../../../components/Article/One/Body/ArticleBodySkeleton";
import {Box, Skeleton} from "@chakra-ui/react";
import ArticleSpaceProvider, {useArticleContext} from "./ArticleSpace.Provider";
import Layout, {Page} from "../../../components/Layout/Layout";
import ErrorBlock from "../../../components/Layout/ErrorBlock";
import {ErrorsHandler} from "../../../utils/ErrorsHandler";

const ArticleSpace: Page = () => {
  const {articleAccessor, localizationAccessor} = useArticleContext();
  const {id, code} = useParams();

  const config = {
    Center: {
      showError: true,
    },
    Right: {
      showError: true,
    }
  }

  const isSuccess = articleAccessor.isSuccess && localizationAccessor.isSuccess;
  const isLocalizationError = localizationAccessor.isError;

  return <Layout Config={config}>
    <Box width={'100%'} display={'flex'} flexDirection={'column'}>
      {
        isLocalizationError ?
          <ErrorBlock>
            {ErrorsHandler.localization(localizationAccessor.error.statusCode)}
          </ErrorBlock> :
          !isSuccess
            ? <ArticleBodySkeleton/>
            : <ArticleBody/>
      }
      {<CommentsWidget display={isSuccess} deps={[id, code]}/>}
    </Box>
    {isLocalizationError ? <></> :
      !isSuccess
        ? <Skeleton height={200}/>
        : <ArticleInfo/>}
  </Layout>
};

ArticleSpace.Provider = ArticleSpaceProvider;
export default ArticleSpace;
