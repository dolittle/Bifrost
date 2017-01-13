---
title: About JavaScript Specifications
description: Learn about how to write JavaScript Specifications
keywords: Contributing
author: einari
---

# JavaScript Specifications

The key words “MUST”, “MUST NOT”, “REQUIRED”, “SHALL”, “SHALL NOT”, “SHOULD”, “SHOULD NOT”,
“RECOMMENDED”, “MAY”, and “OPTIONAL” in this document are to be interpreted as described in
[RFC 2119](https://tools.ietf.org/html/rfc2119).

All JavaScript code has been specified using [Jasmine](http://pivotal.github.com/jasmine/) with a similar style and structure as with the [C# code](csharp_specifications.md).
The folder structure is pretty much the same, except for the given folder. And since we've for now settled on Jasmine, the code is a bit different.



```js
    describe("when creating with configuration", function () {
        var options = {
            error: function () {
                print("Error");
            },
            success: function () {
            }
        };
        var command = Bifrost.commands.Command.create(options);

        it("should create an instance", function () {
            expect(command).toBeDefined();
        });

        it("should include options", function () {
            for (var property in options) {
                expect(command.options[property]).toEqual(options[property]);
            }
        });
    );
```