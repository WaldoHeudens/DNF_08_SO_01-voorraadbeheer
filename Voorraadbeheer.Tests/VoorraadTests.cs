using Voorraadbeheer;

namespace Voorraadbeheer.Tests;

public class VoorraadTests
{
    private static Product Toetsenbord => new("TB-01", "Toetsenbord");

    [Fact]
    public void VoegToe_TeltStuksOpBijBestaandeVoorraad()
    {
        var voorraad = new Voorraad();
        voorraad.VoegToe(Toetsenbord, 10);
        voorraad.VoegToe(Toetsenbord, 3);

        Assert.Equal(13, voorraad.AantalVan("TB-01"));
    }

    [Fact]
    public void AantalVan_OnbekendProduct_IsNul()
    {
        var voorraad = new Voorraad();
        Assert.Equal(0, voorraad.AantalVan("XX-99"));
    }

    [Fact]
    public void NeemWeg_VerlaagtDeVoorraad()
    {
        var voorraad = new Voorraad();
        voorraad.VoegToe(Toetsenbord, 10);
        voorraad.NeemWeg("TB-01", 4);

        Assert.Equal(6, voorraad.AantalVan("TB-01"));
    }

    [Fact]
    public void NeemWeg_ExactAlles_IsToegestaan()
    {
        var voorraad = new Voorraad();
        voorraad.VoegToe(Toetsenbord, 5);
        voorraad.NeemWeg("TB-01", 5);

        Assert.Equal(0, voorraad.AantalVan("TB-01"));
    }

    [Fact]
    public void VoegToe_NullProduct_GooitArgumentNullException()
    {
        var voorraad = new Voorraad();
        Assert.Throws<ArgumentNullException>(() => voorraad.VoegToe(null!, 5));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-3)]
    public void VoegToe_OngeldigAantal_GooitArgumentOutOfRangeException(int aantal)
    {
        var voorraad = new Voorraad();
        Assert.Throws<ArgumentOutOfRangeException>(() => voorraad.VoegToe(Toetsenbord, aantal));
    }

    [Fact]
    public void NeemWeg_TeVeel_GooitOnvoldoendeVoorraadException()
    {
        var voorraad = new Voorraad();
        voorraad.VoegToe(Toetsenbord, 5);

        var ex = Assert.Throws<OnvoldoendeVoorraadException>(
            () => voorraad.NeemWeg("TB-01", 8));

        // De exception draagt de juiste context mee.
        Assert.Equal("TB-01", ex.ProductCode);
        Assert.Equal(8, ex.Gevraagd);
        Assert.Equal(5, ex.Beschikbaar);
    }

    [Fact]
    public void NeemWeg_TeVeel_LaatVoorraadOngewijzigd()
    {
        var voorraad = new Voorraad();
        voorraad.VoegToe(Toetsenbord, 5);

        Assert.Throws<OnvoldoendeVoorraadException>(() => voorraad.NeemWeg("TB-01", 8));

        // Na een mislukte wegname mag er niets veranderd zijn.
        Assert.Equal(5, voorraad.AantalVan("TB-01"));
    }
}
