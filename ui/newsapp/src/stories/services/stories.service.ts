import { Injectable } from '@angular/core'; 
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { neweststories } from '../models/neweststories.model';

@Injectable({
  providedIn: 'root'
})
export class StoriesService {

  apiUrl = 'https://localhost:7189/api/Stories/GetNewestStories/'; 
  constructor(private http: HttpClient) { }
  
  // fetching news data from API
  getNewStory(noOfNewestStrory:number): Observable<neweststories[]> {
    let endpoint=this.apiUrl+noOfNewestStrory.toString();
    var result = this.http.get<neweststories[]>(endpoint);
    return result;
  }
}
