# SoftCircuits.MutableString

[![NuGet version (SoftCircuits.MutableString)](https://img.shields.io/nuget/v/SoftCircuits.MutableString.svg?style=flat-square)](https://www.nuget.org/packages/SoftCircuits.MutableString/)

```
Install-Package SoftCircuits.MutableString
```

## Overview

MutableString is a .NET library that provides a mutable string class that can be modified without creating a new instance. For many operations, this is more efficient than modifying a regular `string`, which requires creating a new instance each time.

## MutableString Class

Here's a simple example showing some of the included methods.

``` cs
MutableString s = "Test!";      // Test!
s.Insert(4, " this");           // Test this!
s.Copy(5, 0, 4);                // this this!
s.Replace(5, "test", 4);        // this test!
s[0] = 'T';                     // This test!
s.Insert(4, " is a ");          // This is a test!
```
