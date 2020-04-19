namespace DataGridViewIUD
{
    partial class FormDgvIud
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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvMyDist = new System.Windows.Forms.DataGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvMyOwn = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMyDist)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMyOwn)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvMyDist);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(273, 224);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Районы";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvMyDist
            // 
            this.dgvMyDist.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMyDist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMyDist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMyDist.Location = new System.Drawing.Point(2, 2);
            this.dgvMyDist.Name = "dgvMyDist";
            this.dgvMyDist.Size = new System.Drawing.Size(269, 220);
            this.dgvMyDist.TabIndex = 1;
            this.dgvMyDist.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMyDist_CellValueChanged);
            this.dgvMyDist.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvMyDist_RowValidating);
            this.dgvMyDist.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvMyDist_UserDeletingRow);
            this.dgvMyDist.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.dgvMyDist_PreviewKeyDown);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvMyOwn);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(273, 224);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Страна";
            // 
            // dgvMyOwn
            // 
            this.dgvMyOwn.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMyOwn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMyOwn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMyOwn.Location = new System.Drawing.Point(2, 2);
            this.dgvMyOwn.Name = "dgvMyOwn";
            this.dgvMyOwn.Size = new System.Drawing.Size(269, 220);
            this.dgvMyOwn.TabIndex = 0;
            this.dgvMyOwn.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMyOwn_CellContentClick);
            this.dgvMyOwn.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMu_CellValueChanged);
            this.dgvMyOwn.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvMu_RowValidating);
            this.dgvMyOwn.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvMu_UserDeletingRow);
            this.dgvMyOwn.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.dgvMu_PreviewKeyDown);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(2, 1);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(281, 250);
            this.tabControl1.TabIndex = 2;
            // 
            // FormDgvIud
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormDgvIud";
            this.Text = "DataGridView";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMyDist)).EndInit();
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMyOwn)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvMyDist;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgvMyOwn;
        private System.Windows.Forms.TabControl tabControl1;
    }
}