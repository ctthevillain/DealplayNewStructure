using System;

namespace J4RV.VisualDebug {

    public static class TimeUtils {
        
        public static string GetFormattedMoment(){
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

    }
    
}