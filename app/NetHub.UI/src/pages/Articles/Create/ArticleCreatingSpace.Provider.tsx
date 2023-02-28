import React, { createContext, FC, PropsWithChildren, useContext, useMemo, useState } from 'react';
import { ArticleStorage } from "../../../utils/localStorageProvider";
import { useQuery, UseQueryResult } from "react-query";
import { ApiError } from "../../../types/ApiError";
import { useParams } from "react-router-dom";
import { z as u } from "zod";
import { IArticleLocalizationCreateRequest } from "../../../api/_api";

export interface IArticleLocalizationCreateExtendedRequest extends IArticleLocalizationCreateRequest{
  tags: string[],
  originalLink: string | null
}

interface ContextType {
  article: IArticleLocalizationCreateExtendedRequest,
  defaultArticleState: IArticleLocalizationCreateExtendedRequest,
  setArticle: React.Dispatch<React.SetStateAction<IArticleLocalizationCreateExtendedRequest>>,
  setArticleValue: (key: string) => (value: any) => void
  images?: UseQueryResult<string[], ApiError>,
  errors: CreateArticleFormError,
  setErrors: (errors: CreateArticleFormError) => void
}

const defaultState: () => IArticleLocalizationCreateExtendedRequest = () => {
  return {
    title: ArticleStorage.getTitle() ?? '',
    description: ArticleStorage.getDescription() ?? '',
    html: ArticleStorage.getHtml() ?? '',
    // tags: ArticleStorage.getTags() ? JSON.parse(ArticleStorage.getTags()!) : [] as string[],
    tags: ArticleStorage.getTags() ? JSON.parse(ArticleStorage.getTags()!) : ['test1', 'test2', 'test3'],
    originalLink: ''
  } as unknown as IArticleLocalizationCreateExtendedRequest
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

  const [article, setArticle] = useState<IArticleLocalizationCreateExtendedRequest>(defaultState);
  const images = useQuery<string[], ApiError>('articleImages',
    // () => _articlesApi.getArticleImages(id),
    () => [
      'https://upload.wikimedia.org/wikipedia/commons/e/ed/Gibson_Les_Paul%28sg%29_1962.jpg',
      'https://ru.wargaming.net/clans/media/clans/emblems/cl_1/1/emblem_195x195.png',
      'https://ru-wotp.wgcdn.co/dcont/fb/image/wgfest_ps__006.jpg'
    ],
    {enabled: !!id});
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