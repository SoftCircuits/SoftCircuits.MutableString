# SoftCircuits.MutableString

[![NuGet version (SoftCircuits.MutableString)](https://img.shields.io/nuget/v/SoftCircuits.MutableString.svg?style=flat-square)](https://www.nuget.org/packages/SoftCircuits.MutableString/)

```
Install-Package SoftCircuits.MutableString
```

## Overview

MutableString is a .NET library that provides a mutable string class that can be modified without creating a new instance. This is useful for scenarios where you need to frequently modify string data, as it avoids the overhead of creating new string instances.

## MutableString Class



``` cs
MutableString s = "Test!";      // Test!
s.Insert(4, " this");           // Test this!
s.Copy(5, 0, 4);                // this this!
s.Replace(5, "test", 4);        // this test!
s.Replace(0, "T", 1);           // This test!
s.Insert(4, " is a ");          // This is a test!
```

