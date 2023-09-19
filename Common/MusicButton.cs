using Godot;

namespace Common
{
    public class MusicButton : TextureButton
    {
        [Export] public string OstName = "";

        private void Init()
        {
            if (OstName == "")
            {
                Disabled = true;
                Pressed = false;
                return;
            }

            if (SoundPlayerBackground.Get() == null)
            {
                SoundPlayerBackground.Init(GetTree().Root);
                SoundPlayerBackground.Play(Res.Sound(OstName));
            }
            else
            {
                SoundPlayerBackground.ResetPitchScale();
            }

            Pressed = !SoundPlayerBackground.Get().Playing;
        }

        public override void _Ready()
        {
            base._Ready();
            Init();
        }

        public override void _Input(InputEvent @event)
        {
            base._Input(@event);
            if (@event.IsActionPressed("mute")) Pressed = !Pressed;
        }

        public override void _Toggled(bool buttonPressed)
        {
            base._Toggled(buttonPressed);
            SoundPlayerBackground.Get().Playing = !Pressed;
        }
    }
}