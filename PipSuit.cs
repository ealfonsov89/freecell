namespace FreeCell.Card;

public readonly struct PipSuit
{
    readonly Suit suit;
    readonly Pip pip;

    public Suit Suit => suit;

    public Pip Pip => pip;

    public PipSuit(Suit suit, Pip pip)
    {
        this.suit = suit;
        this.pip = pip;
    }
}