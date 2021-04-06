using System;
using System.Text;
using System.Security.Cryptography;

class AES 
{
	//默认密钥向量 
    private static byte[] sIVBytes = {0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x12, 0x34, 0x56,  0x78, 0x90, 0xAB, 0xCD, 0xEF};

	// AES加密，如果待加密字符串不是16的整数倍尾部填充空格
	public static string AESEncrypt(string plainText, string strKey)
	{
		byte[] keyBytes = Encoding.UTF8.GetBytes(strKey);
		return AESEncrypt(plainText, keyBytes);
	}

    public static string AESEncrypt(string plainText, byte[] keyBytes)
	{
		byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

		// 必须16的倍数，尾部填充空格
		int len = plainBytes.Length;
		if (len % 16 != 0) {
			int needlen = len + 16 - len%16;
			if (len < needlen) {
				for (int i=len; i<needlen; i++) {
					plainText += " ";
				}
			}
			plainBytes = Encoding.UTF8.GetBytes(plainText);
		}

		Rijndael rijndael = Rijndael.Create();
		rijndael.Mode = CipherMode.ECB;
		rijndael.Padding = PaddingMode.None;
		ICryptoTransform transform = rijndael.CreateEncryptor(keyBytes, sIVBytes);
		byte[] cipherTextBuffer = transform.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
		transform.Dispose();
		return Convert.ToBase64String(cipherTextBuffer);
    }

	// AES解密，会截掉所有尾部空格
	public static string AESDecrypt(string cipherText , string strKey)
	{
		byte[] keyBytes = Encoding.UTF8.GetBytes(strKey.ToCharArray());
		return AESDecrypt(cipherText, keyBytes);
	}

    public static string AESDecrypt(string cipherText, byte[] keyBytes)
    {
		Rijndael rijndael = Rijndael.Create();
		rijndael.Mode = CipherMode.ECB;
		rijndael.Padding = PaddingMode.None;
		ICryptoTransform transform = rijndael.CreateDecryptor(keyBytes, sIVBytes);
		byte[] cipherBytes = Convert.FromBase64String(cipherText);
		byte[] plainBytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
		transform.Dispose();
		string plainText = Encoding.UTF8.GetString(plainBytes, 0, plainBytes.Length);
		return plainText.TrimEnd(' ');
	}
}