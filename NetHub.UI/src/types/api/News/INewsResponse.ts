export default interface INewsResponse {
  id: number;
  date: string;
  guid: IRenderedModel;
  slug: string;
  status: string;
  type: string;
  link: string;
  title: IRenderedModel;
  content: IRenderedModel;
  excerpt: IRenderedModel;
  author: number;
  featuredMedia: number;
  commentStatus: string;
  categories: number[];
  tags: number[];
  jetpackFeaturedMediaUrl: string;
  jetpackSharingEnabled: boolean;
}

interface IRenderedModel {
  rendered: string;
  protected: boolean | null;
}
