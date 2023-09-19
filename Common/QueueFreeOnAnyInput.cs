using Godot;

namespace Common
{
    public class QueueFreeOnAnyInput : Node
    {
        public override void _Input(InputEvent @event)
        {
            base._Input(@event);
            if (@event.IsPressed())
                QueueFree();
        }
    }
}