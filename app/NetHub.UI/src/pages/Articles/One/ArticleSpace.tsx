import React from 'react';
import ArticleBody from "../../../components/Article/One/Body/ArticleBody";
import CommentsWidget from "../../../components/Shared/CommentsWidget";
import ArticleInfo from "../../../components/Article/One/ArticleInfo";
import {useParams} from "react-router-dom";
import ArticleBodySkeleton from "../../../components/Article/One/Body/ArticleBodySkeleton";
import {Box, Skeleton} from "@chakra-ui/react";
import ArticleSpaceProvider, {useArticleContext} from "./ArticleSpace.Provider";
import ErrorBlock from "../../../components/Layout/ErrorBlock";
import {ErrorsHandler} from "../../../utils/ErrorsHandler";
import Dynamic, { IPage } from "../../../components/Layout/Dynamic";

const ArticleSpace: IPage = () => {
  const {articleSetAccessor, articleAccessor} = useArticleContext();
  const {id, code} = useParams();

  const config = {
    Center: {
      showError: true,
    },
    Right: {
      showError: true,
    }
  }

  const isSuccess = articleSetAccessor.isSuccess && articleAccessor.isSuccess;
  const isArticleError = articleAccessor.isError;

  return <Dynamic Config={config}>
    <Box width={'100%'} display={'flex'} flexDirection={'column'}>
      {
        isArticleError ?
          <ErrorBlock>
            {ErrorsHandler.article(articleAccessor.error.statusCode)}
          </ErrorBlock> :
          !isSuccess
            ? <ArticleBodySkeleton/>
            : <ArticleBody/>
      }
      {<CommentsWidget display={isSuccess} deps={[id, code]}/>}
    </Box>
    {isArticleError ? <></> :
      !isSuccess
        ? <Skeleton height={200}/>
        : <ArticleInfo/>}
  </Dynamic>
};

ArticleSpace.Provider = ArticleSpaceProvider;
export default ArticleSpace;
