namespace DataGridViewIUD
{
    partial class FormDgvAdvanced
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.dgvResources = new System.Windows.Forms.DataGridView();
            this.dgvMu = new System.Windows.Forms.DataGridView();
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResources)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMu)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Controls.Add(this.dgvResources, 0, 0);
            this.tlpMain.Controls.Add(this.dgvMu, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Size = new System.Drawing.Size(379, 322);
            this.tlpMain.TabIndex = 0;
            // 
            // dgvResources
            // 
            this.dgvResources.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResources.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResources.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResources.Location = new System.Drawing.Point(4, 4);
            this.dgvResources.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvResources.Name = "dgvResources";
            this.dgvResources.Size = new System.Drawing.Size(371, 153);
            this.dgvResources.TabIndex = 0;
            this.dgvResources.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResources_CellValueChanged);
            // 
            // dgvMu
            // 
            this.dgvMu.AllowUserToAddRows = false;
            this.dgvMu.AllowUserToDeleteRows = false;
            this.dgvMu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMu.Location = new System.Drawing.Point(4, 165);
            this.dgvMu.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvMu.Name = "dgvMu";
            this.dgvMu.ReadOnly = true;
            this.dgvMu.Size = new System.Drawing.Size(371, 153);
            this.dgvMu.TabIndex = 1;
            // 
            // FormDgvAdvanced
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 322);
            this.Controls.Add(this.tlpMain);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormDgvAdvanced";
            this.Text = "Продвинутые возможности DataGridView";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormDgvAdvanced_Load);
            this.tlpMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResources)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.DataGridView dgvResources;
        private System.Windows.Forms.DataGridView dgvMu;
    }
}