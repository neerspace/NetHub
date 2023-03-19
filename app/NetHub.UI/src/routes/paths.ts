import ArticlesThreadSpace from '../pages/Articles/Thread/ArticlesThreadSpace';
import ArticleSpace from "../pages/Articles/One/ArticleSpace";
import ArticleCreatingSpace from '../pages/Articles/Create/ArticleCreatingSpace';
import SavedSpace from "../pages/Saved/SavedSpace";
import ProfileSpace from "../pages/Profile/ProfileSpace";
import ContributorArticlesSpace from "../pages/Articles/Contributor/ContributorArticlesSpace";
import TestSpace from "../pages/TestSpace";
import AuthSpace from "../pages/Auth/AuthSpace";
import TeamSpace from "../pages/Team/TeamSpace";
import ByYouSpace from "../pages/ByYou/ByYouSpace";
import { IPage } from "../components/Layout/Dynamic";

interface IPath {
  path: string,
  Component: IPage,
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
    path: '/article/add',
    Component: ArticleCreatingSpace,
    requireAuthorization: true,
  },
  {
    path: '/article/:id/translate',
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
    path: '/by-you',
    Component: ByYouSpace,
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
    path: '/article/creator/:contributorUsername',
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
