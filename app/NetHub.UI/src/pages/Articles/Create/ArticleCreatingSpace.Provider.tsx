import React, { createContext, FC, PropsWithChildren, useContext, useMemo, useState } from 'react';
import { ArticleStorage } from "../../../utils/localStorageProvider";
import { useQuery, UseQueryResult } from "react-query";
import { ApiError } from "../../../types/ApiError";
import { useParams } from "react-router-dom";
import { z as u } from "zod";
import { IArticleCreateRequest, IArticleSetModelExtended, LanguageModel } from '../../../api/_api';
import { QueryClientKeysHelper } from "../../../utils/QueryClientKeysHelper";
import { _articlesSetsApi, _languagesApi } from "../../../api";

export interface IArticleCreateExtendedRequest extends IArticleCreateRequest {
  tags: string[],
  originalLink: string | null,
  language: string | null
}

interface ContextType {
  article: IArticleCreateExtendedRequest,
  defaultArticleState: IArticleCreateExtendedRequest,
  setArticle: React.Dispatch<React.SetStateAction<IArticleCreateExtendedRequest>>,
  setArticleValue: (key: keyof IArticleCreateExtendedRequest) => (value: any) => void,
  languagesAccessor: UseQueryResult<LanguageModel[], ApiError>,
  images?: UseQueryResult<string[], ApiError>,
  errors: CreateArticleFormError,
  setErrors: (errors: CreateArticleFormError) => void,
  withoutSet: boolean
}

const defaultState: () => IArticleCreateExtendedRequest = () => {
  return {
    title: ArticleStorage.getTitle() ?? '',
    description: ArticleStorage.getDescription() ?? '',
    html: ArticleStorage.getHtml() ?? '',
    // tags: ArticleStorage.getTags() ? JSON.parse(ArticleStorage.getTags()!) : [] as string[],
    tags: ArticleStorage.getTags() ? JSON.parse(ArticleStorage.getTags()!) : ['test1', 'test2', 'test3'],
    originalLink: ''
  } as unknown as IArticleCreateExtendedRequest
};

const InitialContextValue: ContextType = {
  article: defaultState(),
  defaultArticleState: defaultState(),
  setArticle: () => {
  },
  setArticleValue: () => () => {
  },
  languagesAccessor: {} as UseQueryResult<LanguageModel[], ApiError>,
  errors: {_errors: []},
  setErrors: () => {
  },
  withoutSet: true
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

  const withoutSet = !id;

  const [article, setArticle] = useState<IArticleCreateExtendedRequest>(defaultState);
  const languagesAccessor = useQuery<LanguageModel[], ApiError>(QueryClientKeysHelper.Languages(), () => _languagesApi.getAll())
  const images = useQuery<string[], ApiError>(QueryClientKeysHelper.ArticleSetImages(+id!),
    () => _articlesSetsApi.getImages(+id!),
    {enabled: !!id});
  const [errors, setErrors] = useState<CreateArticleFormError>({_errors: []});

  const setArticleValue = (key: string) => (value: any) => {
    setArticle((prev) => {
      return {...prev, [ key ]: value}
    })
  }

  const value: ContextType = useMemo(() => {
    return {
      article,
      setArticle,
      setArticleValue,
      languagesAccessor,
      images,
      defaultArticleState: defaultState(),
      errors,
      setErrors,
      withoutSet
    }
  }, [article, setArticle, setArticleValue, languagesAccessor, images, errors, setErrors])

  return (
    <ArticleCreatingContext.Provider value={value}>
      {children}
    </ArticleCreatingContext.Provider>
  );
};

export default ArticleCreatingProvider;