import { Component, OnDestroy, OnInit } from '@angular/core';
import { Message } from '../Shared/Models/message';
import { ChatService } from '../Shared/Service/chat.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-messenger',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './messenger.component.html',
  styleUrl: './messenger.component.css'
})
export class MessengerComponent implements OnInit,OnDestroy {
  messages: Message[] = [];
  username = '';
  message = '';

  constructor(private chatService: ChatService) {}

  ngOnInit() {
    this.chatService.setupSignalRConnection();
    this.chatService.getStartMessages();

    this.chatService.messages$.subscribe(
      messages => this.messages = messages
    );
  }

  ngOnDestroy() {
    this.chatService.disconectSignalR();
  }

  sendMessage() {
    if (this.username && this.message) {
      this.chatService.sendMessage(this.username, this.message)
        .then(() => {
          this.message = '';
        });
    }
  }

  getSentimentClass(sentiment: number): string {
    switch (sentiment) {
      case 1: return 'message-positive';
      case 2: return 'message-negative';
      default: return 'message-neutral';
    }
  }
}
