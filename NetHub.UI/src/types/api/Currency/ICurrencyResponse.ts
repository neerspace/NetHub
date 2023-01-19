export default interface ICurrencyResponse {
  exchanges: IExchangesResponse,
  crypto: ICryptoResponse,
  updated: string
}

export interface IExchangesResponse {
  usd: IExchangeObject,
  euro: IExchangeObject
}

interface IExchangeObject {
  currencyFrom: string,
  currencyTo: string,
  date: number,
  rateBuy: number,
  rateSell: number
}

export interface ICryptoResponse {
  btc: ICryptoObject,
  ton: ICryptoObject
}

interface ICryptoObject {
  usd: number,
  usd24Change: number,
  uah: number,
  uah24Change: number
}
