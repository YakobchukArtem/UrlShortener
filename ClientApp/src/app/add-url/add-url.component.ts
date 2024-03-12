import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthorizeService } from 'src/api-authorization/authorize.service';

@Component({
  selector: 'app-add-url',
  templateUrl: './add-url.component.html'
})
export class AddUrlComponent {
  newUrl: UrlLinkDTO = { longForm: '', createdBy: '' };
  errorMessage: string = '';

  constructor(
    private http: HttpClient,
    private authorizeService: AuthorizeService,
    @Inject('BASE_URL') private baseUrl: string
  ) {}

  addUrl() {
    this.errorMessage = '';
    this.authorizeService.getUser().subscribe(user => { 
      if (user) {
        this.newUrl.createdBy = user.name || '';
      } else {
        console.error('User information not available.');
      }
  
      const urlData = {
        longForm: this.newUrl.longForm,
        createdBy: this.newUrl.createdBy
      };
  
      this.http.post<any>(this.baseUrl + 'api/url', urlData)
        .subscribe(
          response => {
            // Очистити дані перед відправленням нового URL
            this.newUrl = { longForm: '', createdBy: '' };
          },
          error => {
            console.error('Error adding URL:', error);
            this.errorMessage = 'Error adding URL: ' + error.message;
          }
        );
    });
  }
}

interface UrlLinkDTO {
  longForm: string;
  createdBy: string;
}
