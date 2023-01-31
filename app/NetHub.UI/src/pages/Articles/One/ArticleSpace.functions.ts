import {articlesApi, userApi} from "../../../api/api";
import {IContributor} from "../../../types/api/Article/IArticleLocalizationResponse";
import IUserInfoResponse from "../../../types/api/User/IUserInfoResponse";
import {articleUserRoles} from "../../../constants/article";


export async function getArticle(id: string) {
  return await articlesApi.getArticle(id);
}

export async function getLocalization(id: string, code: string) {
  return await articlesApi.getLocalization(id, code)
}

export async function getArticleContributors(contributors: IContributor[]) {
  const usernames = contributors.map((contributor: IContributor) => contributor.userName);
  const users = await userApi.getUsersInfo(usernames);

  return contributors.map(c => {
    const user = users.find(u => u.userName === c.userName)!;
    return {...user, role: articleUserRoles.find(r => r.en.toLowerCase() === c.role.toLowerCase())?.ua ?? c.role}
  });
}

export async function getArticleActions(id: string, code: string) {
  const [isSaved, rate] = await Promise.all([
    articlesApi.isArticleSavedByUser(id, code),
    articlesApi.getRate(id)
  ]);
  return {isSaved, rate: rate.rating};
}

export function getAuthor(contributors: IContributor[], users: IUserInfoResponse[]) {
  const authorUserName = contributors.find(a => a.role === 'Author')?.userName;

  return users.find(u => u.userName === authorUserName);
}
