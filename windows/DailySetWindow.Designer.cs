namespace CultivationHouseTools
{
    partial class DailySetWindow
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.boss = new System.Windows.Forms.ComboBox();
            this.setConfirm = new System.Windows.Forms.Button();
            this.monday = new System.Windows.Forms.CheckBox();
            this.tuesday = new System.Windows.Forms.CheckBox();
            this.wednesday = new System.Windows.Forms.CheckBox();
            this.thursday = new System.Windows.Forms.CheckBox();
            this.friday = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "是否进行每日兑换";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "boss报名";
            // 
            // boss
            // 
            this.boss.FormattingEnabled = true;
            this.boss.Items.AddRange(new object[] {
            "饕餮",
            "穷奇",
            "毕方",
            "朱厌",
            "麒麟",
            "貔貅",
            "白泽"});
            this.boss.Location = new System.Drawing.Point(136, 147);
            this.boss.Name = "boss";
            this.boss.Size = new System.Drawing.Size(121, 20);
            this.boss.TabIndex = 3;
            this.boss.Text = "饕餮";
            // 
            // setConfirm
            // 
            this.setConfirm.Location = new System.Drawing.Point(182, 193);
            this.setConfirm.Name = "setConfirm";
            this.setConfirm.Size = new System.Drawing.Size(75, 23);
            this.setConfirm.TabIndex = 4;
            this.setConfirm.Text = "确定";
            this.setConfirm.UseVisualStyleBackColor = true;
            this.setConfirm.Click += new System.EventHandler(this.setConfirm_Click);
            // 
            // monday
            // 
            this.monday.AutoSize = true;
            this.monday.Location = new System.Drawing.Point(161, 24);
            this.monday.Name = "monday";
            this.monday.Size = new System.Drawing.Size(48, 16);
            this.monday.TabIndex = 5;
            this.monday.Text = "周一";
            this.monday.UseVisualStyleBackColor = true;
            // 
            // tuesday
            // 
            this.tuesday.AutoSize = true;
            this.tuesday.Location = new System.Drawing.Point(161, 46);
            this.tuesday.Name = "tuesday";
            this.tuesday.Size = new System.Drawing.Size(48, 16);
            this.tuesday.TabIndex = 6;
            this.tuesday.Text = "周二";
            this.tuesday.UseVisualStyleBackColor = true;
            // 
            // wednesday
            // 
            this.wednesday.AutoSize = true;
            this.wednesday.Location = new System.Drawing.Point(161, 68);
            this.wednesday.Name = "wednesday";
            this.wednesday.Size = new System.Drawing.Size(48, 16);
            this.wednesday.TabIndex = 7;
            this.wednesday.Text = "周三";
            this.wednesday.UseVisualStyleBackColor = true;
            // 
            // thursday
            // 
            this.thursday.AutoSize = true;
            this.thursday.Location = new System.Drawing.Point(161, 90);
            this.thursday.Name = "thursday";
            this.thursday.Size = new System.Drawing.Size(48, 16);
            this.thursday.TabIndex = 8;
            this.thursday.Text = "周四";
            this.thursday.UseVisualStyleBackColor = true;
            // 
            // friday
            // 
            this.friday.AutoSize = true;
            this.friday.Location = new System.Drawing.Point(161, 112);
            this.friday.Name = "friday";
            this.friday.Size = new System.Drawing.Size(48, 16);
            this.friday.TabIndex = 9;
            this.friday.Text = "周五";
            this.friday.UseVisualStyleBackColor = true;
            // 
            // DailySetWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 257);
            this.Controls.Add(this.friday);
            this.Controls.Add(this.thursday);
            this.Controls.Add(this.wednesday);
            this.Controls.Add(this.tuesday);
            this.Controls.Add(this.monday);
            this.Controls.Add(this.setConfirm);
            this.Controls.Add(this.boss);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "DailySetWindow";
            this.Text = "DailySet";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox boss;
        private System.Windows.Forms.Button setConfirm;
        private System.Windows.Forms.CheckBox monday;
        private System.Windows.Forms.CheckBox tuesday;
        private System.Windows.Forms.CheckBox wednesday;
        private System.Windows.Forms.CheckBox thursday;
        private System.Windows.Forms.CheckBox friday;
    }
}