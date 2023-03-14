import React from 'react';
import { useByYouContext } from "../../../pages/ByYou/ByYouSpace.Provider";
import ArticleShort from "../Shared/ArticleShort";
import SavedArticlesSkeleton from "../Saved/SavedArticlesSkeleton";
import ErrorBlock from "../../Layout/ErrorBlock";
import { ISimpleArticle } from "../../../types/api/ISimpleArticle";
import { _myArticlesApi } from "../../../api";

const ByYouArticles = () => {
  const {articlesAccessor, setArticles} = useByYouContext();

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
  }


  return !articlesAccessor.isSuccess
    ? <SavedArticlesSkeleton/>
    : articlesAccessor.data.length === 0
      ? <ErrorBlock>Упс, Ви ще не написали жодної статі</ErrorBlock>
      : articlesAccessor.data!.map((article) => (
          <ArticleShort
            key={article.articleSetId + article.languageCode}
            article={article}
            setArticle={handleSetArticle}
            afterCounterRequest={afterCounterRequest(article)}
            save={{
              actual: true,
              handle: async () =>
                await saveHandle(
                  article.articleSetId,
                  article.languageCode
                ),
            }}
            time={{before: 'створено'}}
            footerVariant={"created"}
          />
        )
      )
};

export default ByYouArticles;