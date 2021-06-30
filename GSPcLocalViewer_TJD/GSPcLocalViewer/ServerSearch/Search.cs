using GSPcLocalViewer.Properties;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;

namespace GSPcLocalViewer.ServerSearch
{
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("System.Web.Services", "2.0.50727.1433")]
	[WebServiceBinding(Name="SearchSoap", Namespace="http://tempuri.org/")]
	public class Search : SoapHttpClientProtocol
	{
		private SendOrPostCallback SearchBookSchemaOperationCompleted;

		private SendOrPostCallback SearchPageSchemaOperationCompleted;

		private SendOrPostCallback SearchPartSchemaOperationCompleted;

		private SendOrPostCallback SearchPagesOperationCompleted;

		private SendOrPostCallback SearchPartsOperationCompleted;

		private bool useDefaultCredentialsSetExplicitly;

		public new string Url
		{
			get
			{
				return base.Url;
			}
			set
			{
				if (this.IsLocalFileSystemWebService(base.Url) && !this.useDefaultCredentialsSetExplicitly && !this.IsLocalFileSystemWebService(value))
				{
					base.UseDefaultCredentials = false;
				}
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

		public Search()
		{
			this.Url = Settings.Default.GSPcLocalViewer_localhost_Search;
			if (!this.IsLocalFileSystemWebService(this.Url))
			{
				this.useDefaultCredentialsSetExplicitly = true;
				return;
			}
			this.UseDefaultCredentials = true;
			this.useDefaultCredentialsSetExplicitly = false;
		}

		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}

		private bool IsLocalFileSystemWebService(string url)
		{
			if (url == null || url == string.Empty)
			{
				return false;
			}
			System.Uri uri = new System.Uri(url);
			if (uri.Port >= 1024 && string.Compare(uri.Host, "localHost", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return true;
			}
			return false;
		}

		private void OnSearchBookSchemaOperationCompleted(object arg)
		{
			if (this.SearchBookSchemaCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this.SearchBookSchemaCompleted(this, new SearchBookSchemaCompletedEventArgs(invokeCompletedEventArg.Results, invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
			}
		}

		private void OnSearchPageSchemaOperationCompleted(object arg)
		{
			if (this.SearchPageSchemaCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this.SearchPageSchemaCompleted(this, new SearchPageSchemaCompletedEventArgs(invokeCompletedEventArg.Results, invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
			}
		}

		private void OnSearchPagesOperationCompleted(object arg)
		{
			if (this.SearchPagesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this.SearchPagesCompleted(this, new SearchPagesCompletedEventArgs(invokeCompletedEventArg.Results, invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
			}
		}

		private void OnSearchPartSchemaOperationCompleted(object arg)
		{
			if (this.SearchPartSchemaCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this.SearchPartSchemaCompleted(this, new SearchPartSchemaCompletedEventArgs(invokeCompletedEventArg.Results, invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
			}
		}

		private void OnSearchPartsOperationCompleted(object arg)
		{
			if (this.SearchPartsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this.SearchPartsCompleted(this, new SearchPartsCompletedEventArgs(invokeCompletedEventArg.Results, invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/SearchBookSchema", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public XmlNode SearchBookSchema(string sServerKey)
		{
			object[] objArray = new object[] { sServerKey };
			return (XmlNode)base.Invoke("SearchBookSchema", objArray)[0];
		}

		public void SearchBookSchemaAsync(string sServerKey)
		{
			this.SearchBookSchemaAsync(sServerKey, null);
		}

		public void SearchBookSchemaAsync(string sServerKey, object userState)
		{
			if (this.SearchBookSchemaOperationCompleted == null)
			{
				this.SearchBookSchemaOperationCompleted = new SendOrPostCallback(this.OnSearchBookSchemaOperationCompleted);
			}
			object[] objArray = new object[] { sServerKey };
			base.InvokeAsync("SearchBookSchema", objArray, this.SearchBookSchemaOperationCompleted, userState);
		}

		[SoapDocumentMethod("http://tempuri.org/SearchPages", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public XmlNode SearchPages(string Keyword, string FieldToSearch, bool MatchCase, bool ExactMatch, string[] BookCodes, string sServerKey, string[] p_ArgsF)
		{
			object[] keyword = new object[] { Keyword, FieldToSearch, MatchCase, ExactMatch, BookCodes, sServerKey, p_ArgsF };
			return (XmlNode)base.Invoke("SearchPages", keyword)[0];
		}

		public void SearchPagesAsync(string Keyword, string FieldToSearch, bool MatchCase, bool ExactMatch, string[] BookCodes, string sServerKey, string[] p_ArgsF)
		{
			this.SearchPagesAsync(Keyword, FieldToSearch, MatchCase, ExactMatch, BookCodes, sServerKey, p_ArgsF, null);
		}

		public void SearchPagesAsync(string Keyword, string FieldToSearch, bool MatchCase, bool ExactMatch, string[] BookCodes, string sServerKey, string[] p_ArgsF, object userState)
		{
			if (this.SearchPagesOperationCompleted == null)
			{
				this.SearchPagesOperationCompleted = new SendOrPostCallback(this.OnSearchPagesOperationCompleted);
			}
			object[] keyword = new object[] { Keyword, FieldToSearch, MatchCase, ExactMatch, BookCodes, sServerKey, p_ArgsF };
			base.InvokeAsync("SearchPages", keyword, this.SearchPagesOperationCompleted, userState);
		}

		[SoapDocumentMethod("http://tempuri.org/SearchPageSchema", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public XmlNode SearchPageSchema(string BookCode, string sServerKey)
		{
			object[] bookCode = new object[] { BookCode, sServerKey };
			return (XmlNode)base.Invoke("SearchPageSchema", bookCode)[0];
		}

		public void SearchPageSchemaAsync(string BookCode, string sServerKey)
		{
			this.SearchPageSchemaAsync(BookCode, sServerKey, null);
		}

		public void SearchPageSchemaAsync(string BookCode, string sServerKey, object userState)
		{
			if (this.SearchPageSchemaOperationCompleted == null)
			{
				this.SearchPageSchemaOperationCompleted = new SendOrPostCallback(this.OnSearchPageSchemaOperationCompleted);
			}
			object[] bookCode = new object[] { BookCode, sServerKey };
			base.InvokeAsync("SearchPageSchema", bookCode, this.SearchPageSchemaOperationCompleted, userState);
		}

		[SoapDocumentMethod("http://tempuri.org/SearchParts", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public XmlNode SearchParts(string Keyword, string FieldToSearch, bool MatchCase, bool ExactMatch, string sServerKey, string[] BookCodes)
		{
			object[] keyword = new object[] { Keyword, FieldToSearch, MatchCase, ExactMatch, sServerKey, BookCodes };
			return (XmlNode)base.Invoke("SearchParts", keyword)[0];
		}

		public void SearchPartsAsync(string Keyword, string FieldToSearch, bool MatchCase, bool ExactMatch, string sServerKey, string[] BookCodes)
		{
			this.SearchPartsAsync(Keyword, FieldToSearch, MatchCase, ExactMatch, sServerKey, BookCodes, null);
		}

		public void SearchPartsAsync(string Keyword, string FieldToSearch, bool MatchCase, bool ExactMatch, string sServerKey, string[] BookCodes, object userState)
		{
			if (this.SearchPartsOperationCompleted == null)
			{
				this.SearchPartsOperationCompleted = new SendOrPostCallback(this.OnSearchPartsOperationCompleted);
			}
			object[] keyword = new object[] { Keyword, FieldToSearch, MatchCase, ExactMatch, sServerKey, BookCodes };
			base.InvokeAsync("SearchParts", keyword, this.SearchPartsOperationCompleted, userState);
		}

		[SoapDocumentMethod("http://tempuri.org/SearchPartSchema", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public XmlNode SearchPartSchema(string BookCode, string sServerKey)
		{
			object[] bookCode = new object[] { BookCode, sServerKey };
			return (XmlNode)base.Invoke("SearchPartSchema", bookCode)[0];
		}

		public void SearchPartSchemaAsync(string BookCode, string sServerKey)
		{
			this.SearchPartSchemaAsync(BookCode, sServerKey, null);
		}

		public void SearchPartSchemaAsync(string BookCode, string sServerKey, object userState)
		{
			if (this.SearchPartSchemaOperationCompleted == null)
			{
				this.SearchPartSchemaOperationCompleted = new SendOrPostCallback(this.OnSearchPartSchemaOperationCompleted);
			}
			object[] bookCode = new object[] { BookCode, sServerKey };
			base.InvokeAsync("SearchPartSchema", bookCode, this.SearchPartSchemaOperationCompleted, userState);
		}

		public event SearchBookSchemaCompletedEventHandler SearchBookSchemaCompleted;

		public event SearchPageSchemaCompletedEventHandler SearchPageSchemaCompleted;

		public event SearchPagesCompletedEventHandler SearchPagesCompleted;

		public event SearchPartSchemaCompletedEventHandler SearchPartSchemaCompleted;

		public event SearchPartsCompletedEventHandler SearchPartsCompleted;
	}
}