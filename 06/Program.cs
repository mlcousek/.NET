using System;
using System.Collections.Generic;
using Microsoft.FSharp.Collections;
using PNE06;

class Program
{
    static void Main()
    {
        var list = new List<int> { 3, 6, 8, 10, 1, 2, 1 };

        var fsharpList = ListModule.OfSeq(list);

        var sortedFsharpList = MySort.quickSortFun(fsharpList);

        var sortedList = new List<int>(sortedFsharpList);

        Console.WriteLine("Sorted list (functional): " + string.Join(", ", sortedList));

        var array = new int[] { 3, 6, 8, 10, 1, 2, 1 };
        var sortedArray = MySort.quickSortImp(array);
        Console.WriteLine("Sorted array (imperative): " + string.Join(", ", sortedArray));




        var mySet = new MySet.Set();

        // Test 1: Přidání prvků
        mySet.Add(1);
        mySet.Add(2);
        mySet.Add(3);

        Console.WriteLine($"Množina obsahuje 1: {mySet.Contains(1)}"); // Očekáváme true
        Console.WriteLine($"Množina obsahuje 2: {mySet.Contains(2)}"); // Očekáváme true
        Console.WriteLine($"Množina obsahuje 3: {mySet.Contains(3)}"); // Očekáváme true
        Console.WriteLine($"Množina obsahuje 4: {mySet.Contains(4)}"); // Očekáváme false

        // Test 2: Velikost množiny
        Console.WriteLine($"Velikost množiny: {mySet.Size()}"); // Očekáváme 3

        // Test 3: Odebrání prvku
        mySet.Remove(2);
        Console.WriteLine($"Množina obsahuje 2 po odebrání: {mySet.Contains(2)}"); // Očekáváme false

        // Test 4: Velikost po odebrání
        Console.WriteLine($"Velikost množiny po odebrání: {mySet.Size()}"); // Očekáváme 2

        // Test 5: Operace Union
        var otherSet = new MySet.Set();
        otherSet.Add(2);
        otherSet.Add(4);
        var unionSet = mySet.Union(otherSet);

        Console.WriteLine($"Union množina obsahuje 1: {unionSet.Contains(1)}"); // Očekáváme true
        Console.WriteLine($"Union množina obsahuje 2: {unionSet.Contains(2)}"); // Očekáváme true
        Console.WriteLine($"Union množina obsahuje 4: {unionSet.Contains(4)}"); // Očekáváme true

        // Test 6: Operace Intersection
        var intersectionSet = mySet.Intersection(otherSet);
        Console.WriteLine($"Intersection množina obsahuje 2: {intersectionSet.Contains(2)}"); // Očekáváme false
        Console.WriteLine($"Intersection množina obsahuje 1: {intersectionSet.Contains(1)}"); // Očekáváme false

        // Test 7: Operace Subtract
        var subtractSet = mySet.Subtract(otherSet);
        Console.WriteLine($"Subtract množina obsahuje 1: {subtractSet.Contains(1)}"); // Očekáváme true
        Console.WriteLine($"Subtract množina obsahuje 2: {subtractSet.Contains(2)}"); // Očekáváme false

        // Test 8: Kontrola podmnožiny
        mySet.Add(4);
        Console.WriteLine($"mySet je podmnožinou otherSet: {mySet.IsSubsetOf(otherSet)}"); // Očekáváme false
    }
}
