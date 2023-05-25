using Sirenix.OdinInspector;
using System;
public static class OdinTool
{

    public static string GetLabelText(Type t, string fieldName)
    {
        var f = t.GetField(fieldName);
        if (f != null)
        {
            var arr = f.GetCustomAttributes(false);
            foreach (var item in arr)
            {
                if (item is LabelTextAttribute lt)
                {
                    return lt.Text;
                }
            }
        }
        return null;
    }

    public static string GetLabelText<T>(T t, string fieldName)
    {
        var f = t.GetType().GetField(fieldName);
        if (f != null)
        {
            var arr = f.GetCustomAttributes(false);
            foreach (var item in arr)
            {
                if (item is LabelTextAttribute lt)
                {
                    return lt.Text;
                }
            }
        }
        return null;
    }
}