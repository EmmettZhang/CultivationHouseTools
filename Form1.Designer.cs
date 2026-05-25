namespace CultivationHouseTools
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.refresh = new System.Windows.Forms.Button();
            this.flip = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.flipNum = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.title = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.stopRefresh = new System.Windows.Forms.Button();
            this.stopFlip = new System.Windows.Forms.Button();
            this.message = new System.Windows.Forms.TextBox();
            this.shopNum = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.stopUnknownBox = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.unknownNum = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.unknownBox = new System.Windows.Forms.Button();
            this.dayTask = new System.Windows.Forms.Button();
            this.unknownIndex = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // refresh
            // 
            this.refresh.Location = new System.Drawing.Point(304, 44);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(75, 23);
            this.refresh.TabIndex = 0;
            this.refresh.Text = "开始购物";
            this.refresh.UseVisualStyleBackColor = true;
            this.refresh.Click += new System.EventHandler(this.refresh_ClickAsync);
            // 
            // flip
            // 
            this.flip.Location = new System.Drawing.Point(304, 117);
            this.flip.Name = "flip";
            this.flip.Size = new System.Drawing.Size(75, 23);
            this.flip.TabIndex = 1;
            this.flip.Text = "开始翻卡";
            this.flip.UseVisualStyleBackColor = true;
            this.flip.Click += new System.EventHandler(this.flip_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(92, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "翻卡次数";
            // 
            // flipNum
            // 
            this.flipNum.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.flipNum.Location = new System.Drawing.Point(187, 130);
            this.flipNum.Name = "flipNum";
            this.flipNum.Size = new System.Drawing.Size(100, 23);
            this.flipNum.TabIndex = 3;
            this.flipNum.Text = "3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "心悦卡牌";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "幸运商店";
            // 
            // title
            // 
            this.title.ForeColor = System.Drawing.SystemColors.WindowText;
            this.title.Location = new System.Drawing.Point(95, 12);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(193, 21);
            this.title.TabIndex = 6;
            this.title.Text = "修仙小屋 v2.29.0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "小屋窗口标题";
            // 
            // stopRefresh
            // 
            this.stopRefresh.Location = new System.Drawing.Point(304, 73);
            this.stopRefresh.Name = "stopRefresh";
            this.stopRefresh.Size = new System.Drawing.Size(75, 23);
            this.stopRefresh.TabIndex = 8;
            this.stopRefresh.Text = "结束购物";
            this.stopRefresh.UseVisualStyleBackColor = true;
            this.stopRefresh.Click += new System.EventHandler(this.stopRefresh_Click);
            // 
            // stopFlip
            // 
            this.stopFlip.Location = new System.Drawing.Point(304, 146);
            this.stopFlip.Name = "stopFlip";
            this.stopFlip.Size = new System.Drawing.Size(75, 23);
            this.stopFlip.TabIndex = 9;
            this.stopFlip.Text = "结束翻卡";
            this.stopFlip.UseVisualStyleBackColor = true;
            this.stopFlip.Click += new System.EventHandler(this.stopFlip_Click);
            // 
            // message
            // 
            this.message.Location = new System.Drawing.Point(14, 267);
            this.message.Multiline = true;
            this.message.Name = "message";
            this.message.ReadOnly = true;
            this.message.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.message.Size = new System.Drawing.Size(377, 266);
            this.message.TabIndex = 10;
            // 
            // shopNum
            // 
            this.shopNum.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.shopNum.Location = new System.Drawing.Point(188, 57);
            this.shopNum.Name = "shopNum";
            this.shopNum.Size = new System.Drawing.Size(100, 23);
            this.shopNum.TabIndex = 12;
            this.shopNum.Text = "5";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(93, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "购物次数";
            // 
            // stopUnknownBox
            // 
            this.stopUnknownBox.Location = new System.Drawing.Point(304, 219);
            this.stopUnknownBox.Name = "stopUnknownBox";
            this.stopUnknownBox.Size = new System.Drawing.Size(75, 23);
            this.stopUnknownBox.TabIndex = 17;
            this.stopUnknownBox.Text = "结束抽盲盒";
            this.stopUnknownBox.UseVisualStyleBackColor = true;
            this.stopUnknownBox.Click += new System.EventHandler(this.stopUnknownBox_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 208);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "心愿盲盒";
            // 
            // unknownNum
            // 
            this.unknownNum.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.unknownNum.Location = new System.Drawing.Point(188, 190);
            this.unknownNum.Name = "unknownNum";
            this.unknownNum.Size = new System.Drawing.Size(100, 23);
            this.unknownNum.TabIndex = 15;
            this.unknownNum.Text = "9999";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(93, 195);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "盲盒次数";
            // 
            // unknownBox
            // 
            this.unknownBox.Location = new System.Drawing.Point(304, 190);
            this.unknownBox.Name = "unknownBox";
            this.unknownBox.Size = new System.Drawing.Size(75, 23);
            this.unknownBox.TabIndex = 13;
            this.unknownBox.Text = "开始抽盲盒";
            this.unknownBox.UseVisualStyleBackColor = true;
            this.unknownBox.Click += new System.EventHandler(this.unknownBox_Click);
            // 
            // dayTask
            // 
            this.dayTask.Location = new System.Drawing.Point(304, 11);
            this.dayTask.Name = "dayTask";
            this.dayTask.Size = new System.Drawing.Size(75, 23);
            this.dayTask.TabIndex = 18;
            this.dayTask.Text = "一键日常";
            this.dayTask.UseVisualStyleBackColor = true;
            // 
            // unknownIndex
            // 
            this.unknownIndex.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.unknownIndex.Location = new System.Drawing.Point(188, 219);
            this.unknownIndex.Name = "unknownIndex";
            this.unknownIndex.Size = new System.Drawing.Size(100, 23);
            this.unknownIndex.TabIndex = 20;
            this.unknownIndex.Text = "1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(93, 224);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 19;
            this.label8.Text = "指定盲盒";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 545);
            this.Controls.Add(this.unknownIndex);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dayTask);
            this.Controls.Add(this.stopUnknownBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.unknownNum);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.unknownBox);
            this.Controls.Add(this.shopNum);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.message);
            this.Controls.Add(this.stopFlip);
            this.Controls.Add(this.stopRefresh);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.title);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.flipNum);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flip);
            this.Controls.Add(this.refresh);
            this.Name = "Form1";
            this.Text = "CultivationHouseTools";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button refresh;
        private System.Windows.Forms.Button flip;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox flipNum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox title;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button stopRefresh;
        private System.Windows.Forms.Button stopFlip;
        public System.Windows.Forms.TextBox message;
        private System.Windows.Forms.TextBox shopNum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button stopUnknownBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox unknownNum;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button unknownBox;
        private System.Windows.Forms.Button dayTask;
        private System.Windows.Forms.TextBox unknownIndex;
        private System.Windows.Forms.Label label8;
    }
}

