import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ImageStreamingService {

  constructor(private httpClient: HttpClient) {
  }

  startImageStreaming(): void {
    this.httpClient.post('https://localhost:5001/Image/Start', null)
      .subscribe(response => console.log(response));
  }

}
