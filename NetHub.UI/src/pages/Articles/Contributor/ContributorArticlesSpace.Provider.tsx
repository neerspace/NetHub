import React, {FC, PropsWithChildren, useState} from 'react';
import {useParams} from "react-router-dom";
import {UkrainianLanguage} from "../../../utils/constants";
import {useQuery, useQueryClient, UseQueryResult} from "react-query";
import IUserInfoResponse from "../../../types/api/User/IUserInfoResponse";
import {articlesApi, userApi} from "../../../api/api";
import IExtendedArticle from "../../../types/IExtendedArticle";
import {ApiError} from "../../../types/ApiError";

type ContextType = {
  languages: { title: string, value: string }[],
  articlesLanguage: string,
  setArticlesLanguage: (language: string) => void,
  contributorAccessor: UseQueryResult<IUserInfoResponse, ApiError>,
  contributorArticlesAccessor: UseQueryResult<IExtendedArticle[], ApiError>,
  setContributorArticles: (articles: IExtendedArticle[]) => void
}

const InitialContextValue: ContextType = {
  languages: [{title: 'UA', value: 'ua'}, {title: 'EN', value: 'en'}],
  articlesLanguage: localStorage.getItem('contributorArticlesLanguage') ?? UkrainianLanguage,
  setArticlesLanguage: () => {
  },
  contributorAccessor: {} as UseQueryResult<IUserInfoResponse, ApiError>,
  contributorArticlesAccessor: {} as UseQueryResult<IExtendedArticle[], ApiError>,
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

  const contributorAccessor = useQuery<IUserInfoResponse, ApiError>(['contributor', contributorId],
    async () => (await userApi.getUsersInfo([contributorId!]))[0])
  const contributorArticlesAccessor = useQuery<IExtendedArticle[], ApiError>(['contributor', 'articles', contributorId, articlesLanguage],
    () => articlesApi.getUserArticles(contributorId!, articlesLanguage), {enabled: !!contributorAccessor.data})
  const handleSetArticlesLanguage = (value: string) => {
    localStorage.setItem('contributorArticlesLanguage', value);
    setArticlesLanguage(value);
  }

  const handleSetArticles = (newArticles: IExtendedArticle[]) => {
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