import React from 'react';
import { useQueryClient } from 'react-query';
import { CSSTransition, TransitionGroup } from 'react-transition-group';
import { useSavedArticlesContext } from '../../../pages/Saved/SavedSpace.Provider';
import ErrorBlock from '../../Layout/ErrorBlock';
import ArticleShort from '../Shared/ArticleShort';
import cl from './SavedArticles.module.sass';
import SavedArticlesSkeleton from './SavedArticlesSkeleton';
import './transitions.css';
import { _myArticlesApi } from "../../../api";
import { ISimpleArticle } from "../../../types/api/ISimpleArticle";
import { QueryClientKeysHelper } from "../../../utils/QueryClientKeysHelper";

const SavedArticles = () => {
  const { savedArticles, setSavedArticles } = useSavedArticlesContext();
  const queryClient = useQueryClient();

  async function handleSetArticle(article: ISimpleArticle) {
    setSavedArticles(
      savedArticles.data!.map((a) =>
        a.id === article.id ? article : a
      )
    );
  }

  async function removeFromSavedHandle(id: number, code: string) {
    await _myArticlesApi.toggleSave(id, code);
    const savedArticleIndex = savedArticles.data!.findIndex(
      (a) => a.articleSetId === id && a.languageCode === code
    );
    setSavedArticles(
      savedArticles.data!.filter((a, index) => index !== savedArticleIndex)
    );
  }

  const afterCounterRequest = (article: ISimpleArticle) =>
    async function () {
      await queryClient.invalidateQueries(QueryClientKeysHelper.Keys.articles);
      await queryClient.invalidateQueries(QueryClientKeysHelper.ArticleSet(article.id));
      await queryClient.invalidateQueries(QueryClientKeysHelper.Article(article.id, article.languageCode));
    };

  return !savedArticles.isSuccess ? (
    <SavedArticlesSkeleton />
  ) : savedArticles.data.length === 0 ? (
    <ErrorBlock>Упс, Ви ще не зберегли статтей</ErrorBlock>
  ) : (
    <TransitionGroup className={cl.savedWrapper}>
      {savedArticles.data!.map((article) => (
        <CSSTransition
          key={article.articleSetId}
          timeout={500}
          classNames='item'
        >
          <ArticleShort
            article={article}
            setArticle={handleSetArticle}
            afterCounterRequest={afterCounterRequest(article)}
            save={{
              actual: true,
              handle: async () =>
                await removeFromSavedHandle(
                  article.articleSetId,
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
