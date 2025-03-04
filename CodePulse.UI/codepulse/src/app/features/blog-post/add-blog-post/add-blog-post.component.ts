import { Component, OnDestroy, OnInit } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-post.model';
import { BlogPostService } from '../services/blog-post.service';
import { Router } from '@angular/router';
import { CategoryService } from '../../category/services/category.service';
import { Observable, Subscription } from 'rxjs';
import { Category } from '../../category/models/category.model';
import { SharedService } from '../services/shared.service';

@Component({
  selector: 'app-add-blog-post',
  templateUrl: './add-blog-post.component.html',
  styleUrls: ['./add-blog-post.component.css'],
})
export class AddBlogPostComponent implements OnInit, OnDestroy {
  model: AddBlogPost;
  categories$?: Observable<Category[]>
  isImageSelectorModalOpen: boolean = false;
  shareServiceSubscription?: Subscription;

  constructor(
    private _blogPostService: BlogPostService,
    private _categoriesService: CategoryService,
    private _router: Router,
    private shareService: SharedService
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
    this.shareServiceSubscription = this.shareService.data$.subscribe({
      next: (value) => {
        if (this.model) {
          this.model.featuredImageUrl = value.url;
          this.closeImageSelector();
        }
      },
    });
  }

  ngOnDestroy(): void {
    this.shareServiceSubscription?.unsubscribe();
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

  openImageSelector(): void {
    this.isImageSelectorModalOpen = true;
  }

  closeImageSelector(): void {
    this.isImageSelectorModalOpen = false;
  }
}
