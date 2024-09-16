using Godot;
namespace FreeCell.Card;

public partial class FreeCell : PanelContainer
{
	[Signal]
	public delegate void SelectedEventHandler(FreeCell freeCell, Card? card);
	private Card? card;

	public override void _Ready()
	{
		GuiInput += OnGuiInput;
	}


	internal void RemoveCard()
	{
		if (card == null)
		{
			return;
		}
		RemoveChild(card);
	}

	public void AddCard(Card card)
	{
		if (this.card == null)
		{
			this.card = card;
			AddChild(this.card);
		}
	}

	public bool IsEmpty()
	{
		return card == null;
	}
	private void OnGuiInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseButton && mouseButton.ButtonIndex == MouseButton.Left && mouseButton.Pressed)
		{
			if (card == null)
			{
				EmitSignal(nameof(Selected), new Variant[] { this });
			}
			else
			{
				EmitSignal(nameof(Selected), new Variant[] { this, card });
			}
			GD.Print($"FreeCell emit Selected signal");
		}
	}

}
