import React, { FC, PropsWithChildren } from 'react';
import { useQuery, useQueryClient, UseQueryResult } from "react-query";
import { useParams } from "react-router-dom";
import { ApiError } from "../../../types/ApiError";
import { _articlesApi, _localizationsApi } from "../../../api";
import {
  ArticleLocalizationModel,
  ArticleModelExtended,
  IArticleLocalizationModel,
  IArticleModelExtended
} from "../../../api/_api";
import { QueryClientKeysHelper } from "../../../utils/QueryClientKeysHelper";

type ContextType = {
  articleAccessor: UseQueryResult<IArticleModelExtended, ApiError>,
  setArticle: (article: IArticleModelExtended) => void,
  localizationAccessor: UseQueryResult<IArticleLocalizationModel, ApiError>,
  setLocalization: (localization: IArticleLocalizationModel) => void
}

const InitialContextValue: ContextType = {
  articleAccessor: {} as UseQueryResult<IArticleModelExtended, ApiError>,
  setArticle: () => {
  },
  localizationAccessor: {} as UseQueryResult<IArticleLocalizationModel, ApiError>,
  setLocalization: () => {
  }
};

const ArticleContext = React.createContext<ContextType>(InitialContextValue);

export const useArticleContext = (): ContextType => React.useContext(ArticleContext);

const ArticleSpaceProvider: FC<PropsWithChildren> = ({children}) => {
  const queryClient = useQueryClient();

  const {id, code} = useParams();

  const articleAccessor = useQuery<ArticleModelExtended, ApiError>(QueryClientKeysHelper.Article(+id!),
    () => _articlesApi.getById(+id!),
    {refetchIntervalInBackground: true});
  const localizationAccessor = useQuery<ArticleLocalizationModel, ApiError>(QueryClientKeysHelper.ArticleLocalization(+id!, code!),
    () => _localizationsApi.getByIdAndCode(+id!, code!),
    {refetchIntervalInBackground: true});


  const setArticle = (article: IArticleModelExtended) =>
    queryClient.setQueryData(QueryClientKeysHelper.Article(+id!), article);
  const setLocalization = (localization: IArticleLocalizationModel) =>
    queryClient.setQueryData(QueryClientKeysHelper.ArticleLocalization(+id!, code!), localization);

  const value: ContextType = React.useMemo(
    () => ({
      articleAccessor,
      setArticle,
      localizationAccessor,
      setLocalization
    }),
    [articleAccessor, setArticle, localizationAccessor, setLocalization]
  );

  return (
    <ArticleContext.Provider value={value}>
      {children}
    </ArticleContext.Provider>
  );
};

export default ArticleSpaceProvider;