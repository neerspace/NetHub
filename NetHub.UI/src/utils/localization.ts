import Localizations from "../constants/localizations";
import moment from "moment/moment";
import { ILanguage } from "../vite-env";

export const switchLocal = (language: ILanguage) => {
  switch (language) {
    case Localizations.Ukrainian:
      moment.locale(Localizations.Ukrainian);
      break;
    case Localizations.English:
      moment.locale(Localizations.English);
      break;
  }
};
