using Godot;

namespace Common
{
    public class SoundPlayerBackground : AudioStreamPlayer
    {
        private static SoundPlayerBackground _instance;
        private float _pitchScaleAnimationTime;
        private float _targetPitchScale = 1.0f;

        private SoundPlayerBackground(Node root)
        {
            root.CallDeferred("add_child", this);
        }

        public static SoundPlayerBackground Get()
        {
            return _instance;
        }

        public static void AnimatePitchScale(float target, float time)
        {
            if (_instance == null || target <= 0 || time <= 0)
                return;
            _instance._targetPitchScale = target;
            _instance._pitchScaleAnimationTime = time;
        }

        public static void ResetPitchScale()
        {
            if (_instance == null)
                return;
            _instance._targetPitchScale = 1.0f;
            _instance._pitchScaleAnimationTime = 0.0f;
            _instance.PitchScale = 1.0f;
        }

        public override void _PhysicsProcess(float delta)
        {
            base._PhysicsProcess(delta);
            if (_pitchScaleAnimationTime > 0.0f)
            {
                PitchScale += (_targetPitchScale - PitchScale) * Mathf.Max(delta / _pitchScaleAnimationTime, 1.0f);
                _pitchScaleAnimationTime -= delta;
            }
        }

        public static void Init(Node root)
        {
            if (_instance == null)
                _instance = new SoundPlayerBackground(root);
        }

        public static void Play(AudioStream stream)
        {
            if (_instance == null)
                return;
            _instance.Stream = stream;
            _instance.Play();
        }
    }
}