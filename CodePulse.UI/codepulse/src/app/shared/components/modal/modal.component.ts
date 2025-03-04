import {
  Component,
  EventEmitter,
  HostListener,
  Input,
  Output,
} from '@angular/core';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.css'],
})
export class ModalComponent {
  @Input() isOpen: boolean = false; // Controls modal visibility
  @Output() close = new EventEmitter<void>(); // Emits event when closing

  closeModal(): void {
    this.close.emit();
  }

  // Close modal when pressing Escape key
  @HostListener('document:keydown.escape')
  onEscPress(): void {
    this.closeModal();
  }
}
