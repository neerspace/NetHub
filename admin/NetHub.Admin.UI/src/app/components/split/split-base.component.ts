import {Injector} from '@angular/core';
import {IOutputAreaSizes} from 'angular-split';
import {StorageService} from "../../services/storage";
import {ViewportService} from "../../theme/viewport.service";
import {SettingsKey} from "../../services/storage/types";
import {ColumnInfo, ITableAction} from "../table/types";


export abstract class SplitBaseComponent<T> {
  private storage: StorageService;
  private device: ViewportService;

  firstComponentSize: number = 100;
  secondComponentSize: number = 0;

  protected constructor(injector: Injector, private splitSizesStorageKey: SettingsKey) {
    this.storage = injector.get(StorageService);
    this.device = injector.get(ViewportService);
    [this.firstComponentSize, this.secondComponentSize] = this.storage.get(this.splitSizesStorageKey) || [100, 0];
  }

  abstract get columns(): ColumnInfo[];

  abstract get buttons(): ITableAction<T>[];

  resize(newSizes: number[] | IOutputAreaSizes) {
    const listSize = newSizes[0] as number;
    const formSize = newSizes[1] as number;

    if (listSize < 20 && listSize < this.firstComponentSize) {
      this.firstComponentSize = 0;
      this.secondComponentSize = 100;
    } else if (formSize < 20 && formSize < this.secondComponentSize) {
      this.firstComponentSize = 100;
      this.secondComponentSize = 0;
    } else if (Math.abs(listSize - formSize) < 10) {
      this.firstComponentSize = 50;
      this.secondComponentSize = 50;
    } else {
      this.firstComponentSize = listSize;
      this.secondComponentSize = formSize;
    }

    this.storage.set(this.splitSizesStorageKey, [this.firstComponentSize, this.secondComponentSize]);

    return this.firstComponentSize;
  }

  maximizeForm() {
    if (this.device.isTablet) {
      this.firstComponentSize = this.resize([0, 100]);
    } else {
      this.firstComponentSize = this.resize([50, 50]);
    }
  }

  closeForm() {
    this.firstComponentSize = this.resize([100, 0]);
  }
}
