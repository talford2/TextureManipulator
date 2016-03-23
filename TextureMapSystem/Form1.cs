using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TextureMapSystem
{
	public partial class Form1 : Form
	{
		private PictureBox _currentPicBox;

		public Form1()
		{
			InitializeComponent();
		}

		private void pictureBox1_DoubleClick(object sender, EventArgs e)
		{
			_currentPicBox = sender as PictureBox;

			var lastDir = Properties.Settings.Default.LastDirectory;
			if (!string.IsNullOrWhiteSpace(lastDir))
			{
				imagePicker.InitialDirectory = lastDir;
			}

			if (imagePicker.ShowDialog() == DialogResult.OK)
			{
				var bmp = new Bitmap(imagePicker.FileName);
				_currentPicBox.Image = bmp;

				Properties.Settings.Default.LastDirectory = Path.GetDirectoryName(imagePicker.FileName);
				Properties.Settings.Default.Save();
				UpdatePreview();
			}
		}

		private void UpdatePreview()
		{
			if (pictureBoxMetal.Image != null && pictureBoxShiny.Image != null)
			{
				pictureBoxResult.Image = GenerateMetallicMap((Bitmap)pictureBoxMetal.Image, (Bitmap)pictureBoxShiny.Image);
			}
		}

		private Bitmap GenerateMetallicMap(Bitmap metallicMap, Bitmap shinyMap)
		{
			var startTime = DateTime.Now;

			var res = ImageManipulator.BlendBitmaps(metallicMap, shinyMap, (m, s) =>
			{
				m.A = s.R;
				return m;
			});

			var timeTaken = DateTime.Now.Subtract(startTime);
			labelAlphaTime.Text = timeTaken.TotalSeconds.ToString();

			return res;
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			if (pictureBoxResult.Image != null)
			{
				if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.SaveDirectory))
				{
					saveFileDialog.InitialDirectory = Properties.Settings.Default.SaveDirectory;
				}
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					buttonSave.Enabled = false;
					Properties.Settings.Default.SaveDirectory = Path.GetDirectoryName(saveFileDialog.FileName);
					Properties.Settings.Default.Save();
					pictureBoxResult.Image.Save(saveFileDialog.FileName);
					//Process.Start(Properties.Settings.Default.SaveDirectory);
					buttonSave.Enabled = true;
				}
			}
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			UpdatePreview();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			button1.Enabled = false;


			if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.AlphaDirectory))
			{
				openFileDialog1.InitialDirectory = Properties.Settings.Default.AlphaDirectory;
			}
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				Properties.Settings.Default.AlphaDirectory = Path.GetDirectoryName(openFileDialog1.FileName);
				var startTime = DateTime.Now;
				var newFolder = Path.Combine(Properties.Settings.Default.AlphaDirectory, "Processed");
				if (!Directory.Exists(newFolder))
				{
					Directory.CreateDirectory(newFolder);
				}
				var files = Directory.GetFiles(Properties.Settings.Default.AlphaDirectory, "*.png");
				foreach (var file in files)
				{
					var fileName = Path.GetFileName(file);
					var bmp = new Bitmap(file);
					ImageManipulator.FixAlpha(bmp);
					var newFile = Path.Combine(newFolder, fileName);
					bmp.Save(newFile);
					bmp.Dispose();
				}

				var timeTaken = DateTime.Now.Subtract(startTime);
				labelAlphaTime.Text = timeTaken.TotalSeconds.ToString();
			}

			button1.Enabled = true;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			button2.Enabled = false;

			var bmp1 = new Bitmap(@"C:\Users\talfo\Desktop\Add\p1.png");
			var bmp2 = new Bitmap(@"C:\Users\talfo\Desktop\Add\p2.png");

			var r1 = ImageManipulator.Add(bmp1, bmp2);
			r1.Save(@"C:\Users\talfo\Desktop\Add\add.png");
			r1.Dispose();

			var r2 = ImageManipulator.Overlay(bmp1, bmp2);
			r2.Save(@"C:\Users\talfo\Desktop\Add\overlay.png");
			r2.Dispose();

			var r3 = ImageManipulator.ColorDodge(bmp1, bmp2);
			r3.Save(@"C:\Users\talfo\Desktop\Add\colorDodger.png");
			r3.Dispose();

			button2.Enabled = true;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			var f = @"D:\Documents\Unity\_SpaceX Proj\SpaceX\Assets\Resources\CallSigns.txt";
			var reader = new StreamReader(f);
			var res = reader.ReadToEnd();
			reader.Close();

			var bits = res.Split(',').OrderBy(s => s);

			string sss = "";
			foreach (var s in bits)
			{
				sss += s.Trim() + ",";
			}
			sss = sss.TrimEnd(',');

			var f2 = @"D:\Documents\Unity\_SpaceX Proj\SpaceX\Assets\Resources\CallSigns222.txt";
			var writer = new StreamWriter(f2);
			writer.Write(sss);
			writer.Close();

			var sssssss = "";
		}

		private void button3_Click(object sender, EventArgs e)
		{
			ChannelCompiler cc = new ChannelCompiler();
			cc.Show();
		}
	}
}
