using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ET
{
    public static class GlobalDefine
    {
        public static ILog ILog;

        public static int Console { get; set; } = 0;
        public static int LogLevel { get; set; } = 2;
    }
}