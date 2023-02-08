import React, { FC, PropsWithChildren, useState } from 'react';
import { useParams } from "react-router-dom";
import { UkrainianLanguage } from "../../../utils/constants";
import { useQuery, useQueryClient, UseQueryResult } from "react-query";
import { ApiError } from "../../../types/ApiError";
import { _localizationsApi, _usersApi } from "../../../api";
import { PrivateUserResult, ViewLocalizationModel } from "../../../api/_api";
import { QueryClientConstants } from "../../../constants/queryClientConstants";
import { ILocalizationExtended } from "../../../types/api/ILocalizationExtended";

type ContextType = {
  languages: { title: string, value: string }[],
  articlesLanguage: string,
  setArticlesLanguage: (language: string) => void,
  contributorAccessor: UseQueryResult<PrivateUserResult, ApiError>,
  contributorArticlesAccessor: UseQueryResult<ILocalizationExtended[], ApiError>,
  setContributorArticles: (articles: ILocalizationExtended[]) => void
}

const InitialContextValue: ContextType = {
  languages: [{title: 'UA', value: 'ua'}, {title: 'EN', value: 'en'}],
  articlesLanguage: localStorage.getItem('contributorArticlesLanguage') ?? UkrainianLanguage,
  setArticlesLanguage: () => {
  },
  contributorAccessor: {} as UseQueryResult<PrivateUserResult, ApiError>,
  contributorArticlesAccessor: {} as UseQueryResult<ILocalizationExtended[], ApiError>,
  setContributorArticles: () => {
  }
};

const ContributorArticlesContext = React.createContext<ContextType>(InitialContextValue);

export const useContributorArticlesContext = (): ContextType => React.useContext(ContributorArticlesContext);
const ContributorArticlesSpaceProvider: FC<PropsWithChildren> = ({children}) => {
  const queryClient = useQueryClient();
  const {contributorId} = useParams();

  const languages = [{title: 'UA', value: 'ua'}, {title: 'EN', value: 'en'}]
  const [articlesLanguage, setArticlesLanguage] = useState<string>(localStorage.getItem('contributorArticlesLanguage') ?? UkrainianLanguage);

  const contributorAccessor = useQuery<PrivateUserResult, ApiError>([QueryClientConstants.contributor, contributorId],
    async () => (await _usersApi.usersInfo([contributorId!]))[0]);
  const contributorArticlesAccessor = useQuery<ViewLocalizationModel[], ApiError>
  ([QueryClientConstants.contributor, QueryClientConstants.articles, contributorId, articlesLanguage],
    () => _localizationsApi.search(articlesLanguage, "contributorId==" + contributorId), {enabled: !!contributorAccessor.data});
  // articlesApi.getUserArticles(, articlesLanguage), )
  const handleSetArticlesLanguage = (value: string) => {
    localStorage.setItem('contributorArticlesLanguage', value);
    setArticlesLanguage(value);
  }

  const handleSetArticles = (newArticles: ILocalizationExtended[]) => {
    queryClient.setQueryData(['contributor', 'articles', contributorId, articlesLanguage], newArticles);
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