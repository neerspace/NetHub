import { Injectable } from '@angular/core';
import { Observable, of, tap } from 'rxjs';
import { EnumMetadataModel, MetadataApi } from '../api';

@Injectable({ providedIn: 'root' })
export class MetadataService {
  private contentStatuses?: EnumMetadataModel[];

  constructor(private readonly metadataApi: MetadataApi) {}

  getContentStatuses(): Observable<EnumMetadataModel[]> {
    if (this.contentStatuses) {
      return of(this.contentStatuses);
    }

    return this.metadataApi.getContentStatuses().pipe(
      tap(result => {
        this.contentStatuses = result;
      }),
    );
  }
}
