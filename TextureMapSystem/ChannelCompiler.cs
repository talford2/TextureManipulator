using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TextureMapSystem.ImageManipulator;

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
				pictureBox4.Image != null) {

				pictureBox5.Image = ImageManipulator.BlendBitmaps(new List<Bitmap> {
					(Bitmap)pictureBox1.Image,
					(Bitmap)pictureBox2.Image,
					(Bitmap)pictureBox3.Image,
					(Bitmap)pictureBox4.Image
				}, (bmps) => {
					return new RGBAColor(bmps[0].R, bmps[1].R, bmps[2].R, bmps[3].R);
				});
			}
		}
	}
}
