using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ET
{
    public static class GlobalDefine
    {
        public static ILog ILog;

        public static Options Options = new Options();
        /// <summary>
        /// 固定间隔的目标FPS
        /// </summary>
        public const int FixedUpdateTargetFPS = 30;
        public const float FixedUpdateTargetDTTime_Float = 1f / FixedUpdateTargetFPS;

        public const long FixedUpdateTargetDTTime_Long = (long) (FixedUpdateTargetDTTime_Float * 1000);
    }
    
    public class Options
    {
        // [Option("AppType", Required = false, Default = AppType.Server, HelpText = "serverType enum")]
        // public AppType AppType { get; set; }

        // [Option("Process", Required = false, Default = 1)]
        public int Process { get; set; } = 1;
        
        // [Option("Develop", Required = false, Default = 1, HelpText = "develop mode, 0正式 1开发 2压测")]
        public int Develop { get; set; } = 1;

        // [Option("LogLevel", Required = false, Default = 2)]
        public int LogLevel { get; set; } = 2;
        
        // [Option("Console", Required = false, Default = 0)]
        public int Console { get; set; } = 0;
        
        // 进程启动是否创建该进程的scenes
        // [Option("CreateScenes", Required = false, Default = 1)]
        public int CreateScenes { get; set; } = 1;
    }
}