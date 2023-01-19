import React, {FC} from 'react';
import classes from './ArticlesThread.module.sass';
import {articlesApi} from "../../../api/api";
import IExtendedArticle from "../../../types/IExtendedArticle";
import {useQueryClient} from "react-query";
import ErrorBlock from "../../Layout/ErrorBlock";
import ArticleShort from "../Shared/ArticleShort";
import {QueryClientConstants} from "../../../constants/queryClientConstants";

interface IArticlesThreadProps {
  articles: IExtendedArticle[],
  setArticles: (articles: IExtendedArticle[]) => void
  byUser?: boolean
}

const ArticlesThread: FC<IArticlesThreadProps> = ({articles, setArticles, byUser}) => {

  const queryClient = useQueryClient();

  const handleSaving = (localization: IExtendedArticle) => async () => {
    await articlesApi.toggleSavingLocalization(localization.articleId, localization.languageCode);
    setArticles(articles.map((a) => a.localizationId === localization.localizationId
      ? {...a, isSaved: !a.isSaved} : a));
    await queryClient.invalidateQueries(QueryClientConstants.savedArticles);
    await queryClient.invalidateQueries([QueryClientConstants.articleLocalization, localization.articleId, localization.languageCode]);
  }

  const handleSetLocalization = (localization: IExtendedArticle) => {
    setArticles(articles.map((a) => a.localizationId === localization.localizationId ? localization : a));
  }

  const afterRequest = (item: IExtendedArticle) => async function () {
    await queryClient.invalidateQueries(QueryClientConstants.savedArticles);
    await queryClient.invalidateQueries([QueryClientConstants.articleLocalization, item.articleId, item.languageCode]);
    await queryClient.invalidateQueries([QueryClientConstants.article, item.articleId]);
  }

  return (
    <div className={classes.thread}>
      {articles.length > 0
        ? articles.map((item) => (
          <ArticleShort
            key={item.localizationId}
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
