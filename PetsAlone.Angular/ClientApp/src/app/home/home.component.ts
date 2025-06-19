import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})

export class HomeComponent implements OnInit {
  pets: any[] = [];
  petTypes: any[] = [];
  selectedPetType: number | null = null;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getPets();
  }

  getPets(petType?: number) {
    let params = new HttpParams();
    if (petType != null) {
      params = params.set('petType', petType.toString());
    }

    this.http.get<any>('http://localhost:27544/api/pets/all', { params }).subscribe(data => {
      this.pets = data.pets;
      this.petTypes = data.petTypes;
      this.selectedPetType = data.selectedPetType;
    });
  }
}
