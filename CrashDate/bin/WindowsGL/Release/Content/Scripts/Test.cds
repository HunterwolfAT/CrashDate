bgfade schneelandschaft
fichar Testy
body 2
s Heute ist ein schöner Tag!
bgset twgok
song Ohne Titel
	s So, ich bin hier also... wo zum geier bin ich denn hier?\nHab ich wieder zuviel gesoffen??
choice Geh auf sie zu.
choice Sag weiter nichts.
choice ask
// Geh auf sie zu
	choice case 1
		s Okay, ich werd mal versuchen sie anzusprechen.
		s Hallo? Mädchen? Was... bist du?
		s ...
		s Keine Antwort.
		choice Ach, mir doch egal.
		choice Genauer hinsehen.
		choice ask
		// Ach, mir doch egal
			choice case 1
				s Tjoa, wenn sie nicht will, braucht es mich auch nicht zu stören.
				s Wo ist den hier der Ausgang?
		// Genauer hinsehen.
			choice case 2
				s Hey, Moment mal! Das ist ja nur ein Hintergrund! Pah!
				s So wird man auch schlauer.
		choice case end
// Sag weiter nichts
	choice case 2
		s Ja, ich denke es ist besser wenn ich mich aus dem Staub mache.
		s Nicht noch dass sie mir hier was vorsingt oder so. *Schauder*
choice case end

	s Ich, ähm, bin dann mal weg. Raus hier.
bgfade schneelandschaft
	s Ah, viel besser.
	s Oh, das ist ja Jemand!
fichar Testy
body 2
	s fchar p Hallo!
fchar Testy
body 0
	s fchar Testy ...
	s fchar p Kannst du nicht sprechen?\nDas ist okay. Ich spreche auch nicht so gern.\nAusser halt jetzt grade.
	s Wir können auch beide zusammen nicht sprechen! Pass auf!
	s fchar Testy ...
body 3
	s ... ...
	s fchar p ... ... ...
fchar Testy
body 1
	s ... ... ... ...
	s fchar p Oh, oh. Ich glaub das mag sie gar nicht.
	s Was soll ich am besten tun?
choice Geh auf sie zu.
choice Sag weiter nichts.
choice ask
fochar Testy
	s fchar p Dann halt nicht....
	s ENDE
run Test2