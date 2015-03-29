using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Doctor.Util
{
    class Encryption
    {
        /// <summary> 
        /// DES加密
        /// </summary>
        /// <param name= "strSource" >待加密字串</param>
        /// <param name= "key" >32位Key值</param>
        /// <returns>加密后的字符串</returns> 
        public string DESEncrypt(string strSource, byte[] key) 
        { 
            SymmetricAlgorithm sa = Rijndael.Create(); 
            sa.Key = key; sa.Mode = CipherMode.ECB; 
            sa.Padding = PaddingMode.Zeros; 
            MemoryStream ms = new MemoryStream(); 
            CryptoStream cs = new CryptoStream(ms, sa.CreateEncryptor(), CryptoStreamMode.Write);
            byte[] byt = Encoding. Unicode .GetBytes(strSource);
            cs.Write(byt, 0, byt.Length); 
            cs.FlushFinalBlock(); 
            cs.Close(); 
            return Convert.ToBase64String(ms.ToArray()); 
        } 
        
        /// <summary> 
        /// DES解密 
        /// </summary>
        /// <param name= "strSource" >待解密的字串</param>
        /// <param name= "key" >32位Key值</param>
        /// <returns>解密后的字符串</returns> 
        public string DESDecrypt(string strSource, byte[] key) 
        { 
            SymmetricAlgorithm sa = Rijndael.Create(); 
            sa.Key = key; sa.Mode = CipherMode.ECB; 
            sa.Padding = PaddingMode.Zeros; 
            ICryptoTransform ct = sa.CreateDecryptor(); 
            byte[] byt = Convert.FromBase64String(strSource); 
            MemoryStream ms = new MemoryStream(byt); 
            CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs, Encoding. Unicode );
            return sr.ReadToEnd(); 
        } 
    }
}
