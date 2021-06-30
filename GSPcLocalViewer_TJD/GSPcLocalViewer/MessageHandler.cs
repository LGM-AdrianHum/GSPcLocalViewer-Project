using System;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
	internal static class MessageHandler
	{
		public static void ShowError(string msg)
		{
			MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			Environment.Exit(0);
		}

		public static void ShowError(string msg, string title)
		{
			MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Hand);
			Environment.Exit(0);
		}

		public static void ShowError1(string msg)
		{
			MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}

		public static void ShowInformation(string msg)
		{
			MessageBox.Show(msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		public static void ShowInformation(string msg, string title)
		{
			MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		public static DialogResult ShowMessage(IWin32Window owner, string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			DialogResult dialogResult;
			try
			{
				MessageBoxIndirect messageBoxIndirect = new MessageBoxIndirect(owner, message, caption)
				{
					Modality = MessageBoxIndirect.MessageBoxExModality.AppModal,
					Icon = icon,
					Buttons = buttons
				};
				dialogResult = messageBoxIndirect.Show();
			}
			catch
			{
				dialogResult = MessageBox.Show(message, caption, buttons, icon);
			}
			return dialogResult;
		}

		public static void ShowQuestion(string msg)
		{
			if (MessageBox.Show(msg, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
			{
				Environment.Exit(0);
			}
		}

		public static void ShowWarning(string msg)
		{
			MessageBox.Show(msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}
}