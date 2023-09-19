using System.Collections.Generic;
using Godot;

namespace Common
{
    public class Res
    {
        private static readonly Dictionary<string, PackedScene> PackedScenes = new Dictionary<string, PackedScene>();
        private static readonly Dictionary<string, AudioStream> Sounds = new Dictionary<string, AudioStream>();

        static Res()
        {
            LoadResources<PackedScene, PackedScene>(PackedScenes, "res://Scenes/", "tscn");
            LoadResources<AudioStreamSample, AudioStream>(Sounds, "res://Sounds/", "wav", true);
            LoadResources<AudioStreamOGGVorbis, AudioStream>(Sounds, "res://Sounds/", "ogg", true);
        }

        private static void LoadResources<T, TU>(IDictionary<string, TU> container, string path, string extension,
            bool addImport = false)
            where T : class, TU
        {
            foreach (var file in GetFiles(path, addImport ? "import" : extension))
            {
                var fileName = file;
                if (addImport)
                {
                    fileName = file.BaseName();
                    if (fileName.Extension() != extension)
                        continue;
                }

                var resource = ResourceLoader.Load<T>(fileName);
                var name = fileName.Substring(path.Length, fileName.Length - path.Length - extension.Length - 1);
                container[name] = resource;
            }
        }

        private static List<string> GetFiles(string path, string extension)
        {
            var dir = new Directory();
            dir.Open(path);
            dir.ListDirBegin(true);
            var files = new List<string>();
            var fileName = dir.GetNext();
            while (!fileName.Empty())
            {
                var fullName = dir.GetCurrentDir().PlusFile(fileName);
                if (dir.CurrentIsDir())
                    files.AddRange(GetFiles(fullName, extension));
                else if (fileName.Extension() == extension) files.Add(fullName);
                fileName = dir.GetNext();
            }

            return files;
        }

        public static T Scene<T>(string name) where T : class
        {
            return PackedScenes[name].Instance<T>();
        }

        public static AudioStream Sound(string name)
        {
            return Sounds[name];
        }

        public static Node Scene(string name)
        {
            return Scene<Node>(name);
        }
    }
}