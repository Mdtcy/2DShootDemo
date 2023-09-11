using System;
using UnityEngine;

namespace BerserkPixel.Tilemap_Generator.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class NoteAttribute : PropertyAttribute
    {
        public string Text = string.Empty;

        public NoteAttribute(string text)
        {
            Text = text;
        }
    }
}