import { Component, ViewChild } from '@angular/core';
import { GridComponent, ToolbarItems, EditSettingsModel, EditService, ToolbarService} from '@syncfusion/ej2-angular-grids';
import { DataManager, WebApiAdaptor } from '@syncfusion/ej2-data';
import { GridModule,  } from '@syncfusion/ej2-angular-grids';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: true,   
  imports: [GridModule],  
  providers: [EditService, ToolbarService, ]

})
export class AppComponent {
  @ViewChild('grid')
  public grid?: GridComponent;
  public data?: DataManager;
  public editSettings?: EditSettingsModel;
  public toolbar?: ToolbarItems[];

  ngOnInit(): void {
    this.data = new DataManager({
      url: 'http://localhost:5070/api/Orders',
      adaptor: new WebApiAdaptor(),
      crossDomain:true
    });

    this.editSettings = { allowEditing: true, allowAdding: true, allowDeleting: true, mode: 'Normal' };
    this.toolbar = ['Add', 'Edit', 'Delete', 'Update', 'Cancel', 'Search'];
  }
}
