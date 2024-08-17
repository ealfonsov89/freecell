namespace FreeCell.Card;

using System;
using System.Collections.Generic;
using Godot;
public partial class Deck : Node
{
    private static readonly PipSuit heartsSuit = new(Suit.red, Pip.hearts);
    private static readonly PipSuit diamondsSuit = new(Suit.red, Pip.diamonds);
    private static readonly PipSuit spadesSuit = new(Suit.black, Pip.spades);
    private static readonly PipSuit clubsSuit = new(Suit.black, Pip.clubs);

    private readonly PipSuit[] pipSuits = new PipSuit[]
    {
        heartsSuit,
        diamondsSuit,
        spadesSuit,
        clubsSuit
    };

    private readonly Card[] deck = new Card[52];
    private readonly Random random = new();

    [Export]
    private Pile[] drawPiles = new Pile[8];

    [Export]
    private PackedScene cardNodePad;
    public override void _Ready()
    {
        List<Card> cards = CreateCards();
        Shuffle(cards);
        GeneratePiles();
    }

    private void GeneratePiles()
    {
        for (int i = 0; i < deck.Length; i++)
        {
            int pileIndex = i % 8;
            drawPiles[pileIndex].AddCard(deck[i]);
        }
    }

    private void Shuffle(List<Card> cards)
    {
        for (int i = 0; i < deck.Length; i++)
        {
            int cardIndex = random.Next(0, cards.Count);
            deck[i] = cards[cardIndex];
            cards.RemoveAt(cardIndex);
        }
    }

    private List<Card> CreateCards()
    {
        List<Card> cards = new();
        foreach (var pipSuit in pipSuits)
        {
            for (int i = 0; i < 13; i++)
            {
                Card card = cardNodePad.Instantiate<Card>();
                card.PipSuit = pipSuit;
                card.Value = i;
                cards.Add(card);
            }
        }

        return cards;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}