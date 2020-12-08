import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-company-component',
  templateUrl: './company.component.html'
})
export class CompanyComponent {
  public companies: Company[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Company[]>(baseUrl + 'Company/GetCompanies').subscribe(result => {
      this.companies = result;
    }, error => console.error(error));
  }

}

interface Company {
  id: number;
  comValue: string;
  chName: string;
  chAbbreviation: string;
  engName: string;
  engAbbreviation: string;
  address: string;
  createUser: string;
  createDate: Date;
  updateUser: string;
  updateDate: Date;
}
