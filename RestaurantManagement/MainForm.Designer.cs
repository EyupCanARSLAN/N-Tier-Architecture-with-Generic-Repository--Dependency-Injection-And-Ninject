namespace RestaurantManagement
{
    partial class MainForm
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
            this.menuList = new System.Windows.Forms.ComboBox();
            this.content = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // menuList
            // 
            this.menuList.FormattingEnabled = true;
            this.menuList.Location = new System.Drawing.Point(62, 37);
            this.menuList.Name = "menuList";
            this.menuList.Size = new System.Drawing.Size(334, 21);
            this.menuList.TabIndex = 0;
            this.menuList.SelectedIndexChanged += new System.EventHandler(this.menuList_SelectedIndexChanged);
            // 
            // content
            // 
            this.content.FormattingEnabled = true;
            this.content.Location = new System.Drawing.Point(62, 81);
            this.content.Name = "content";
            this.content.Size = new System.Drawing.Size(334, 199);
            this.content.TabIndex = 1;
            this.content.SelectedIndexChanged += new System.EventHandler(this.content_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Menu";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(65, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Content";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 311);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.content);
            this.Controls.Add(this.menuList);
            this.Name = "MainForm";
            this.Text = "Main Form";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox menuList;
        private System.Windows.Forms.ListBox content;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

