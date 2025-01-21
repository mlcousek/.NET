.assembly extern mscorlib {}
.assembly DiscriminantApp {}
.module DiscriminantApp.exe

.class public DiscriminantApp
{
    // Metoda pro výpoèet diskriminantu
    .method public hidebysig static float64 CalculateDiscriminant(float64 a, float64 b, float64 c) cil managed
    {
        .maxstack 3 
        .locals init (
            [0] float64 discriminant 
        )

        // Výpoèet diskriminantu: b^2 - 4ac
        IL_0000: ldarg.1           
        IL_0001: ldarg.1            
        IL_0002: mul                 
        IL_0003: ldc.r8 4.0         
        IL_0004: ldarg.0            
        IL_0005: mul                 
        IL_0006: ldarg.2            
        IL_0007: mul                 
        IL_0008: sub                 // b^2 - (4 * a * c)

        // Uložení výsledku do lokální promìnné
        IL_0009: stloc.0            

        // Návrat výsledku
        IL_000A: ldloc.0           
        IL_000B: ret               
    }

    .method public hidebysig static void Main() cil managed
    {
        .entrypoint
        .maxstack 3

        // Zavolání metody CalculateDiscriminant s hodnotami a, b, c
        IL_0000: ldc.r8 2.0         // Naètení a = 2.0
        IL_0004: ldc.r8 2.0        // Naètení b = 2.0
        IL_0008: ldc.r8 5.0         // Naètení c = 5.0
        IL_000C: call float64 DiscriminantApp::CalculateDiscriminant(float64, float64, float64) 

        // Výpis výsledku
        IL_0011: call void [mscorlib]System.Console::WriteLine(float64) 

        // Konec metody
        IL_0016: ret
    }
}




