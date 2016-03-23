namespace TextureMapSystem
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.imagePicker = new System.Windows.Forms.OpenFileDialog();
			this.pictureBoxMetal = new System.Windows.Forms.PictureBox();
			this.pictureBoxShiny = new System.Windows.Forms.PictureBox();
			this.pictureBoxResult = new System.Windows.Forms.PictureBox();
			this.buttonSave = new System.Windows.Forms.Button();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.checkBoxVisualMode = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.labelAlphaTime = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.button3 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxMetal)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxShiny)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxResult)).BeginInit();
			this.SuspendLayout();
			// 
			// imagePicker
			// 
			this.imagePicker.DefaultExt = "*.png, *.jpg";
			this.imagePicker.FileName = "openFileDialog1";
			// 
			// pictureBoxMetal
			// 
			this.pictureBoxMetal.AccessibleName = "Metal";
			this.pictureBoxMetal.BackColor = System.Drawing.SystemColors.ControlDark;
			this.pictureBoxMetal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBoxMetal.Location = new System.Drawing.Point(12, 36);
			this.pictureBoxMetal.Name = "pictureBoxMetal";
			this.pictureBoxMetal.Size = new System.Drawing.Size(300, 300);
			this.pictureBoxMetal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBoxMetal.TabIndex = 0;
			this.pictureBoxMetal.TabStop = false;
			this.pictureBoxMetal.DoubleClick += new System.EventHandler(this.pictureBox1_DoubleClick);
			// 
			// pictureBoxShiny
			// 
			this.pictureBoxShiny.BackColor = System.Drawing.SystemColors.ControlDark;
			this.pictureBoxShiny.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBoxShiny.Location = new System.Drawing.Point(318, 36);
			this.pictureBoxShiny.Name = "pictureBoxShiny";
			this.pictureBoxShiny.Size = new System.Drawing.Size(300, 300);
			this.pictureBoxShiny.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBoxShiny.TabIndex = 1;
			this.pictureBoxShiny.TabStop = false;
			this.pictureBoxShiny.DoubleClick += new System.EventHandler(this.pictureBox1_DoubleClick);
			// 
			// pictureBoxResult
			// 
			this.pictureBoxResult.BackColor = System.Drawing.SystemColors.ControlDark;
			this.pictureBoxResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBoxResult.Location = new System.Drawing.Point(624, 36);
			this.pictureBoxResult.Name = "pictureBoxResult";
			this.pictureBoxResult.Size = new System.Drawing.Size(300, 300);
			this.pictureBoxResult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBoxResult.TabIndex = 5;
			this.pictureBoxResult.TabStop = false;
			// 
			// buttonSave
			// 
			this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSave.Location = new System.Drawing.Point(854, 359);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 6;
			this.buttonSave.Text = "Save";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.DefaultExt = "png";
			this.saveFileDialog.Filter = "Png Image (.png)|*.png";
			// 
			// checkBoxVisualMode
			// 
			this.checkBoxVisualMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.checkBoxVisualMode.AutoSize = true;
			this.checkBoxVisualMode.Location = new System.Drawing.Point(764, 359);
			this.checkBoxVisualMode.Name = "checkBoxVisualMode";
			this.checkBoxVisualMode.Size = new System.Drawing.Size(84, 17);
			this.checkBoxVisualMode.TabIndex = 7;
			this.checkBoxVisualMode.Text = "Visual Mode";
			this.checkBoxVisualMode.UseVisualStyleBackColor = true;
			this.checkBoxVisualMode.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(55, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "Metalness";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(315, 20);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 13);
			this.label2.TabIndex = 9;
			this.label2.Text = "Smoothness";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(621, 20);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(37, 13);
			this.label3.TabIndex = 10;
			this.label3.Text = "Result";
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button1.Location = new System.Drawing.Point(15, 359);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 11;
			this.button1.Text = "Alpha Fix";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// labelAlphaTime
			// 
			this.labelAlphaTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelAlphaTime.AutoSize = true;
			this.labelAlphaTime.Location = new System.Drawing.Point(177, 364);
			this.labelAlphaTime.Name = "labelAlphaTime";
			this.labelAlphaTime.Size = new System.Drawing.Size(10, 13);
			this.labelAlphaTime.TabIndex = 12;
			this.labelAlphaTime.Text = "-";
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(96, 359);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 13;
			this.button2.Text = "Experiment";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(380, 359);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(120, 23);
			this.button3.TabIndex = 14;
			this.button3.Text = "Channel Compiler";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(941, 394);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.labelAlphaTime);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.checkBoxVisualMode);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.pictureBoxResult);
			this.Controls.Add(this.pictureBoxShiny);
			this.Controls.Add(this.pictureBoxMetal);
			this.Name = "Form1";
			this.Text = "Metallic Map Maker";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxMetal)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxShiny)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxResult)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.OpenFileDialog imagePicker;
		private System.Windows.Forms.PictureBox pictureBoxMetal;
		private System.Windows.Forms.PictureBox pictureBoxShiny;
		private System.Windows.Forms.PictureBox pictureBoxResult;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.CheckBox checkBoxVisualMode;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label labelAlphaTime;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Button button3;
	}
}

