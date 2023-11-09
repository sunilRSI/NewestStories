import { Component, OnInit, ViewChild } from '@angular/core';
import { StoriesService } from '../../services/stories.service';
import { neweststories } from 'src/stories/models/neweststories.model';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';

@Component({
  selector: 'app-neweststories',
  templateUrl: './neweststories.component.html',
  styleUrls: ['./neweststories.component.scss']
})

export class NeweststoriesComponent implements OnInit {

  @ViewChild(MatPaginator) paginators!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  showLoader: boolean = false;
  posts: neweststories[] = [];
  displayedColumns: string[] = ['Title'];
  dataSource: any;
  noOfNewestStrory: number = 200;
  constructor(public storiesService: StoriesService) {

  }

  // Applies filter on datasource
  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.dataSource.filter = filterValue;
  }

  ngOnInit(): void {
    this.GetNewestStories();
  }

  GetNewestStories() {
    this.dataSource=[];
    this.showLoader = true;
    this.storiesService.getNewStory(this.noOfNewestStrory).subscribe(data => {
      this.posts = data;
      this.dataSource = new MatTableDataSource<neweststories>(this.posts);
      this.dataSource.paginator = this.paginators;
      this.dataSource.sort = this.sort;
      this.showLoader = false;
    });
  }
}
