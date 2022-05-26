import { Injectable } from '@angular/core';
import { Message, MessageService } from 'primeng/api';

@Injectable({
  providedIn: 'root',
})
export class MessageToastService {
  public constructor(private messageService: MessageService) {}

  public show(message: Message) {
    this.messageService.add(message);
  }
}
