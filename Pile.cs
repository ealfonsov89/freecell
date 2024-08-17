namespace FreeCell.Card;

using Godot;
using System.Collections.Generic;

public partial class Pile : Container
{
	[Export]
	private int cardMargin = 110;
	private List<Card> cards = new();

	public void AddCard(Card card)
	{
		cards.Add(card);
		AddChild(card);
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
