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
    }
}