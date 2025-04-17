namespace Ketkeorasmy_C969Project
{
    partial class Reports
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
            this.NumAppTypesComboBox = new System.Windows.Forms.ComboBox();
            this.dgvNumAppTypes = new System.Windows.Forms.DataGridView();
            this.cancelButton = new System.Windows.Forms.Button();
            this.userComboBox = new System.Windows.Forms.ComboBox();
            this.dgvUserSchedule = new System.Windows.Forms.DataGridView();
            this.customerNameComboBox = new System.Windows.Forms.ComboBox();
            this.dgvNumCustomerAppointments = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNumAppTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserSchedule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNumCustomerAppointments)).BeginInit();
            this.SuspendLayout();
            // 
            // NumAppTypesComboBox
            // 
            this.NumAppTypesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NumAppTypesComboBox.FormattingEnabled = true;
            this.NumAppTypesComboBox.Location = new System.Drawing.Point(89, 67);
            this.NumAppTypesComboBox.Name = "NumAppTypesComboBox";
            this.NumAppTypesComboBox.Size = new System.Drawing.Size(121, 21);
            this.NumAppTypesComboBox.TabIndex = 0;
            this.NumAppTypesComboBox.SelectedIndexChanged += new System.EventHandler(this.NumAppTypesComboBox_SelectedIndexChanged);
            // 
            // dgvNumAppTypes
            // 
            this.dgvNumAppTypes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNumAppTypes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNumAppTypes.Location = new System.Drawing.Point(314, 26);
            this.dgvNumAppTypes.Name = "dgvNumAppTypes";
            this.dgvNumAppTypes.Size = new System.Drawing.Size(349, 103);
            this.dgvNumAppTypes.TabIndex = 1;
            this.dgvNumAppTypes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNumAppTypes_CellContentClick);
            // 
            // cancelButton
            // 
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cancelButton.Location = new System.Drawing.Point(689, 391);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 31);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // userComboBox
            // 
            this.userComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.userComboBox.FormattingEnabled = true;
            this.userComboBox.Location = new System.Drawing.Point(89, 195);
            this.userComboBox.Name = "userComboBox";
            this.userComboBox.Size = new System.Drawing.Size(121, 21);
            this.userComboBox.TabIndex = 3;
            this.userComboBox.SelectedIndexChanged += new System.EventHandler(this.userComboBox_SelectedIndexChanged);
            // 
            // dgvUserSchedule
            // 
            this.dgvUserSchedule.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUserSchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUserSchedule.Location = new System.Drawing.Point(314, 158);
            this.dgvUserSchedule.Name = "dgvUserSchedule";
            this.dgvUserSchedule.Size = new System.Drawing.Size(349, 100);
            this.dgvUserSchedule.TabIndex = 4;
            this.dgvUserSchedule.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUserSchedule_CellContentClick);
            // 
            // customerNameComboBox
            // 
            this.customerNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.customerNameComboBox.FormattingEnabled = true;
            this.customerNameComboBox.Location = new System.Drawing.Point(89, 322);
            this.customerNameComboBox.Name = "customerNameComboBox";
            this.customerNameComboBox.Size = new System.Drawing.Size(121, 21);
            this.customerNameComboBox.TabIndex = 5;
            this.customerNameComboBox.SelectedIndexChanged += new System.EventHandler(this.customerNameComboBox_SelectedIndexChanged);
            // 
            // dgvNumCustomerAppointments
            // 
            this.dgvNumCustomerAppointments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNumCustomerAppointments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNumCustomerAppointments.Location = new System.Drawing.Point(314, 286);
            this.dgvNumCustomerAppointments.Name = "dgvNumCustomerAppointments";
            this.dgvNumCustomerAppointments.Size = new System.Drawing.Size(349, 98);
            this.dgvNumCustomerAppointments.TabIndex = 6;
            this.dgvNumCustomerAppointments.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNumCustomerAppointments_CellContentClick);
            // 
            // Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvNumCustomerAppointments);
            this.Controls.Add(this.customerNameComboBox);
            this.Controls.Add(this.dgvUserSchedule);
            this.Controls.Add(this.userComboBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.dgvNumAppTypes);
            this.Controls.Add(this.NumAppTypesComboBox);
            this.Name = "Reports";
            this.Text = "Reports";
            ((System.ComponentModel.ISupportInitialize)(this.dgvNumAppTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserSchedule)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNumCustomerAppointments)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox NumAppTypesComboBox;
        private System.Windows.Forms.DataGridView dgvNumAppTypes;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ComboBox userComboBox;
        private System.Windows.Forms.DataGridView dgvUserSchedule;
        private System.Windows.Forms.ComboBox customerNameComboBox;
        private System.Windows.Forms.DataGridView dgvNumCustomerAppointments;
    }
}