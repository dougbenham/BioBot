using System;
using System.Drawing;
using System.Windows.Forms;

namespace BioBot
{
	public class myGroupBox : GroupBox
	{
		private Color borderColor;

		public Color BorderColorz
		{
			get
			{
				return this.borderColor;
			}
			set
			{
				this.borderColor = value;
			}
		}

		public myGroupBox()
		{
			this.borderColor = Color.Black;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Size size = TextRenderer.MeasureText(this.Text, this.Font);
			Rectangle clipRectangle = e.ClipRectangle;
			checked
			{
				clipRectangle.Y = (int)Math.Round(unchecked((double)clipRectangle.Y + (double)size.Height / 2.0));
				clipRectangle.Height = (int)Math.Round(unchecked((double)clipRectangle.Height - (double)size.Height / 2.0));
				ControlPaint.DrawBorder(e.Graphics, clipRectangle, this.borderColor, ButtonBorderStyle.Solid);
				Rectangle clipRectangle2 = e.ClipRectangle;
				clipRectangle2.X += 6;
				clipRectangle2.Width = size.Width;
				clipRectangle2.Height = size.Height;
				e.Graphics.FillRectangle(new SolidBrush(this.BackColor), clipRectangle2);
				e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), clipRectangle2);
			}
		}
	}
}
