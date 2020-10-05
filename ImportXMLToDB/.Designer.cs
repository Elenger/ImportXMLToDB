namespace ImportXMLToDB
{
    partial class ImportFromXML
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
            this.ButtonSelect = new System.Windows.Forms.Button();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.buttonImport = new System.Windows.Forms.Button();
            this.buttonRead = new System.Windows.Forms.Button();
            this.ButtonSelectDB = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonSelect
            // 
            this.ButtonSelect.Location = new System.Drawing.Point(80, 12);
            this.ButtonSelect.Name = "ButtonSelect";
            this.ButtonSelect.Size = new System.Drawing.Size(181, 37);
            this.ButtonSelect.TabIndex = 0;
            this.ButtonSelect.Text = "Select XML file";
            this.ButtonSelect.UseVisualStyleBackColor = true;
            this.ButtonSelect.Click += new System.EventHandler(this.ButtonSelect_Click);
            // 
            // dataGrid
            // 
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Location = new System.Drawing.Point(12, 55);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.Size = new System.Drawing.Size(776, 337);
            this.dataGrid.TabIndex = 1;
            this.dataGrid.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGrid_CellLeave);
            this.dataGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGrid_CellValueChanged);
            this.dataGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGrid_RowEnter);
            // 
            // buttonImport
            // 
            this.buttonImport.Location = new System.Drawing.Point(80, 397);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(166, 41);
            this.buttonImport.TabIndex = 2;
            this.buttonImport.Text = "Import to DB";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.ButtonImport_Click);
            // 
            // buttonRead
            // 
            this.buttonRead.Location = new System.Drawing.Point(538, 398);
            this.buttonRead.Name = "buttonRead";
            this.buttonRead.Size = new System.Drawing.Size(167, 40);
            this.buttonRead.TabIndex = 4;
            this.buttonRead.Text = "Read from DB";
            this.buttonRead.UseVisualStyleBackColor = true;
            this.buttonRead.Click += new System.EventHandler(this.ButtonRead_Click);
            // 
            // ButtonSelectDB
            // 
            this.ButtonSelectDB.Location = new System.Drawing.Point(524, 12);
            this.ButtonSelectDB.Name = "ButtonSelectDB";
            this.ButtonSelectDB.Size = new System.Drawing.Size(181, 37);
            this.ButtonSelectDB.TabIndex = 5;
            this.ButtonSelectDB.Text = "Select .mdf file";
            this.ButtonSelectDB.UseVisualStyleBackColor = true;
            this.ButtonSelectDB.Click += new System.EventHandler(this.ButtonSelectDB_Click);
            // 
            // ImportFromXML
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ButtonSelectDB);
            this.Controls.Add(this.buttonRead);
            this.Controls.Add(this.buttonImport);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.ButtonSelect);
            this.Name = "ImportFromXML";
            this.Text = "Import from XML";
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ButtonSelect;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.Button buttonRead;
        private System.Windows.Forms.Button ButtonSelectDB;
    }
}

