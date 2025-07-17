import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BlogPostService } from '../../blog-post/services/blog-post.service';
import { Observable } from 'rxjs';
import { BlogPost } from '../../blog-post/models/blog-post.model';

@Component({
  selector: 'app-blog-details',
  templateUrl: './blog-details.component.html',
  styleUrls: ['./blog-details.component.css'],
})
export class BlogDetailsComponent implements OnInit {
  blogPost?: Observable<BlogPost>;

  constructor(
    private route: ActivatedRoute,
    private blogPostService: BlogPostService
  ) {}

  ngOnInit() {
    this.route.paramMap.subscribe({
      next: (params) => {
        const url = params.get('url');
        if (url) {
          this.blogPost = this.blogPostService.getBlogPostByUrl(url);
        }
      },
    });
  }
}
