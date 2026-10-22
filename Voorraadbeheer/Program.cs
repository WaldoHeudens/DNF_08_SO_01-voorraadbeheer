using Voorraadbeheer;

var voorraad = new Voorraad();
var toetsenbord = new Product("TB-01", "Toetsenbord");
var muis = new Product("MU-01", "Muis");

voorraad.VoegToe(toetsenbord, 10);
voorraad.VoegToe(muis, 5);
voorraad.VoegToe(toetsenbord, 3);   // vult aan tot 13

Console.WriteLine($"Toetsenborden in voorraad: {voorraad.AantalVan("TB-01")}");

voorraad.NeemWeg("TB-01", 4);
Console.WriteLine($"Na wegnemen van 4: {voorraad.AantalVan("TB-01")}");

// Probeer meer weg te nemen dan er in voorraad is.
try
{
    voorraad.NeemWeg("MU-01", 8);
}
catch (OnvoldoendeVoorraadException ex)
{
    Console.WriteLine($"Geweigerd: {ex.Message}");
    Console.WriteLine($"  (gevraagd {ex.Gevraagd}, beschikbaar {ex.Beschikbaar})");
}
