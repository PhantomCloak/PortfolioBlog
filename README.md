# PortfolioBlog
It allows us to create content types and their fields dynamically.

## Prerequisites

.NET SDK 3.1.108

Microsoft SQL Server Version 15.0.4083.2 (X64)

## Build Instructions

```
cd /src
dotnet build
cd Presentation
dotnet run 
```
After running solution database user must be set in the `appconfig.json` (create manually if don't exist) by default user name is SA and password is Badf00d11
and after settiling up database config project is ready to go/

## Documentation
Application contains swagger after launching the application it can be found in `/swagger` or better check http://efurni.phantom-dev.com:22003/swagger (not operational reason is in the end of the page

### List all contents
Get request to the`/api/v1/content` endpoint.

Returns: 

200 - OK along with contents

204 - NoContent If there is a no content

400 - Badrequest if something fails e.g database or constraint check

### Get spesific content
Get request to the`/api/v1/content/{contentKey}` endpoint for example `/api/v1/content/Category`

Returns: 

200 - OK along with a content

404 - NotFound if content not present in database

400 - Badrequest if something fails e.g database or constraint check

### Create content
Post request to the `/api/v1/content` endpoint, the model must be in json format at should be sent via body.

Example
```
{
  "contentName": "Post",
  "contentFields": {
    "Category": "History",
    "Rating": "5.3",
    "Publish Date": "05/29/2015 05:50 AM"
  }
}
```
Returns: 

201 - Created along with a content

400 - Badrequest if something fails e.g database or constraint check

### Update content
Put request to the`/api/v1/content/{contentKey}` endpoint for example `/api/v1/content/Post`
```
{
  "contentName": "Post",
  "contentFields": {
    "Category": "History",
    "Rating": "8.3",
    "Publish Date": "05/29/2015 05:50 AM",
    "View": "2147483647"
  }
}
```

Returns: 

200 - OK without any content

404 - NotFound if content not present in database

400 - Badrequest if something fails e.g database or constraint check

### Delete content
Delete request to the `/api/v1/content/{contentKey}` endpoint for example `/api/v1/content/Post`

Returns: 

200 - OK without any content

404 - NotFound if content not present in database

400 - Badrequest if something fails e.g database or constraint check

### Tests
Before the executing tests make sure `appsettings.json` is in the corresponding directory.

# Why example web site is not working?
MS-SQL requires minimum 2 gig of ram to run and current machine that I have has only 1 gig of ram so in the end MS-SQL was too heavy to lift for my cloud instance but it should work with operational database.  
