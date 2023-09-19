using Godot;
using static Godot.Engine;

namespace Common
{
    [Tool]
    public class Animation : Sprite
    {
        [Signal]
        public delegate void Finished();

        private bool _finishedEmitted;

        private float _time;
        [Export] public bool Loop;
        [Export] public bool QueueFreeOnFinished = true;
        [Export] public float SecondsPerFrame = 0.1f;

        [Export]
        public int HorizontalFrames
        {
            get => Hframes;
            set => Hframes = value;
        }

        public void Reset()
        {
            Frame = 0;
            _time = 0;
            _finishedEmitted = false;
        }

        public override void _Ready()
        {
            base._Ready();
            Reset();
        }

        public override void _PhysicsProcess(float delta)
        {
            base._PhysicsProcess(delta);
            if (SecondsPerFrame <= 0.0f)
                return;
            _time += delta;
            var frame = (int)(_time / SecondsPerFrame);
            if (frame < Hframes * Vframes)
            {
                Frame = frame;
                return;
            }

            if (!_finishedEmitted)
            {
                EmitSignal(nameof(Finished));
                _finishedEmitted = true;
            }

            if (QueueFreeOnFinished & !EditorHint)
            {
                QueueFree();
                return;
            }

            if (Loop) Reset();
        }
    }
}