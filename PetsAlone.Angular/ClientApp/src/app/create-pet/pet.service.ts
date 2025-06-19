import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';

@Injectable({ providedIn: 'root' })
export class PetService {
  constructor(private http: HttpClient) { }

  getAllPets(petType?: number) {
    const params = petType != null ? { petType: petType.toString() } : {};
    return this.http.get<any>('http://localhost:27544/api/pets/all', { params });
  }

  createPet(pet: any) {
    const token = localStorage.getItem('currentUser'); 
    
    /*const headers = new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': `Bearer ${token}` 
    });*/
    console.log('Token:', token);//console.log('Header:',headers)
    /*return this.http.post('http://localhost:27544/api/pets/create', pet, { headers });*/
    return this.http.post('http://localhost:27544/api/pets/create', pet, );
  }
}
