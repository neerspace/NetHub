import React from 'react';
import { useByYouContext } from "../../../pages/ByYou/ByYouSpace.Provider";
import ArticleShort from "../Shared/ArticleShort";
import ErrorBlock from "../../Layout/ErrorBlock";
import { ISimpleArticle } from "../../../types/api/ISimpleArticle";
import { _myArticlesApi } from "../../../api";
import { QueryClientKeysHelper } from "../../../utils/QueryClientKeysHelper";
import { useQueryClient } from "react-query";
import ArticlesShortSkeleton from '../Shared/ArticlesShortSkeleton';

const ByYouArticles = () => {
  const {articlesAccessor, setArticles} = useByYouContext();
  const queryClient = useQueryClient();

  async function handleSetArticle(article: ISimpleArticle) {
    setArticles(
      articlesAccessor.data!.map((a) =>
        a.id === article.id ? article : a
      )
    );
  }

  async function saveHandle(id: number, code: string) {
    await _myArticlesApi.toggleSave(id, code);
  }

  const afterCounterRequest = (article: ISimpleArticle) => async function () {
    await queryClient.invalidateQueries(QueryClientKeysHelper.Keys.articles);
    await queryClient.invalidateQueries(QueryClientKeysHelper.ArticleSet(article.articleSetId));
    await queryClient.invalidateQueries(QueryClientKeysHelper.Article(article.articleSetId, article.languageCode));
    await queryClient.invalidateQueries(QueryClientKeysHelper.SavedArticles());
  }


  return !articlesAccessor.isSuccess
    ? <ArticlesShortSkeleton/>
    : articlesAccessor.data.length === 0
      ? <ErrorBlock>Упс, Ви ще не написали жодної статі</ErrorBlock>
      : articlesAccessor.data!.map((article) => (
          <ArticleShort
            key={article.articleSetId + article.languageCode}
            article={article}
            setArticle={handleSetArticle}
            afterCounterRequest={afterCounterRequest(article)}
            save={{
              actual: article.isSaved,
              handle: async () =>
                await saveHandle(
                  article.articleSetId,
                  article.languageCode
                ),
            }}
            time={{before: 'створено'}}
            variant={'private'}
          />
        )
      )
};

export default ByYouArticles;