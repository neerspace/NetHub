import React, {FC, PropsWithChildren, useState} from 'react';
import {useQuery, useQueryClient, UseQueryResult} from "react-query";
import {getArticle, getArticleActions, getLocalization} from "./ArticleSpace.functions";
import {useParams} from "react-router-dom";
import IArticleLocalizationResponse from "../../../types/api/Article/IArticleLocalizationResponse";
import {ApiError} from "../../../types/ApiError";
import {RateVariants} from "../../../components/Article/Shared/ArticlesRateCounter";
import {useAppStore} from "../../../store/config";
import IArticleResponse from "../../../types/api/Article/IArticleResponse";
import {AxiosError, AxiosResponse} from "axios";
import IExtendedArticle from "../../../types/IExtendedArticle";
import {QueryClientConstants} from "../../../constants/queryClientConstants";

type ContextType = {
  articleAccessor: UseQueryResult<IArticleResponse, ApiError>,
  setArticle: (article: IArticleResponse) => void,
  localizationAccessor: UseQueryResult<IArticleLocalizationResponse, ApiError>,
  setLocalization: (localization: IArticleLocalizationResponse) => void
}

const InitialContextValue: ContextType = {
  articleAccessor: {} as UseQueryResult<IArticleResponse, ApiError>,
  setArticle: () => {
  },
  localizationAccessor: {} as UseQueryResult<IArticleLocalizationResponse, ApiError>,
  setLocalization: () => {
  }
};

const ArticleContext = React.createContext<ContextType>(InitialContextValue);

export const useArticleContext = (): ContextType => React.useContext(ArticleContext);

const ArticleSpaceProvider: FC<PropsWithChildren> = ({children}) => {
  const queryClient = useQueryClient();

  const {id, code} = useParams();

  const articleAccessor = useQuery<IArticleResponse, ApiError>([QueryClientConstants.article, Number(id)], () => getArticle(id!));
  const localizationAccessor = useQuery<IArticleLocalizationResponse, ApiError>([QueryClientConstants.articleLocalization, Number(id), code], () => getLocalization(id!, code!));


  const setArticle = (article: IArticleResponse) => queryClient.setQueryData([QueryClientConstants.article, Number(id)], article);
  const setLocalization = (localization: IArticleLocalizationResponse) => queryClient.setQueryData([QueryClientConstants.articleLocalization, Number(id), code], localization);

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