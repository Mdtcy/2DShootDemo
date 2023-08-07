using UnityEngine;

namespace GameMain
{
    public static class MonoExtension
    {
        public static T GetOrAddComponent<T>(this MonoBehaviour script) where T : Component
        {
            T result = script.gameObject.GetComponent<T>();
            if (result == null)
            {
                result = script.gameObject.AddComponent<T>();
            }
            return result;
        }

        public static RectTransform RTrans(this MonoBehaviour script)
        {
            return script.gameObject.GetComponent<RectTransform>();
        }
    }
}