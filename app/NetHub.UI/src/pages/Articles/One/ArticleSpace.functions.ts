import { articleUserRoles } from "../../../constants/article";
import { IArticleContributorModel } from "../../../api/_api";
import { _usersApi } from "../../../api";

export async function getArticleContributors(contributors: IArticleContributorModel[]) {
  const usernames = contributors.map((contributor) => contributor.userName);
  const users = await _usersApi.usersInfo(usernames);

  return contributors.map(c => {
    const user = users.find(u => u.userName === c.userName)!;
    return {...user, role: articleUserRoles.find(r => r.en.toLowerCase() === c.role.toLowerCase())?.ua ?? c.role}
  });
}

export function getAuthor(contributors: IArticleContributorModel[], users: { userName: string }[]) {
  const authorUserName = contributors.find(a => a.role === 'Author')?.userName;

  return users.find(u => u.userName === authorUserName);
}
