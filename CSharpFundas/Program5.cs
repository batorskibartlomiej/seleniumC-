using System;

//
String[] a = {"bartek", "jan", "maja"};
int[] b = {1,2, 3, 4, 6};

String[] a1 = new string[4];
a1[0] = "hello";
a1[1] = "bye";
int[] b1 = new int[4];



for (int i = 0; i < a.Length; i++)
{
    Console.WriteLine(a[i]);
    if (a[i] == "jan")
    {
        Console.WriteLine("match found");
        break;
    }
    
}

//foreach (int i in b)
//{
//    Console.WriteLine(i);
//}

