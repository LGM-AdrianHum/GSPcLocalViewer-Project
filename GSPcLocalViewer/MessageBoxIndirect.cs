// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.MessageBoxIndirect
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
  public class MessageBoxIndirect
  {
    private IntPtr _userIcon = IntPtr.Zero;
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
    private MessageBoxIcon _icon;
    private MessageBoxDefaultButton _defaultButton;
    private MessageBoxOptions _options;
    private MessageBoxIndirect.MessageBoxExModality _modality;
    private int hHook;

    [DllImport("user32", EntryPoint = "MessageBoxIndirect")]
    private static extern int _MessageBoxIndirect(ref MessageBoxIndirect.MSGBOXPARAMS msgboxParams);

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    public static extern int SetWindowsHookEx(int idHook, MessageBoxIndirect.HookProc lpfn, IntPtr hInstance, int threadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    public static extern bool UnhookWindowsHookEx(int idHook);

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    public static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll")]
    public static extern int GetClassName(IntPtr hwnd, StringBuilder lpClassName, int nMaxCount);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

    [DllImport("User32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr LoadIcon(IntPtr hInstance, IntPtr lpIconName);

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

    private void EnsureInstance()
    {
      if (!(this.Instance == IntPtr.Zero))
        return;
      this.Instance = Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]);
    }

    private uint BuildStyle()
    {
      uint num = 0;
      if (this.Buttons == MessageBoxButtons.OK)
        num = num;
      else if (this.Buttons == MessageBoxButtons.OKCancel)
        num |= 1U;
      else if (this.Buttons == MessageBoxButtons.AbortRetryIgnore)
        num |= 2U;
      else if (this.Buttons == MessageBoxButtons.RetryCancel)
        num |= 5U;
      else if (this.Buttons == MessageBoxButtons.YesNo)
        num |= 4U;
      else if (this.Buttons == MessageBoxButtons.YesNoCancel)
        num |= 3U;
      if (this.ShowHelp)
        num |= 16384U;
      if (this.UserIcon != IntPtr.Zero)
      {
        num |= 128U;
        this.EnsureInstance();
      }
      if (this.Icon == MessageBoxIcon.Asterisk)
        num |= 64U;
      else if (this.Icon == MessageBoxIcon.Hand)
        num |= 16U;
      else if (this.Icon == MessageBoxIcon.Exclamation)
        num |= 48U;
      else if (this.Icon == MessageBoxIcon.Hand)
        num |= 16U;
      else if (this.Icon == MessageBoxIcon.Asterisk)
        num |= 64U;
      else if (this.Icon == MessageBoxIcon.Question)
        num |= 32U;
      else if (this.Icon == MessageBoxIcon.Hand)
        num |= 16U;
      else if (this.Icon == MessageBoxIcon.Exclamation)
        num |= 48U;
      if (this.DefaultButton == MessageBoxDefaultButton.Button1)
        num = num;
      else if (this.DefaultButton == MessageBoxDefaultButton.Button2)
        num |= 256U;
      else if (this.DefaultButton == MessageBoxDefaultButton.Button3)
        num |= 512U;
      return (uint) ((MessageBoxOptions) num | this.Options) | (uint) this.Modality;
    }

    private int CbtHookProc(int nCode, IntPtr wParam, IntPtr lParam)
    {
      if (nCode == 3)
      {
        StringBuilder lpClassName = new StringBuilder();
        lpClassName.Capacity = 100;
        MessageBoxIndirect.GetClassName(wParam, lpClassName, lpClassName.Capacity);
        if (lpClassName.ToString() == "#32770" && this._sysSmallIcon != IntPtr.Zero)
        {
          this.EnsureInstance();
          IntPtr lParam1 = MessageBoxIndirect.LoadIcon(this.Instance, new IntPtr((long) (short) this._sysSmallIcon.ToInt32()));
          if (lParam1 != IntPtr.Zero)
            MessageBoxIndirect.SendMessage(wParam, 128U, new IntPtr(0L), lParam1);
        }
      }
      return MessageBoxIndirect.CallNextHookEx(this.hHook, nCode, wParam, lParam);
    }

    public DialogResult Show()
    {
      MessageBoxIndirect.MSGBOXPARAMS msgboxParams = new MessageBoxIndirect.MSGBOXPARAMS();
      msgboxParams.dwStyle = this.BuildStyle();
      msgboxParams.lpszText = this.Text;
      msgboxParams.lpszCaption = this.Caption;
      if (this.Owner != null)
        msgboxParams.hwndOwner = this.Owner.Handle;
      msgboxParams.hInstance = this.Instance;
      msgboxParams.cbSize = (uint) Marshal.SizeOf(typeof (MessageBoxIndirect.MSGBOXPARAMS));
      msgboxParams.lpfnMsgBoxCallback = this.Callback;
      msgboxParams.lpszIcon = this.UserIcon;
      msgboxParams.dwLanguageId = this.LanguageID;
      msgboxParams.dwContextHelpId = new IntPtr(this._contextID);
      try
      {
        if (this._sysSmallIcon != IntPtr.Zero)
          this.hHook = MessageBoxIndirect.SetWindowsHookEx(5, new MessageBoxIndirect.HookProc(this.CbtHookProc), (IntPtr) 0, AppDomain.GetCurrentThreadId());
        return (DialogResult) MessageBoxIndirect._MessageBoxIndirect(ref msgboxParams);
      }
      finally
      {
        if (this.hHook > 0)
        {
          MessageBoxIndirect.UnhookWindowsHookEx(this.hHook);
          this.hHook = 0;
        }
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

    public enum MessageBoxExModality : uint
    {
      AppModal = 0,
      SystemModal = 4096, // 0x00001000
      TaskModal = 8192, // 0x00002000
    }

    public struct POINT
    {
      public long x;
      public long y;
    }

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
        return (MessageBoxIndirect.HELPINFO) Marshal.PtrToStructure(lParam, typeof (MessageBoxIndirect.HELPINFO));
      }
    }

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

    public delegate void MsgBoxCallback(MessageBoxIndirect.HELPINFO lpHelpInfo);

    public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);
  }
}
