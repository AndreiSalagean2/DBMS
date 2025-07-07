namespace LibraryWinForms
{
    partial class Form1
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
            this.dgvAuthors = new System.Windows.Forms.DataGridView();
            this.dgvBooks = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAuthors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAuthors
            // 
            this.dgvAuthors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAuthors.Location = new System.Drawing.Point(401, 12);
            this.dgvAuthors.Name = "dgvAuthors";
            this.dgvAuthors.Size = new System.Drawing.Size(439, 219);
            this.dgvAuthors.TabIndex = 0;
            // 
            // dgvBooks
            // 
            this.dgvBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBooks.Location = new System.Drawing.Point(350, 259);
            this.dgvBooks.Name = "dgvBooks";
            this.dgvBooks.Size = new System.Drawing.Size(552, 323);
            this.dgvBooks.TabIndex = 1;
            this.dgvBooks.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBooks_CellContentClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1082, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 79);
            this.button1.TabIndex = 2;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1487, 662);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgvBooks);
            this.Controls.Add(this.dgvAuthors);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAuthors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAuthors;
        private System.Windows.Forms.DataGridView dgvBooks;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
    }
}

