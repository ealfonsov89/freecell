using Godot;
using System.Collections.Generic;
namespace FreeCell.Card;

public partial class Pack : PanelContainer
{
	[Signal]
	public delegate void SelectedEventHandler(Pack pack);
	readonly LinkedList<Card> cardsPack = new();

	public override void _Ready()
	{
		GuiInput += OnGuiInput;
	}

	internal void RemoveCard()
	{
		if (cardsPack.Last != null)
		{
			RemoveChild(cardsPack.Last.Value);
			cardsPack.RemoveLast();
			if (cardsPack.Last != null)
			{
				AddChild(cardsPack.Last.Value);
			}
		}
	}

	public void AddCard(Card card)
	{
		if (cardsPack.Last != null)
		{
			RemoveChild(cardsPack.Last.Value);
		}
		cardsPack.AddLast(card);
		AddChild(card);
	}

	private void OnGuiInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseButton && mouseButton.ButtonIndex == MouseButton.Left && mouseButton.Pressed)
		{
			EmitSignal(nameof(Selected), new Variant[] { this });
			GD.Print($"Pack emit Selected signal");
		}
	}
}
