import React, {createContext, FC, PropsWithChildren, useContext, useEffect, useMemo, useState} from 'react';
import {useQuery, useQueryClient, UseQueryResult} from "react-query";
import {ApiError} from "../../../types/ApiError";
import {UkrainianLanguage} from "../../../utils/constants";
import IExtendedArticle from "../../../types/IExtendedArticle";
import {articlesApi} from "../../../api/api";
import {useAppStore} from "../../../store/config";

type ContextType = {
  languages: { title: string, value: string }[],
  articlesAccessor: UseQueryResult<IExtendedArticle[], ApiError>,
  setArticles: (articles: IExtendedArticle[]) => void,
  articlesLanguage: string,
  setArticlesLanguage: (language: string) => void
}

const InitialContextValue: ContextType = {
  languages: [{title: 'UA', value: 'ua'}, {title: 'EN', value: 'en'}],
  articlesAccessor: {} as UseQueryResult<IExtendedArticle[], ApiError>,
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

  const languages = [{title: 'UA', value: 'ua'}, {title: 'EN', value: 'en'}]
  const [articlesLanguage, setArticlesLanguage] = useState<string>(localStorage.getItem('articlesLanguage') ?? UkrainianLanguage);

  const articlesAccessor: any = useQuery<IExtendedArticle[], ApiError>(['articles', articlesLanguage, isLogin],
    () => articlesApi.getArticles(articlesLanguage)
  );

  const handleSetArticles = (newArticles: IExtendedArticle[]) => {
    queryClient.setQueryData(['articles', articlesLanguage, isLogin], newArticles);
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