using System.Collections.Generic;
using UnityEngine;

namespace J4RV.VisualDebug{

    [CreateAssetMenu(fileName = "LogColors", menuName = "VisualDebug/LogColors", order = 1)]
    public class LogColors : ScriptableObject {

        public Color32 Log;
        public Color32 Warning;
        public Color32 Exception;
        public Color32 Error;
        public Color32 Assert;        

        internal Dictionary<LogType, string> GetDictionary(){
            return new Dictionary<LogType, string>(){
                { LogType.Log,       HexString(Log) },
                { LogType.Warning,   HexString(Warning) },
                { LogType.Exception, HexString(Exception) },
                { LogType.Error,     HexString(Error) },
                { LogType.Assert,    HexString(Assert) },
            };
        }

        private string HexString(Color32 color) {
            return color.r.ToString("X2")
                + color.g.ToString("X2")
                + color.b.ToString("X2");
        }
        
    }

}
