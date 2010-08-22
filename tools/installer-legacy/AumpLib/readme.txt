Arbingersys.Audio.Aumplib 

A C# namespace that is comprised of a set of classes that interface to 
several prominent Open Source audio conversion libraries via P/Invoke.



DEPENDENCIES

- lame_enc.dll, from the LAME project
- libsndfile.dll, from the libsndfile project
- madlldlib.dll, from the madlldlib project (uses libmad)
- libmad, from Underbit Technologies



GETTING STARTED

You will need to get the dependencies mentioned above from their respective
parties and put them in the path of the Arbingersys.Audio.Aumplib project 
(I recommend putting them in the '/tests' directory of this distribution). 

You will probably need to compile some of them. A Google search should return 
the websites where you can download the dependencies. I have purposely left out 
the URLs because they may change and I don't want the hassle of trying to keep 
those references up-to-date. The dependencies should have instructions 
describing the compilation procedure. 

Arbingersys.Audio.Aumplib has been created using a Makefile, and will compile 
with the .NET SDK (which is free), so you do not need to own Visual Studio 
.NET to make it work.




LICENSING

DISCLAIMER: This is merely my understanding of the licensing, GPL and 
otherwise, in regard to Arbingersys.Audio.Aumplib and the related software. I
may misunderstand/misinterpret some or all of the licensing terms of the 
parties involved in creating Arbingersys.Audio.Aumplib. If you need to be 
sure, get a lawyer.

Arbingersys.Audio.Aumplib is released under the Lesser/Library GNU Public 
License (LGPL). However, a class within the Arbingersys.Audio.Aumplib 
namespace, "MadlldlibWrapper" is necessarily released under the GNU Public 
License (GPL), which is more restrictive. This is because it interfaces a DLL, 
madlldlib.dll (and is thus a derivative work of madlldlib.dll), which is a 
derivative work of libmad (http://www.underbit.com/products/mad/), which is 
released under the GPL. The GPL states plainly that if a work is a "derivative 
work" of one released under the GPL, it also must be released under the GPL. 
In short, if you use this class in Arbingersys.Audio.Aumplib for your project, 
you must release your work under the GPL terms. If you do not, you can release 
your work under the terms of the LGPL.

The main interface class "Aumpel", is released under the LGPL. There is one 
overload of the "Convert()" method that uses data structures from 
MadlldlibWrapper and the MadlldlibWrapper class itself. If you use this 
particular overload of Convert(), you will probably also have to release 
your code under the GPL. IF YOU DO NOT WISH TO DO THIS, SIMPLY DELETE THIS 
METHOD OVERLOAD.

There is another class, "MadnpsrvWrapper" which provides the same function as 
MadlldlibWrapper (MP3 decoding), and is released under the LGPL. If you are not 
writing open source software, use this class or its overload of the 
Aumpel.Convert() method instead of MadlldlibWrapper.
