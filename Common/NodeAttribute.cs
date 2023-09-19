using System;

namespace Common
{
    [AttributeUsage(AttributeTargets.Field)]
    public class NodeAttribute : Attribute
    {
        public readonly string NodePath;

        public NodeAttribute(string nodePath = null)
        {
            NodePath = nodePath;
        }
    }
}