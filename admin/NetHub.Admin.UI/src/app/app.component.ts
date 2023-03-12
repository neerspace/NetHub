import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { ViewportService } from 'neercms/services/viewport';
import { Subscription } from 'rxjs';
import { ThemeService } from './theme/theme.service';

@Component({
  selector: 'body',
  templateUrl: './app.component.html',
})
export class AppComponent implements OnInit, OnDestroy {
  private resizeEvent!: Subscription;

  constructor(
    private viewport: ViewportService,
    private changeDetectorRef: ChangeDetectorRef,
    private theme: ThemeService,
  ) {}

  ngOnInit(): void {
    this.resizeEvent = this.viewport.addResizeListener(() => {
      this.changeDetectorRef.detectChanges();
      console.log('Switch on ' + (this.viewport.isMobile ? 'mobile view' : 'desktop view'));
    });
    this.theme.refreshTheme();
  }

  ngOnDestroy(): void {
    this.viewport.dispose();
  }
}
