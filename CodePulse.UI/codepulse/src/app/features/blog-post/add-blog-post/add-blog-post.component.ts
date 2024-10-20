import { Component, OnInit } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-post.model';
import { BlogPostService } from '../services/blog-post.service';
import { Router } from '@angular/router';
import { CategoryService } from '../../category/services/category.service';
import { Observable } from 'rxjs';
import { Category } from '../../category/models/category.model';

@Component({
  selector: 'app-add-blog-post',
  templateUrl: './add-blog-post.component.html',
  styleUrls: ['./add-blog-post.component.css'],
})
export class AddBlogPostComponent implements OnInit {
  model: AddBlogPost;
  categories$?: Observable<Category[]>
  constructor(
    private _blogPostService: BlogPostService,
    private _categoriesService: CategoryService,
    private _router: Router
  ) {
    this.model = {
      title: '',
      shortDescription: '',
      content: '',
      featuredImageUrl: '',
      urlHandle: '',
      author: '',
      publishedDate: new Date(),
      isVisible: true,
      categories: [],
    };
  }

  ngOnInit(): void {
    this.categories$ = this._categoriesService.getAllCategories();
  }

  onFormSubmit(): void {
    this._blogPostService.createBlogPost(this.model).subscribe({
      next: (response) => {
        this._router.navigateByUrl('admin/blog-posts');
      },
      error: (response) => {
        console.log(response);
      },
    });
  }
}
