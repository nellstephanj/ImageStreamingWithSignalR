import {Injectable} from '@angular/core';
import * as signalR from '@microsoft/signalr';
import {ImageContent} from '../domain/image-content';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  private readonly hubConnection: signalR.HubConnection;
  image: string;


  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:5001/imageStreamHub')
      .configureLogging(signalR.LogLevel.Trace)
      .build();
  }

  startConnection = () => {
    this.hubConnection.start()
      .then(() => console.log('Connection with Image Streaming Hub started'))
      .catch(err => console.log(`Error while starting connection with Image Streaming Hub: ${err}`));
  };

  stopConnection = () => {
    if (this.hubConnection) {
      this.hubConnection.stop().then(() => {
        console.log('Connection closed with Image Streaming Hub');
      });
    }
  };

  addImageStreamListenerListener = () => {
    this.hubConnection.on('StreamImage', (content: ImageContent) => {
      this.image = "data:image/jpeg;base64," + content.binary;
    })
  };
}
