namespace StandardTestBench
{
    partial class AdjSensor
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
            this.Grp_SetPara_Style = new System.Windows.Forms.GroupBox();
            this.DGV_Sensor = new System.Windows.Forms.DataGridView();
            this.Grp_Operation = new System.Windows.Forms.GroupBox();
            this.button_Return = new System.Windows.Forms.Button();
            this.button_Adj = new System.Windows.Forms.Button();
            this.Grp_Info = new System.Windows.Forms.GroupBox();
            this.TB_Info = new System.Windows.Forms.TextBox();
            this.Grp_SetPara_Style.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Sensor)).BeginInit();
            this.Grp_Operation.SuspendLayout();
            this.Grp_Info.SuspendLayout();
            this.SuspendLayout();
            // 
            // Grp_SetPara_Style
            // 
            this.Grp_SetPara_Style.Controls.Add(this.DGV_Sensor);
            this.Grp_SetPara_Style.Location = new System.Drawing.Point(88, 60);
            this.Grp_SetPara_Style.Name = "Grp_SetPara_Style";
            this.Grp_SetPara_Style.Size = new System.Drawing.Size(1051, 333);
            this.Grp_SetPara_Style.TabIndex = 2;
            this.Grp_SetPara_Style.TabStop = false;
            this.Grp_SetPara_Style.Text = "传感器信息";
            // 
            // DGV_Sensor
            // 
            this.DGV_Sensor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_Sensor.Location = new System.Drawing.Point(6, 20);
            this.DGV_Sensor.Name = "DGV_Sensor";
            this.DGV_Sensor.RowTemplate.Height = 23;
            this.DGV_Sensor.Size = new System.Drawing.Size(1038, 307);
            this.DGV_Sensor.TabIndex = 1;
            // 
            // Grp_Operation
            // 
            this.Grp_Operation.Controls.Add(this.button_Return);
            this.Grp_Operation.Controls.Add(this.button_Adj);
            this.Grp_Operation.Location = new System.Drawing.Point(88, 596);
            this.Grp_Operation.Name = "Grp_Operation";
            this.Grp_Operation.Size = new System.Drawing.Size(1051, 100);
            this.Grp_Operation.TabIndex = 4;
            this.Grp_Operation.TabStop = false;
            this.Grp_Operation.Text = "操作";
            // 
            // button_Return
            // 
            this.button_Return.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button_Return.Location = new System.Drawing.Point(698, 47);
            this.button_Return.Name = "button_Return";
            this.button_Return.Size = new System.Drawing.Size(75, 23);
            this.button_Return.TabIndex = 1;
            this.button_Return.Text = "返回";
            this.button_Return.UseVisualStyleBackColor = true;
            this.button_Return.Click += new System.EventHandler(this.button_Return_Click);
            // 
            // button_Adj
            // 
            this.button_Adj.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button_Adj.Location = new System.Drawing.Point(207, 47);
            this.button_Adj.Name = "button_Adj";
            this.button_Adj.Size = new System.Drawing.Size(75, 23);
            this.button_Adj.TabIndex = 0;
            this.button_Adj.Text = "确定";
            this.button_Adj.UseVisualStyleBackColor = true;
            this.button_Adj.Click += new System.EventHandler(this.button_Adj_Click);
            // 
            // Grp_Info
            // 
            this.Grp_Info.Controls.Add(this.TB_Info);
            this.Grp_Info.Location = new System.Drawing.Point(88, 409);
            this.Grp_Info.Name = "Grp_Info";
            this.Grp_Info.Size = new System.Drawing.Size(1051, 125);
            this.Grp_Info.TabIndex = 3;
            this.Grp_Info.TabStop = false;
            this.Grp_Info.Text = "操作说明";
            // 
            // TB_Info
            // 
            this.TB_Info.BackColor = System.Drawing.SystemColors.Control;
            this.TB_Info.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TB_Info.Enabled = false;
            this.TB_Info.Font = new System.Drawing.Font("宋体", 15F);
            this.TB_Info.Location = new System.Drawing.Point(3, 54);
            this.TB_Info.Multiline = true;
            this.TB_Info.Name = "TB_Info";
            this.TB_Info.Size = new System.Drawing.Size(1041, 23);
            this.TB_Info.TabIndex = 0;
            this.TB_Info.Text = "校零前，请按下紧急停止，确保系统泄压完毕！";
            this.TB_Info.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // AdjSensor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1230, 890);
            this.Controls.Add(this.Grp_Operation);
            this.Controls.Add(this.Grp_Info);
            this.Controls.Add(this.Grp_SetPara_Style);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(5, 15);
            this.Name = "AdjSensor";
            this.Text = "AdjSensor";
            this.Load += new System.EventHandler(this.AdjSensor_Load);
            this.Grp_SetPara_Style.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Sensor)).EndInit();
            this.Grp_Operation.ResumeLayout(false);
            this.Grp_Info.ResumeLayout(false);
            this.Grp_Info.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Grp_SetPara_Style;
        private System.Windows.Forms.DataGridView DGV_Sensor;
        private System.Windows.Forms.GroupBox Grp_Operation;
        private System.Windows.Forms.Button button_Return;
        private System.Windows.Forms.Button button_Adj;
        private System.Windows.Forms.GroupBox Grp_Info;
        private System.Windows.Forms.TextBox TB_Info;
    }
}