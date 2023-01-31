import IArticleLocalizationResponse from "./IArticleLocalizationResponse";

export default interface IArticleResponse {
  id: number,
  name: string,
  authorId: number,
  created: string,
  updated?: string
  originalArticleLink?: string,
  rate: number,
  localizations?: IArticleLocalizationResponse[],
  imagesLinks?: string[],
  tags: string[]
}
