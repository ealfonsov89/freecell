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

    private Array<Card>? deck;
    [Export]
    private Pile[] drawPiles = new Pile[8];
    [Export]
    private FreeCell[] freeCells = new FreeCell[4];

    [Export]
    private PackedScene? cardNodePad;
    private List<Card> selectedCards = new();
    private Pile? selectedPile;
    private int cardsToMoveAmount = 4;
    public override void _Ready()
    {
        deck = CreateCards();
        GeneratePiles();
    }

    public void OnPackSelected(Pack pack)
    {
        if (selectedCards.Count == 0)
        {
            return;
        }
        selectedPile?.RemoveCard(selectedCards.First());
        pack.AddCard(selectedCards.First());
        selectedCards = new();
        cardsToMoveAmount--;
    }

    public void OnFreeCellSelected(FreeCell freeCell)
    {
        if (freeCell.IsEmpty())
        {
            if (selectedCards.Count == 0)
            {
                return;
            }
            selectedPile?.RemoveCard(selectedCards.First());
            freeCell.AddCard(selectedCards.First());
            selectedCards = new();
            cardsToMoveAmount--;
        }
        else
        {
            return;
        }
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
        int indexForMove = GetAmountToMove(cards);
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

    private int GetAmountToMove(Array<Card> cards)
    {
        if (cards.Count == 0)
        {
            GD.Print("Move Cards to empty pile");
            return cardsToMoveAmount;
        }
        else
        {
            int indexForMove = selectedCards.Take(cardsToMoveAmount).ToList().FindIndex(selectedCard => selectedCard.Value == cards.First().Value - 1 && selectedCard.PipSuit.Suit != cards.First().PipSuit.Suit);
            GD.Print($"Index for move {indexForMove}");
            return indexForMove;
        }
    }

    private void MoveCard(Pile pile, int indexForMove)
    {
        GD.Print($"Move {indexForMove + 1} cards");
        var cardsToMove = selectedCards.Take(indexForMove + 1).ToList();
        cardsToMove.Reverse();
        cardsToMove.ForEach(card =>
        {
            selectedPile?.RemoveCard(card);
            pile.AddCard(card);
        });
    }


    private void GeneratePiles()
    {
        if (deck == null)
        {
            return;
        }
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
                Card? card = FactoryCards(pipSuit, i);
                if (card == null)
                {
                    continue;
                }
                cards.Add(card);
            }
        }
        Array<Card> unShuffleDeck = new(cards);
        unShuffleDeck.Shuffle();
        return unShuffleDeck;
    }

    private Card? FactoryCards(PipSuit pipSuit, int i)
    {
        if (cardNodePad == null)
        {
            return null;
        }
        Card card = cardNodePad.Instantiate<Card>();
        card.PipSuit = pipSuit;
        card.Value = i;
        return card;
    }

}