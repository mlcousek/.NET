.assembly extern mscorlib {}
.assembly FizzBuzzApp {}
.module FizzBuzzApp.exe

.class public FizzBuzzApp
{
    .method public hidebysig static void FizzBuzz(int32 limit) cil managed
    {
        .maxstack 2 
        .locals init (
            [0] int32 i 
        )

        // Inicializace poèítadla
        IL_0000: ldc.i4.1           
        IL_0001: stloc.0           

        // Zaèátek smyèky
        IL_0002: br.s IL_0004       

        // Kontrola podmínky smyèky
        IL_0004: ldloc.0            
        IL_0005: ldarg.0            
        IL_0006: ble.s IL_000A      

        // Konec smyèky
        IL_0008: ret                // Návrat z metody

        // Kontrola dìlitelnosti 3
        IL_000A: ldloc.0            
        IL_000B: ldc.i4.3           
        IL_000C: rem                 
        IL_000D: brtrue.s IL_000M    

        // Kontrola dìlitelnosti 5
        IL_000E: ldloc.0            
        IL_000F: ldc.i4.5           
        IL_000G: rem                 
        IL_000H: brtrue.s IL_000I    

        // Výpis "Fizz Buzz"
        IL_0009: ldstr "Fizz Buzz"       
        IL_0010: call void [mscorlib]System.Console::Write(string) 
        IL_0011: br.s IL_0018       

        // Kontrola dìlitelnosti 3
        IL_000I: ldloc.0            
        IL_000J: ldc.i4.3           
        IL_000K: rem                 
        IL_000L: brtrue.s IL_000Q    

        // Výpis "Fizz"
        IL_0012: ldstr "Fizz"       
        IL_0013: call void [mscorlib]System.Console::Write(string) 
        IL_0014: br.s IL_0018      

        // Kontrola dìlitelnosti 5
        IL_000M: ldloc.0            
        IL_000N: ldc.i4.5           
        IL_000O: rem                 
        IL_000P: brtrue.s IL_000Q    

        // Výpis "Buzz"
        IL_0015: ldstr "Buzz"       
        IL_0016: call void [mscorlib]System.Console::Write(string) 
        IL_0017: br.s IL_0018      

        // Vypisování èísla
        IL_000Q: ldloc.0            
        IL_000R: call void [mscorlib]System.Console::Write(int32) 

        // Kontrola, zda je èíslo poslední
        IL_0018: ldloc.0            
        IL_0019: ldarg.0            
        IL_0020: beq.s IL_0008      

        // Výpis èárky
        IL_0021: ldstr ", "         
        IL_001A: call void [mscorlib]System.Console::Write(string) 

        // Zvyšování poèítadla
        IL_001F: ldloc.0            
        IL_0022: ldc.i4.1           
        IL_0023: add                 
        IL_0024: stloc.0            

        // Návrat na zaèátek smyèky
        IL_0025: br.s IL_0004       
    }

    .method public hidebysig static void Main() cil managed
    {
        .entrypoint
        .maxstack 1

        // Zavolání metody FizzBuzz s limitem 
        IL_0000: ldc.i4.s 100       
        IL_0002: call void FizzBuzzApp::FizzBuzz(int32)

        // Konec metody
        IL_0007: ret
    }
}


