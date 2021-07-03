# PolyTool
![Downloads](https://img.shields.io/github/downloads/XyLe-GBP/PolyTool/total.svg)  
---
ITU G.722.1 Annex C (Polycom Siren14) Converter for IDOLM@STER.  
This tool is a multi-functional version of unkTool.exe.  

---

## What is this?
---
This application decodes an audio file encoded in ITU G.722.1 Annex C (Polycom Siren14) format into Wave (Microsoft) format.  
This application supports the user interface function.  

Decoding of some RAW format files is also possible.  
`(e.g. *.nub2, *.nus3bank, *.bnsf)`  
However, encoding of these formats is currently not supported.  

The application currently supports only Japanese and English languages.  

---

## Usage
---
This application uses Perl.  
You will need to install ActivePerl or Strawberry Perl.  
If you do not install it, you will not be able to run this application.  

[ActivePerl](https://www.activestate.com/products/perl/downloads/)

[Strawberry Perl](https://strawberryperl.com/)

.NET Framework 4.7.2 is used for this application.  
You will need to install the following packages on your PC.  

[Microsoft .NET Framework 4.7.2 Web installer](http://go.microsoft.com/fwlink/?linkid=863262)

[Microsoft .NET Framework 4.7.2 Offline installer](http://go.microsoft.com/fwlink/?linkid=863265)

* Supported decode formats
  * `Polycom Siren14 (*.unk, *.s14, *.sss, *.bnsf)`
  * `Container (*.nub2, *.nus3bank)`

* Supported encode formats
  * `Polycom Siren14 (*.unk, *.s14, *.sss)`

`UNK: IDOLM@STER SP`  
`S14: IDOLM@STER DS`  
`SSS: IDOLM@STER DS`  
`BNSF: IDOLM@STER 2, OFA, PLATINUM STARS, STELLA STAGE`  
`NUB2: IDOLM@STER 2`  
`NUS3BANK: IDOLM@STER OFA, PLATINUM STARS, STELLA STAGE`  

## About Licensing
---
This tool is released under the MIT license.
