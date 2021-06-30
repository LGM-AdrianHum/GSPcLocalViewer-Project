// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.MessageHandler
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using System;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
  internal static class MessageHandler
  {
    public static DialogResult ShowMessage(IWin32Window owner, string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
    {
      try
      {
        return new MessageBoxIndirect(owner, message, caption)
        {
          Modality = MessageBoxIndirect.MessageBoxExModality.AppModal,
          Icon = icon,
          Buttons = buttons
        }.Show();
      }
      catch
      {
        return MessageBox.Show(message, caption, buttons, icon);
      }
    }

    public static void ShowInformation(string msg)
    {
      int num = (int) MessageBox.Show(msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    public static void ShowInformation(string msg, string title)
    {
      int num = (int) MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    public static void ShowWarning(string msg)
    {
      int num = (int) MessageBox.Show(msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    public static void ShowError(string msg)
    {
      int num = (int) MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      Environment.Exit(0);
    }

    public static void ShowError1(string msg)
    {
      int num = (int) MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }

    public static void ShowError(string msg, string title)
    {
      int num = (int) MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Hand);
      Environment.Exit(0);
    }

    public static void ShowQuestion(string msg)
    {
      if (MessageBox.Show(msg, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.No)
        return;
      Environment.Exit(0);
    }
  }
}
