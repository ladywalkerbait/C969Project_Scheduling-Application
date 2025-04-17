namespace Ketkeorasmy_C969Project
{
    partial class Form4
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
            this.nameBox1 = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.addressLabel = new System.Windows.Forms.Label();
            this.addressBox1 = new System.Windows.Forms.TextBox();
            this.cityBox1 = new System.Windows.Forms.TextBox();
            this.cityLabel = new System.Windows.Forms.Label();
            this.countryLabel = new System.Windows.Forms.Label();
            this.countryBox1 = new System.Windows.Forms.TextBox();
            this.phoneLabel = new System.Windows.Forms.Label();
            this.phoneBox1 = new System.Windows.Forms.TextBox();
            this.addCustomerButton = new System.Windows.Forms.Button();
            this.cancelAddCustomerButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // nameBox1
            // 
            this.nameBox1.Location = new System.Drawing.Point(266, 63);
            this.nameBox1.Name = "nameBox1";
            this.nameBox1.Size = new System.Drawing.Size(246, 20);
            this.nameBox1.TabIndex = 0;
            this.nameBox1.TextChanged += new System.EventHandler(this.nameBox1_TextChanged);
            this.nameBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nameBox1_KeyPress);
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.nameLabel.Location = new System.Drawing.Point(184, 66);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(45, 17);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.Text = "Name";
            // 
            // addressLabel
            // 
            this.addressLabel.AutoSize = true;
            this.addressLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.addressLabel.Location = new System.Drawing.Point(169, 131);
            this.addressLabel.Name = "addressLabel";
            this.addressLabel.Size = new System.Drawing.Size(60, 17);
            this.addressLabel.TabIndex = 2;
            this.addressLabel.Text = "Address";
            // 
            // addressBox1
            // 
            this.addressBox1.Location = new System.Drawing.Point(266, 128);
            this.addressBox1.Name = "addressBox1";
            this.addressBox1.Size = new System.Drawing.Size(246, 20);
            this.addressBox1.TabIndex = 3;
            this.addressBox1.TextChanged += new System.EventHandler(this.addressBox1_TextChanged);
            this.addressBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.addressBox1_KeyPress);
            // 
            // cityBox1
            // 
            this.cityBox1.Location = new System.Drawing.Point(266, 192);
            this.cityBox1.Name = "cityBox1";
            this.cityBox1.Size = new System.Drawing.Size(246, 20);
            this.cityBox1.TabIndex = 4;
            this.cityBox1.TextChanged += new System.EventHandler(this.cityBox1_TextChanged);
            this.cityBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cityBox1_KeyPress);
            // 
            // cityLabel
            // 
            this.cityLabel.AutoSize = true;
            this.cityLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cityLabel.Location = new System.Drawing.Point(198, 195);
            this.cityLabel.Name = "cityLabel";
            this.cityLabel.Size = new System.Drawing.Size(31, 17);
            this.cityLabel.TabIndex = 5;
            this.cityLabel.Text = "City";
            this.cityLabel.Click += new System.EventHandler(this.cityLabel_Click);
            // 
            // countryLabel
            // 
            this.countryLabel.AutoSize = true;
            this.countryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.countryLabel.Location = new System.Drawing.Point(172, 258);
            this.countryLabel.Name = "countryLabel";
            this.countryLabel.Size = new System.Drawing.Size(57, 17);
            this.countryLabel.TabIndex = 6;
            this.countryLabel.Text = "Country";
            // 
            // countryBox1
            // 
            this.countryBox1.Location = new System.Drawing.Point(266, 255);
            this.countryBox1.Name = "countryBox1";
            this.countryBox1.Size = new System.Drawing.Size(246, 20);
            this.countryBox1.TabIndex = 7;
            this.countryBox1.TextChanged += new System.EventHandler(this.countryBox1_TextChanged);
            this.countryBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.countryBox1_KeyPress);
            // 
            // phoneLabel
            // 
            this.phoneLabel.AutoSize = true;
            this.phoneLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.phoneLabel.Location = new System.Drawing.Point(180, 319);
            this.phoneLabel.Name = "phoneLabel";
            this.phoneLabel.Size = new System.Drawing.Size(49, 17);
            this.phoneLabel.TabIndex = 8;
            this.phoneLabel.Text = "Phone";
            // 
            // phoneBox1
            // 
            this.phoneBox1.Location = new System.Drawing.Point(266, 316);
            this.phoneBox1.Name = "phoneBox1";
            this.phoneBox1.Size = new System.Drawing.Size(246, 20);
            this.phoneBox1.TabIndex = 9;
            this.phoneBox1.TextChanged += new System.EventHandler(this.phoneBox1_TextChanged);
            this.phoneBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.phoneBox1_KeyPress);
            // 
            // addCustomerButton
            // 
            this.addCustomerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.addCustomerButton.Location = new System.Drawing.Point(318, 390);
            this.addCustomerButton.Name = "addCustomerButton";
            this.addCustomerButton.Size = new System.Drawing.Size(141, 32);
            this.addCustomerButton.TabIndex = 10;
            this.addCustomerButton.Text = "Add Customer";
            this.addCustomerButton.UseVisualStyleBackColor = true;
            this.addCustomerButton.Click += new System.EventHandler(this.addCustomerButton_Click);
            // 
            // cancelAddCustomerButton
            // 
            this.cancelAddCustomerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cancelAddCustomerButton.Location = new System.Drawing.Point(699, 390);
            this.cancelAddCustomerButton.Name = "cancelAddCustomerButton";
            this.cancelAddCustomerButton.Size = new System.Drawing.Size(75, 32);
            this.cancelAddCustomerButton.TabIndex = 11;
            this.cancelAddCustomerButton.Text = "Cancel";
            this.cancelAddCustomerButton.UseVisualStyleBackColor = true;
            this.cancelAddCustomerButton.Click += new System.EventHandler(this.cancelAddCustomerButton_Click);
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cancelAddCustomerButton);
            this.Controls.Add(this.addCustomerButton);
            this.Controls.Add(this.phoneBox1);
            this.Controls.Add(this.phoneLabel);
            this.Controls.Add(this.countryBox1);
            this.Controls.Add(this.countryLabel);
            this.Controls.Add(this.cityLabel);
            this.Controls.Add(this.cityBox1);
            this.Controls.Add(this.addressBox1);
            this.Controls.Add(this.addressLabel);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.nameBox1);
            this.Name = "Form4";
            this.Text = "Form4";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox nameBox1;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label addressLabel;
        private System.Windows.Forms.TextBox addressBox1;
        private System.Windows.Forms.TextBox cityBox1;
        private System.Windows.Forms.Label cityLabel;
        private System.Windows.Forms.Label countryLabel;
        private System.Windows.Forms.TextBox countryBox1;
        private System.Windows.Forms.Label phoneLabel;
        private System.Windows.Forms.TextBox phoneBox1;
        private System.Windows.Forms.Button addCustomerButton;
        private System.Windows.Forms.Button cancelAddCustomerButton;
    }
}