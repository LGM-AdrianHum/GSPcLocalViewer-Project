// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.ServerSearch.SearchPartSchemaCompletedEventArgs
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;

namespace GSPcLocalViewer.ServerSearch
{
  [DesignerCategory("code")]
  [GeneratedCode("System.Web.Services", "2.0.50727.1433")]
  [DebuggerStepThrough]
  public class SearchPartSchemaCompletedEventArgs : AsyncCompletedEventArgs
  {
    private object[] results;

    internal SearchPartSchemaCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
      : base(exception, cancelled, userState)
    {
      this.results = results;
    }

    public XmlNode Result
    {
      get
      {
        this.RaiseExceptionIfNecessary();
        return (XmlNode) this.results[0];
      }
    }
  }
}
