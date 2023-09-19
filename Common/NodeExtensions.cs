using System.Linq;
using System.Reflection;
using Godot;

namespace Common
{
    public static class NodeExtensions
    {
        private static string ToNodeName(string name)
        {
            var normalized = name.StartsWith("_") ? name.Substring(1) : name;
            return normalized.Length < 2 ? normalized : normalized.Substring(0, 1).ToUpper() + normalized.Substring(1);
        }

        public static void InitNodeFields(this Node node)
        {
            var fieldInfos = node.GetType()
                .GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            foreach (var fieldInfo in fieldInfos)
            {
                var nodeAttributes = fieldInfo.GetCustomAttributes(typeof(NodeAttribute), false);
                if (!nodeAttributes.Any())
                    continue;
                var nodePath = (nodeAttributes.First() as NodeAttribute)?.NodePath ?? ToNodeName(fieldInfo.Name);
                fieldInfo.SetValue(node, node.GetNode(nodePath));
            }
        }

        public static void PlaySound(this Node node, string sound)
        {
            SoundPlayer2D.Play(node.GetTree().Root, Res.Sound(sound), node.GetViewport().Size / 2);
        }

        public static void PlayPositionalSound(this Node2D node, string sound)
        {
            SoundPlayer2D.Play(node.GetTree().Root, Res.Sound(sound), node.GlobalPosition);
        }
    }
}