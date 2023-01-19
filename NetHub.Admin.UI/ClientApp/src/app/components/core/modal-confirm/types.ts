import { Variant } from '../button/types';

export interface IModalHandlers {
  confirmed?: () => void;
  closed?: () => void;
}

export interface IModalInfo extends IModalHandlers {
  title: string;
  text: string;
  closeText?: string;
  closeVariant?: Variant;
  confirmText?: string;
  confirmVariant?: Variant;
}
