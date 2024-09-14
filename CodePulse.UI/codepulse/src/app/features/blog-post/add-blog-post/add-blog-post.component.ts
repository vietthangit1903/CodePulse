import { Component } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-post.model';
import { BlogPostService } from '../services/blog-post.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-blog-post',
  templateUrl: './add-blog-post.component.html',
  styleUrls: ['./add-blog-post.component.css'],
})
export class AddBlogPostComponent {
  model: AddBlogPost;
  constructor(
    private _blogPostService: BlogPostService,
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
    };
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
