namespace Voorraadbeheer;

/// <summary>
/// Houdt per productcode bij hoeveel stuks er in voorraad zijn.
/// Elke publieke method bewaakt zijn invoer met guard clauses, zodat de voorraad
/// nooit in een ongeldige toestand terechtkomt.
/// </summary>
public class Voorraad
{
    // readonly: het field verwijst altijd naar dezelfde dictionary. De inhoud mag
    // wijzigen, maar we kunnen het field zelf niet per ongeluk vervangen.
    private readonly Dictionary<string, int> _aantallen = [];

    /// <summary>Voegt een aantal stuks van een product toe aan de voorraad.</summary>
    public void VoegToe(Product product, int aantal)
    {
        ArgumentNullException.ThrowIfNull(product);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(aantal);

        int huidig = _aantallen.GetValueOrDefault(product.Code);
        _aantallen[product.Code] = huidig + aantal;
    }

    /// <summary>
    /// Neemt een aantal stuks weg van een product. Gooit een
    /// <see cref="OnvoldoendeVoorraadException"/> als er te weinig in voorraad is.
    /// </summary>
    public void NeemWeg(string productCode, int aantal)
    {
        ArgumentNullException.ThrowIfNull(productCode);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(aantal);

        int beschikbaar = _aantallen.GetValueOrDefault(productCode);
        if (aantal > beschikbaar)
        {
            throw new OnvoldoendeVoorraadException(productCode, aantal, beschikbaar);
        }

        _aantallen[productCode] = beschikbaar - aantal;
    }

    /// <summary>Het aantal stuks van een product in voorraad (0 als onbekend).</summary>
    public int AantalVan(string productCode)
    {
        ArgumentNullException.ThrowIfNull(productCode);
        return _aantallen.GetValueOrDefault(productCode);
    }
}
