
namespace Cold_War_Class_Storage
{
    partial class AddGunAttachment
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
            this.button1 = new System.Windows.Forms.Button();
            this.AttachmentTypeCombo = new System.Windows.Forms.ComboBox();
            this.AttachmentTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.GunNameTextbox = new System.Windows.Forms.TextBox();
            this.PrimaryCombo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(155, 68);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Add Attachment";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.addAttachment);
            // 
            // AttachmentTypeCombo
            // 
            this.AttachmentTypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AttachmentTypeCombo.FormattingEnabled = true;
            this.AttachmentTypeCombo.Items.AddRange(new object[] {
            "OPTIC",
            "MUZZLE",
            "BARREL",
            "BODY",
            "UNDERBARREL",
            "MAGAZINE",
            "HANDLE",
            "STOCK"});
            this.AttachmentTypeCombo.Location = new System.Drawing.Point(150, 41);
            this.AttachmentTypeCombo.Name = "AttachmentTypeCombo";
            this.AttachmentTypeCombo.Size = new System.Drawing.Size(100, 21);
            this.AttachmentTypeCombo.TabIndex = 1;
            // 
            // AttachmentTextbox
            // 
            this.AttachmentTextbox.Location = new System.Drawing.Point(150, 15);
            this.AttachmentTextbox.Name = "AttachmentTextbox";
            this.AttachmentTextbox.Size = new System.Drawing.Size(100, 20);
            this.AttachmentTextbox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Attachment Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label3.Location = new System.Drawing.Point(6, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Attachment Type:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.AttachmentTypeCombo);
            this.groupBox1.Controls.Add(this.AttachmentTextbox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(270, 104);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add Attachment";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.PrimaryCombo);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.GunNameTextbox);
            this.groupBox2.Location = new System.Drawing.Point(12, 122);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(270, 108);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Add Gun";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(2, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Gun Name:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(150, 71);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "Add Gun";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.addGun_Click);
            // 
            // GunNameTextbox
            // 
            this.GunNameTextbox.Location = new System.Drawing.Point(150, 15);
            this.GunNameTextbox.Name = "GunNameTextbox";
            this.GunNameTextbox.Size = new System.Drawing.Size(100, 20);
            this.GunNameTextbox.TabIndex = 2;
            // 
            // PrimaryCombo
            // 
            this.PrimaryCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PrimaryCombo.FormattingEnabled = true;
            this.PrimaryCombo.Items.AddRange(new object[] {
            "Primary",
            "Secondary"});
            this.PrimaryCombo.Location = new System.Drawing.Point(150, 43);
            this.PrimaryCombo.Name = "PrimaryCombo";
            this.PrimaryCombo.Size = new System.Drawing.Size(100, 21);
            this.PrimaryCombo.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label4.Location = new System.Drawing.Point(1, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(142, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "Type:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AddGunAttachment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 242);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "AddGunAttachment";
            this.Text = "AddGunAttachment";
            this.Load += new System.EventHandler(this.AddGunAttachment_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox AttachmentTypeCombo;
        private System.Windows.Forms.TextBox AttachmentTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox GunNameTextbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox PrimaryCombo;
    }
}