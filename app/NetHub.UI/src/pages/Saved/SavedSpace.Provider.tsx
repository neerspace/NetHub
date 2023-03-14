import React, { createContext, FC, PropsWithChildren, useContext, useMemo } from 'react';
import { useQuery, useQueryClient, UseQueryResult } from "react-query";
import { ApiError } from "../../types/ApiError";
import { _myArticlesApi } from "../../api";
import { ISimpleArticle } from "../../types/api/ISimpleArticle";
import { QueryClientKeysHelper } from "../../utils/QueryClientKeysHelper";

type ContextType = {
  savedArticles: UseQueryResult<ISimpleArticle[], ApiError>,
  setSavedArticles: (articles: ISimpleArticle[]) => void
}

const InitialContextValue: ContextType = {
  savedArticles: {} as UseQueryResult<ISimpleArticle[], ApiError>,
  setSavedArticles: () => {
  }
}

const SavedContext = createContext<ContextType>(InitialContextValue);

export const useSavedArticlesContext = (): ContextType => useContext<ContextType>(SavedContext);

const SavedSpaceProvider: FC<PropsWithChildren> = ({children}) => {
  const queryClient = useQueryClient();
  const savedArticles = useQuery<ISimpleArticle[], ApiError>(QueryClientKeysHelper.SavedArticles(), () => _myArticlesApi.savedArticles(),
    {refetchIntervalInBackground: true});

  const setSavedArticles = (articles: ISimpleArticle[]) => queryClient.setQueryData(QueryClientKeysHelper.SavedArticles(), articles);

  const value: ContextType = useMemo(() => {
    return {
      savedArticles,
      setSavedArticles
    }
  }, [savedArticles, setSavedArticles])

  return <SavedContext.Provider value={value}>
    {children}
  </SavedContext.Provider>
};

export default SavedSpaceProvider;