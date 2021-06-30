// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.ServerSearch.Search
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using GSPcLocalViewer.Properties;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;

namespace GSPcLocalViewer.ServerSearch
{
  [DebuggerStepThrough]
  [WebServiceBinding(Name = "SearchSoap", Namespace = "http://tempuri.org/")]
  [DesignerCategory("code")]
  [GeneratedCode("System.Web.Services", "2.0.50727.1433")]
  public class Search : SoapHttpClientProtocol
  {
    private SendOrPostCallback SearchBookSchemaOperationCompleted;
    private SendOrPostCallback SearchPageSchemaOperationCompleted;
    private SendOrPostCallback SearchPartSchemaOperationCompleted;
    private SendOrPostCallback SearchPagesOperationCompleted;
    private SendOrPostCallback SearchPartsOperationCompleted;
    private bool useDefaultCredentialsSetExplicitly;

    public Search()
    {
      this.Url = Settings.Default.GSPcLocalViewer_localhost_Search;
      if (this.IsLocalFileSystemWebService(this.Url))
      {
        this.UseDefaultCredentials = true;
        this.useDefaultCredentialsSetExplicitly = false;
      }
      else
        this.useDefaultCredentialsSetExplicitly = true;
    }

    public new string Url
    {
      get
      {
        return base.Url;
      }
      set
      {
        if (this.IsLocalFileSystemWebService(base.Url) && !this.useDefaultCredentialsSetExplicitly && !this.IsLocalFileSystemWebService(value))
          base.UseDefaultCredentials = false;
        base.Url = value;
      }
    }

    public new bool UseDefaultCredentials
    {
      get
      {
        return base.UseDefaultCredentials;
      }
      set
      {
        base.UseDefaultCredentials = value;
        this.useDefaultCredentialsSetExplicitly = true;
      }
    }

    public event SearchBookSchemaCompletedEventHandler SearchBookSchemaCompleted;

    public event SearchPageSchemaCompletedEventHandler SearchPageSchemaCompleted;

    public event SearchPartSchemaCompletedEventHandler SearchPartSchemaCompleted;

    public event SearchPagesCompletedEventHandler SearchPagesCompleted;

    public event SearchPartsCompletedEventHandler SearchPartsCompleted;

    [SoapDocumentMethod("http://tempuri.org/SearchBookSchema", ParameterStyle = SoapParameterStyle.Wrapped, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal)]
    public XmlNode SearchBookSchema(string sServerKey)
    {
      return (XmlNode) this.Invoke(nameof (SearchBookSchema), new object[1]
      {
        (object) sServerKey
      })[0];
    }

    public void SearchBookSchemaAsync(string sServerKey)
    {
      this.SearchBookSchemaAsync(sServerKey, (object) null);
    }

    public void SearchBookSchemaAsync(string sServerKey, object userState)
    {
      if (this.SearchBookSchemaOperationCompleted == null)
        this.SearchBookSchemaOperationCompleted = new SendOrPostCallback(this.OnSearchBookSchemaOperationCompleted);
      this.InvokeAsync("SearchBookSchema", new object[1]
      {
        (object) sServerKey
      }, this.SearchBookSchemaOperationCompleted, userState);
    }

