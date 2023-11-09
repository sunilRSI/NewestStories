# Introduction 
TODO: Consuming two external API through .net core project to make a wrapper for story listing. UI project is based on Angular 16 and angular/material is used for listing, paging and searching. 

# Getting Started
TODO: .net core project is running on http://localhost:50719/ and data can be accessed on https://localhost:7189/api/Stories/GetNewestStories/{noOfNewestStories}.

1.	Software dependencies - [ASP.NET Core] [Visual Studio Code]
4.	External API references - (https://hacker-news.firebaseio.com/v0/newstories.json?print=pretty) (https://hacker-news.firebaseio.com/v0/item/{StoryId}.json?print=pretty)

# Build and Test
TODO: .net core project can build through ctrl+shift+B and can be run directly through F5 to make it available for UI project.
For UI project after opening a new terminal it is important to run npm i to install required packages. After npm i it is required to run
ng serve -o to run application. 

# Result
A single page application will be launched where data(number of newest stories according to noOfNewestStories parameter) will be loaded and loaded data will be stored in in-memory caching to make it
available for next search. On this page paging will be there and search will also work for listed items.
