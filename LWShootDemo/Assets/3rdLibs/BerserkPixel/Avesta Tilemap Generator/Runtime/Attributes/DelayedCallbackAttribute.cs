using System;
using UnityEngine;

namespace BerserkPixel.Tilemap_Generator.Attributes
{
    /// <summary>
    ///   <para>Attribute used to make a variable in a script be delayed and afterwards invoke a callback.</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class DelayedCallbackAttribute : PropertyAttribute
    {
        public readonly float time;
        public readonly string propertyName;

        private const float DEFAULT_TIME = .1f; 
        
        public DelayedCallbackAttribute(string propertyName, float time)
        {
            this.propertyName = propertyName;
            this.time = time;
        }
        
        public DelayedCallbackAttribute(string propertyName)
        {
            this.propertyName = propertyName;
            this.time = DEFAULT_TIME;
        }
    }
}