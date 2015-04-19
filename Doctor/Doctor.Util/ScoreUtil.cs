using Doctor.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor.Util
{
    public class ScoreUtil
    {
        /// <summary>
        /// 将一次自检的图片分析结果折合成分数
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        public static float GetScore(List<CVResultModel> results)
        {
            //通过数据库得到分析的结果
            float score = 100;
            foreach (var result in results)
            {
                string[] illnesses = result.Teeth_illnesses.Split(new char[] { '_' });
                //Illnesses: 
                //0：正常
                //1: 浅龋 2：中龋 4：深龋
                foreach (var illness in illnesses)
                {
                    score -= int.Parse(illness);
                }
            }
            return score;
        }
    }
}
