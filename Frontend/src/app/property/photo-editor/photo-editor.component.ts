import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Photo } from 'src/app/model/Photo';
import { Property } from 'src/app/model/property';
import { HousingService } from 'src/app/services/housing.service';
import { FileUploader } from 'ng2-file-upload';
import { AlertifyService } from 'src/app/services/alertify.service';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {
@Input() property:Property;
@Output() mainPhotoChangedEvent =new EventEmitter<string>();
mainPhotoChanged(url:string){
this.mainPhotoChangedEvent.emit(url);
}
uploader:FileUploader;
hasBaseDropZoneOver: boolean;
baseUrl = 'http://localhost:5283/api';
maxAllowedFileSize=1*1024*1024;

response: string;
  constructor(private housingservice:HousingService,private alertify:AlertifyService) { }
  public fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
}


  ngOnInit(): void {
    this.initializeFileUploader();
  }
  setPrimaryPhoto(propertyid:number,photo:Photo){
    this.housingservice.setPrimaryPhoto(propertyid,photo.publicId).subscribe(()=>{
      this.mainPhotoChanged(photo.imageUrl);
      this.property.photos.forEach(p=>{
        if(p.isPrimary) {p.isPrimary=false;}
        if(p.publicId===photo.publicId){p.isPrimary=true;}
      });
    });
  }
  deletePhoto(propertyid:number,photo:Photo){
    this.housingservice.DeletePhoto(propertyid,photo.publicId).subscribe(()=>{
     this.property.photos=this.property.photos.filter(p=>
     p.publicId !== photo.publicId );
    });

  }
  initializeFileUploader() {
    this.uploader = new FileUploader({
        url: this.baseUrl +'/property/add/photo/'+ String(this.property.id),
        authToken: 'Bearer '+ localStorage.getItem('token'),
        isHTML5: true,
        allowedFileType: ['image'],
        removeAfterUpload: true,
        autoUpload: true,
        maxFileSize:this.maxAllowedFileSize
    });
    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
  };

  this.uploader.onSuccessItem = (item, response, status, headers) => {
    console.log(response);
      if (response) {
          const photo = JSON.parse(response);
          this.property.photos.push(photo);
      }
  };
  this.uploader.onErrorItem = (item, response, status, headers) => {
    console.log(response);
    let errorMessage = 'Some unknown error occured';
    if (status===401) {
        errorMessage ='Your session has expired, login again';
    }

    if (response) {
        errorMessage = response;

    }
this.alertify.error(errorMessage);
};
}
}

