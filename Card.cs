namespace FreeCell.Card;
using Godot;

public partial class Card : Control
{
	private int value = 0;
	private PipSuit pipSuit = new(Suit.black, Pip.clubs);

	[Export]
	public Label valueLabel;
	[Export]
	public Label pipLabel;

	public PipSuit PipSuit { get => pipSuit; set => pipSuit = value; }
	public int Value { get => value; set => this.value = value; }

	// Called when the node enters the scene tree for the first time.

	public override void _Ready()
	{
		string pipText = $"{(char)pipSuit.Pip}";

		string valueText;
		switch (value)
		{
			case 0:
				valueText = "A";
				break;
			case 10:
				valueText = "J";
				break;
			case 11:
				valueText = "Q";
				break;
			case 12:
				valueText = "K";
				break;
			default:
				valueText = $"{value + 1}";
				break;
		}
		valueLabel.Text = $"{pipText} {valueText}";
		pipLabel.Text = pipText;
		Color color = pipSuit.Suit == Suit.red ? new Color(255, 0, 0) : new Color(0, 0, 0);
		valueLabel.AddThemeColorOverride("font_color", color);
		pipLabel.AddThemeColorOverride("font_color", color);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
