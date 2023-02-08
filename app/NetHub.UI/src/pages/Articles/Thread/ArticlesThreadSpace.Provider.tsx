import React, { createContext, FC, PropsWithChildren, useContext, useMemo, useState } from 'react';
import { useQuery, useQueryClient, UseQueryResult } from "react-query";
import { ApiError } from "../../../types/ApiError";
import { UkrainianLanguage } from "../../../utils/constants";
import { useAppStore } from "../../../store/config";
import { _localizationsApi } from "../../../api";
import { ViewLocalizationModel } from "../../../api/_api";
import { ILocalizationExtended } from "../../../types/api/ILocalizationExtended";
import { FilterInfo } from "../../../types/api/IFilterInfo";

type ContextType = {
  languages: { title: string, value: string }[],
  articlesAccessor: UseQueryResult<ILocalizationExtended[], ApiError>,
  setArticles: (articles: ILocalizationExtended[]) => void,
  articlesLanguage: string,
  setArticlesLanguage: (language: string) => void
}

const InitialContextValue: ContextType = {
  languages: [{title: 'UA', value: 'ua'}, {title: 'EN', value: 'en'}],
  articlesAccessor: {} as UseQueryResult<ILocalizationExtended[], ApiError>,
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

  console.log('language', articlesLanguage)

  const articlesAccessor: any = useQuery<ViewLocalizationModel[], ApiError>(['articles', articlesLanguage, isLogin],
    () => loadArticles()
  );

  const loadArticles = async () => {
    const f = new FilterInfo();
    return await _localizationsApi.search(articlesLanguage, f.filters, f.sorts, f.page, f.pageSize);
  }

  const handleSetArticles = (newArticles: ILocalizationExtended[]) => {
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