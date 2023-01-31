import React from 'react';
import { useQueryClient } from 'react-query';
import { CSSTransition, TransitionGroup } from 'react-transition-group';
import { articlesApi } from '../../../api/api';
import { QueryClientConstants } from '../../../constants/queryClientConstants';
import { useSavedArticlesContext } from '../../../pages/Saved/SavedSpace.Provider';
import IExtendedArticle from '../../../types/IExtendedArticle';
import ErrorBlock from '../../Layout/ErrorBlock';
import ArticleShort from '../Shared/ArticleShort';
import cl from './SavedArticles.module.sass';
import SavedArticlesSkeleton from './SavedArticlesSkeleton';
import './transitions.css';

const SavedArticles = () => {
  const { savedArticles, setSavedArticles } = useSavedArticlesContext();
  const queryClient = useQueryClient();

  async function handleSetArticle(localization: IExtendedArticle) {
    setSavedArticles(
      savedArticles.data!.map((a) =>
        a.localizationId === localization.localizationId ? localization : a
      )
    );
  }

  async function removeFromSavedHandle(id: number, code: string) {
    await articlesApi.toggleSavingLocalization(id, code);
    const savedArticleIndex = savedArticles.data!.findIndex(
      (a) => a.articleId === id && a.languageCode === code
    );
    setSavedArticles(
      savedArticles.data!.filter((a, index) => index !== savedArticleIndex)
    );
  }

  const afterCounterRequest = (article: IExtendedArticle) =>
    async function () {
      await queryClient.invalidateQueries(QueryClientConstants.articles);
      await queryClient.invalidateQueries([
        QueryClientConstants.article,
        article.articleId,
      ]);

      await queryClient.invalidateQueries([
        QueryClientConstants.articleLocalization,
        article.articleId,
        article.languageCode,
      ]);
    };

  return !savedArticles.isSuccess ? (
    <SavedArticlesSkeleton />
  ) : savedArticles.data.length === 0 ? (
    <ErrorBlock>Упс, Ви ще не зберегли статтей</ErrorBlock>
  ) : (
    <TransitionGroup className={cl.savedWrapper}>
      {savedArticles.data!.map((article) => (
        <CSSTransition
          key={article.localizationId}
          timeout={500}
          classNames='item'
        >
          <ArticleShort
            localization={article}
            setLocalization={handleSetArticle}
            afterCounterRequest={afterCounterRequest(article)}
            save={{
              actual: true,
              handle: async () =>
                await removeFromSavedHandle(
                  article.articleId,
                  article.languageCode
                ),
            }}
            time={{ before: 'збережено', show: 'saved' }}
          />
        </CSSTransition>
      ))}
    </TransitionGroup>
  );
};

export default SavedArticles;
