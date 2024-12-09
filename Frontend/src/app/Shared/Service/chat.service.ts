import { Injectable } from '@angular/core';
import {
  HttpTransportType,
  HubConnection,
  HubConnectionBuilder,
} from '@microsoft/signalr';
import { Message } from '../Models/message';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from '../../../environments/environment.prod';  
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class ChatService {
  private hubConnection!: HubConnection;  
  private messagesSubject = new BehaviorSubject<Message[]>([]);
  public messages$ = this.messagesSubject.asObservable();
  constructor(private http: HttpClient) {}

  public setupSignalRConnection(): void {
    this.hubConnection = new  HubConnectionBuilder()
      .withUrl(`${environment.apiUrl}/chatHub`, {
        skipNegotiation: true,
        transport:  HttpTransportType.WebSockets
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection.on('ReceiveMessage', (message: Message) => {
      const currentMessages = this.messagesSubject.getValue();
      this.messagesSubject.next([message, ...currentMessages]);
    });

    this.hubConnection.start()
    .catch((err: any) => console.error('SignalR connection error:', err));
  }

  public disconectSignalR(): void {
    if (this.hubConnection) {
      this.hubConnection.stop();
    }
  }

  public sendMessage(username: string, message: string): Promise<void> {
    return this.hubConnection.invoke('SendMessage', username, message)
      .catch((err:any) => console.error(err));
  }

  public getStartMessages(): void {
    this.http.get<Message[]>(`${environment.apiUrl}/api/message/messages`)
      .subscribe({
        next:(messages) => this.messagesSubject.next(messages),
        error:(error) => console.error('Failed to fetch messages:', error)
      });
  }

  public testApi ():Observable<any>
  {
    const api= environment.apiUrl2;
    return this.http.get<any>(`${api}/api/facts`)

  }
}
