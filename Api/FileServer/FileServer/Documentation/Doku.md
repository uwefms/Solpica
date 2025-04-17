This API is a file server API. It is used to upload, download, delete and create files and directories. 
The API is secured with an API key. The API key is used to authenticate the user. 
The API key is sent in the header of the request as X-Api-Key. An Alternative way to authenticate the user 
is to send the API key as a query parameter. 

### The following Methods are available:

## - GET /weatherforecast

This method is used to test the API. It returns a list of weather forecasts.
It is called with the following URL: /weatherforecast/?ApiKey=bubu2

## - GET /api/file/getallfilesbydir

This method is used to get all files in a directory. It returns a list of files.
It is called with the following URL: /api/file/getallfilesbydir?dir=TestDir

## - GET /api/file/getalldirs

This method is used to get all directories. It returns a list of directories.
It is called with the following URL: /api/file/getalldirs

## - GET /api/file/getallfilesindirs

This method is used to get all files in all directories. It returns a list of files.
It is called with the following URL: /api/file/getallfilesindirs

## - POST /api/file/upload

This method is used to upload a file. It is called with the following URL: /api/file/upload
The file is sent as a multipart form data. The file is sent with the key "File".
The name of the file is sent with the key "Name".
The description of the file is sent with the key "Description".
The author of the file is sent with the key "Author".
The directory of the file is sent with the key "FileDir".

## - POST /api/file/multipleupload

This method is used to upload multiple files. It is called with the following URL: /api/file/multipleupload
The files are sent as a multipart form data. The files are sent with the key "Files".
The name of the files is sent with the key "Name".
The description of the files is sent with the key "Description".
The author of the files is sent with the key "Author".
The directory of the files is sent with the key "FileDir".

## - GET /api/file/download

This method is used to download a file. 
It is called with the following URL: /api/file/download?dir=TestDir&filename=readme.md

## - DELETE /api/file/delete

This method is used to delete a file.
It is called with the following URL: /api/file/delete
The directory of the file is sent with the key "DirName".
The name of the file is sent with the key "FileName".

## - DELETE /api/file/deletedir

This method is used to delete a directory.
It is called with the following URL: /api/file/deletedir
The name of the directory is sent with the key "DirName".

## - POST /api/file/createdir

This method is used to create a directory.
It is called with the following URL: /api/file/createdir
The name of the directory is sent with the key "DirName".


## - GET /api/file/getallfiles

This method is used to get all files. It returns a list of files.
It is called with the following URL: /api/file/getallfiles





# File Management API Documentation

## Introduction
This API provides functionalities to upload, download, manage files and directories. It supports uploading single and multiple files, downloading files, deleting files, creating directories, and deleting directories.

## Base URL
- Base URL: `https://localhost:7137`

## Authentication
- API Key is required for authentication and should be included in the `X-Api-Key` header.

## Endpoints

### 1. GET /api/file/getallfilesbydir
- **Description**: Get all files in a specific directory.
- **Parameters**:
  - `dir`: Directory name to retrieve files from.
- **Headers**:
  - `X-Api-Key`: API Key for authentication.

### 2. GET /api/file/getalldirs
- **Description**: Get all directories.
- **Headers**:
  - `X-Api-Key`: API Key for authentication.

### 3. GET /api/file/getallfilesindirs
- **Description**: Get all files in all directories.
- **Headers**:
  - `X-Api-Key`: API Key for authentication.

### 4. POST /api/file/upload
- **Description**: Upload a single file.
- **Headers**:
  - `X-Api-Key`: API Key for authentication.
- **Body**:
  - `File`: File to upload.
  - `Name`: Name of the file.
  - `Description`: Description of the file.
  - `Author`: Author of the file.
  - `FileDir`: Directory to upload the file to.

### 5. POST /api/file/multipleupload
- **Description**: Upload multiple files.
- **Headers**:
  - `X-Api-Key`: API Key for authentication.
- **Body**:
  - `Files`: Array of files to upload.
  - `Name`: Name of the files.
  - `Description`: Description of the files.
  - `Author`: Author of the files.
  - `FileDir`: Directory to upload the files to.

### 6. GET /api/file/download
- **Description**: Download a file.
- **Parameters**:
  - `dir`: Directory name where the file is located.
  - `filename`: Name of the file to download.
- **Headers**:
  - `X-Api-Key`: API Key for authentication.

### 7. DELETE /api/file/delete
- **Description**: Delete a file.
- **Headers**:
  - `X-Api-Key`: API Key for authentication.
- **Body**:
  - `DirName`: Directory name where the file is located.
  - `FileName`: Name of the file to delete.

### 8. DELETE /api/file/deletedir
- **Description**: Delete a directory.
- **Headers**:
  - `X-Api-Key`: API Key for authentication.
- **Body**:
  - `DirName`: Directory name to delete.

### 9. POST /api/file/createdir
- **Description**: Create a directory.
- **Headers**:
  - `X-Api-Key`: API Key for authentication.
- **Body**:
  - `DirName`: Name of the directory to create.

## Response Codes
- `200`: Successful operation.
- `400`: Bad request.
- `401`: Unauthorized (missing or invalid API Key).
- `404`: Resource not found.
- `500`: Internal server error.

## Response Body
- Successful responses will return JSON objects with relevant data.
- Error responses will include an error message.

## Note
- The API supports file upload with multipart form data.
- File deletion operations are irreversible.
- Ensure proper authentication using the API Key for secure operations.

This documentation provides a detailed overview of the API endpoints, their functionalities, required parameters, headers, request bodies, response codes, and response formats. Please refer to this documentation for utilizing the API effectively.

