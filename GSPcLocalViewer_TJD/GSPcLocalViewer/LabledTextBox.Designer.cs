using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
	public class LabledTextBox : UserControl
	{
		private IContainer components;

		private Panel pnlControl;

		private Label lbl;

		private TextBox txt;

		private ToolTip tt;

		public string _Caption
		{
			get
			{
				return this.lbl.Text;
			}
			set
			{
				this.tt.SetToolTip(this.lbl, value);
				this.lbl.Text = value;
			}
		}

		public string _ID
		{
			get
			{
				return this.lbl.Name;
			}
			set
			{
				this.tt.SetToolTip(this.lbl, value);
				this.lbl.Name = value;
			}
		}

		public string _Name
		{
			get;
			set;
		}

		public string _Text
		{
			get
			{
				return this.txt.Text;
			}
			set
			{
				this.txt.Text = value;
			}
		}

		public LabledTextBox()
		{
			this.InitializeComponent();
			this._Name = string.Empty;
			this.tt.SetToolTip(this.lbl, this.lbl.Text);
		}

		public LabledTextBox(string Caption)
		{
			this.InitializeComponent();
			this._Name = string.Empty;
			this.lbl.Text = Caption;
			this.tt.SetToolTip(this.lbl, this.lbl.Text);
		}

		public LabledTextBox(string Caption, string Text)
		{
			this.InitializeComponent();
			this._Name = string.Empty;
			this.lbl.Text = Caption;
			this.txt.Text = Text;
			this.tt.SetToolTip(this.lbl, this.lbl.Text);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.pnlControl = new Panel();
			this.txt = new TextBox();
			this.lbl = new Label();
			this.tt = new ToolTip(this.components);
			this.pnlControl.SuspendLayout();
			base.SuspendLayout();
			this.pnlControl.Controls.Add(this.txt);
			this.pnlControl.Controls.Add(this.lbl);
			this.pnlControl.Dock = DockStyle.Fill;
			this.pnlControl.Location = new Point(0, 0);
			this.pnlControl.Name = "pnlControl";
			this.pnlControl.Padding = new System.Windows.Forms.Padding(2);
			this.pnlControl.Size = new System.Drawing.Size(213, 24);
			this.pnlControl.TabIndex = 0;
			this.txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt.Dock = DockStyle.Left;
			this.txt.Location = new Point(102, 2);
			this.txt.Name = "txt";
			this.txt.Size = new System.Drawing.Size(109, 20);
			this.txt.TabIndex = 1;
			this.lbl.Dock = DockStyle.Left;
			this.lbl.Location = new Point(2, 2);
			this.lbl.Name = "lbl";
			this.lbl.Size = new System.Drawing.Size(100, 20);
			this.lbl.TabIndex = 0;
			this.lbl.Text = "Caption";
			this.lbl.TextAlign = ContentAlignment.MiddleLeft;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.pnlControl);
			base.Name = "LabledTextBox";
			base.Size = new System.Drawing.Size(213, 24);
			this.pnlControl.ResumeLayout(false);
			this.pnlControl.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}