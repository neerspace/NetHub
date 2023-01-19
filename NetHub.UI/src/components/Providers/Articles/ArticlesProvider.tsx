import React, {createContext, FC, PropsWithChildren, useContext, useMemo} from 'react';

interface ContextType {

}

const InitialContextValue: ContextType = {

}

const ArticlesContext = createContext<ContextType>(InitialContextValue);

export const useArticlesContext = (): ContextType => useContext<ContextType>(ArticlesContext);

const ArticlesProvider: FC<PropsWithChildren> = ({children}) => {
 const value: ContextType = useMemo(() => {
    return {

    }
  }, [])

 return (
  <ArticlesContext.Provider value={value}>
    {children}
  </ArticlesContext.Provider>
 );
};

export default ArticlesProvider;