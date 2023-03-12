import React, {createContext, FC, PropsWithChildren, useContext, useMemo} from 'react';
import { IArticleSetModel } from "../../api/_api";
import { useQuery } from "react-query";
import { QueryClientKeysHelper } from "../../utils/QueryClientKeysHelper";
import { _articlesSetsApi } from "../../api";

interface ContextType {
  articleSets: IArticleSetModel[]
}

const InitialContextValue: ContextType = {

}

const ByYouContext = createContext<ContextType>(InitialContextValue);

export const useByYouContext = (): ContextType => useContext<ContextType>(ByYouContext);

const ByYouProvider: FC<PropsWithChildren> = ({children}) => {
  const articleSetsAccessor = useQuery(QueryClientKeysHelper.ArticleSets, () => _articlesSetsApi.)

 const value: ContextType = useMemo(() => {
    return {

    }
  }, [])

 return (
  <ByYouContext.Provider value={value}>
    {children}
  </ByYouContext.Provider>
 );
};

export default ByYouProvider;