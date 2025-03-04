import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SharedService {
  // Private BehaviorSubject to store the state
  private dataSubject = new BehaviorSubject<any>(undefined);

  data$ = this.dataSubject.asObservable();

  updateData(newValue: any): void {
    this.dataSubject.next(newValue);
  }

  getCurrentValue(): any {
    return this.dataSubject.getValue();
  }
}
