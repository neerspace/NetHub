import React, { FC, PropsWithChildren } from 'react';
import { useQuery, useQueryClient, UseQueryResult } from "react-query";
import { useParams } from "react-router-dom";
import { ApiError } from "../../../types/ApiError";
import { QueryClientConstants } from "../../../constants/queryClientConstants";
import { _articlesApi, _localizationsApi } from "../../../api";
import {
  ArticleLocalizationModel,
  ArticleModelExtended,
  IArticleLocalizationModel,
  IArticleModelExtended
} from "../../../api/_api";

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

  const articleAccessor = useQuery<ArticleModelExtended, ApiError>([QueryClientConstants.article, +id!], () => _articlesApi.getById(+id!));
  const localizationAccessor = useQuery<ArticleLocalizationModel, ApiError>([QueryClientConstants.articleLocalization, +id!, code],
    () => _localizationsApi.getByIdAndCode(+id!, code!));


  const setArticle = (article: IArticleModelExtended) => queryClient.setQueryData([QueryClientConstants.article, +id!], article);
  const setLocalization = (localization: IArticleLocalizationModel) => queryClient.setQueryData([QueryClientConstants.articleLocalization, +id!, code], localization);

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