# How to resize images in .NET Core using C#
Since Microsoft has released [System.Drawing.Common](https://www.nuget.org/packages/System.Drawing.Common) to provide access to GDI+ graphics functionality across cross-platform, i am using the same to resize the images.

In this example, i have implemented two methods to resize the images. 

**Resize(string filePath, int width, int height)** 
This will resize the image to the specified width and height without maintining the aspect ratio, and it will be saved in **.png** format in the same file path.

**ResizeImageWithAspectRatio(string filePath, int width, int height)**
This will resize the image to the specified width and height by maintining the aspect ratio, and it will be saved in **.png** format in the same file path.


You can run the example using below command.

```
dotnet run C:\Pictures\IMG_0845.jpg 128 128
```

**This .NET Core app runs in both Windows and Linux.**

## Contributing
Pull requests are welcome.
