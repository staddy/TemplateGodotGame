using Godot;

namespace Common
{
    public class SoundPlayer2D : AudioStreamPlayer2D
    {
        private readonly float _lifeTime;
        private readonly Node2D _source;
        private float _life;

        private SoundPlayer2D(AudioStream sound, Vector2 position)
        {
            Stream = sound;
            GlobalPosition = position;
            _lifeTime = sound.GetLength();
        }

        private SoundPlayer2D(AudioStream sound, Node2D source) : this(sound, source.GlobalPosition)
        {
            _source = source;
        }

        public static void Play(Node world, AudioStream sound, Vector2 position)
        {
            world.AddChild(new SoundPlayer2D(sound, position));
        }

        public static void Play(Node world, AudioStream sound, Node2D source)
        {
            world.AddChild(new SoundPlayer2D(sound, source));
        }

        public override void _Ready()
        {
            base._Ready();
            Play();
        }

        public override void _PhysicsProcess(float delta)
        {
            base._PhysicsProcess(delta);
            _life += delta;
            if (_life >= _lifeTime)
                QueueFree();
            if (_source != null)
            {
                if (IsInstanceValid(_source))
                    GlobalPosition = _source.GlobalPosition;
                else
                    QueueFree();
            }
        }
    }
}