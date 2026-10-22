namespace Voorraadbeheer;

/// <summary>
/// Gegooid wanneer je meer stuks wil wegnemen dan er in voorraad zijn.
/// De exception draagt de context mee (welk product, hoeveel gevraagd, hoeveel
/// beschikbaar), zodat de aanroeper daar iets zinnigs mee kan doen.
/// </summary>
public class OnvoldoendeVoorraadException : Exception
{
    public string ProductCode { get; }
    public int Gevraagd { get; }
    public int Beschikbaar { get; }

    public OnvoldoendeVoorraadException(string productCode, int gevraagd, int beschikbaar)
        : base($"Onvoldoende voorraad voor '{productCode}': gevraagd {gevraagd}, beschikbaar {beschikbaar}.")
    {
        ProductCode = productCode;
        Gevraagd = gevraagd;
        Beschikbaar = beschikbaar;
    }
}
