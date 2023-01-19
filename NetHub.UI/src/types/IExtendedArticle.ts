import {ContentStatus} from "./ContentStatus";
import {RateVariants} from "../components/Article/Shared/ArticlesRateCounter";

export default interface IExtendedArticle {
  // [key: string]: any,
  userId?: number,
  isSaved?: boolean,
  savedDate?: string,
  vote?: RateVariants
  title: string,
  description: string,
  html: string,
  created: string,
  updated?: string
  published?: string
  banned?: string
  views: number,
  articleId: number,
  languageCode: string,
  status: ContentStatus,
  localizationId: number,
  rate: number
}
