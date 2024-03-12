import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-url-info',
  templateUrl: './url-info.component.html'
})
export class UrlInfoComponent implements OnInit {
    public UrlLink: UrlLink = { id: '', longForm: '', shortForm: '', createdBy: '', createdAt: new Date() };
    private id: string = '';

  constructor(
    private http: HttpClient,
    private route: ActivatedRoute,
    @Inject('BASE_URL') private baseUrl: string
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
        this.id = params.get('id') || '';
      this.fetchUrlInfo();
    });
  }

  fetchUrlInfo() {
    this.http.get<UrlLink>(`${this.baseUrl}api/url/${this.id}`).subscribe(
      result => {
        this.UrlLink = result;
      },
      error => {
        console.error(error);
      }
    );
  }
}

interface UrlLink {
  id: string;
  longForm: string;
  shortForm: string;
  createdBy: string;
  createdAt: Date;
}
