import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-url-table',
  templateUrl: './url-table.component.html'
})
export class UrlTableComponent {
  public urls: UrlLink[] = [];

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router, private authorizeService: AuthorizeService) {
    this.fetchUrls();
  }

  fetchUrls() {
    this.http.get<UrlLink[]>(this.baseUrl + 'api/url').subscribe(result => {
      this.urls = result;
    }, error => console.error(error));
  }

  openUrl(url: UrlLink) {
    console.log("open");
    this.router.navigate(['/url-info', url.id]);
  }

  deleteUrl(event: MouseEvent, id: string) {
    event.stopPropagation();

    this.authorizeService.isAuthenticated().subscribe(authenticated => {
      if (authenticated) {
        this.http.delete(this.baseUrl + 'api/url/' + id).subscribe(
          () => {
            console.log('URL with ID ' + id + ' deleted successfully');
            this.fetchUrls();
          },
          error => {
            console.error('Error deleting URL:', error);
          }
        );
      } else {
        console.log('User is not authenticated');
      }
    });
  }
}

interface UrlLink {
  id: string;
  longForm: string;
  shortForm: string;
  createdBy: string;
  createdAt: Date;
}
