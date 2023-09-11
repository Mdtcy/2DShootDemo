using BerserkPixel.Tilemap_Generator.Attributes;
using UnityEditor;
using UnityEngine;

namespace BerserkPixel.Tilemap_Generator.Utilities
{
    [CustomPropertyDrawer(typeof(NoteAttribute))]
    public class NoteDrawer : DecoratorDrawer
    {
        private const float _padding = 10;
        private float _height;

        public override float GetHeight()
        {
            var noteAttribute = attribute as NoteAttribute;

            var style = EditorStyles.helpBox;
            style.alignment = TextAnchor.MiddleLeft;
            style.wordWrap = true;
            style.padding = new RectOffset(8, 8, 8, 8);
            style.fontSize = 12;

            _height = style.CalcHeight(new GUIContent(noteAttribute.Text), Screen.width);

            return _height + _padding;
        }

        public override void OnGUI(Rect position)
        {
            var noteAttribute = attribute as NoteAttribute;

            position.height = _height;
            position.y += _padding * .5f;

            EditorGUI.HelpBox(position, noteAttribute.Text, MessageType.None);
        }
    }
}