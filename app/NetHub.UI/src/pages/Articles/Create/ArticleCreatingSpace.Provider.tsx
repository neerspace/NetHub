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
  ArticleSetModel, ArticleSetModelExtended,
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
  originalArticleLink: string | null,
  language: string
}

interface ContextType {
  article: IArticleCreateExtendedRequest,
  setArticle: React.Dispatch<React.SetStateAction<IArticleCreateExtendedRequest>>,
  setArticleValue: (key: keyof IArticleCreateExtendedRequest) => (value: any) => void,
  languagesAccessor: UseQueryResult<LanguageModel[], ApiError>,
  articleSet?: UseQueryResult<ArticleSetModelExtended, ApiError>,
  errors: CreateArticleFormError,
  setErrors: (errors: CreateArticleFormError) => void,
  isFirst: boolean
}

const InitialContextValue: ContextType = {
  article: {} as IArticleCreateExtendedRequest,
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
  originalLink: string | null,
  language: string
}>;

const ArticleCreatingProvider: FC<PropsWithChildren> = ({children}) => {
  const {id} = useParams();
  const isFirst = !id;

  const articleSet = useQuery<ArticleSetModel, ApiError>(QueryClientKeysHelper.ArticleSet(+id!), () => _articlesSetsApi.getById(+id!),
    {
      enabled: !isFirst, onSuccess: (result) => {
        setArticle(prev => ({
            ...prev,
            tags: result.tags,
            originalArticleLink: result.originalArticleLink ?? ''
          })
        )
      },
    });


  const [article, setArticle] = useState<IArticleCreateExtendedRequest>(
    {
      title: isFirst ? ArticleStorage.getTitle() ?? '' : '',
      description: isFirst ? ArticleStorage.getDescription() ?? '' : '',
      html: isFirst ? ArticleStorage.getHtml() ?? '' : '',
      tags: JSON.parse(ArticleStorage.getTags() ?? '[]'),
      originalArticleLink: ArticleStorage.getLink() ?? ''
    } as IArticleCreateExtendedRequest
  );
  const languagesAccessor = useQuery<LanguageModel[], ApiError>(QueryClientKeysHelper.Languages(), () => _languagesApi.getAll())

  const [errors, setErrors] = useState<CreateArticleFormError>({_errors: []});
  useEffect(() => {
    setErrors({_errors: []})

    //refetch every time articleSet id changes
    if (isFirst)
      setArticle({
        ...article,
        tags: JSON.parse(ArticleStorage.getTags() ?? '[]'),
        originalArticleLink: ArticleStorage.getLink() ?? ''
      })
    else
      articleSet.refetch()
  }, [id])

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