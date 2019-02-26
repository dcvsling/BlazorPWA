using System;

namespace BlazorPWA.Components.Editor.Services
{
    public class EditorModel
    {
        public string Script { get; set; } = "// Code goes here";
        public string Language { get; set; } = "javascript";
        public string Id { get; set; } = $"Editor_{new Random().Next(0, 1000000).ToString()}";
        public int Width { get; set; } = 800;
        public int Height { get; set; } = 600;
    }
}
