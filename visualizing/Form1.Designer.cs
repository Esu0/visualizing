namespace visualizing
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.stk_que = new System.Windows.Forms.Button();
            this.qsort = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // stk_que
            // 
            this.stk_que.Font = new System.Drawing.Font("游ゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.stk_que.Location = new System.Drawing.Point(12, 12);
            this.stk_que.Name = "stk_que";
            this.stk_que.Size = new System.Drawing.Size(200, 50);
            this.stk_que.TabIndex = 0;
            this.stk_que.Text = "マージソート";
            this.stk_que.UseVisualStyleBackColor = true;
            this.stk_que.Click += new System.EventHandler(this.msort_Click);
            // 
            // qsort
            // 
            this.qsort.Font = new System.Drawing.Font("游ゴシック", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.qsort.Location = new System.Drawing.Point(218, 12);
            this.qsort.Name = "qsort";
            this.qsort.Size = new System.Drawing.Size(200, 50);
            this.qsort.TabIndex = 1;
            this.qsort.Text = "クイックソート";
            this.qsort.UseVisualStyleBackColor = true;
            this.qsort.Click += new System.EventHandler(this.qsort_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 75);
            this.Controls.Add(this.qsort);
            this.Controls.Add(this.stk_que);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button stk_que;
        private System.Windows.Forms.Button qsort;
    }
}

