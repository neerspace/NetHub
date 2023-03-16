import React, {
  createContext,
  FC,
  PropsWithChildren,
  useCallback,
  useContext, useEffect,
  useMemo,
  useState
} from 'react';
import { ArticleStorage } from "../../../utils/localStorageProvider";
import { useQuery, UseQueryResult } from "react-query";
import { ApiError } from "../../../types/ApiError";
import { useParams } from "react-router-dom";
import { z as u } from "zod";
import {
  ArticleSetModel,
  IArticleCreateRequest,
  IArticleSetModelExtended,
  LanguageModel
} from '../../../api/_api';
import { QueryClientKeysHelper } from "../../../utils/QueryClientKeysHelper";
import { _articlesSetsApi, _languagesApi } from "../../../api";
import firebase from "firebase/compat";
import Query = firebase.firestore.Query;
import { Article } from "@mui/icons-material";

export interface IArticleCreateExtendedRequest extends IArticleCreateRequest {
  tags: string[],
  originalLink: string | null,
  language: string | null
}

interface ContextType {
  article: IArticleCreateExtendedRequest,
  defaultArticleState: (isFirst: boolean) => IArticleCreateExtendedRequest,
  setArticle: React.Dispatch<React.SetStateAction<IArticleCreateExtendedRequest>>,
  setArticleValue: (key: keyof IArticleCreateExtendedRequest) => (value: any) => void,
  languagesAccessor: UseQueryResult<LanguageModel[], ApiError>,
  articleSet?: UseQueryResult<ArticleSetModel, ApiError>,
  errors: CreateArticleFormError,
  setErrors: (errors: CreateArticleFormError) => void,
  isFirst: boolean
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
  article: {} as IArticleCreateExtendedRequest,
  defaultArticleState: () => {},
  setArticle: () => {
  },
  setArticleValue: () => () => {
  },
  languagesAccessor: {} as UseQueryResult<LanguageModel[], ApiError>,
  errors: {_errors: []},
  setErrors: () => {
  },
  isFirst: true
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
  const isFirst = !id;

  const articleSet = useQuery<ArticleSetModel, ApiError>(QueryClientKeysHelper.ArticleSet(+id!), () => _articlesSetsApi.getById(+id!),
    {enabled: !isFirst})

  const getDefaultState = useCallback((isFirst: boolean): IArticleCreateExtendedRequest => {
    return {
      title: isFirst ? ArticleStorage.getTitle() ?? '' : '',
      description: isFirst ? ArticleStorage.getDescription() ?? '' : '',
      html: isFirst ? ArticleStorage.getHtml() ?? '' : '',
      tags: isFirst ? JSON.parse(ArticleStorage.getTags()) ?? [] as string[] : articleSet?.data?.tags ?? [] as string[],
      originalLink: isFirst ? ArticleStorage.getLink() ?? '' : articleSet?.data?.originalArticleLink ?? ''
    } as IArticleCreateExtendedRequest
  },[id, articleSet.data, isFirst]);

  useEffect(() =>{
    setArticle(getDefaultState(isFirst))
  }, [id, articleSet.data, isFirst])

  const [article, setArticle] = useState<IArticleCreateExtendedRequest>(getDefaultState(isFirst));
  const languagesAccessor = useQuery<LanguageModel[], ApiError>(QueryClientKeysHelper.Languages(), () => _languagesApi.getAll())



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
      articleSet,
      defaultArticleState: (isFirst: boolean) => getDefaultState(isFirst),
      errors,
      setErrors,
      isFirst
    }
  }, [article, setArticle, setArticleValue, languagesAccessor, articleSet, errors, setErrors])

  return (
    <ArticleCreatingContext.Provider value={value}>
      {children}
    </ArticleCreatingContext.Provider>
  );
};

export default ArticleCreatingProvider;