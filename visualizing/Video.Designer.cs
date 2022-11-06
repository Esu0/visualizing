namespace visualizing
{
    partial class Video
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
            this.components = new System.ComponentModel.Container();
            this.init_button = new System.Windows.Forms.Button();
            this.stopstart_button = new System.Windows.Forms.Button();
            this.back_button = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // init_button
            // 
            this.init_button.Location = new System.Drawing.Point(88, 461);
            this.init_button.Name = "init_button";
            this.init_button.Size = new System.Drawing.Size(200, 80);
            this.init_button.TabIndex = 0;
            this.init_button.Text = "最初から";
            this.init_button.UseVisualStyleBackColor = true;
            this.init_button.Click += new System.EventHandler(this.init_button_Click);
            // 
            // stopstart_button
            // 
            this.stopstart_button.Location = new System.Drawing.Point(473, 461);
            this.stopstart_button.Name = "stopstart_button";
            this.stopstart_button.Size = new System.Drawing.Size(200, 80);
            this.stopstart_button.TabIndex = 1;
            this.stopstart_button.Text = "一時停止";
            this.stopstart_button.UseVisualStyleBackColor = true;
            this.stopstart_button.Click += new System.EventHandler(this.stopstart_button_Click);
            // 
            // back_button
            // 
            this.back_button.Font = new System.Drawing.Font("游ゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.back_button.Location = new System.Drawing.Point(648, 12);
            this.back_button.Name = "back_button";
            this.back_button.Size = new System.Drawing.Size(122, 81);
            this.back_button.TabIndex = 2;
            this.back_button.Text = "戻る";
            this.back_button.UseVisualStyleBackColor = true;
            this.back_button.Click += new System.EventHandler(this.back_button_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 96);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(758, 359);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 20;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Video
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 553);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.back_button);
            this.Controls.Add(this.stopstart_button);
            this.Controls.Add(this.init_button);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 600);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "Video";
            this.Text = "Video";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Video_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button init_button;
        private System.Windows.Forms.Button stopstart_button;
        private System.Windows.Forms.Button back_button;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
    }
}