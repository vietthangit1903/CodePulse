import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { SharedService } from 'src/app/features/blog-post/services/shared.service';
import { ImageService } from './image.service';
import { from, Observable } from 'rxjs';
import { BlogImage } from '../../models/blog-image.model';
import { environment } from 'src/environments/environment.development';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-image-selector',
  templateUrl: './image-selector.component.html',
  styleUrls: ['./image-selector.component.css'],
})
export class ImageSelectorComponent implements OnInit {
  private file?: File;
  fileName: string = '';
  fileTitle: string = '';
  images?: BlogImage[];
  @ViewChild('form', { static: false }) imageUploadForm?: NgForm;

  constructor(
    private imageService: ImageService,
    private shareService: SharedService
  ) {}

  ngOnInit(): void {
    this.getImages();
  }

  onFileUploadChange(event: Event): void {
    const element = event.currentTarget as HTMLInputElement;
    this.file = element.files?.[0];
    var fileName = this.getFileName(this.file?.name || '');
    this.fileName = fileName;
    this.fileTitle = fileName;
  }

  onUploadImageFormSubmit() {
    if (this.file && this.fileName !== '' && this.fileTitle !== '') {
      this.imageService
        .uploadImage(this.file, this.fileName, this.fileTitle)
        .subscribe({
          next: (response) => {
            this.getImages();
            this.imageUploadForm?.resetForm();
          },
        });
    }
  }

  private getImages(): void {
    this.imageService.getAllImages().subscribe({
      next: (response) => {
        this.images = response.map((item) => ({
          ...item,
          url: `${environment.apiBaseUrl}${item.url}`,
        }));
      },
    });
  }

  selectImage(selectedImage: BlogImage): void {
    this.shareService.updateData(selectedImage);
  }

  private getFileName(fileName: string): string {
    return fileName.split('.').slice(0, -1).join('.') || fileName;
  }
}
