import { ChangeDetectorRef, Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { IPropertyBase } from 'src/app/model/ipropertybase';
import { AlertifyService } from 'src/app/services/alertify.service';
import { AuthService } from 'src/app/services/auth.service';
import { HousingService } from 'src/app/services/housing.service';


@Component({
  selector: 'app-property-card',
  // template: `<h1>I am a card</h1>`,
  templateUrl: 'property-card.component.html',
  // styles: ['h1 {font-weight: normal;}']
  styleUrls: ['property-card.component.css']
}

)
export class PropertyCardComponent {
@Input() property: IPropertyBase;
@Input() hideIcons: boolean;

properties: IPropertyBase[] = [];

constructor(private ref: ChangeDetectorRef,private house:HousingService,private a:AlertifyService,private auth:AuthService,private router:Router ) {


}
deleteProperty(propertyId: number):void {

   if (this.auth.isLoggedIn()) {
    this.house.DeleteProperty(propertyId).subscribe(() =>{

        this.properties = this.properties.filter(p => p.id !== propertyId);
        
      }

    );
    }
  else{
this.a.error("Please Login to Delete This House Property");
this.router.navigate(['user/login']);
  }

}
isLoggedIn(): boolean {
  return this.auth.isLoggedIn(); // Assuming this method checks if the user is logged in
}
}
