using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace BioBot
{
	[DesignerGenerated]
	public class AboutForm : Form
	{
		private IContainer components;

		[AccessedThroughProperty("Label1")]
		private Label _Label1;

		internal virtual Label Label1
		{
			get
			{
				return this._Label1;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Label1_Click);
				if (this._Label1 != null)
				{
					this._Label1.Click -= value2;
				}
				this._Label1 = value;
				if (this._Label1 != null)
				{
					this._Label1.Click += value2;
				}
			}
		}

		[DebuggerNonUserCode]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this.components != null)
				{
					this.components.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		[DebuggerStepThrough]
		private void InitializeComponent()
		{
			this.Label1 = new Label();
			this.SuspendLayout();
			this.Label1.AutoSize = true;
			Control arg_2F_0 = this.Label1;
			Point location = new Point(13, 16);
			arg_2F_0.Location = location;
			Control arg_46_0 = this.Label1;
			Padding margin = new Padding(4, 0, 4, 0);
			arg_46_0.Margin = margin;
			this.Label1.Name = "Label1";
			Control arg_71_0 = this.Label1;
			Size size = new Size(237, 34);
			arg_71_0.Size = size;
			this.Label1.TabIndex = 0;
			this.Label1.Text = "Developper: Dezimtox\r\nContact me at: Dezimtox@gmail.com\r\n";
			SizeF autoScaleDimensions = new SizeF(8f, 16f);
			this.AutoScaleDimensions = autoScaleDimensions;
			this.AutoScaleMode = AutoScaleMode.Font;
			size = new Size(273, 62);
			this.ClientSize = size;
			this.Controls.Add(this.Label1);
			this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			margin = new Padding(4, 4, 4, 4);
			this.Margin = margin;
			this.Name = "AboutForm";
			this.Text = "About DBot";
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		public AboutForm()
		{
			base.FormClosing += new FormClosingEventHandler(this.AboutForm_FormClosing);
			this.InitializeComponent();
			this.Hide();
		}

		private void AboutForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			this.Hide();
		}

		private void Label1_Click(object sender, EventArgs e)
		{
		}
	}
}
