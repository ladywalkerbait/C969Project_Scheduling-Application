namespace Ketkeorasmy_C969Project
{
    partial class Form5
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
            this.nameTextBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.addressTextBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cityTextBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.countryTextBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.phoneTextBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.updateCustomerButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // nameTextBox1
            // 
            this.nameTextBox1.Location = new System.Drawing.Point(290, 63);
            this.nameTextBox1.Name = "nameTextBox1";
            this.nameTextBox1.Size = new System.Drawing.Size(264, 20);
            this.nameTextBox1.TabIndex = 0;
            this.nameTextBox1.TextChanged += new System.EventHandler(this.nameTextBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(202, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name";
            // 
            // addressTextBox1
            // 
            this.addressTextBox1.Location = new System.Drawing.Point(290, 122);
            this.addressTextBox1.Name = "addressTextBox1";
            this.addressTextBox1.Size = new System.Drawing.Size(264, 20);
            this.addressTextBox1.TabIndex = 2;
            this.addressTextBox1.TextChanged += new System.EventHandler(this.addressTextBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(187, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Address";
            // 
            // cityTextBox1
            // 
            this.cityTextBox1.Location = new System.Drawing.Point(290, 181);
            this.cityTextBox1.Name = "cityTextBox1";
            this.cityTextBox1.Size = new System.Drawing.Size(264, 20);
            this.cityTextBox1.TabIndex = 4;
            this.cityTextBox1.TextChanged += new System.EventHandler(this.cityTextBox1_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.Location = new System.Drawing.Point(212, 184);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "City";
            // 
            // countryTextBox1
            // 
            this.countryTextBox1.Location = new System.Drawing.Point(290, 244);
            this.countryTextBox1.Name = "countryTextBox1";
            this.countryTextBox1.Size = new System.Drawing.Size(264, 20);
            this.countryTextBox1.TabIndex = 6;
            this.countryTextBox1.TextChanged += new System.EventHandler(this.countryTextBox1_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.Location = new System.Drawing.Point(186, 245);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Country";
            // 
            // phoneTextBox1
            // 
            this.phoneTextBox1.Location = new System.Drawing.Point(290, 305);
            this.phoneTextBox1.Name = "phoneTextBox1";
            this.phoneTextBox1.Size = new System.Drawing.Size(264, 20);
            this.phoneTextBox1.TabIndex = 8;
            this.phoneTextBox1.TextChanged += new System.EventHandler(this.phoneTextBox1_TextChanged);
            this.phoneTextBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.phoneTextBox1_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label5.Location = new System.Drawing.Point(198, 306);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "Phone";
            // 
            // updateCustomerButton
            // 
            this.updateCustomerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.updateCustomerButton.Location = new System.Drawing.Point(377, 375);
            this.updateCustomerButton.Name = "updateCustomerButton";
            this.updateCustomerButton.Size = new System.Drawing.Size(75, 30);
            this.updateCustomerButton.TabIndex = 10;
            this.updateCustomerButton.Text = "Update";
            this.updateCustomerButton.UseVisualStyleBackColor = true;
            this.updateCustomerButton.Click += new System.EventHandler(this.updateCustomerButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cancelButton.Location = new System.Drawing.Point(659, 375);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 30);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.updateCustomerButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.phoneTextBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.countryTextBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cityTextBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.addressTextBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nameTextBox1);
            this.Name = "Form5";
            this.Text = "Form5";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox nameTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox addressTextBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox cityTextBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox countryTextBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox phoneTextBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button updateCustomerButton;
        private System.Windows.Forms.Button cancelButton;
    }
}