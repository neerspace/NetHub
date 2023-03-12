import { ArticleCreateRequest } from "../../../api/_api";
import { _articlesApi } from "../../../api";
import { UkrainianLanguage } from "../../../utils/constants";
import { ArticleStorage } from "../../../utils/localStorageProvider";
import { CreateArticleFormSchema } from "../../../types/schemas/Article/CreateArticleFormSchema";
import useCustomSnackbar from "../../../hooks/useCustomSnackbar";
import { useNavigate, useParams } from "react-router-dom";
import { useArticleCreatingContext } from "./ArticleCreatingSpace.Provider";
import { useMutation } from "react-query";
import { IMainArticleHandle } from "../../../components/Article/Create/CreateArticleForm";

export const useArticleCreatingSpace = (articleCreationRef: React.RefObject<IMainArticleHandle>) => {
  const {enqueueSuccess, enqueueError, enqueueSnackBar} = useCustomSnackbar('info');
  const navigate = useNavigate();
  const {article, setArticle, defaultArticleState, setErrors, withoutSet} = useArticleCreatingContext();
  const createMutation = useMutation('createArticle', () => createArticle());
  const {id} = useParams();

  const createArticle = async () => {
    if (!await validateArticleForm()) return;
    enqueueSnackBar('Стаття зберігається')

    try {
      const tinyRef = articleCreationRef.current?.getTinyRef()?.current!;
      let articleSetId = id;

      if (withoutSet){
        article.language = UkrainianLanguage;
        articleSetId = (await tinyRef.createArticleSet(article))!.toString();
      }

      await tinyRef?.uploadImages(+articleSetId!);

      //get actual data with remove links to photos and update html
      const request = new ArticleCreateRequest(article);
      request.html = tinyRef.getHtmlBody();

      const result = await _articlesApi.create(articleSetId!, article.language!, request);

      ArticleStorage.clearArticleData();
      setArticle(defaultArticleState);

      enqueueSuccess('Збережено!')
      navigate('/article/' + result.articleSetId + '/' + result.languageCode);
    } catch (e: any) {
      console.log('e', e)
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

  return{
    createArticle: createMutation
  }
}