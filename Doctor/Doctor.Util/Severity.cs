using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor.Util
{
    public class Severity
    {
        public enum SeverityEnum 
        {
            Unanalyzed = -1,
            Normal = 0,
            Light = 1,
            Medium = 2,
            Severe = 3
        }

        /// <summary>
        /// 根据分数返回严重程度
        /// </summary>
        /// <param name="score"></param>
        /// <returns></returns>
        public static SeverityEnum Group(double? score)
        {
            if (!score.HasValue)
	        {
		        return SeverityEnum.Unanalyzed;
	        }

            if (score < 80)
            {
                return SeverityEnum.Severe;
            }
            else if (score < 90)
            {
                return SeverityEnum.Medium;
            }
            else if (score < 99)
            {
                return SeverityEnum.Light;
            }
            else
            {
                return SeverityEnum.Normal;
            }
        }
    }
}
