import React, { FC } from 'react';
import cl from './ArticlesList.module.sass';
import { useQueryClient } from "react-query";
import ErrorBlock from "../../UI/Error/ErrorBlock";
import ArticleShort from "../Shared/ArticleShort";
import { _myArticlesApi } from "../../../api";
import { ISimpleArticle } from "../../../types/api/ISimpleArticle";
import { QueryClientKeysHelper } from "../../../utils/QueryClientKeysHelper";

interface IArticlesThreadProps {
  articles: ISimpleArticle[],
  setArticles: (articles: ISimpleArticle[]) => void
  byUser?: boolean
}

const ArticlesList: FC<IArticlesThreadProps> = ({articles, setArticles, byUser}) => {

  const queryClient = useQueryClient();

  const handleSaving = (article: ISimpleArticle) => async () => {
    await _myArticlesApi.toggleSave(article.articleSetId, article.languageCode);
    setArticles(articles.map((a) => a.articleSetId === article.articleSetId
      ? {...a, isSaved: !a.isSaved} : a));
    await queryClient.invalidateQueries(QueryClientKeysHelper.SavedArticles());
    await queryClient.invalidateQueries(QueryClientKeysHelper.Article(article.articleSetId, article.languageCode));
  }

  const handleSetArticle = (article: ISimpleArticle) => {
    setArticles(articles.map((a) => a.articleSetId === article.articleSetId ? article : a));
  }

  const afterRequest = (item: ISimpleArticle) => async function () {
    await queryClient.invalidateQueries(QueryClientKeysHelper.ArticleSet(item.articleSetId));
    await queryClient.invalidateQueries(QueryClientKeysHelper.Article(item.articleSetId, item.languageCode));
    await queryClient.invalidateQueries(QueryClientKeysHelper.SavedArticles());
    await queryClient.invalidateQueries(QueryClientKeysHelper.ArticlesByYou());
  }

  return (
    <div className={cl.thread}>
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

export default ArticlesList;
