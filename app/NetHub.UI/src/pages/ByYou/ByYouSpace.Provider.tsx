import React, { createContext, FC, PropsWithChildren, useContext, useMemo } from 'react';
import { useQuery, useQueryClient, UseQueryResult } from "react-query";
import { QueryClientKeysHelper } from "../../utils/QueryClientKeysHelper";
import { _myArticlesApi } from "../../api";
import { ApiError } from "../../types/ApiError";
import { ISimpleArticle } from "../../types/api/ISimpleArticle";

interface ContextType {
  articlesAccessor: UseQueryResult<ISimpleArticle[], ApiError>
  setArticles: (articles: ISimpleArticle[]) => void
}

const InitialContextValue: ContextType = {
  articlesAccessor: {} as UseQueryResult<ISimpleArticle[], ApiError>,
  setArticles: () => {
  }
}

const ByYouContext = createContext<ContextType>(InitialContextValue);

export const useByYouContext = (): ContextType => useContext<ContextType>(ByYouContext);

const ByYouProvider: FC<PropsWithChildren> = ({children}) => {
  const queryClient = useQueryClient();
  const articlesAccessor = useQuery<ISimpleArticle[], ApiError>(QueryClientKeysHelper.ArticlesByYou(), () => _myArticlesApi.articles(),
    {refetchIntervalInBackground: true})

  const setArticles = (articles: ISimpleArticle[]) => queryClient.setQueryData(QueryClientKeysHelper.ArticlesByYou(), articles);

  const value: ContextType = useMemo(() => {
    return {
      articlesAccessor,
      setArticles
    }
  }, [articlesAccessor, setArticles])

  return <ByYouContext.Provider value={value}>
    {children}
  </ByYouContext.Provider>
};

export default ByYouProvider;