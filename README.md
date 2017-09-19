# ASP.NET Core Photo Gallery 

A photo gallery site implemented in ASP.NET Core 2.0 Razor Pages.

**Demo website**: <https://gallerytemplate.azurewebsites.net/>

![Masonry](art/masonry.jpg "Masonry layout of images")

## Features

- Elegant masonry layout of images
- Easy to manage through simple [admin interface](#admin-interface)
- All major browsers fully supported (IE 10+)
- Social media integration (Facebook, Twitter, Pinterest)
- Follows best practices for web applications
  - [See DareBoost report](https://www.dareboost.com/en/report/59b6e5510cf2502fdb8c1ad5)

## Technical features
- High performance. Gets 100/100 points on Google PageSpeed Insights 
  - [Run PageSpeed Insights](https://developers.google.com/speed/pagespeed/insights/?url=https%3A%2F%2Fgallerytemplate.azurewebsites.net%2F)
- Speed Index < 1000
  - [See WebPageTest](http://www.webpagetest.org/result/170919_G1_c46349f8918b37979f2b23987df4dd35/) 
- Meets highest accessibility standards 
  - [Run accessibility validator](http://wave.webaim.org/report#/https://gallerytemplate.azurewebsites.net)
- W3C standards compliant HTML and CSS 
  - [Run HTML validator](https://html5.validator.nu/?doc=https%3A%2F%2Fgallerytemplate.azurewebsites.net%2F)
- Automatic thumbnail generation
- Responsive web design
  - [See mobile emulators](https://www.responsinator.com/?url=https%3A%2F%2Fgallerytemplate.azurewebsites.net%2Falbum%2Fwinter%2F)
- Mobile friendly
  - [Run Mobile-Friendly Test](https://search.google.com/test/mobile-friendly?id=_iz8MUAbMZ6TqziOOgxISA)
- Responsive image sizes using the `srcset` attribute
- Schema.org support with HTML 5 Microdata 
  - [Run testing tool](https://search.google.com/structured-data/testing-tool#url=https%3A%2F%2Fgallerytemplate.azurewebsites.net%2Fphoto%2Fgarden%2Fa%2520couple%2520of%2520squash%2F)
- OpenGraph support for Facebook, Twitter, Pinterest and more
  - [Check the tags](http://opengraphcheck.com/result.php?url=https%3A%2F%2Fgallerytemplate.azurewebsites.net%2F#.WbB4bLpFzK4)
- Seach engine optimized
  - [Run SEO Site Checkup](https://seositecheckup.com/seo-audit/gallerytemplate.azurewebsites.net)
- Security HTTP headers set
  - [Run security scan](https://securityheaders.io/?q=https%3A%2F%2Fgallerytemplate.azurewebsites.net%2F&hide=on&followRedirects=on)
- Uses the [Azure Image Optimizer](https://github.com/madskristensen/ImageOptimizerWebJob) for superb image compression

## Admin interface
When logged in, you can manage the albums and photos easily through a simple-to-use admin bar located under the header on the website.

This is how to create new albums directly from the home page:

![Admin Album View](art/admin-album-view.png)

Uploading photos to an album or deleting the entire album:

![Admin Album Upload](art/admin-album-upload.png)

Rename or delete a photo:

![Admin Photo](art/admin-photo.png)

## How to use

1. Fork and/or clone this repo
2. Change user settings in `\src\appsettings.json`
3. Make any modifications you want
4. Deploy