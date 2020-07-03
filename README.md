# Online Web Image Extractor - Blazor

Online tool to extract images and icons from websites.

You can use the tool here:  
[https://image-extractor.jam-es.com/](https://image-extractor.jam-es.com/)  

It is written in Client Side Blazor (Blazor WebAssembly) to demonstrate use of my [WebImageExtractor library](https://github.com/James231/WebImageExtractor) in Blazor applications.

## Building from Source

Clone the repository and open in Visual Studio 2019 (ideally the latest version). Make sure you have the .NET Core Visual Studio workload installed. When you build and run the application, it should 'just work'.

## References

My [WebImageExtractor library](https://github.com/James231/WebImageExtractor) makes use of the [Html Agility Pack](https://html-agility-pack.net/) and [Magick.NET](https://github.com/dlemstra/Magick.NET) (.NET bindings for ImageMagick).

Web browsers will only send requests accept responses from other domains if the responses contain a CORS header. To avoid this issue, the Blazor app uses CORS Anywhere. This service acts as a proxy for all requests and adds a CORS header onto the responses. You can find out more about [CORS Anywhere here](https://cors-anywhere.herokuapp.com/).

## License

This code is released under MIT license. Modify, distribute, sell, fork, and use this as much as you like. Both for personal and commercial use. I hold no responsibility if anything goes wrong.

If you use this, you don't need to refer to this repo, or give me any kind of credit but it would be appreciated. At least a :star: would be nice.

## Contributing

Contributions are welcome in the forms of Pull Requests and Issues. Please also consider contributing to the [WebImageExtractor library](https://github.com/James231/WebImageExtractor).