.assembly extern mscorlib {}
.assembly DiscriminantApp {}
.module DiscriminantApp.exe

.class public DiscriminantApp
{
    // Metoda pro v�po�et diskriminantu
    .method public hidebysig static float64 CalculateDiscriminant(float64 a, float64 b, float64 c) cil managed
    {
        .maxstack 3 
        .locals init (
            [0] float64 discriminant 
        )

        // V�po�et diskriminantu: b^2 - 4ac
        IL_0000: ldarg.1           
        IL_0001: ldarg.1            
        IL_0002: mul                 
        IL_0003: ldc.r8 4.0         
        IL_0004: ldarg.0            
        IL_0005: mul                 
        IL_0006: ldarg.2            
        IL_0007: mul                 
        IL_0008: sub                 // b^2 - (4 * a * c)

        // Ulo�en� v�sledku do lok�ln� prom�nn�
        IL_0009: stloc.0            

        // N�vrat v�sledku
        IL_000A: ldloc.0           
        IL_000B: ret               
    }

    .method public hidebysig static void Main() cil managed
    {
        .entrypoint
        .maxstack 3

        // Zavol�n� metody CalculateDiscriminant s hodnotami a, b, c
        IL_0000: ldc.r8 2.0         // Na�ten� a = 2.0
        IL_0004: ldc.r8 2.0        // Na�ten� b = 2.0
        IL_0008: ldc.r8 5.0         // Na�ten� c = 5.0
        IL_000C: call float64 DiscriminantApp::CalculateDiscriminant(float64, float64, float64) 

        // V�pis v�sledku
        IL_0011: call void [mscorlib]System.Console::WriteLine(float64) 

        // Konec metody
        IL_0016: ret
    }
}




