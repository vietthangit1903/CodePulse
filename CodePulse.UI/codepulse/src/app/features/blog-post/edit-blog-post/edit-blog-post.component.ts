import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { BlogPostService } from '../services/blog-post.service';
import { BlogPost } from '../models/blog-post.model';
import { CategoryService } from '../../category/services/category.service';
import { Category } from '../../category/models/category.model';
import { UpdateBlogPost } from '../models/update-blog-post';

@Component({
  selector: 'app-edit-blog-post',
  templateUrl: './edit-blog-post.component.html',
  styleUrls: ['./edit-blog-post.component.css'],
})
export class EditBlogPostComponent implements OnInit, OnDestroy {
  id: string | null = null;
  editPost?: BlogPost;
  categories$?: Observable<Category[]>;
  selectedCategories?: string[];
  routeSubscription?: Subscription;
  getBlogPostSubscription?: Subscription;
  updateBlogPostSubscription?: Subscription;

  constructor(
    private route: ActivatedRoute,
    private blogPostService: BlogPostService,
    private categoryService: CategoryService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.categories$ = this.categoryService.getAllCategories();

    this.routeSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');

        if (this.id) {
          this.getBlogPostSubscription = this.blogPostService
            .getBlogPostById(this.id)
            .subscribe({
              next: (response) => {
                this.editPost = response;
                this.selectedCategories = response.categories.map((e) => e.id);
              },
            });
        }
      },
    });
  }

  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
    this.getBlogPostSubscription?.unsubscribe();
    this.updateBlogPostSubscription?.unsubscribe();
  }

  onFormSubmit(): void {
    if (this.editPost && this.editPost.id) {
      var updateBlogPost: UpdateBlogPost = {
        title: this.editPost.title,
        shortDescription: this.editPost.shortDescription,
        content: this.editPost.content,
        featuredImageUrl: this.editPost.featuredImageUrl,
        urlHandle: this.editPost.urlHandle,
        author: this.editPost.author,
        publishedDate: this.editPost.publishedDate,
        isVisible: this.editPost.isVisible,
        categories: this.selectedCategories ?? [],
      };
      this.updateBlogPostSubscription = this.blogPostService
        .updateBlogPost(this.editPost.id, updateBlogPost)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl('admin/blog-posts');
          },
        });
    }
  }
}
