namespace FreeCell.Card;

using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Linq;


public partial class Pile : Container
{
	[Signal]
	public delegate void SelectedEventHandler(Pile pile, Array<Card> cards);
	private readonly List<Card> cards = new();


	internal void RemoveCard(Card card)
	{
		card.GuiInput -= OnGuiInput;
		cards.Remove(card);
		RemoveChild(card);
	}

	public void AddCard(Card card)
	{
		card.GuiInput += OnGuiInput;
		cards.Add(card);
		AddChild(card);
	}

	private void OnGuiInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseButton && mouseButton.ButtonIndex == MouseButton.Left && mouseButton.Pressed)
		{
			List<Card> cardsList = SelectCards();
			EmitSignal(nameof(Selected), new Variant[] { this, new Array<Card>(cardsList) });
			GD.Print($"Pile emit Selected signal with: {cardsList.Count} cards");
		}
	}

	private List<Card> SelectCards()
	{
		List<Card> cardsList = new();

		for (int i = cards.Count; i > 0; i--)
		{
			Card card = cards[i - 1];
			if (i == cards.Count || card.Value == cardsList.Last().Value + 1 && card.PipSuit.Suit != cardsList.Last().PipSuit.Suit)
			{
				cardsList.Add(card);
			}
			else
			{
				break;
			}
		}

		return cardsList;
	}


}
