Ausf�hrung in der Konsole:

	0. Visual Studio Projekt im Ordner src/ �ffnen
	1. Verweise auf NX-Bibliotheken anpassen (%UGII_ROOTDIR%\UGII\managed\*.DLL)
	2. Ggf. auf *.dll im Ordner lib/ verweisen (nicht lib/dotsesame2_3_0/*.dll)
	3. Kompilieren (in Visual Studio 2008)
	(6. Evtl. Pfade in den Batch-Dateien, z.B. 'Schnittstelle_NX_Flaschenpicker.bat' anpassen)
    7. Alle DLL-Dateien im Unterordner 'lib/dotsesame2_3_0' in den Ordner '%UGII_ROOTDIR%\UGII\managed\' kopieren (wegen OntologyAccessHelper)
	8. Batch-Datei ausf�hren
	9. Folgende Dateien werden generiert:
		a) bin/Schnittstelle_NX.config.xml enth�lt die Konfiguration f�r das Ontologie-Konzept
		b) Informationsmodell/Export/*.owl enth�lt die aus NX extrahierte Informationen im RDF/XML Format
		
Schnittstelle_NX erweitern:

	Siehe 'doc/Dokumentation_Schnittstelle_NX.doc'	