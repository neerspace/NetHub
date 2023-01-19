import {articlesApi} from "../../../api/api";

export async function loadSavedArticles(){
  return await articlesApi.getSavedArticlesByUser();
}
