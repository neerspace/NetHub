import ArticlesThreadSpace from '../pages/Articles/Thread/ArticlesThreadSpace';
import {Page} from "../components/Layout/Layout";
import ArticleSpace from "../pages/Articles/One/ArticleSpace";
import ArticleCreatingSpace from '../pages/Articles/Create/ArticleCreatingSpace';
import SavedSpace from "../pages/Saved/SavedSpace";
import ProfileSpace from "../pages/Profile/ProfileSpace";
import ContributorArticlesSpace from "../pages/Articles/Contributor/ContributorArticlesSpace";
import TestSpace from "../pages/TestSpace";
import AuthSpace from "../pages/Auth/AuthSpace";
import TeamSpace from "../pages/Team/TeamSpace";

interface IPath {
  path: string,
  Component: Page,
  requireAuthorization: boolean,
}

export const paths: IPath[] = [
  {
    path: '/',
    Component: ArticlesThreadSpace,
    requireAuthorization: false,
  },
  {
    path: '/login',
    Component: AuthSpace,
    requireAuthorization: false
  },
  {
    path: '/articles/add',
    Component: ArticleCreatingSpace,
    requireAuthorization: true,
  },
  {
    path: '/articles/:id/add-localization',
    Component: ArticleCreatingSpace,
    requireAuthorization: true
  },
  {
    path: '/article/:id/:code',
    Component: ArticleSpace,
    requireAuthorization: false
  },
  {
    path: '/saved',
    Component: SavedSpace,
    requireAuthorization: true
  },
  {
    path: '/profile',
    Component: ProfileSpace,
    requireAuthorization: true
  },
  {
    path: '/profile/:username',
    Component: ProfileSpace,
    requireAuthorization: false
  },
  {
    path: '/articles/by/:contributorId',
    Component: ContributorArticlesSpace,
    requireAuthorization: false
  },
  {
    path: '/team',
    Component: TeamSpace,
    requireAuthorization: false
  },
  {
    path: '/test',
    Component: TestSpace,
    requireAuthorization: false
  }
]
