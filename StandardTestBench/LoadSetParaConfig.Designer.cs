namespace StandardTestBench
{
    partial class LoadSetParaConfig
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
            this.Grp_ParaList = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.BT_Sure = new System.Windows.Forms.Button();
            this.BT_Exit = new System.Windows.Forms.Button();
            this.Grp_ParaList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // Grp_ParaList
            // 
            this.Grp_ParaList.Controls.Add(this.dataGridView1);
            this.Grp_ParaList.Location = new System.Drawing.Point(12, 12);
            this.Grp_ParaList.Name = "Grp_ParaList";
            this.Grp_ParaList.Size = new System.Drawing.Size(760, 209);
            this.Grp_ParaList.TabIndex = 0;
            this.Grp_ParaList.TabStop = false;
            this.Grp_ParaList.Text = "设置参数配置表";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 20);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(748, 165);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // BT_Sure
            // 
            this.BT_Sure.Location = new System.Drawing.Point(591, 227);
            this.BT_Sure.Name = "BT_Sure";
            this.BT_Sure.Size = new System.Drawing.Size(75, 23);
            this.BT_Sure.TabIndex = 1;
            this.BT_Sure.Text = "确定";
            this.BT_Sure.UseVisualStyleBackColor = true;
            this.BT_Sure.Click += new System.EventHandler(this.BT_Sure_Click);
            // 
            // BT_Exit
            // 
            this.BT_Exit.Location = new System.Drawing.Point(697, 227);
            this.BT_Exit.Name = "BT_Exit";
            this.BT_Exit.Size = new System.Drawing.Size(75, 23);
            this.BT_Exit.TabIndex = 2;
            this.BT_Exit.Text = "取消";
            this.BT_Exit.UseVisualStyleBackColor = true;
            this.BT_Exit.Click += new System.EventHandler(this.BT_Exit_Click);
            // 
            // LoadSetParaConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 262);
            this.ControlBox = false;
            this.Controls.Add(this.BT_Exit);
            this.Controls.Add(this.BT_Sure);
            this.Controls.Add(this.Grp_ParaList);
            this.Name = "LoadSetParaConfig";
            this.Text = "LoadSetParaConfig";
            this.Load += new System.EventHandler(this.LoadSetParaConfig_Load);
            this.Grp_ParaList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Grp_ParaList;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button BT_Sure;
        private System.Windows.Forms.Button BT_Exit;
    }
}