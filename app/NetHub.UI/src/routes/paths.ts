import ArticlesListSpace from '../pages/Articles/Thread/ArticlesListSpace';
import ArticleSpace from "../pages/Articles/One/ArticleSpace";
import ArticleCreateSpace from '../pages/Articles/Create/ArticleCreateSpace';
import SavedSpace from "../pages/Saved/SavedSpace";
import ProfileSpace from "../pages/Profile/ProfileSpace";
import CreatorArticlesSpace from "../pages/Articles/Creator/CreatorArticlesSpace";
import TestSpace from "../pages/TestSpace";
import LoginSpace from "../pages/Login/LoginSpace";
import TeamSpace from "../pages/Team/TeamSpace";
import ByYouSpace from "../pages/ByYou/ByYouSpace";
import { IPage } from "../components/Dynamic/Dynamic";

interface IPath {
  path: string,
  Component: IPage,
  requireAuthorization: boolean,
}

export const paths: IPath[] = [
  {
    path: '/',
    Component: ArticlesListSpace,
    requireAuthorization: false,
  },
  {
    path: '/login',
    Component: LoginSpace,
    requireAuthorization: false
  },
  {
    path: '/article/add',
    Component: ArticleCreateSpace,
    requireAuthorization: true,
  },
  {
    path: '/article/:id/translate',
    Component: ArticleCreateSpace,
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
    Component: CreatorArticlesSpace,
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
