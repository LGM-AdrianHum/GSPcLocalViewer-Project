// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.AES
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

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

    [DllImport("AESEncoder.dll")]
    internal static extern IntPtr Encode128(string value, string key);

    [DllImport("AESEncoder.dll")]
    internal static extern IntPtr Decode128(string value, string key);

    public string DLLEncode(string value, string key)
    {
      try
      {
        string empty = string.Empty;
        IntPtr ptr = AES.Encode128(value, key);
        string stringBstr = Marshal.PtrToStringBSTR(ptr);
        Marshal.FreeBSTR(ptr);
        return stringBstr;
      }
      catch (Exception ex)
      {
        return string.Empty;
      }
    }

    public string DLLDecode(string value, string key)
    {
      try
      {
        string empty = string.Empty;
        IntPtr ptr = AES.Decode128(value, key);
        string stringBstr = Marshal.PtrToStringBSTR(ptr);
        Marshal.FreeBSTR(ptr);
        return stringBstr;
      }
      catch (Exception ex)
      {
        return string.Empty;
      }
    }

    private string Encrypt(string PlainText, string Password, string Salt, string HashAlgorithm, int PasswordIterations, string InitialVector, int KeySize)
    {
      try
      {
        if (string.IsNullOrEmpty(PlainText))
          return "";
        byte[] bytes1 = Encoding.ASCII.GetBytes(InitialVector);
        byte[] bytes2 = Encoding.ASCII.GetBytes(Salt);
        byte[] bytes3 = Encoding.UTF8.GetBytes(PlainText);
        byte[] bytes4 = new PasswordDeriveBytes(Password, bytes2, HashAlgorithm, PasswordIterations).GetBytes(KeySize / 8);
        RijndaelManaged rijndaelManaged = new RijndaelManaged();
        rijndaelManaged.Mode = CipherMode.CBC;
        byte[] inArray = (byte[]) null;
        using (ICryptoTransform encryptor = rijndaelManaged.CreateEncryptor(bytes4, bytes1))
        {
          using (MemoryStream memoryStream = new MemoryStream())
          {
            using (CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, encryptor, CryptoStreamMode.Write))
            {
              cryptoStream.Write(bytes3, 0, bytes3.Length);
              cryptoStream.FlushFinalBlock();
              inArray = memoryStream.ToArray();
              memoryStream.Close();
              cryptoStream.Close();
            }
          }
        }
        rijndaelManaged.Clear();
        return Convert.ToBase64String(inArray);
      }
      catch
      {
        throw;
      }
    }

    private string Decrypt(string CipherText, string Password, string Salt, string HashAlgorithm, int PasswordIterations, string InitialVector, int KeySize)
    {
      try
      {
        if (string.IsNullOrEmpty(CipherText))
          return "";
        byte[] bytes1 = Encoding.ASCII.GetBytes(InitialVector);
        byte[] bytes2 = Encoding.ASCII.GetBytes(Salt);
        byte[] buffer = Convert.FromBase64String(CipherText);
        byte[] bytes3 = new PasswordDeriveBytes(Password, bytes2, HashAlgorithm, PasswordIterations).GetBytes(KeySize / 8);
        RijndaelManaged rijndaelManaged = new RijndaelManaged();
        rijndaelManaged.Mode = CipherMode.CBC;
        byte[] numArray = new byte[buffer.Length];
        int count = 0;
        using (ICryptoTransform decryptor = rijndaelManaged.CreateDecryptor(bytes3, bytes1))
        {
          using (MemoryStream memoryStream = new MemoryStream(buffer))
          {
            using (CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, decryptor, CryptoStreamMode.Read))
            {
              count = cryptoStream.Read(numArray, 0, numArray.Length);
              memoryStream.Close();
              cryptoStream.Close();
            }
          }
        }
        rijndaelManaged.Clear();
        return Encoding.UTF8.GetString(numArray, 0, count);
      }
      catch
      {
        throw;
      }
    }

    private string AsciiTextToHex(string asciiText)
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < asciiText.Length; ++index)
        stringBuilder.Append(Convert.ToInt32(asciiText[index]).ToString("x"));
      return stringBuilder.ToString().ToUpper();
    }

    private string HexTextToAscii(string hexText)
    {
      StringBuilder stringBuilder = new StringBuilder();
      int startIndex = 0;
      while (startIndex <= hexText.Length - 2)
      {
        stringBuilder.Append(Convert.ToString(Convert.ToChar(int.Parse(hexText.Substring(startIndex, 2), NumberStyles.HexNumber))));
        startIndex += 2;
      }
      return stringBuilder.ToString();
    }

    public string Encode(string value, string key)
    {
      try
      {
        return this.AsciiTextToHex(this.Encrypt(value, key, key, "SHA1", 2, "16CHARSLONG12345", 256));
      }
      catch
      {
        return string.Empty;
      }
    }

    public string Decode(string value, string key)
    {
      try
      {
        return this.Decrypt(this.HexTextToAscii(value), key, key, "SHA1", 2, "16CHARSLONG12345", 256);
      }
      catch
      {
        return string.Empty;
      }
    }
  }
}
