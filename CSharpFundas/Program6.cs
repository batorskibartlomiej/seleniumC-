using System;
using System.Collections;


ArrayList a = new ArrayList();
a.Add("bhello");
a.Add("ahello1");
a.Add("chello2");

//Console.WriteLine(a[1]);

foreach (String i in a)
{
    Console.WriteLine(i);
}

Console.WriteLine( a.Contains("hello1"));
a.Sort();
Console.WriteLine("After sorting");

foreach (String i in a)
{
    Console.WriteLine(i);
}
