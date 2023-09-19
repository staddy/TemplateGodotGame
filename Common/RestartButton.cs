using Godot;

namespace Common
{
    public class RestartButton : TextureButton
    {
        public override void _Input(InputEvent @event)
        {
            base._Input(@event);
            if (@event.IsActionPressed("restart"))
                GetTree().ReloadCurrentScene();
        }

        public override void _Pressed()
        {
            base._Pressed();
            GetTree().ReloadCurrentScene();
        }
    }
}