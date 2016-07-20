using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BioBot
{
	public class LogBox : RichTextBox
	{
		private delegate void AddaLine(string Text, Color Color, HorizontalAlignment Alignement);

		private int RegisteredTime;

		public LogBox()
		{
			base.Resize += new EventHandler(this.LogBox_Resize);
			base.Multiline = true;
			base.Width = 445;
			base.Height = 276;
			Point location = new Point(140, 20);
			base.Location = location;
			base.BackColor = Color.Black;
			base.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
		}

		public void AddLine(string Text, Color Color, HorizontalAlignment Alignement = HorizontalAlignment.Left)
		{
			if (DateTime.Now.Minute != this.RegisteredTime)
			{
				this.RegisteredTime = DateTime.Now.Minute;
				this.AddLine(string.Concat(new string[]
				{
					"[",
					Conversions.ToString(DateTime.Now.Hour),
					":",
					Conversions.ToString(DateTime.Now.Minute),
					"]"
				}), Color.Pink, HorizontalAlignment.Center);
			}
			if (this.InvokeRequired)
			{
				LogBox.AddaLine method = new LogBox.AddaLine(this.AddLine);
				object[] args = new object[]
				{
					Text,
					Color,
					Alignement
				};
				try
				{
					this.Invoke(method, args);
					return;
				}
				catch (Exception expr_CC)
				{
					ProjectData.SetProjectError(expr_CC);
					ProjectData.ClearProjectError();
					return;
				}
			}
			try
			{
				base.AppendText(Environment.NewLine + Text);
				base.Select(checked(base.TextLength - Text.Length), Text.Length);
				base.SelectionColor = Color;
				base.SelectionAlignment = Alignement;
				base.SelectionProtected = true;
				base.ScrollToCaret();
			}
			catch (Exception expr_121)
			{
				ProjectData.SetProjectError(expr_121);
				ProjectData.ClearProjectError();
			}
		}

		private void LogBox_Resize(object sender, EventArgs e)
		{
			this.ScrollToCaret();
		}
	}
}
