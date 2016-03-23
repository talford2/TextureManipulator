using System;
using System.Drawing;
using System.Windows.Forms;

namespace TextureMapSystem
{
	public partial class ChannelCompiler : Form
	{
		public ChannelCompiler()
		{
			InitializeComponent();
		}

		private void SelectImage(object sender, EventArgs e)
		{
			var pb = (PictureBox)sender;
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "*.png|*.jpg";
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				pb.Image = Image.FromFile(ofd.FileName);
			}

			if (pictureBox1.Image != null &&
				pictureBox2.Image != null &&
				pictureBox3.Image != null &&
				pictureBox4.Image != null)
			{
				pictureBox5.Image = ImageManipulator.BlendBitmaps(
					(Bitmap)pictureBox1.Image,
					(Bitmap)pictureBox2.Image,
					(Bitmap)pictureBox3.Image,
					(Bitmap)pictureBox4.Image,
					(a, b, c, d) => new RGBAColor(a.R, b.R, c.R, d.R));
			}
		}
	}
}
