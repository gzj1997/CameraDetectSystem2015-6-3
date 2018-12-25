namespace CameraDetectSystem.Test
{
    partial class CardAndCamera
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
            this.hWindowControlSet = new HalconDotNet.HWindowControl();
            this.button_jog2 = new System.Windows.Forms.Button();
            this.button_jog1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_currentPos = new System.Windows.Forms.TextBox();
            this.textBox_oldSpeed = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_posName = new System.Windows.Forms.ComboBox();
            this.textBox_jogSpeed = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button_saveSpeed = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.button_savePos = new System.Windows.Forms.Button();
            this.button_saveCamera = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox_exposureTime = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_offset = new System.Windows.Forms.TextBox();
            this.radioButton_trigger = new System.Windows.Forms.RadioButton();
            this.radioButton_continue = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // hWindowControlSet
            // 
            this.hWindowControlSet.BackColor = System.Drawing.Color.Black;
            this.hWindowControlSet.BorderColor = System.Drawing.Color.Black;
            this.hWindowControlSet.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControlSet.Location = new System.Drawing.Point(0, -1);
            this.hWindowControlSet.Name = "hWindowControlSet";
            this.hWindowControlSet.Size = new System.Drawing.Size(528, 476);
            this.hWindowControlSet.TabIndex = 12;
            this.hWindowControlSet.WindowSize = new System.Drawing.Size(528, 476);
            // 
            // button_jog2
            // 
            this.button_jog2.Location = new System.Drawing.Point(713, 391);
            this.button_jog2.Name = "button_jog2";
            this.button_jog2.Size = new System.Drawing.Size(108, 56);
            this.button_jog2.TabIndex = 13;
            this.button_jog2.Text = "点动";
            this.button_jog2.UseVisualStyleBackColor = true;
            // 
            // button_jog1
            // 
            this.button_jog1.Location = new System.Drawing.Point(534, 391);
            this.button_jog1.Name = "button_jog1";
            this.button_jog1.Size = new System.Drawing.Size(108, 56);
            this.button_jog1.TabIndex = 13;
            this.button_jog1.Text = "蠕动";
            this.button_jog1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "当前位置";
            // 
            // textBox_currentPos
            // 
            this.textBox_currentPos.Location = new System.Drawing.Point(64, 12);
            this.textBox_currentPos.Name = "textBox_currentPos";
            this.textBox_currentPos.Size = new System.Drawing.Size(100, 21);
            this.textBox_currentPos.TabIndex = 15;
            // 
            // textBox_oldSpeed
            // 
            this.textBox_oldSpeed.Location = new System.Drawing.Point(64, 94);
            this.textBox_oldSpeed.Name = "textBox_oldSpeed";
            this.textBox_oldSpeed.Size = new System.Drawing.Size(100, 21);
            this.textBox_oldSpeed.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "原始速度";
            // 
            // comboBox_posName
            // 
            this.comboBox_posName.FormattingEnabled = true;
            this.comboBox_posName.Location = new System.Drawing.Point(64, 54);
            this.comboBox_posName.Name = "comboBox_posName";
            this.comboBox_posName.Size = new System.Drawing.Size(100, 20);
            this.comboBox_posName.TabIndex = 18;
            // 
            // textBox_jogSpeed
            // 
            this.textBox_jogSpeed.Location = new System.Drawing.Point(64, 136);
            this.textBox_jogSpeed.Name = "textBox_jogSpeed";
            this.textBox_jogSpeed.Size = new System.Drawing.Size(100, 21);
            this.textBox_jogSpeed.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 19;
            this.label3.Text = "蠕动速度";
            // 
            // button_saveSpeed
            // 
            this.button_saveSpeed.Location = new System.Drawing.Point(174, 94);
            this.button_saveSpeed.Name = "button_saveSpeed";
            this.button_saveSpeed.Size = new System.Drawing.Size(100, 36);
            this.button_saveSpeed.TabIndex = 13;
            this.button_saveSpeed.Text = "保存速度";
            this.button_saveSpeed.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 21;
            this.label4.Text = "位置名称";
            // 
            // button_savePos
            // 
            this.button_savePos.Location = new System.Drawing.Point(174, 15);
            this.button_savePos.Name = "button_savePos";
            this.button_savePos.Size = new System.Drawing.Size(100, 36);
            this.button_savePos.TabIndex = 13;
            this.button_savePos.Text = "保存位置";
            this.button_savePos.UseVisualStyleBackColor = true;
            // 
            // button_saveCamera
            // 
            this.button_saveCamera.Location = new System.Drawing.Point(190, 94);
            this.button_saveCamera.Name = "button_saveCamera";
            this.button_saveCamera.Size = new System.Drawing.Size(84, 35);
            this.button_saveCamera.TabIndex = 13;
            this.button_saveCamera.Text = "相机保存";
            this.button_saveCamera.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox_jogSpeed);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBox_posName);
            this.groupBox1.Controls.Add(this.textBox_oldSpeed);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox_currentPos);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button_savePos);
            this.groupBox1.Controls.Add(this.button_saveSpeed);
            this.groupBox1.Location = new System.Drawing.Point(534, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(288, 177);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "运动设定 ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.radioButton_continue);
            this.groupBox2.Controls.Add(this.radioButton_trigger);
            this.groupBox2.Controls.Add(this.textBox_offset);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.textBox_exposureTime);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.button_saveCamera);
            this.groupBox2.Location = new System.Drawing.Point(534, 207);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(287, 144);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "相机设定 ";
            // 
            // textBox_exposureTime
            // 
            this.textBox_exposureTime.Location = new System.Drawing.Point(64, 20);
            this.textBox_exposureTime.Name = "textBox_exposureTime";
            this.textBox_exposureTime.Size = new System.Drawing.Size(100, 21);
            this.textBox_exposureTime.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 22;
            this.label5.Text = "曝光时间";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 22;
            this.label6.Text = "增益调节";
            // 
            // textBox_offset
            // 
            this.textBox_offset.Location = new System.Drawing.Point(64, 47);
            this.textBox_offset.Name = "textBox_offset";
            this.textBox_offset.Size = new System.Drawing.Size(100, 21);
            this.textBox_offset.TabIndex = 23;
            // 
            // radioButton_trigger
            // 
            this.radioButton_trigger.AutoSize = true;
            this.radioButton_trigger.Location = new System.Drawing.Point(64, 85);
            this.radioButton_trigger.Name = "radioButton_trigger";
            this.radioButton_trigger.Size = new System.Drawing.Size(47, 16);
            this.radioButton_trigger.TabIndex = 24;
            this.radioButton_trigger.TabStop = true;
            this.radioButton_trigger.Text = "触发";
            this.radioButton_trigger.UseVisualStyleBackColor = true;
            // 
            // radioButton_continue
            // 
            this.radioButton_continue.AutoSize = true;
            this.radioButton_continue.Location = new System.Drawing.Point(64, 113);
            this.radioButton_continue.Name = "radioButton_continue";
            this.radioButton_continue.Size = new System.Drawing.Size(47, 16);
            this.radioButton_continue.TabIndex = 24;
            this.radioButton_continue.TabStop = true;
            this.radioButton_continue.Text = "连续";
            this.radioButton_continue.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 25;
            this.label7.Text = "拍照模式";
            // 
            // CardAndCamera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 474);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button_jog1);
            this.Controls.Add(this.button_jog2);
            this.Controls.Add(this.hWindowControlSet);
            this.Name = "CardAndCamera";
            this.Text = " 卡与相机内参调整";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private HalconDotNet.HWindowControl hWindowControlSet;
        private System.Windows.Forms.Button button_jog2;
        private System.Windows.Forms.Button button_jog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_currentPos;
        private System.Windows.Forms.TextBox textBox_oldSpeed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_posName;
        private System.Windows.Forms.TextBox textBox_jogSpeed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_saveSpeed;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_savePos;
        private System.Windows.Forms.Button button_saveCamera;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton radioButton_continue;
        private System.Windows.Forms.RadioButton radioButton_trigger;
        private System.Windows.Forms.TextBox textBox_offset;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_exposureTime;
        private System.Windows.Forms.Label label5;
    }
}