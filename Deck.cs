namespace FreeCell.Card;

using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;


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

    private Array<Card> deck;
    [Export]
    private Pile[] drawPiles = new Pile[8];

    [Export]
    private PackedScene cardNodePad;
    private List<Card> selectedCards = new();
    private Pile selectedPile;
    private int cardsToMoveAmount = 4;
    public override void _Ready()
    {
        deck = CreateCards();
        GeneratePiles();
    }


    public void OnPileSelected(Pile pile, Array<Card> cards)
    {
        if (selectedCards.Count == 0)
        {
            GD.Print($"Select {cards.Count} cards");
            selectedCards = cards.ToList();
            selectedPile = pile;
        }
        else
        {
            TryToMove(pile, cards);
        }
    }

    private void TryToMove(Pile pile, Array<Card> cards)
    {
        int indexForMove = selectedCards.Take(cardsToMoveAmount).ToList().FindIndex(selectedCard => selectedCard.Value == cards.Last().Value - 1 && selectedCard.PipSuit.Suit != cards.Last().PipSuit.Suit);
        if (indexForMove != -1)
        {
            MoveCard(pile, indexForMove);
        }
        else
        {
            GD.Print("Can't move cards");
        }
        GD.Print($"Unselect {selectedCards.Count} cards");
        selectedCards = new();
    }


    private void MoveCard(Pile pile, int indexForMove)
    {
        GD.Print($"Move {indexForMove + 1} cards");
        var cardsToMove = selectedCards.Take(indexForMove + 1).ToList();
        cardsToMove.ForEach(card =>
        {
            selectedPile.RemoveCard(card);
            pile.AddCard(card);
        });
    }


    private void GeneratePiles()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            int pileIndex = i % 8;
            drawPiles[pileIndex].AddCard(deck[i]);
        }
    }

    private Array<Card> CreateCards()
    {
        List<Card> cards = new();
        foreach (var pipSuit in pipSuits)
        {
            for (int i = 0; i < 13; i++)
            {
                Card card = FactoryCards(pipSuit, i);
                cards.Add(card);
            }
        }
        Array<Card> unShuffleDeck = new(cards);
        unShuffleDeck.Shuffle();
        return unShuffleDeck;
    }

    private Card FactoryCards(PipSuit pipSuit, int i)
    {
        Card card = cardNodePad.Instantiate<Card>();
        card.PipSuit = pipSuit;
        card.Value = i;
        return card;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.

    public override void _Process(double delta)
    {
    }
}