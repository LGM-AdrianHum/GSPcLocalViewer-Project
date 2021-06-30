using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
	internal class CustomPrintPreviewControl : UserControl
	{
		private const int MARGIN = 4;

		private PrintDocument _doc;

		private GSPcLocalViewer.ZoomMode _zoomMode;

		private double _zoom;

		private int _startPage;

		private Brush _backBrush;

		private Point _ptLast;

		private PointF _himm2pix = new PointF(-1f, -1f);

		private PageImageList _img = new PageImageList();

		private bool _cancel;

		private bool _rendering;

		[DefaultValue(typeof(Color), "AppWorkspace")]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
				this._backBrush = new SolidBrush(value);
			}
		}

		public PrintDocument Document
		{
			get
			{
				return this._doc;
			}
			set
			{
				if (value != this._doc)
				{
					this._doc = value;
					this.RefreshPreview();
				}
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsRendering
		{
			get
			{
				return this._rendering;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int PageCount
		{
			get
			{
				return this._img.Count;
			}
		}

		[Browsable(false)]
		public PageImageList PageImages
		{
			get
			{
				return this._img;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int StartPage
		{
			get
			{
				return this._startPage;
			}
			set
			{
				if (value > this.PageCount - 1)
				{
					value = this.PageCount - 1;
				}
				if (value < 0)
				{
					value = 0;
				}
				if (value != this._startPage)
				{
					this._startPage = value;
					this.UpdateScrollBars();
					this.OnStartPageChanged(EventArgs.Empty);
				}
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public double Zoom
		{
			get
			{
				return this._zoom;
			}
			set
			{
				if (value != this._zoom || this.ZoomMode != GSPcLocalViewer.ZoomMode.Custom)
				{
					this.ZoomMode = GSPcLocalViewer.ZoomMode.Custom;
					this._zoom = value;
					this.UpdateScrollBars();
					this.OnZoomModeChanged(EventArgs.Empty);
				}
			}
		}

		[DefaultValue(GSPcLocalViewer.ZoomMode.FullPage)]
		public GSPcLocalViewer.ZoomMode ZoomMode
		{
			get
			{
				return this._zoomMode;
			}
			set
			{
				if (value != this._zoomMode)
				{
					this._zoomMode = value;
					this.UpdateScrollBars();
					this.OnZoomModeChanged(EventArgs.Empty);
				}
			}
		}

		public CustomPrintPreviewControl()
		{
			this.BackColor = SystemColors.AppWorkspace;
			this.ZoomMode = GSPcLocalViewer.ZoomMode.FullPage;
			this.StartPage = 0;
			base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
		}

		private void _doc_EndPrint(object sender, PrintEventArgs e)
		{
			try
			{
				this.SyncPageImages(true);
			}
			catch
			{
			}
		}

		private void _doc_PrintPage(object sender, PrintPageEventArgs e)
		{
			try
			{
				this.SyncPageImages(false);
				if (this._cancel)
				{
					e.Cancel = true;
				}
			}
			catch
			{
			}
		}

		public void Cancel()
		{
			this._cancel = true;
		}

		private Image GetImage(int page)
		{
			Image image;
			Image item;
			try
			{
				if (page <= -1 || page >= this.PageCount)
				{
					item = null;
				}
				else
				{
					item = this._img[page];
				}
				image = item;
			}
			catch
			{
				image = null;
			}
			return image;
		}

		private Rectangle GetImageRectangle(Image img)
		{
			double num;
			System.Drawing.Size imageSizeInPixels = this.GetImageSizeInPixels(img);
			Rectangle rectangle = new Rectangle(0, 0, imageSizeInPixels.Width, imageSizeInPixels.Height);
			try
			{
				Rectangle clientRectangle = base.ClientRectangle;
				switch (this._zoomMode)
				{
					case GSPcLocalViewer.ZoomMode.ActualSize:
					{
						this._zoom = 1;
						break;
					}
					case GSPcLocalViewer.ZoomMode.FullPage:
					{
						num = (rectangle.Width > 0 ? (double)clientRectangle.Width / (double)rectangle.Width : 0);
						double num1 = num;
						this._zoom = Math.Min(num1, (rectangle.Height > 0 ? (double)clientRectangle.Height / (double)rectangle.Height : 0));
						break;
					}
					case GSPcLocalViewer.ZoomMode.PageWidth:
					{
						this._zoom = (rectangle.Width > 0 ? (double)clientRectangle.Width / (double)rectangle.Width : 0);
						break;
					}
					case GSPcLocalViewer.ZoomMode.TwoPages:
					{
						rectangle.Width = rectangle.Width * 2;
						goto case GSPcLocalViewer.ZoomMode.FullPage;
					}
				}
				rectangle.Width = (int)((double)rectangle.Width * this._zoom);
				rectangle.Height = (int)((double)rectangle.Height * this._zoom);
				int width = (clientRectangle.Width - rectangle.Width) / 2;
				if (width > 0)
				{
					rectangle.X = rectangle.X + width;
				}
				int height = (clientRectangle.Height - rectangle.Height) / 2;
				if (height > 0)
				{
					rectangle.Y = rectangle.Y + height;
				}
				rectangle.Inflate(-4, -4);
				if (this._zoomMode == GSPcLocalViewer.ZoomMode.TwoPages)
				{
					rectangle.Inflate(-2, 0);
				}
			}
			catch
			{
			}
			return rectangle;
		}

		private System.Drawing.Size GetImageSizeInPixels(Image img)
		{
			SizeF physicalDimension = img.PhysicalDimension;
			try
			{
				if (img is Metafile)
				{
					if (this._himm2pix.X < 0f)
					{
						using (Graphics graphic = base.CreateGraphics())
						{
							this._himm2pix.X = graphic.DpiX / 2540f;
							this._himm2pix.Y = graphic.DpiY / 2540f;
						}
					}
					physicalDimension.Width = physicalDimension.Width * this._himm2pix.X;
					physicalDimension.Height = physicalDimension.Height * this._himm2pix.Y;
				}
			}
			catch
			{
			}
			return System.Drawing.Size.Truncate(physicalDimension);
		}

		protected override bool IsInputKey(Keys keyData)
		{
			switch (keyData)
			{
				case Keys.Prior:
				case Keys.Next:
				case Keys.End:
				case Keys.Home:
				case Keys.Left:
				case Keys.Up:
				case Keys.Right:
				case Keys.Down:
				{
					return true;
				}
			}
			return base.IsInputKey(keyData);
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			try
			{
				if (!e.Handled)
				{
					switch (e.KeyCode)
					{
						case Keys.Prior:
						{
							CustomPrintPreviewControl startPage = this;
							startPage.StartPage = startPage.StartPage - 1;
							break;
						}
						case Keys.Next:
						{
							CustomPrintPreviewControl customPrintPreviewControl = this;
							customPrintPreviewControl.StartPage = customPrintPreviewControl.StartPage + 1;
							break;
						}
						case Keys.End:
						{
							base.AutoScrollPosition = Point.Empty;
							this.StartPage = this.PageCount - 1;
							break;
						}
						case Keys.Home:
						{
							base.AutoScrollPosition = Point.Empty;
							this.StartPage = 0;
							break;
						}
						case Keys.Left:
						case Keys.Up:
						case Keys.Right:
						case Keys.Down:
						{
							if (this.ZoomMode == GSPcLocalViewer.ZoomMode.FullPage || this.ZoomMode == GSPcLocalViewer.ZoomMode.TwoPages)
							{
								switch (e.KeyCode)
								{
									case Keys.Left:
									case Keys.Up:
									{
										CustomPrintPreviewControl startPage1 = this;
										startPage1.StartPage = startPage1.StartPage - 1;
										break;
									}
									case Keys.Right:
									case Keys.Down:
									{
										CustomPrintPreviewControl customPrintPreviewControl1 = this;
										customPrintPreviewControl1.StartPage = customPrintPreviewControl1.StartPage + 1;
										break;
									}
								}
							}
							else
							{
								Point autoScrollPosition = base.AutoScrollPosition;
								switch (e.KeyCode)
								{
									case Keys.Left:
									{
										autoScrollPosition.X = autoScrollPosition.X + 20;
										break;
									}
									case Keys.Up:
									{
										autoScrollPosition.Y = autoScrollPosition.Y + 20;
										break;
									}
									case Keys.Right:
									{
										autoScrollPosition.X = autoScrollPosition.X - 20;
										break;
									}
									case Keys.Down:
									{
										autoScrollPosition.Y = autoScrollPosition.Y - 20;
										break;
									}
								}
								base.AutoScrollPosition = new Point(-autoScrollPosition.X, -autoScrollPosition.Y);
								break;
							}
							break;
						}
						default:
						{
							return;
						}
					}
					e.Handled = true;
				}
			}
			catch
			{
			}
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			try
			{
				if (e.Button == System.Windows.Forms.MouseButtons.Left && base.AutoScrollMinSize != System.Drawing.Size.Empty)
				{
					this.Cursor = Cursors.NoMove2D;
					this._ptLast = new Point(e.X, e.Y);
				}
			}
			catch
			{
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			try
			{
				if (this.Cursor == Cursors.NoMove2D)
				{
					int x = e.X - this._ptLast.X;
					int y = e.Y - this._ptLast.Y;
					if (x != 0 || y != 0)
					{
						Point autoScrollPosition = base.AutoScrollPosition;
						base.AutoScrollPosition = new Point(-(autoScrollPosition.X + x), -(autoScrollPosition.Y + y));
						this._ptLast = new Point(e.X, e.Y);
					}
				}
			}
			catch
			{
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			try
			{
				if (e.Button == System.Windows.Forms.MouseButtons.Left && this.Cursor == Cursors.NoMove2D)
				{
					this.Cursor = Cursors.Default;
				}
			}
			catch
			{
			}
		}

		protected void OnPageCountChanged(EventArgs e)
		{
			try
			{
				if (this.PageCountChanged != null)
				{
					this.PageCountChanged(this, e);
				}
			}
			catch
			{
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			try
			{
				Image image = this.GetImage(this.StartPage);
				if (image != null)
				{
					Rectangle imageRectangle = this.GetImageRectangle(image);
					if (imageRectangle.Width > 2 && imageRectangle.Height > 2)
					{
						imageRectangle.Offset(base.AutoScrollPosition);
						if (this._zoomMode == GSPcLocalViewer.ZoomMode.TwoPages)
						{
							imageRectangle.Width = (imageRectangle.Width - 4) / 2;
							this.RenderPage(e.Graphics, image, imageRectangle);
							image = this.GetImage(this.StartPage + 1);
							if (image != null)
							{
								imageRectangle = this.GetImageRectangle(image);
								imageRectangle.Width = (imageRectangle.Width - 4) / 2;
								imageRectangle.Offset(imageRectangle.Width + 4, 0);
								this.RenderPage(e.Graphics, image, imageRectangle);
							}
						}
						else
						{
							this.RenderPage(e.Graphics, image, imageRectangle);
						}
					}
				}
				e.Graphics.FillRectangle(this._backBrush, base.ClientRectangle);
			}
			catch
			{
			}
		}

		protected override void OnPaintBackground(PaintEventArgs e)
		{
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			this.UpdateScrollBars();
			base.OnSizeChanged(e);
		}

		protected void OnStartPageChanged(EventArgs e)
		{
			try
			{
				if (this.StartPageChanged != null)
				{
					this.StartPageChanged(this, e);
				}
			}
			catch
			{
			}
		}

		protected void OnZoomModeChanged(EventArgs e)
		{
			try
			{
				if (this.ZoomModeChanged != null)
				{
					this.ZoomModeChanged(this, e);
				}
			}
			catch
			{
			}
		}

		public void Print()
		{
			try
			{
				PrinterSettings printerSettings = this._doc.PrinterSettings;
				int minimumPage = printerSettings.MinimumPage - 1;
				int maximumPage = printerSettings.MaximumPage - 1;
				PrintRange printRange = printerSettings.PrintRange;
				switch (printRange)
				{
					case PrintRange.AllPages:
					{
						this.Document.Print();
						return;
					}
					case PrintRange.Selection:
					{
						int startPage = this.StartPage;
						maximumPage = startPage;
						minimumPage = startPage;
						if (this.ZoomMode != GSPcLocalViewer.ZoomMode.TwoPages)
						{
							break;
						}
						maximumPage = Math.Min(minimumPage + 1, this.PageCount - 1);
						break;
					}
					case PrintRange.SomePages:
					{
						minimumPage = printerSettings.FromPage - 1;
						maximumPage = printerSettings.ToPage - 1;
						break;
					}
					default:
					{
						if (printRange == PrintRange.CurrentPage)
						{
							int num = this.StartPage;
							maximumPage = num;
							minimumPage = num;
							break;
						}
						else
						{
							break;
						}
					}
				}
				(new CustomPrintPreviewControl.DocumentPrinter(this, minimumPage, maximumPage)).Print();
			}
			catch
			{
			}
		}

		public void RefreshPreview()
		{
			if (this._doc != null)
			{
				this._img.Clear();
				PrintController printController = this._doc.PrintController;
				try
				{
					this._cancel = false;
					this._rendering = true;
					this._doc.PrintController = new PreviewPrintController();
					this._doc.PrintPage += new PrintPageEventHandler(this._doc_PrintPage);
					this._doc.EndPrint += new PrintEventHandler(this._doc_EndPrint);
					this._doc.Print();
				}
				finally
				{
					this._cancel = false;
					this._rendering = false;
					this._doc.PrintPage -= new PrintPageEventHandler(this._doc_PrintPage);
					this._doc.EndPrint -= new PrintEventHandler(this._doc_EndPrint);
					this._doc.PrintController = printController;
				}
			}
			this.OnPageCountChanged(EventArgs.Empty);
			this.UpdatePreview();
			this.UpdateScrollBars();
		}

		private void RenderPage(Graphics g, Image img, Rectangle rc)
		{
			try
			{
				rc.Offset(1, 1);
				g.DrawRectangle(Pens.Black, rc);
				rc.Offset(-1, -1);
				g.FillRectangle(Brushes.White, rc);
				g.DrawImage(img, rc);
				g.DrawRectangle(Pens.Black, rc);
				rc.Width = rc.Width + 1;
				rc.Height = rc.Height + 1;
				g.ExcludeClip(rc);
				rc.Offset(1, 1);
				g.ExcludeClip(rc);
			}
			catch
			{
			}
		}

		private void SyncPageImages(bool lastPageReady)
		{
			try
			{
				PreviewPrintController printController = (PreviewPrintController)this._doc.PrintController;
				if (printController != null)
				{
					PreviewPageInfo[] previewPageInfo = printController.GetPreviewPageInfo();
					int num = (lastPageReady ? (int)previewPageInfo.Length : (int)previewPageInfo.Length - 1);
					for (int i = this._img.Count; i < num; i++)
					{
						Image image = previewPageInfo[i].Image;
						this._img.Add(image);
						this.OnPageCountChanged(EventArgs.Empty);
						if (this.StartPage < 0)
						{
							this.StartPage = 0;
						}
						if (i == this.StartPage || i == this.StartPage + 1)
						{
							this.Refresh();
						}
						Application.DoEvents();
					}
				}
			}
			catch
			{
			}
		}

		private void UpdatePreview()
		{
			try
			{
				if (this._startPage < 0)
				{
					this._startPage = 0;
				}
				if (this._startPage > this.PageCount - 1)
				{
					this._startPage = this.PageCount - 1;
				}
				base.Invalidate();
			}
			catch
			{
			}
		}

		private void UpdateScrollBars()
		{
			try
			{
				Rectangle empty = Rectangle.Empty;
				Image image = this.GetImage(this.StartPage);
				if (image != null)
				{
					empty = this.GetImageRectangle(image);
				}
				System.Drawing.Size size = new System.Drawing.Size(0, 0);
				switch (this._zoomMode)
				{
					case GSPcLocalViewer.ZoomMode.ActualSize:
					case GSPcLocalViewer.ZoomMode.Custom:
					{
						size = new System.Drawing.Size(empty.Width + 8, empty.Height + 8);
						goto case GSPcLocalViewer.ZoomMode.TwoPages;
					}
					case GSPcLocalViewer.ZoomMode.FullPage:
					case GSPcLocalViewer.ZoomMode.TwoPages:
					{
						if (size != base.AutoScrollMinSize)
						{
							base.AutoScrollMinSize = size;
						}
						this.UpdatePreview();
						break;
					}
					case GSPcLocalViewer.ZoomMode.PageWidth:
					{
						size = new System.Drawing.Size(0, empty.Height + 8);
						goto case GSPcLocalViewer.ZoomMode.TwoPages;
					}
					default:
					{
						goto case GSPcLocalViewer.ZoomMode.TwoPages;
					}
				}
			}
			catch
			{
			}
		}

		public event EventHandler PageCountChanged;

		public event EventHandler StartPageChanged;

		public event EventHandler ZoomModeChanged;

		internal class DocumentPrinter : PrintDocument
		{
			private int _first;

			private int _last;

			private int _index;

			private PageImageList _imgList;

			public DocumentPrinter(CustomPrintPreviewControl preview, int first, int last)
			{
				try
				{
					this._first = first;
					this._last = last;
					this._imgList = preview.PageImages;
					base.DefaultPageSettings = preview.Document.DefaultPageSettings;
					base.PrinterSettings = preview.Document.PrinterSettings;
				}
				catch
				{
				}
			}

			protected override void OnBeginPrint(PrintEventArgs e)
			{
				try
				{
					this._index = this._first;
				}
				catch
				{
				}
			}

			protected override void OnPrintPage(PrintPageEventArgs e)
			{
				try
				{
					e.Graphics.PageUnit = GraphicsUnit.Display;
					Graphics graphics = e.Graphics;
					PageImageList pageImageList = this._imgList;
					CustomPrintPreviewControl.DocumentPrinter documentPrinter = this;
					int num = documentPrinter._index;
					int num1 = num;
					documentPrinter._index = num + 1;
					graphics.DrawImage(pageImageList[num1], e.PageBounds);
					e.HasMorePages = this._index <= this._last;
				}
				catch
				{
				}
			}
		}
	}
}