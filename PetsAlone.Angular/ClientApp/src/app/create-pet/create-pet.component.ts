import { Component, OnInit } from '@angular/core';
import { PetService } from './pet.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-pet',
  templateUrl: './create-pet.component.html'
})
export class CreatePetComponent implements OnInit {
  pet = { name: '', petType: null, missingSince: new Date().toISOString().split('T')[0] };
  petTypes: any[] = [];

  constructor(private petService: PetService, private router: Router) { }

  ngOnInit() {
    this.petService.getAllPets().subscribe(data => {
      this.petTypes = data.petTypes;
    });
  }

  onSubmit() {console.log(this.pet)
    this.petService.createPet(this.pet).subscribe(() => {
      this.router.navigate(['/home']);
    });
  }
}
