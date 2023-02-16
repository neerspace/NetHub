import React, { FC, PropsWithChildren, useState } from 'react';
import { useParams } from "react-router-dom";
import { UkrainianLanguage } from "../../../utils/constants";
import { useQuery, useQueryClient, UseQueryResult } from "react-query";
import { ApiError } from "../../../types/ApiError";
import { _localizationsApi, _usersApi } from "../../../api";
import { PrivateUserResult } from "../../../api/_api";
import { QueryClientConstants } from "../../../constants/queryClientConstants";
import { ISimpleLocalization } from "../../../types/api/ISimpleLocalization";

type ContextType = {
  languages: { title: string, value: string }[],
  articlesLanguage: string,
  setArticlesLanguage: (language: string) => void,
  contributorAccessor: UseQueryResult<PrivateUserResult, ApiError>,
  contributorArticlesAccessor: UseQueryResult<ISimpleLocalization[], ApiError>,
  setContributorArticles: (articles: ISimpleLocalization[]) => void
}

const InitialContextValue: ContextType = {
  languages: [{title: 'UA', value: 'ua'}, {title: 'EN', value: 'en'}],
  articlesLanguage: localStorage.getItem('contributorArticlesLanguage') ?? UkrainianLanguage,
  setArticlesLanguage: () => {
  },
  contributorAccessor: {} as UseQueryResult<PrivateUserResult, ApiError>,
  contributorArticlesAccessor: {} as UseQueryResult<ISimpleLocalization[], ApiError>,
  setContributorArticles: () => {
  }
};

const ContributorArticlesContext = React.createContext<ContextType>(InitialContextValue);

export const useContributorArticlesContext = (): ContextType => React.useContext(ContributorArticlesContext);
const ContributorArticlesSpaceProvider: FC<PropsWithChildren> = ({children}) => {
  const queryClient = useQueryClient();
  const {contributorUsername} = useParams();

  const languages = [{title: 'UA', value: 'ua'}, {title: 'EN', value: 'en'}]
  const [articlesLanguage, setArticlesLanguage] = useState<string>(localStorage.getItem('contributorArticlesLanguage') ?? UkrainianLanguage);

  const contributorAccessor = useQuery<PrivateUserResult, ApiError>([QueryClientConstants.contributor, contributorUsername],
    async () => (await _usersApi.usersInfo([contributorUsername!]))[0]);
  const contributorArticlesAccessor = useQuery<ISimpleLocalization[], ApiError>
  ([QueryClientConstants.contributor, QueryClientConstants.articles, contributorUsername, articlesLanguage],
    () => _localizationsApi.search(articlesLanguage, contributorUsername), {enabled: !!contributorAccessor.data});
  const handleSetArticlesLanguage = (value: string) => {
    localStorage.setItem('contributorArticlesLanguage', value);
    setArticlesLanguage(value);
  }

  const handleSetArticles = (newArticles: ISimpleLocalization[]) => {
    queryClient.setQueryData(['contributor', 'articles', contributorUsername, articlesLanguage], newArticles);
  }

  const value: ContextType = React.useMemo(() => {
      return {
        languages,
        articlesLanguage,
        setArticlesLanguage: handleSetArticlesLanguage,
        contributorAccessor,
        contributorArticlesAccessor,
        setContributorArticles: handleSetArticles
      }
    },
    [languages, articlesLanguage, handleSetArticlesLanguage, contributorAccessor, contributorArticlesAccessor, handleSetArticles],
  );

  return (
    <ContributorArticlesContext.Provider value={value}>
      {children}
    </ContributorArticlesContext.Provider>
  );
};

export default ContributorArticlesSpaceProvider;