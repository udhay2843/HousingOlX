import { Component, OnInit } from '@angular/core';
import { HousingService } from 'src/app/services/housing.service';
import { ActivatedRoute } from '@angular/router';
import { IPropertyBase } from 'src/app/model/ipropertybase';
import { AlertifyService } from 'src/app/services/alertify.service';

@Component({
  selector: 'app-property-list',
  templateUrl: './property-list.component.html',
  styleUrls: ['./property-list.component.css']
})
export class PropertyListComponent implements OnInit {
  SellRent = 1;
  properties: IPropertyBase[];
  Today = new Date();
  City = '';
  SearchCity = '';
  SortbyParam = '';
  SortDirection ='asc';
  cities:any[];


  constructor(private route: ActivatedRoute, private housingService: HousingService,private alertify:AlertifyService) { }

  ngOnInit(): void {
    if (this.route.snapshot.url.toString()) {
      this.SellRent = 2; // Means we are on rent-property URL else we are on base URL
    }
    this.housingService.getAllProperties(this.SellRent).subscribe(
        data => {
        this.properties = data;
        console.log(data);
      }, error => {
        console.log('httperror:');
        console.log(error);
      }

    );
    this.housingService.GetAllCities().subscribe(
      data=>{
        this.cities=data;
        console.log(this.cities);
      },
      error => {
        console.error('Error fetching cities:', error);
      }
    );
  }
  isValidCity(city: string): boolean {
    return this.cities && this.cities.find(c => c.name.toLowerCase() === city.toLowerCase()) !== undefined;

  }
  // onCityFilter() {
  //   if (this.City) {
  //     if (!this.isValidCity(this.City)) {
  //       this.alertify.error('No City Found Please Try Again...');
  //       return;
  //     }

  //   this.SearchCity = this.City;
  //   }
  // }
  onCityInputChange(city: string) {
    if (city && this.isValidCity(city)) {
      this.SearchCity = city;
    } else {
      this.SearchCity = '';
    }
  }
  onCityFilterClear() {
    this.SearchCity = '';
    this.City = '';
  }

  onSortDirection() {
    if (this.SortDirection === 'desc') {
      this.SortDirection = 'asc';
    } else {
      this.SortDirection = 'desc';
    }
  }


  stopAnimation() {
    document.querySelector('.hello').classList.add('paused');
  }

  startAnimation() {
    document.querySelector('.hello').classList.remove('paused');
  }
}
