using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace GSPcLocalViewer
{
	public class AES
	{
		private const string dllEncoder = "AESEncoder.dll";

		public AES()
		{
		}

		private string AsciiTextToHex(string asciiText)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < asciiText.Length; i++)
			{
				int num = Convert.ToInt32(asciiText[i]);
				stringBuilder.Append(num.ToString("x"));
			}
			return stringBuilder.ToString().ToUpper();
		}

		public string Decode(string value, string key)
		{
			string empty;
			try
			{
				string ascii = this.HexTextToAscii(value);
				empty = this.Decrypt(ascii, key, key, "SHA1", 2, "16CHARSLONG12345", 256);
			}
			catch
			{
				empty = string.Empty;
			}
			return empty;
		}

		[DllImport("AESEncoder.dll", CharSet=CharSet.None, ExactSpelling=false)]
		internal static extern IntPtr Decode128(string value, string key);

		private string Decrypt(string CipherText, string Password, string Salt, string HashAlgorithm, int PasswordIterations, string InitialVector, int KeySize)
		{
			string str;
			try
			{
				if (!string.IsNullOrEmpty(CipherText))
				{
					byte[] bytes = Encoding.ASCII.GetBytes(InitialVector);
					byte[] numArray = Encoding.ASCII.GetBytes(Salt);
					byte[] numArray1 = Convert.FromBase64String(CipherText);
					PasswordDeriveBytes passwordDeriveByte = new PasswordDeriveBytes(Password, numArray, HashAlgorithm, PasswordIterations);
					byte[] bytes1 = passwordDeriveByte.GetBytes(KeySize / 8);
					RijndaelManaged rijndaelManaged = new RijndaelManaged()
					{
						Mode = CipherMode.CBC
					};
					byte[] numArray2 = new byte[(int)numArray1.Length];
					int num = 0;
					using (ICryptoTransform cryptoTransform = rijndaelManaged.CreateDecryptor(bytes1, bytes))
					{
						using (MemoryStream memoryStream = new MemoryStream(numArray1))
						{
							using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Read))
							{
								num = cryptoStream.Read(numArray2, 0, (int)numArray2.Length);
								memoryStream.Close();
								cryptoStream.Close();
							}
						}
					}
					rijndaelManaged.Clear();
					str = Encoding.UTF8.GetString(numArray2, 0, num);
				}
				else
				{
					str = "";
				}
			}
			catch
			{
				throw;
			}
			return str;
		}

		public string DLLDecode(string value, string key)
		{
			string empty;
			try
			{
				string stringBSTR = string.Empty;
				IntPtr intPtr = AES.Decode128(value, key);
				stringBSTR = Marshal.PtrToStringBSTR(intPtr);
				Marshal.FreeBSTR(intPtr);
				empty = stringBSTR;
			}
			catch (Exception exception)
			{
				empty = string.Empty;
			}
			return empty;
		}

		public string DLLEncode(string value, string key)
		{
			string empty;
			try
			{
				string stringBSTR = string.Empty;
				IntPtr intPtr = AES.Encode128(value, key);
				stringBSTR = Marshal.PtrToStringBSTR(intPtr);
				Marshal.FreeBSTR(intPtr);
				empty = stringBSTR;
			}
			catch (Exception exception)
			{
				empty = string.Empty;
			}
			return empty;
		}

		public string Encode(string value, string key)
		{
			string hex;
			try
			{
				string str = this.Encrypt(value, key, key, "SHA1", 2, "16CHARSLONG12345", 256);
				hex = this.AsciiTextToHex(str);
			}
			catch
			{
				hex = string.Empty;
			}
			return hex;
		}

		[DllImport("AESEncoder.dll", CharSet=CharSet.None, ExactSpelling=false)]
		internal static extern IntPtr Encode128(string value, string key);

		private string Encrypt(string PlainText, string Password, string Salt, string HashAlgorithm, int PasswordIterations, string InitialVector, int KeySize)
		{
			string base64String;
			try
			{
				if (!string.IsNullOrEmpty(PlainText))
				{
					byte[] bytes = Encoding.ASCII.GetBytes(InitialVector);
					byte[] numArray = Encoding.ASCII.GetBytes(Salt);
					byte[] bytes1 = Encoding.UTF8.GetBytes(PlainText);
					PasswordDeriveBytes passwordDeriveByte = new PasswordDeriveBytes(Password, numArray, HashAlgorithm, PasswordIterations);
					byte[] numArray1 = passwordDeriveByte.GetBytes(KeySize / 8);
					RijndaelManaged rijndaelManaged = new RijndaelManaged()
					{
						Mode = CipherMode.CBC
					};
					byte[] array = null;
					using (ICryptoTransform cryptoTransform = rijndaelManaged.CreateEncryptor(numArray1, bytes))
					{
						using (MemoryStream memoryStream = new MemoryStream())
						{
							using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
							{
								cryptoStream.Write(bytes1, 0, (int)bytes1.Length);
								cryptoStream.FlushFinalBlock();
								array = memoryStream.ToArray();
								memoryStream.Close();
								cryptoStream.Close();
							}
						}
					}
					rijndaelManaged.Clear();
					base64String = Convert.ToBase64String(array);
				}
				else
				{
					base64String = "";
				}
			}
			catch
			{
				throw;
			}
			return base64String;
		}

		private string HexTextToAscii(string hexText)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i <= hexText.Length - 2; i += 2)
			{
				stringBuilder.Append(Convert.ToString(Convert.ToChar(int.Parse(hexText.Substring(i, 2), NumberStyles.HexNumber))));
			}
			return stringBuilder.ToString();
		}
	}
}