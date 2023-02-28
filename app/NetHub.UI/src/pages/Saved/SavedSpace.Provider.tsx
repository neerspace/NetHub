import React, { createContext, FC, PropsWithChildren, useContext, useMemo } from 'react';
import { useQuery, useQueryClient, UseQueryResult } from "react-query";
import { ApiError } from "../../types/ApiError";
import { QueryClientConstants } from "../../constants/queryClientConstants";
import { _myArticlesApi } from "../../api";
import { ViewLocalizationModel } from "../../api/_api";
import { ISimpleLocalization } from "../../types/api/ISimpleLocalization";

type ContextType = {
  savedArticles: UseQueryResult<ISimpleLocalization[], ApiError>,
  setSavedArticles: (articles: ISimpleLocalization[]) => void
}

const InitialContextValue: ContextType = {
  savedArticles: {} as UseQueryResult<ISimpleLocalization[], ApiError>,
  setSavedArticles: () => {
  }
}

const SavedContext = createContext<ContextType>(InitialContextValue);

export const useSavedArticlesContext = (): ContextType => useContext<ContextType>(SavedContext);

const SavedSpaceProvider: FC<PropsWithChildren> = ({children}) => {
  const queryClient = useQueryClient();
  const savedArticles = useQuery<ISimpleLocalization[], ApiError>(QueryClientConstants.savedArticles, () => _myArticlesApi.savedArticles());

  const setSavedArticles = (articles: ISimpleLocalization[]) => queryClient.setQueryData(QueryClientConstants.savedArticles, articles);

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