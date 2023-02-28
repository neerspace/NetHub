import React, { FC } from 'react';
import classes from './ArticlesThread.module.sass';
import { useQueryClient } from "react-query";
import ErrorBlock from "../../Layout/ErrorBlock";
import ArticleShort from "../Shared/ArticleShort";
import { QueryClientConstants } from "../../../constants/queryClientConstants";
import { _myArticlesApi } from "../../../api";
import { ISimpleLocalization } from "../../../types/api/ISimpleLocalization";

interface IArticlesThreadProps {
  articles: ISimpleLocalization[],
  setArticles: (articles: ISimpleLocalization[]) => void
  byUser?: boolean
}

const ArticlesThread: FC<IArticlesThreadProps> = ({articles, setArticles, byUser}) => {

  const queryClient = useQueryClient();

  const handleSaving = (localization: ISimpleLocalization) => async () => {
    await _myArticlesApi.toggleSave(localization.articleId, localization.languageCode);
    setArticles(articles.map((a) => a.id === localization.id
      ? {...a, isSaved: !a.isSaved} : a));
    await queryClient.invalidateQueries(QueryClientConstants.savedArticles);
    await queryClient.invalidateQueries([QueryClientConstants.articleLocalization, localization.articleId, localization.languageCode]);
  }

  const handleSetLocalization = (localization: ISimpleLocalization) => {
    setArticles(articles.map((a) => a.id === localization.id ? localization : a));
  }

  const afterRequest = (item: ISimpleLocalization) => async function () {
    await queryClient.invalidateQueries(QueryClientConstants.savedArticles);
    await queryClient.invalidateQueries([QueryClientConstants.articleLocalization, item.articleId, item.languageCode]);
    await queryClient.invalidateQueries([QueryClientConstants.article, item.articleId]);
  }

  return (
    <div className={classes.thread}>
      {articles.length > 0
        ? articles.map((item) => (
          <ArticleShort
            key={item.id}
            localization={item}
            setLocalization={handleSetLocalization}
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
