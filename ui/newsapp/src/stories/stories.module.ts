import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from 'src/material/material.module';
import { NeweststoriesComponent } from './components/neweststories/neweststories.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule ,ReactiveFormsModule} from '@angular/forms';
import { SorttablePipe } from './pipes/sorttable.pipe';


import { Pipe } from '@angular/core'; 



@NgModule({
  declarations: [
    NeweststoriesComponent,
    SorttablePipe
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,ReactiveFormsModule,
    MaterialModule
  ],
  providers: [ 
  ],
  exports:[
    NeweststoriesComponent
  ]

})
export class StoriesModule { }
