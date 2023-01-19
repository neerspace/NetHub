import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class DownloadService {
  downloadAsJson(exportName: string, exportObj: object) {
    const json = JSON.stringify(exportObj, null, 2);
    const dataStr = 'data:text/json;charset=utf-8,' + encodeURIComponent(json);
    const downloadAnchorNode = document.createElement('a');
    downloadAnchorNode.setAttribute('href', dataStr);
    downloadAnchorNode.setAttribute('download', 'nethub-' + exportName + '.json');
    document.body.appendChild(downloadAnchorNode); // required for firefox
    downloadAnchorNode.click();
    downloadAnchorNode.remove();
  }
}
