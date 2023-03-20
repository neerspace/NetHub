import React, { FC, PropsWithChildren, useState } from 'react';
import { useParams } from "react-router-dom";
import { UkrainianLanguage } from "../../../utils/constants";
import { useQuery, useQueryClient, UseQueryResult } from "react-query";
import { ApiError } from "../../../types/ApiError";
import { _articlesApi, _usersApi } from "../../../api";
import { PrivateUserResult } from "../../../api/_api";
import { ISimpleArticle } from "../../../types/api/ISimpleArticle";
import { QueryClientKeysHelper } from "../../../utils/QueryClientKeysHelper";

type ContextType = {
  languages: { title: string, value: string }[],
  articlesLanguage: string,
  setArticlesLanguage: (language: string) => void,
  contributorAccessor: UseQueryResult<PrivateUserResult, ApiError>,
  contributorArticlesAccessor: UseQueryResult<ISimpleArticle[], ApiError>,
  setContributorArticles: (articles: ISimpleArticle[]) => void
}

const InitialContextValue: ContextType = {
  languages: [{title: 'UK', value: 'uk'}, {title: 'EN', value: 'en'}],
  articlesLanguage: localStorage.getItem('contributorArticlesLanguage') ?? UkrainianLanguage,
  setArticlesLanguage: () => {
  },
  contributorAccessor: {} as UseQueryResult<PrivateUserResult, ApiError>,
  contributorArticlesAccessor: {} as UseQueryResult<ISimpleArticle[], ApiError>,
  setContributorArticles: () => {
  }
};

const ContributorArticlesContext = React.createContext<ContextType>(InitialContextValue);

export const useContributorArticlesContext = (): ContextType => React.useContext(ContributorArticlesContext);
const CreatorArticlesSpaceProvider: FC<PropsWithChildren> = ({children}) => {
  const queryClient = useQueryClient();
  const {contributorUsername} = useParams();

  const languages = [{title: 'UK', value: 'uk'}, {title: 'EN', value: 'en'}]
  const [articlesLanguage, setArticlesLanguage] = useState<string>(localStorage.getItem('contributorArticlesLanguage') ?? UkrainianLanguage);

  const contributorAccessor = useQuery<PrivateUserResult, ApiError>(QueryClientKeysHelper.Contributor(contributorUsername!),
    async () => (await _usersApi.usersInfo([contributorUsername!]))[0]);
  const contributorArticlesAccessor = useQuery<ISimpleArticle[], ApiError>(QueryClientKeysHelper.ContributorArticles(contributorUsername!, articlesLanguage),
    () => _articlesApi.search(articlesLanguage, contributorUsername), {enabled: !!contributorAccessor.data});
  const handleSetArticlesLanguage = (value: string) => {
    localStorage.setItem('contributorArticlesLanguage', value);
    setArticlesLanguage(value);
  }

  const handleSetArticles = (newArticles: ISimpleArticle[]) => {
    queryClient.setQueryData(QueryClientKeysHelper.ContributorArticles(contributorUsername!, articlesLanguage), newArticles);
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

export default CreatorArticlesSpaceProvider;