import { Injectable } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { ImageBasicInfoModel } from 'src/app/shared/models/out/image-basic-info-model';

@Injectable({
  providedIn: 'root'
})
export class ImageSanitizerService {

  constructor(private domSanitizer: DomSanitizer) { }

  public sanitizeImagesData(images: ImageBasicInfoModel[]): void{
    images.forEach(image => {
      this.sanitizeImageData(image);
    });
  }

  public sanitizeImageData(image: ImageBasicInfoModel): void{
    image.data = 'data:image/jpg;base64, ' + image.data;
    image.safeResourceURLData = this.domSanitizer.bypassSecurityTrustResourceUrl(image.data);
  }
}
