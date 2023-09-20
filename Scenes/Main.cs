using Common;
using Godot;

public class Main : Node2D
{
    public override void _Ready()
    {
        base._Ready();
        this.InitNodeFields();
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (@event.IsActionPressed("screenshot") && !OS.HasFeature("standalone"))
        {
            var screenshot = GetViewport().GetTexture().GetData();
            screenshot.FlipY();
            screenshot.Resize(screenshot.GetWidth() * 2, screenshot.GetHeight() * 2, Image.Interpolation.Nearest);
            screenshot.SavePng($"res://Output/screenshots/{(long)(Time.GetUnixTimeFromSystem() * 1000)}.png");
        }
    }
}