import { Component } from '@angular/core';
import { AddCategoryRequest } from '../models/add-category-request.model';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.css'],
})
export class AddCategoryComponent {
  addCategoryModel: AddCategoryRequest;

  constructor() {
    this.addCategoryModel = {
      name: '',
      urlHandle: '',
    };
  }

  onFormSubmit() {
    console.log(this.addCategoryModel);
  }
}
