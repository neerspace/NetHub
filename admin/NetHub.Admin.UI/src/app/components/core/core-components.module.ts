import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { IconButtonComponent } from './button/icon-button.component';
import { DropdownComponent } from './dropdown/dropdown.component';
import { LoadingBarComponent } from './loading-bar/loading-bar.component';
import { ModalConfirmComponent } from './modal-confirm/modal-confirm.component';
import { ToastAreaComponent } from './toast/toast-area.component';
import { TabsComponent } from './tabs/tabs.component';
import {TabComponent} from "./tabs/tab/tab.component";

@NgModule({
  declarations: [
    IconButtonComponent,
    DropdownComponent,
    LoadingBarComponent,
    ModalConfirmComponent,
    ToastAreaComponent,
    TabComponent,
    TabsComponent,
  ],
  exports: [LoadingBarComponent, IconButtonComponent, ModalConfirmComponent, ToastAreaComponent, TabsComponent, TabComponent],
  imports: [CommonModule],
})
export class CoreComponentsModule {}
