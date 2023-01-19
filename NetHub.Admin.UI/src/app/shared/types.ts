export interface IDictionary<T> {
  [id: string]: T;
}

export type BoolInput = boolean | 'true' | 'false';

export interface IJwtPayload {
  id: string;
  username: string;
  role: string;
  permissions: string[];
}

export type RGB = `rgb(${number}, ${number}, ${number})`;
export type RGBA = `rgba(${number}, ${number}, ${number}, ${number})`;
export type HEX = `#${string}`;

export type color = RGB | RGBA | HEX;
