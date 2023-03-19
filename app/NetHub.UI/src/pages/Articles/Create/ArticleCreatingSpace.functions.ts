import { ArticleCreateRequest } from "../../../api/_api";
import { _articlesApi } from "../../../api";
import { UkrainianLanguage } from "../../../utils/constants";
import { ArticleStorage } from "../../../utils/localStorageProvider";
import { CreateArticleFormSchema } from "../../../types/schemas/Article/CreateArticleFormSchema";
import useCustomSnackbar from "../../../hooks/useCustomSnackbar";
import { useNavigate, useParams } from "react-router-dom";
import {
  IArticleCreateExtendedRequest,
  useArticleCreatingContext
} from "./ArticleCreatingSpace.Provider";
import { useMutation } from "react-query";
import { IMainArticleHandle } from "../../../components/Article/Create/CreateArticleForm";
import { ApiError } from "../../../types/ApiError";

export const useArticleCreatingSpace = (articleCreationRef: React.RefObject<IMainArticleHandle>) => {
  const {enqueueSuccess, enqueueError, enqueueSnackBar} = useCustomSnackbar('info');
  const navigate = useNavigate();
  const {
    article,
    setArticle,
    setErrors,
    isFirst
  } = useArticleCreatingContext();
  const createMutation = useMutation('createArticle', () => createArticle());
  const {id} = useParams();

  const createArticle = async () => {
    let articleSetId = id;
    const tinyRef = articleCreationRef.current?.getTinyRef()?.current!;


    if (isFirst) {
      article.language = UkrainianLanguage;
      articleSetId = (await tinyRef.createArticleSet(article))!.toString();
    }

    if (!await validateArticleForm()) return;
    enqueueSnackBar('Стаття зберігається')

    try {
      await tinyRef?.uploadImages(+articleSetId!);

      //get actual data with remote links to photos and update html
      const request = new ArticleCreateRequest(article);
      request.html = tinyRef.getHtmlBody();

      const result = await _articlesApi.create(articleSetId!, article.language!, request);

      ArticleStorage.clearArticleData();
      setArticle({} as IArticleCreateExtendedRequest);

      enqueueSuccess('Збережено!')
      navigate('/article/' + result.articleSetId + '/' + result.languageCode);
    } catch (e: any) {
      if (e.message.includes('exists')) {
        enqueueError('Стаття з такою мовою вже існує');
        return;
      }
      enqueueError('Помилка збереження статті');
      return;
    }
  };

  async function validateArticleForm() {
    const validationResult = CreateArticleFormSchema.safeParse(article);

    if (!validationResult.success) {
      const errors = validationResult.error.format()
      setErrors(errors);
      errors._errors.forEach(e => enqueueError(e))
      console.log('errors', errors)
      return;
    }

    setErrors({_errors: []});

    return validationResult.success;
  }

  return {
    createArticle: createMutation
  }
}