    private void OnSearchBookSchemaOperationCompleted(object arg)
    {
      if (this.SearchBookSchemaCompleted == null)
        return;
      InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
      this.SearchBookSchemaCompleted((object) this, new SearchBookSchemaCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
    }

    [SoapDocumentMethod("http://tempuri.org/SearchPageSchema", ParameterStyle = SoapParameterStyle.Wrapped, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal)]
    public XmlNode SearchPageSchema(string BookCode, string sServerKey)
    {
      return (XmlNode) this.Invoke(nameof (SearchPageSchema), new object[2]
      {
        (object) BookCode,
        (object) sServerKey
      })[0];
    }

    public void SearchPageSchemaAsync(string BookCode, string sServerKey)
    {
      this.SearchPageSchemaAsync(BookCode, sServerKey, (object) null);
    }

    public void SearchPageSchemaAsync(string BookCode, string sServerKey, object userState)
    {
      if (this.SearchPageSchemaOperationCompleted == null)
        this.SearchPageSchemaOperationCompleted = new SendOrPostCallback(this.OnSearchPageSchemaOperationCompleted);
      this.InvokeAsync("SearchPageSchema", new object[2]
      {
        (object) BookCode,
        (object) sServerKey
      }, this.SearchPageSchemaOperationCompleted, userState);
    }

    private void OnSearchPageSchemaOperationCompleted(object arg)
    {
      if (this.SearchPageSchemaCompleted == null)
        return;
      InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
      this.SearchPageSchemaCompleted((object) this, new SearchPageSchemaCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
    }

    [SoapDocumentMethod("http://tempuri.org/SearchPartSchema", ParameterStyle = SoapParameterStyle.Wrapped, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal)]
    public XmlNode SearchPartSchema(string BookCode, string sServerKey)
    {
      return (XmlNode) this.Invoke(nameof (SearchPartSchema), new object[2]
      {
        (object) BookCode,
        (object) sServerKey
      })[0];
    }

    public void SearchPartSchemaAsync(string BookCode, string sServerKey)
    {
      this.SearchPartSchemaAsync(BookCode, sServerKey, (object) null);
    }

    public void SearchPartSchemaAsync(string BookCode, string sServerKey, object userState)
    {
      if (this.SearchPartSchemaOperationCompleted == null)
        this.SearchPartSchemaOperationCompleted = new SendOrPostCallback(this.OnSearchPartSchemaOperationCompleted);
      this.InvokeAsync("SearchPartSchema", new object[2]
      {
        (object) BookCode,
        (object) sServerKey
      }, this.SearchPartSchemaOperationCompleted, userState);
    }

    private void OnSearchPartSchemaOperationCompleted(object arg)
    {
      if (this.SearchPartSchemaCompleted == null)
        return;
      InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
      this.SearchPartSchemaCompleted((object) this, new SearchPartSchemaCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
    }

    [SoapDocumentMethod("http://tempuri.org/SearchPages", ParameterStyle = SoapParameterStyle.Wrapped, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal)]
    public XmlNode SearchPages(string Keyword, string FieldToSearch, bool MatchCase, bool ExactMatch, string[] BookCodes, string sServerKey, string[] p_ArgsF)
    {
      return (XmlNode) this.Invoke(nameof (SearchPages), new object[7]
      {
        (object) Keyword,
        (object) FieldToSearch,
        (object) MatchCase,
        (object) ExactMatch,
        (object) BookCodes,
        (object) sServerKey,
        (object) p_ArgsF
      })[0];
    }

    public void SearchPagesAsync(string Keyword, string FieldToSearch, bool MatchCase, bool ExactMatch, string[] BookCodes, string sServerKey, string[] p_ArgsF)
    {
      this.SearchPagesAsync(Keyword, FieldToSearch, MatchCase, ExactMatch, BookCodes, sServerKey, p_ArgsF, (object) null);
    }

    public void SearchPagesAsync(string Keyword, string FieldToSearch, bool MatchCase, bool ExactMatch, string[] BookCodes, string sServerKey, string[] p_ArgsF, object userState)
    {
      if (this.SearchPagesOperationCompleted == null)
        this.SearchPagesOperationCompleted = new SendOrPostCallback(this.OnSearchPagesOperationCompleted);
      this.InvokeAsync("SearchPages", new object[7]
      {
        (object) Keyword,
        (object) FieldToSearch,
        (object) MatchCase,
        (object) ExactMatch,
        (object) BookCodes,
        (object) sServerKey,
        (object) p_ArgsF
      }, this.SearchPagesOperationCompleted, userState);
    }

    private void OnSearchPagesOperationCompleted(object arg)
    {
      if (this.SearchPagesCompleted == null)
        return;
      InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
      this.SearchPagesCompleted((object) this, new SearchPagesCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
    }

    [SoapDocumentMethod("http://tempuri.org/SearchParts", ParameterStyle = SoapParameterStyle.Wrapped, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal)]
    public XmlNode SearchParts(string Keyword, string FieldToSearch, bool MatchCase, bool ExactMatch, string sServerKey, string[] BookCodes)
    {
      return (XmlNode) this.Invoke(nameof (SearchParts), new object[6]
      {
        (object) Keyword,
        (object) FieldToSearch,
        (object) MatchCase,
        (object) ExactMatch,
        (object) sServerKey,
        (object) BookCodes
      })[0];
    }

    public void SearchPartsAsync(string Keyword, string FieldToSearch, bool MatchCase, bool ExactMatch, string sServerKey, string[] BookCodes)
    {
      this.SearchPartsAsync(Keyword, FieldToSearch, MatchCase, ExactMatch, sServerKey, BookCodes, (object) null);
    }

    public void SearchPartsAsync(string Keyword, string FieldToSearch, bool MatchCase, bool ExactMatch, string sServerKey, string[] BookCodes, object userState)
    {
      if (this.SearchPartsOperationCompleted == null)
        this.SearchPartsOperationCompleted = new SendOrPostCallback(this.OnSearchPartsOperationCompleted);
      this.InvokeAsync("SearchParts", new object[6]
      {
        (object) Keyword,
        (object) FieldToSearch,
        (object) MatchCase,
        (object) ExactMatch,
        (object) sServerKey,
        (object) BookCodes
      }, this.SearchPartsOperationCompleted, userState);
    }

    private void OnSearchPartsOperationCompleted(object arg)
    {
      if (this.SearchPartsCompleted == null)
        return;
      InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
      this.SearchPartsCompleted((object) this, new SearchPartsCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
    }

    public new void CancelAsync(object userState)
    {
      base.CancelAsync(userState);
    }

    private bool IsLocalFileSystemWebService(string url)
    {
      if (url == null || url == string.Empty)
        return false;
      Uri uri = new Uri(url);
      return uri.Port >= 1024 && string.Compare(uri.Host, "localHost", StringComparison.OrdinalIgnoreCase) == 0;
    }
  }
}
