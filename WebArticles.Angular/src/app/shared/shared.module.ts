import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from './material/material.module';
import { NotFoundComponent } from '../user/not-found/not-found.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EditDialogComponent } from './edit-dialog/edit-dialog.component';
import { DeleteDialogComponent } from './delete-dialog/delete-dialog.component';
import { AlertDialogComponent } from './alert-dialog/alert-dialog.component';



@NgModule({
  declarations: [
    NotFoundComponent,
    AlertDialogComponent,
    DeleteDialogComponent,
    EditDialogComponent,
  ],
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  entryComponents: [
    EditDialogComponent,
    DeleteDialogComponent,
    AlertDialogComponent
  ],
  exports: [
    MaterialModule,
    NotFoundComponent,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    AlertDialogComponent,
    DeleteDialogComponent,
    EditDialogComponent,
  ]
})
export class SharedModule { }
