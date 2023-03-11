import React, { FC, PropsWithChildren } from 'react';
import { useQuery, useQueryClient, UseQueryResult } from "react-query";
import { useParams } from "react-router-dom";
import { ApiError } from "../../../types/ApiError";
import { _articlesApi, _articlesSetsApi } from "../../../api";
import { QueryClientKeysHelper } from "../../../utils/QueryClientKeysHelper";
import {
  ArticleModel,
  ArticleSetModelExtended,
  IArticleModel,
  IArticleSetModelExtended
} from "../../../api/_api";

type ContextType = {
  articleSetAccessor: UseQueryResult<IArticleSetModelExtended, ApiError>,
  setArticleSet: (article: IArticleSetModelExtended) => void,
  articleAccessor: UseQueryResult<IArticleModel, ApiError>,
  setArticle: (article: IArticleModel) => void
}

const InitialContextValue: ContextType = {
  articleSetAccessor: {} as UseQueryResult<IArticleSetModelExtended, ApiError>,
  setArticleSet: () => {
  },
  articleAccessor: {} as UseQueryResult<IArticleModel, ApiError>,
  setArticle: () => {
  }
};

const ArticleContext = React.createContext<ContextType>(InitialContextValue);

export const useArticleContext = (): ContextType => React.useContext(ArticleContext);

const ArticleSpaceProvider: FC<PropsWithChildren> = ({children}) => {
  const queryClient = useQueryClient();

  const {id, code} = useParams();

  const articleSetAccessor = useQuery<ArticleSetModelExtended, ApiError>(QueryClientKeysHelper.ArticleSet(+id!),
    () => _articlesSetsApi.getById(+id!),
    {refetchIntervalInBackground: true});
  const articleAccessor = useQuery<ArticleModel, ApiError>(QueryClientKeysHelper.Article(+id!, code!),
    () => _articlesApi.getByIdAndCode(+id!, code!),
    {refetchIntervalInBackground: true});


  const setArticleSet = (articleSet: IArticleSetModelExtended) =>
    queryClient.setQueryData(QueryClientKeysHelper.ArticleSet(+id!), articleSet);
  const setArticle = (article: IArticleModel) =>
    queryClient.setQueryData(QueryClientKeysHelper.Article(+id!, code!), article);

  const value: ContextType = React.useMemo(
    () => ({
      articleSetAccessor,
      setArticleSet,
      articleAccessor,
      setArticle
    }),
    [articleSetAccessor, setArticleSet, articleAccessor, setArticle]
  );

  return (
    <ArticleContext.Provider value={value}>
      {children}
    </ArticleContext.Provider>
  );
};

export default ArticleSpaceProvider;