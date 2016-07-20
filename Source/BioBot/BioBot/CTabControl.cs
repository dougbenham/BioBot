using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace BioBot
{
	public class CTabControl : TabControl
	{
		public CTabControl()
		{
			this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.DoubleBuffered = true;
			this.SizeMode = TabSizeMode.Fixed;
			Size itemSize = new Size(0, 0);
			this.ItemSize = itemSize;
		}

		protected override void CreateHandle()
		{
			base.CreateHandle();
			this.Alignment = TabAlignment.Bottom;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Bitmap bitmap = new Bitmap(this.Width, this.Height);
			Graphics graphics = Graphics.FromImage(bitmap);
			Brush brush = new SolidBrush(Color.FromArgb(35, 35, 35));
			Brush brush2 = new SolidBrush(Color.FromArgb(63, 63, 63));
			graphics.Clear(Color.FromArgb(43, 43, 43));
			int arg_57_0 = 0;
			checked
			{
				int num = this.TabCount - 1;
				for (int i = arg_57_0; i <= num; i++)
				{
					Rectangle tabRect = this.GetTabRect(i);
					if (i == this.SelectedIndex)
					{
						graphics.FillRectangle(brush, tabRect);
					}
					else
					{
						graphics.FillRectangle(brush2, tabRect);
					}
					graphics.DrawString(this.TabPages[i].Text, this.Font, Brushes.White, tabRect, new StringFormat
					{
						Alignment = StringAlignment.Center,
						LineAlignment = StringAlignment.Center
					});
				}
				NewLateBinding.LateCall(e.Graphics, null, "DrawImage", new object[]
				{
					RuntimeHelpers.GetObjectValue(bitmap.Clone()),
					0,
					0
				}, null, null, null, true);
				e.Dispose();
				bitmap.Dispose();
				base.OnPaint(e);
			}
		}
	}
}
