import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css']
})
export class NotificationComponent {
  @Input() message: string = '';
  show: boolean = false;

  showNotification(message: string) {
    this.message = message;
    this.show = true;
    setTimeout(() => this.close(), 3000);
  }

  close() {
    this.show = false;
  }
}
