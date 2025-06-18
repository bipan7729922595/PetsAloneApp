import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class PetService {
  constructor(private http: HttpClient) { }

  getAllPets(petType?: number) {
    const params = petType != null ? { petType: petType.toString() } : {};
    return this.http.get<any>('api/pets/all', { params });
  }

  createPet(pet: any) {
    return this.http.post('api/pets/create', pet);
  }
}
