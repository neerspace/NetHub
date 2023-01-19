import create from 'zustand';
import Localizations from "../constants/localizations";
import {JWTStorage} from "../utils/localStorageProvider";
import {IReduxUser} from "../types/IReduxUser";
import {ILanguage} from "../vite-env";

interface IStoreInitialState {
  isLogin: boolean | null,
  user: IReduxUser,
  language: ILanguage
  login: (reduxUser: IReduxUser) => void,
  logout: () => void,
  updateProfile: (reduxUser: IReduxUser) => void,
  setLanguage: (language: ILanguage) => void
}

export const useAppStore = create<IStoreInitialState>(set => ({
  isLogin: null,
  user: {username: '', profilePhotoUrl: null, firstName: ''},
  language: Localizations.Ukrainian,
  login: (reduxUser: IReduxUser) => set({
    isLogin: true,
    user: reduxUser
  }),
  logout: () => {
    console.log('logout clear data')
    JWTStorage.clearTokensData();
    set({isLogin: false, user: {} as IReduxUser})
  },
  updateProfile: (reduxUser: IReduxUser) => set({user: reduxUser}),
  setLanguage: (language: ILanguage) => set({language})
}));
