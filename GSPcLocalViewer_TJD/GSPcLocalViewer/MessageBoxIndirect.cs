using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
	public class MessageBoxIndirect
	{
		private const uint MB_OK = 0;

		private const uint MB_OKCANCEL = 1;

		private const uint MB_ABORTRETRYIGNORE = 2;

		private const uint MB_YESNOCANCEL = 3;

		private const uint MB_YESNO = 4;

		private const uint MB_RETRYCANCEL = 5;

		private const uint MB_HELP = 16384;

		private const uint MB_USERICON = 128;

		private const uint MB_ICONHAND = 16;

		private const uint MB_ICONQUESTION = 32;

		private const uint MB_ICONEXCLAMATION = 48;

		private const uint MB_ICONASTERISK = 64;

		private const uint MB_ICONWARNING = 48;

		private const uint MB_ICONERROR = 16;

		private const uint MB_ICONINFORMATION = 64;

		private const uint MB_ICONSTOP = 16;

		private const uint MB_DEFBUTTON1 = 0;

		private const uint MB_DEFBUTTON2 = 256;

		private const uint MB_DEFBUTTON3 = 512;

		private const uint MB_RTLREADING = 1048576;

		private const uint MB_DEFAULT_DESKTOP_ONLY = 131072;

		private const uint MB_SERVICE_NOTIFICATION = 2097152;

		private const uint MB_RIGHT = 524288;

		private const uint MB_APPLMODAL = 0;

		private const uint MB_SYSTEMMODAL = 4096;

		private const uint MB_TASKMODAL = 8192;

		private const uint WM_SETICON = 128;

		private const uint ICON_SMALL = 0;

		private const uint ICON_BIG = 1;

		private const int WH_CBT = 5;

		private const int HCBT_CREATEWND = 3;

		private string _text;

		private string _caption;

		private IWin32Window _owner;

		private IntPtr _instance;

		private IntPtr _sysSmallIcon;

		private int _contextID;

		private uint _languageID;

		private MessageBoxIndirect.MsgBoxCallback _callback;

		private MessageBoxButtons _buttons;

		private bool _showHelp;

		private IntPtr _userIcon = IntPtr.Zero;

		private MessageBoxIcon _icon;

		private MessageBoxDefaultButton _defaultButton;

		private MessageBoxOptions _options;

		private MessageBoxIndirect.MessageBoxExModality _modality;

		private int hHook;

		public MessageBoxButtons Buttons
		{
			get
			{
				return this._buttons;
			}
			set
			{
				this._buttons = value;
			}
		}

		public MessageBoxIndirect.MsgBoxCallback Callback
		{
			get
			{
				return this._callback;
			}
			set
			{
				this._callback = value;
			}
		}

		public string Caption
		{
			get
			{
				return this._caption;
			}
			set
			{
				this._caption = value;
			}
		}

		public int ContextHelpID
		{
			get
			{
				return this._contextID;
			}
			set
			{
				this._contextID = value;
			}
		}

		public MessageBoxDefaultButton DefaultButton
		{
			get
			{
				return this._defaultButton;
			}
			set
			{
				this._defaultButton = value;
			}
		}

		public MessageBoxIcon Icon
		{
			get
			{
				return this._icon;
			}
			set
			{
				this._icon = value;
			}
		}

		public IntPtr Instance
		{
			get
			{
				return this._instance;
			}
			set
			{
				this._instance = value;
			}
		}

		public uint LanguageID
		{
			get
			{
				return this._languageID;
			}
			set
			{
				this._languageID = value;
			}
		}

		public MessageBoxIndirect.MessageBoxExModality Modality
		{
			get
			{
				return this._modality;
			}
			set
			{
				this._modality = value;
			}
		}

		public MessageBoxOptions Options
		{
			get
			{
				return this._options;
			}
			set
			{
				this._options = value;
			}
		}

		public IWin32Window Owner
		{
			get
			{
				return this._owner;
			}
			set
			{
				this._owner = value;
			}
		}

		public bool ShowHelp
		{
			get
			{
				return this._showHelp;
			}
			set
			{
				this._showHelp = value;
			}
		}

		public IntPtr SysSmallIcon
		{
			get
			{
				return this._sysSmallIcon;
			}
			set
			{
				this._sysSmallIcon = value;
			}
		}

		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				this._text = value;
			}
		}

		public IntPtr UserIcon
		{
			get
			{
				return this._userIcon;
			}
			set
			{
				this._userIcon = value;
			}
		}

		public MessageBoxIndirect(string text)
		{
			this.Text = text;
		}

		public MessageBoxIndirect(IWin32Window owner, string text)
		{
			this.Owner = owner;
			this.Text = text;
		}

		public MessageBoxIndirect(string text, string caption)
		{
			this.Text = text;
			this.Caption = caption;
		}

		public MessageBoxIndirect(IWin32Window owner, string text, string caption)
		{
			this.Owner = owner;
			this.Text = text;
			this.Caption = caption;
		}

		public MessageBoxIndirect(string text, string caption, MessageBoxButtons buttons)
		{
			this.Text = text;
			this.Caption = caption;
			this.Buttons = buttons;
		}

		public MessageBoxIndirect(IWin32Window owner, string text, string caption, MessageBoxButtons buttons)
		{
			this.Owner = owner;
			this.Text = text;
			this.Caption = caption;
			this.Buttons = buttons;
		}

		public MessageBoxIndirect(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			this.Text = text;
			this.Caption = caption;
			this.Buttons = buttons;
			this.Icon = icon;
		}

		public MessageBoxIndirect(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			this.Owner = owner;
			this.Text = text;
			this.Caption = caption;
			this.Buttons = buttons;
			this.Icon = icon;
		}

		public MessageBoxIndirect(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
		{
			this.Text = text;
			this.Caption = caption;
			this.Buttons = buttons;
			this.Icon = icon;
			this.DefaultButton = defaultButton;
		}

		public MessageBoxIndirect(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
		{
			this.Owner = owner;
			this.Text = text;
			this.Caption = caption;
			this.Buttons = buttons;
			this.Icon = icon;
			this.DefaultButton = defaultButton;
		}

		public MessageBoxIndirect(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
		{
			this.Text = text;
			this.Caption = caption;
			this.Buttons = buttons;
			this.Icon = icon;
			this.DefaultButton = defaultButton;
			this.Options = options;
		}

		public MessageBoxIndirect(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
		{
			this.Owner = owner;
			this.Text = text;
			this.Caption = caption;
			this.Buttons = buttons;
			this.Icon = icon;
			this.DefaultButton = defaultButton;
			this.Options = options;
		}

		[DllImport("user32", CharSet=CharSet.None, EntryPoint="MessageBoxIndirect", ExactSpelling=false)]
		private static extern int _MessageBoxIndirect(ref MessageBoxIndirect.MSGBOXPARAMS msgboxParams);

		private uint BuildStyle()
		{
			uint options = 0;
			if (this.Buttons == MessageBoxButtons.OK)
			{
				options = options;
			}
			else if (this.Buttons == MessageBoxButtons.OKCancel)
			{
				options |= 1;
			}
			else if (this.Buttons == MessageBoxButtons.AbortRetryIgnore)
			{
				options |= 2;
			}
			else if (this.Buttons == MessageBoxButtons.RetryCancel)
			{
				options |= 5;
			}
			else if (this.Buttons == MessageBoxButtons.YesNo)
			{
				options |= 4;
			}
			else if (this.Buttons == MessageBoxButtons.YesNoCancel)
			{
				options |= 3;
			}
			if (this.ShowHelp)
			{
				options |= 16384;
			}
			if (this.UserIcon != IntPtr.Zero)
			{
				options |= 128;
				this.EnsureInstance();
			}
			if (this.Icon == MessageBoxIcon.Asterisk)
			{
				options |= 64;
			}
			else if (this.Icon == MessageBoxIcon.Hand)
			{
				options |= 16;
			}
			else if (this.Icon == MessageBoxIcon.Exclamation)
			{
				options |= 48;
			}
			else if (this.Icon == MessageBoxIcon.Hand)
			{
				options |= 16;
			}
			else if (this.Icon == MessageBoxIcon.Asterisk)
			{
				options |= 64;
			}
			else if (this.Icon == MessageBoxIcon.Question)
			{
				options |= 32;
			}
			else if (this.Icon == MessageBoxIcon.Hand)
			{
				options |= 16;
			}
			else if (this.Icon == MessageBoxIcon.Exclamation)
			{
				options |= 48;
			}
			if (this.DefaultButton == MessageBoxDefaultButton.Button1)
			{
				options = options;
			}
			else if (this.DefaultButton == MessageBoxDefaultButton.Button2)
			{
				options |= 256;
			}
			else if (this.DefaultButton == MessageBoxDefaultButton.Button3)
			{
				options |= 512;
			}
			options |= (uint)this.Options;
			options |= (uint)this.Modality;
			return options;
		}

		[DllImport("user32.dll", CallingConvention=CallingConvention.StdCall, CharSet=CharSet.Auto, ExactSpelling=false)]
		public static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr lParam);

		private int CbtHookProc(int nCode, IntPtr wParam, IntPtr lParam)
		{
			if (nCode == 3)
			{
				StringBuilder stringBuilder = new StringBuilder()
				{
					Capacity = 100
				};
				MessageBoxIndirect.GetClassName(wParam, stringBuilder, stringBuilder.Capacity);
				if (stringBuilder.ToString() == "#32770" && this._sysSmallIcon != IntPtr.Zero)
				{
					this.EnsureInstance();
					IntPtr intPtr = MessageBoxIndirect.LoadIcon(this.Instance, new IntPtr((long)((short)this._sysSmallIcon.ToInt32())));
					if (intPtr != IntPtr.Zero)
					{
						MessageBoxIndirect.SendMessage(wParam, 128, new IntPtr((long)0), intPtr);
					}
				}
			}
			return MessageBoxIndirect.CallNextHookEx(this.hHook, nCode, wParam, lParam);
		}

		private void EnsureInstance()
		{
			if (this.Instance == IntPtr.Zero)
			{
				this.Instance = Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]);
			}
		}

		[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false)]
		public static extern int GetClassName(IntPtr hwnd, StringBuilder lpClassName, int nMaxCount);

		[DllImport("User32.dll", CharSet=CharSet.Auto, ExactSpelling=false)]
		public static extern IntPtr LoadIcon(IntPtr hInstance, IntPtr lpIconName);

		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=false)]
		public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll", CallingConvention=CallingConvention.StdCall, CharSet=CharSet.Auto, ExactSpelling=false)]
		public static extern int SetWindowsHookEx(int idHook, MessageBoxIndirect.HookProc lpfn, IntPtr hInstance, int threadId);

		public DialogResult Show()
		{
			MessageBoxIndirect.MSGBOXPARAMS handle = new MessageBoxIndirect.MSGBOXPARAMS()
			{
				dwStyle = this.BuildStyle(),
				lpszText = this.Text,
				lpszCaption = this.Caption
			};
			if (this.Owner != null)
			{
				handle.hwndOwner = this.Owner.Handle;
			}
			handle.hInstance = this.Instance;
			handle.cbSize = (uint)Marshal.SizeOf(typeof(MessageBoxIndirect.MSGBOXPARAMS));
			handle.lpfnMsgBoxCallback = this.Callback;
			handle.lpszIcon = this.UserIcon;
			handle.dwLanguageId = this.LanguageID;
			handle.dwContextHelpId = new IntPtr(this._contextID);
			DialogResult dialogResult = DialogResult.Cancel;
			try
			{
				if (this._sysSmallIcon != IntPtr.Zero)
				{
					MessageBoxIndirect.HookProc hookProc = new MessageBoxIndirect.HookProc(this.CbtHookProc);
					this.hHook = MessageBoxIndirect.SetWindowsHookEx(5, hookProc, (IntPtr)0, AppDomain.GetCurrentThreadId());
				}
				dialogResult = (DialogResult)MessageBoxIndirect._MessageBoxIndirect(ref handle);
			}
			finally
			{
				if (this.hHook > 0)
				{
					MessageBoxIndirect.UnhookWindowsHookEx(this.hHook);
					this.hHook = 0;
				}
			}
			return dialogResult;
		}

		[DllImport("user32.dll", CallingConvention=CallingConvention.StdCall, CharSet=CharSet.Auto, ExactSpelling=false)]
		public static extern bool UnhookWindowsHookEx(int idHook);

		public struct HELPINFO
		{
			public uint cbSize;

			public int iContextType;

			public int iCtrlId;

			public IntPtr hItemHandle;

			public IntPtr dwContextId;

			public MessageBoxIndirect.POINT MousePos;

			public static MessageBoxIndirect.HELPINFO UnmarshalFrom(IntPtr lParam)
			{
				return (MessageBoxIndirect.HELPINFO)Marshal.PtrToStructure(lParam, typeof(MessageBoxIndirect.HELPINFO));
			}
		}

		public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);

		public enum MessageBoxExModality : uint
		{
			AppModal = 0,
			SystemModal = 4096,
			TaskModal = 8192
		}

		public delegate void MsgBoxCallback(MessageBoxIndirect.HELPINFO lpHelpInfo);

		public struct MSGBOXPARAMS
		{
			public uint cbSize;

			public IntPtr hwndOwner;

			public IntPtr hInstance;

			public string lpszText;

			public string lpszCaption;

			public uint dwStyle;

			public IntPtr lpszIcon;

			public IntPtr dwContextHelpId;

			public MessageBoxIndirect.MsgBoxCallback lpfnMsgBoxCallback;

			public uint dwLanguageId;
		}

		public struct POINT
		{
			public long x;

			public long y;
		}
	}
}