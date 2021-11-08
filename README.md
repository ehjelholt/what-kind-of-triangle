# WhatKindOfTriangle

Forretningskoden i projektet ligger i TriangleInspector.DetermineTypeOfTriangle.  
Den resterende kode håndterer UI og parser input fra brugeren.

Metoden TriangleInspector.DetermineTypeOfTriangle er "pure" og dens return-værdi afhænger udelukkende af dens argumenter.  
Alle argumenter er value types og kan ikke ændre værdi mens metoden eksekverer.  
Den har ingen "side effects" og anvender ikke værdier hentet udenfor metoden selv.  
Den anvender således heller ikke værdier hentet via fx injectede services.  
Den kan heller ikke kaste exceptions.

Mht. testing, så kan TriangleInspector.DetermineTypeOfTriangle testes "udefra" ved blot at kalde metoden og checke retur-værdien.  
Der skal ikke bruges mocks eller lignende for at teste metoden.  

TriangleInspector.DetermineTypeOfTriangle er i dette project lavet "static". Dette kunne måske skabe problemer hvis den skulle indgå som en del 
af en kodebase opbygget med dependency injection og testet vha. mock'ede interfaces. Disse problemer kunne imødegåes ved at refaktorere eller ved at gemme 
kaldet til metoden bag et interface eller en Func<>, som kan mock'es eller instantieres som en del af en test.

Test af den resterende "runner-kode" (UI og håndtering af bruger input) ville være meget mere udfordrende. Hvis den skulle testes ville jeg refaktorere.  
Som et minimum skal brugen af Console-metoderne gemmes bag et interface, som kan mock'es og hvor argumenterne til kaldene af de mock'ede metoder kan verificeres.  
Jeg ville nok se mig om efter en NuGet-pakke med en commandline parser, som var "testbar".  
Uagtet om en NuGet-pakke var i brug eller ej, så kunne en sådan test hurtigt gå hen og blive meget sårbar og ofte knække.  
Værdien af sådanne sårbare tests skal overvejes nøje. 

Desværre kommer type-systemet og pattern matching i C# nogle gange til kort, og man er nødt til at skrive mindre læsbar kode eller kode, 
som man ved aldrig kan rammes. Nederst i TriangleInspector.DetermineTypeOfTriangle og i Program.Print er der eksempler på dette, uddybet i en kommentarer.