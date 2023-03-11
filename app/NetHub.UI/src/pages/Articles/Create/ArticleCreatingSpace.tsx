import React, { useRef } from 'react';
import ArticleSettings from '../../../components/Article/Create/ArticleSettings';
import Layout, { Page } from "../../../components/Layout/Layout";
import CreateArticleForm from "../../../components/Article/Create/CreateArticleForm";
import { useTranslation } from "react-i18next";
import useCustomSnackbar from "../../../hooks/useCustomSnackbar";
import { ArticleStorage } from "../../../utils/localStorageProvider";
import { useNavigate } from "react-router-dom";
import { useMutation } from 'react-query';
import ArticleCreatingSpaceProvider, {
  useArticleCreatingContext
} from "./ArticleCreatingSpace.Provider";
import { CreateArticleFormSchema } from "../../../types/schemas/Article/CreateArticleFormSchema";
import { _articlesApi } from "../../../api";
import { UkrainianLanguage } from "../../../utils/constants";
import { ArticleCreateRequest } from "../../../api/_api";

type CreateArticleFormRef = React.ElementRef<typeof CreateArticleForm>

const ArticleCreatingSpace: Page = () => {
  const {t} = useTranslation();

  const {article, setArticle, defaultArticleState, setErrors} = useArticleCreatingContext();

  const createMutation = useMutation('createArticle', () => createArticle());
  const navigate = useNavigate();

  const {enqueueSuccess, enqueueError, enqueueSnackBar} = useCustomSnackbar('info');
  const articleCreationRef = useRef<CreateArticleFormRef>(null);

  async function validateArticleForm() {
    const validationResult = CreateArticleFormSchema.safeParse(article);

    if (!validationResult.success) {
      const errors = validationResult.error.format()
      setErrors(errors);
      return;
    }

    setErrors({_errors: []});

    return validationResult.success;
  }

  const createArticle = async () => {

    if (!await validateArticleForm()) return;

    enqueueSnackBar('Стаття зберігається')

    let articleId;

    try {
      articleId = await articleCreationRef
        .current?.getTinyRef()
        .current?.saveImages(article);

      const request = new ArticleCreateRequest(article);

      const result = await _articlesApi.create(articleId!, UkrainianLanguage, request);

      ArticleStorage.clearArticleData();
      setArticle(defaultArticleState);

      enqueueSuccess('Збережено!')
      navigate('/article/' + result.articleSetId + '/' + result.languageCode);
    } catch (e: any) {
      enqueueError('Помилка збереження статті');
      return;
    }
  };

  const titles = {
    // Center: <h2>{t('article.create.mainSettings')}</h2>,
    Center: <h2>Створення статті</h2>,
    Right: <h2>Налаштування</h2>
  };

  return (
    <Layout Titles={titles}>
      <CreateArticleForm ref={articleCreationRef}/>
      <ArticleSettings createArticle={createMutation.mutateAsync}/>
    </Layout>
  );
}

ArticleCreatingSpace.Provider = ArticleCreatingSpaceProvider;

export default ArticleCreatingSpace;
