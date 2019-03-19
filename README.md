# Console.Abstractions
Simple, versatile, universal abstractions for the console, and bringing performance improvements to the table by minimizing as many console calls as possible.

```
PM> Package-Install Console.Abstractions -pre
```

### Why's it in pre?
It can be used in production but i'm labeling it as pre so it's apparent that some testing needs to be done, as the API for things might change around a bit. Feel free to submit issues about API changes you want or things you don't like!

## Why?

Why would you need such a thing? Doesn't the system console work well enough?

- The System.Console is very slow, calling the kernel for every single call.
- Writing efficient console drawing code is a total pain.
- The System.Console is not very abstracted, and it's difficult to work with it nicely.

Take a look at this routine implemented purely with the console:

![](https://rawcdn.githack.com/SirJosh3917/Console.Abstractions/blob/master/github-assets/system_console.gif)

Now look at the exact same routine, but just using `Helpers.CacheEnMasse(new SystemConsle())`:

![](https://rawcdn.githack.com/SirJosh3917/Console.Abstractions/blob/master/github-assets/console_abstractions_cache_en_masse.gif)