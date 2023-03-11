import React, { FC } from 'react';
import classes from './ArticlesThread.module.sass';
import { useQueryClient } from "react-query";
import ErrorBlock from "../../Layout/ErrorBlock";
import ArticleShort from "../Shared/ArticleShort";
import { _myArticlesApi } from "../../../api";
import { ISimpleArticle } from "../../../types/api/ISimpleArticle";
import { QueryClientKeysHelper } from "../../../utils/QueryClientKeysHelper";

interface IArticlesThreadProps {
  articles: ISimpleArticle[],
  setArticles: (articles: ISimpleArticle[]) => void
  byUser?: boolean
}

const ArticlesThread: FC<IArticlesThreadProps> = ({articles, setArticles, byUser}) => {

  const queryClient = useQueryClient();

  const handleSaving = (article: ISimpleArticle) => async () => {
    await _myArticlesApi.toggleSave(article.id, article.languageCode);
    setArticles(articles.map((a) => a.articleSetId === article.articleSetId
      ? {...a, isSaved: !a.isSaved} : a));
    await queryClient.invalidateQueries(QueryClientKeysHelper.SavedArticles());
    await queryClient.invalidateQueries(QueryClientKeysHelper.Article(article.id, article.languageCode));
  }

  const handleSetArticle = (article: ISimpleArticle) => {
    setArticles(articles.map((a) => a.articleSetId === article.articleSetId ? article : a));
  }

  const afterRequest = (item: ISimpleArticle) => async function () {
    await queryClient.invalidateQueries(QueryClientKeysHelper.ArticleSet(item.id));
    await queryClient.invalidateQueries(QueryClientKeysHelper.Article(item.id, item.languageCode));
    await queryClient.invalidateQueries(QueryClientKeysHelper.SavedArticles());
  }

  return (
    <div className={classes.thread}>
      {articles.length > 0
        ? articles.map((item) => (
          <ArticleShort
            key={item.articleSetId + item.languageCode}
            article={item}
            setArticle={handleSetArticle}
            save={{actual: item.isSaved ?? false, handle: handleSaving(item)}}
            afterCounterRequest={afterRequest(item)}
          />
        ))
        : <ErrorBlock>
          {
            (byUser ?? false)
              ? 'Користувач ще не написав жодної статті'
              : 'На платформі ще немає статтей'
          }
        </ErrorBlock>
      }
    </div>
  );
};

export default ArticlesThread;
