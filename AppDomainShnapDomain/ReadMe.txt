This little project shows how .NET AppDomains work.

AppDomains each have a unique address space.  This means data between two appdomains cannot be accessed
directly, but rather a copy needs to be sent to the other domain to interact with

This small smaple shows how to:

1) Use marshal by ref to allow communication between your domains
2) Use of serializable attribute to allow the content of an object to sent from one domain to the other.
3) Shows common error cases 
4) Provides a working example of setting up two AppDomains and letting them "speak"
