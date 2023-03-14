import React, { createContext, FC, PropsWithChildren, useContext, useMemo, useState } from 'react';
import { useQuery, useQueryClient, UseQueryResult } from "react-query";
import { ApiError } from "../../../types/ApiError";
import { UkrainianLanguage } from "../../../utils/constants";
import { _articlesApi } from "../../../api";
import { ISimpleArticle } from "../../../types/api/ISimpleArticle";
import { QueryClientKeysHelper } from "../../../utils/QueryClientKeysHelper";
import { useAppStore } from "../../../store/store";

type ContextType = {
  languages: { title: string, value: string }[],
  articlesAccessor: UseQueryResult<ISimpleArticle[], ApiError>,
  setArticles: (articles: ISimpleArticle[]) => void,
  articlesLanguage: string,
  setArticlesLanguage: (language: string) => void
}

const InitialContextValue: ContextType = {
  languages: [{title: 'UK', value: 'uk'}, {title: 'EN', value: 'en'}],
  articlesAccessor: {} as UseQueryResult<ISimpleArticle[], ApiError>,
  setArticles: () => {
  },
  articlesLanguage: localStorage.getItem('articlesLanguage') ?? UkrainianLanguage,
  setArticlesLanguage: () => {
  }
}
const ArticlesThreadContext = createContext<ContextType>(InitialContextValue);

export const useArticlesThreadContext = (): ContextType => useContext<ContextType>(ArticlesThreadContext);

const ArticlesThreadSpaceProvider: FC<PropsWithChildren> = ({children}) => {
  const queryClient = useQueryClient();
  const isLogin = useAppStore(state => state.isLogin);


  const languages = [{title: 'UK', value: 'uk'}, {title: 'EN', value: 'en'}]
  const [articlesLanguage, setArticlesLanguage] = useState<string>(localStorage.getItem('articlesLanguage') ?? UkrainianLanguage);

  const articlesAccessor = useQuery<ISimpleArticle[], ApiError>(QueryClientKeysHelper.ArticlesThread(articlesLanguage, isLogin),
    () => loadArticles(), {refetchIntervalInBackground: true});

  const loadArticles = async () => {
    return await _articlesApi.search(articlesLanguage, undefined);
  }

  const handleSetArticles = (newArticles: ISimpleArticle[]) => {
    queryClient.setQueryData(QueryClientKeysHelper.ArticlesThread(articlesLanguage, isLogin), newArticles);
  }

  const handleSetArticlesLanguage = (value: string) => {
    localStorage.setItem('articlesLanguage', value);
    setArticlesLanguage(value);
  }

  const value: ContextType = useMemo(() => {
    return {
      languages,
      articlesAccessor,
      setArticles: handleSetArticles,
      articlesLanguage,
      setArticlesLanguage: handleSetArticlesLanguage,
    }
  }, [articlesAccessor, articlesLanguage, handleSetArticles, languages, handleSetArticlesLanguage])

  return <ArticlesThreadContext.Provider value={value}>
    {children}
  </ArticlesThreadContext.Provider>
};

export default ArticlesThreadSpaceProvider;