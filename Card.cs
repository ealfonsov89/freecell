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

	public override void _Ready()
	{
		string pipText = $"{(char)pipSuit.Pip}";
		string valueText = value switch
		{
			0 => "A",
			10 => "J",
			11 => "Q",
			12 => "K",
			_ => $"{value + 1}",
		};
		valueLabel.Text = $"{pipText} {valueText}";
		pipLabel.Text = pipText;
		Color color = pipSuit.Suit == Suit.red ? new Color(255, 0, 0) : new Color(0, 0, 0);
		valueLabel.AddThemeColorOverride("font_color", color);
		pipLabel.AddThemeColorOverride("font_color", color);
	}
}
