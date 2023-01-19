import React, {createContext, FC, PropsWithChildren, useContext, useMemo, useState} from 'react';
import ILocalization from "../../../types/ILocalization";
import {ArticleStorage} from "../../../utils/localStorageProvider";
import {useQuery, UseQueryResult} from "react-query";
import {articlesApi} from "../../../api/api";
import {ApiError} from "../../../types/ApiError";
import {useParams} from "react-router-dom";
import {z as u} from "zod";

interface ContextType {
  article: ILocalization,
  defaultArticleState: ILocalization,
  setArticle: React.Dispatch<React.SetStateAction<ILocalization>>,
  setArticleValue: (key: string) => (value: any) => void
  images?: UseQueryResult<string[], ApiError>,
  errors: CreateArticleFormError,
  setErrors: (errors: CreateArticleFormError) => void
}

const defaultState = () => {
  return {
    title: ArticleStorage.getTitle() ?? '',
    description: ArticleStorage.getDescription() ?? '',
    html: ArticleStorage.getHtml() ?? '',
    // tags: ArticleStorage.getTags() ? JSON.parse(ArticleStorage.getTags()!) : [] as string[],
    tags: ArticleStorage.getTags() ? JSON.parse(ArticleStorage.getTags()!) : ['test1', 'test2', 'test3'],
    originalLink: ''
  } as ILocalization
};

const InitialContextValue: ContextType = {
  article: defaultState(),
  defaultArticleState: defaultState(),
  setArticle: () => {
  },
  setArticleValue: () => () => {
  },
  errors: {_errors: []},
  setErrors: () => {
  }
}

const ArticleCreatingContext = createContext<ContextType>(InitialContextValue);

export const useArticleCreatingContext = (): ContextType => useContext<ContextType>(ArticleCreatingContext);

export type CreateArticleFormError = u.ZodFormattedError<{
  html: string,
  title: string,
  description: string,
  tags: string[],
  originalLink: string | null
}>;

const ArticleCreatingProvider: FC<PropsWithChildren> = ({children}) => {
  const {id} = useParams();

  const [article, setArticle] = useState<ILocalization>(defaultState);
  const images = useQuery<string[], ApiError>('articleImages', () => articlesApi.getArticleImages(), {enabled: !!id});
  const [errors, setErrors] = useState<CreateArticleFormError>({_errors: []});

  const setArticleValue = (key: string) => (value: any) => {
    setArticle((prev) => {
      return {...prev, [key]: value}
    })
  }

  const value: ContextType = useMemo(() => {
    return {
      article,
      setArticle,
      setArticleValue,
      images,
      defaultArticleState: defaultState(),
      errors,
      setErrors
    }
  }, [article, setArticle, setArticleValue, images, errors, setErrors])

  return (
    <ArticleCreatingContext.Provider value={value}>
      {children}
    </ArticleCreatingContext.Provider>
  );
};

export default ArticleCreatingProvider;