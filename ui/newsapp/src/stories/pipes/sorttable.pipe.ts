import { Pipe, PipeTransform } from '@angular/core';
import { neweststories } from '../models/neweststories.model';

@Pipe({
  name: 'sorttable'
})
export class SorttablePipe implements PipeTransform {

  // Your sort logic is in here
  transform(array: any, field: string): any[] { 
    array.sort((a: any, b: any) => {
      if (a[field] < b[field]) {
        return -1;
      } else if (a[field] > b[field]) {
        return 1;
      } else {
        return 0;
      }
    });
    return array;
  }
}
