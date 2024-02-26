import { Component, ViewChild, Inject } from '@angular/core';
import { DataManager, WebApiAdaptor } from '@syncfusion/ej2-data';
import { GridComponent, EditSettingsModel, ToolbarItems } from '@syncfusion/ej2-angular-grids';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public data?: DataManager;
  public toolbar?: ToolbarItems[];
  public editSettings?: EditSettingsModel;

  @ViewChild('grid')
  public grid?: GridComponent;

  ngOnInit(): void {
    this.data = new DataManager({
      url: 'api/Orders',
      adaptor: new WebApiAdaptor(),
      crossDomain: true
    });
    this.toolbar = ['Add', 'Edit', 'Update', 'Delete', 'Cancel'];
    this.editSettings = {
      allowAdding: true, allowDeleting: true, allowEditing: true, mode: 'Normal'
    }
  }
}
