import {
  AfterContentInit,
  Component, ComponentFactoryResolver,
  ContentChildren,
  OnInit,
  QueryList, TemplateRef, Type,
  ViewChild, ViewContainerRef
} from '@angular/core';
import {TabComponent} from "./tab/tab.component";

@Component({
  selector: 'app-tabs',
  templateUrl: './tabs.component.html',
  styleUrls: ['./tabs.component.scss']
})
export class TabsComponent implements AfterContentInit {
  @ContentChildren(TabComponent) tabs: QueryList<TabComponent> = new QueryList<TabComponent>();
  @ViewChild('container', {read: ViewContainerRef}) dynamicTabsContainer!: ViewContainerRef;
  dynamicTabs: TabComponent[] = [];

  ngAfterContentInit(): void {

    let activeTabs = this.tabs.filter((tab) => tab.active);

    if (activeTabs.length === 0 && this.tabs.first) {
      this.selectTab(this.tabs.first);
    }
  }

  selectTab(tab: TabComponent) {
    this.tabs.toArray().forEach(tab => (tab.active = false));
    this.dynamicTabs.forEach(tab => (tab.active = false));

    tab.active = true;
  }

  openTab(title: string, template: TemplateRef<any>, data: any, isCloseable = false) {

    const tabType: Type<TabComponent> = TabComponent;
    const componentRef = this.dynamicTabsContainer.createComponent<TabComponent>(tabType);
    const instance: TabComponent = componentRef.instance as TabComponent;

    instance.title = title;
    instance.template = template;
    instance.dataContext = data;
    instance.isCloseable = isCloseable;
    instance.active = true;

    this.dynamicTabs.push(componentRef.instance as TabComponent);
    this.selectTab(this.dynamicTabs[0]);
  }

  closeTab(tab: TabComponent) {
    for (let i = 0; i < this.dynamicTabs.length; i++) {
      if (this.dynamicTabs[i] === tab) {
        this.dynamicTabs.splice(i, 1);

        this.dynamicTabsContainer.remove(i);

        if (this.tabs.first) {
          this.selectTab(this.tabs.first);
        } else if (this.dynamicTabs.length > 0) {
          this.selectTab(this.dynamicTabs[0])
        }

        break;
      }
    }
  }

  closeAllTabs() {
    this.dynamicTabs = [];
    this.dynamicTabsContainer.clear()
  }
}
