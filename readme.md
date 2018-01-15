# Antivirus Web Service

DevDragon Web Service is an easy way to scan files uploaded to your web servers.
  
After obtaining your `accessKey` from your DevDragon account, you can post the file you would like us to scan in a `multipart/form` **POST** opeation. Our system will scan the file immediately and return you the results.

## Scanning File via Rest API

Please don't forget to add your `accessKey` in headers as the following:

```
X-AUTH: <your API key>
```

A sample **HTTP POST** operation could be achived as following:

```
X-AUTH: <your API key>
cache-control: no-cache
Accept: */*
Host: <DevDragon API URL>
accept-encoding: gzip, deflate
content-type: multipart/form-data; boundary=--------------------------746262877764172707517154
content-length: 6740
Connection: close

----------------------------746262877764172707517154
Content-Disposition: form-data; name=""; filename="<YOUR FILE NAME>"
Content-Type: image/jpeg

<binary file data>

----------------------------746262877764172707517154--
```

DevDragon Web Service will scan the file immadiately and return you the following response:

```
{
    "RequestId": "5bc3b940-0767-4c4d-924f-a34bf3c2c0e7",
    "HasVirus": false,
    "Message": null,
    "ScanDuration": 2452,
    "ServiceVersion": "1.0.2.302"
}
```

Alternatively, if you are developing on `.Net` environment, you can consume the same service by using our client library.

# Scanning File via .Net Client Library

Official antivirus client library to enable virus scan functionality in your web servers by DevDragon.

## Installation

### Install via NuGet

From the command line:

```
nuget install DevDragon.AV.Client
```

From Package Manager:

```
PM > Install-Package DevDragon.AV.Client
```

From within Visual Studio:

1. Open the Solution Explorer.
2. Right-click on a project within your solution.
3. Click on Manage NuGet Packages...
4. Click on the Browse tab and search for "DevDragon.AV.Client".
5. Click on the DevDragon.AV.Client package, select the appropriate version in the right-tab and click Install.

## Usage

Create an `AntivirusClient` instance using `accessKey` obtained from your DevDragon account page.

```csharp
var antivirusClient = new AntivirusClient("{your accessKey here}");
```

To submit a file for scan, simply profide the file path. `AntivirusClient` will upload the file from your server to DevDragon and give you back `ScanResult` that contains the virus check results.

```csharp
var result = await client.ScanFile("{file path here}");
```

Successful scan operation will fill in the following to the `result` object:

```csharp
    public class ScanResult
    {
        /// <summary>
        /// Unique request Id assigned by Api
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// Has Virus been identified in the uploaded file. If it has, read 
        /// </summary>
        public bool HasVirus { get; set; }

        public string Message { get; set; }

        public string ServiceVersion { get; set; }
    }
```

## Exception Handling

`AntivirusClient` can throw `FileScanException` exceptions. A sample handling can be achieve as the following:

```csharp
var antivirusClient = new AntivirusClient("{your accessKey here}");

try {
    var result = await client.ScanFile("{file path here}");
}
catch(FileScanException fse){
    // Handling logic here
}
catch(Exception e){
    // Other exception handling logic here
}

```

## Help & Documentation

Please visit our website for further help, documentation and other DevDragon features.  
https://www.dev-dragon.com