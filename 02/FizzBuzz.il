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

        // Inicializace po��tadla
        IL_0000: ldc.i4.1           
        IL_0001: stloc.0           

        // Za��tek smy�ky
        IL_0002: br.s IL_0004       

        // Kontrola podm�nky smy�ky
        IL_0004: ldloc.0            
        IL_0005: ldarg.0            
        IL_0006: ble.s IL_000A      

        // Konec smy�ky
        IL_0008: ret                // N�vrat z metody

        // Kontrola d�litelnosti 3
        IL_000A: ldloc.0            
        IL_000B: ldc.i4.3           
        IL_000C: rem                 
        IL_000D: brtrue.s IL_000M    

        // Kontrola d�litelnosti 5
        IL_000E: ldloc.0            
        IL_000F: ldc.i4.5           
        IL_000G: rem                 
        IL_000H: brtrue.s IL_000I    

        // V�pis "Fizz Buzz"
        IL_0009: ldstr "Fizz Buzz"       
        IL_0010: call void [mscorlib]System.Console::Write(string) 
        IL_0011: br.s IL_0018       

        // Kontrola d�litelnosti 3
        IL_000I: ldloc.0            
        IL_000J: ldc.i4.3           
        IL_000K: rem                 
        IL_000L: brtrue.s IL_000Q    

        // V�pis "Fizz"
        IL_0012: ldstr "Fizz"       
        IL_0013: call void [mscorlib]System.Console::Write(string) 
        IL_0014: br.s IL_0018      

        // Kontrola d�litelnosti 5
        IL_000M: ldloc.0            
        IL_000N: ldc.i4.5           
        IL_000O: rem                 
        IL_000P: brtrue.s IL_000Q    

        // V�pis "Buzz"
        IL_0015: ldstr "Buzz"       
        IL_0016: call void [mscorlib]System.Console::Write(string) 
        IL_0017: br.s IL_0018      

        // Vypisov�n� ��sla
        IL_000Q: ldloc.0            
        IL_000R: call void [mscorlib]System.Console::Write(int32) 

        // Kontrola, zda je ��slo posledn�
        IL_0018: ldloc.0            
        IL_0019: ldarg.0            
        IL_0020: beq.s IL_0008      

        // V�pis ��rky
        IL_0021: ldstr ", "         
        IL_001A: call void [mscorlib]System.Console::Write(string) 

        // Zvy�ov�n� po��tadla
        IL_001F: ldloc.0            
        IL_0022: ldc.i4.1           
        IL_0023: add                 
        IL_0024: stloc.0            

        // N�vrat na za��tek smy�ky
        IL_0025: br.s IL_0004       
    }

    .method public hidebysig static void Main() cil managed
    {
        .entrypoint
        .maxstack 1

        // Zavol�n� metody FizzBuzz s limitem 
        IL_0000: ldc.i4.s 100       
        IL_0002: call void FizzBuzzApp::FizzBuzz(int32)

        // Konec metody
        IL_0007: ret
    }
}


