namespace Ketkeorasmy_C969Project
{
    partial class Calendar
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
            this.dgvCalendar = new System.Windows.Forms.DataGridView();
            this.AllAppointmentsButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.monthDayPicker = new System.Windows.Forms.DateTimePicker();
            this.MonthComboBox = new System.Windows.Forms.ComboBox();
            this.MonthLabel = new System.Windows.Forms.Label();
            this.DayLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalendar)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCalendar
            // 
            this.dgvCalendar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCalendar.Location = new System.Drawing.Point(95, 146);
            this.dgvCalendar.Name = "dgvCalendar";
            this.dgvCalendar.Size = new System.Drawing.Size(590, 205);
            this.dgvCalendar.TabIndex = 0;
            this.dgvCalendar.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCalendar_CellContentClick);
            // 
            // AllAppointmentsButton
            // 
            this.AllAppointmentsButton.Location = new System.Drawing.Point(318, 382);
            this.AllAppointmentsButton.Name = "AllAppointmentsButton";
            this.AllAppointmentsButton.Size = new System.Drawing.Size(103, 34);
            this.AllAppointmentsButton.TabIndex = 1;
            this.AllAppointmentsButton.Text = "All Appointments";
            this.AllAppointmentsButton.UseVisualStyleBackColor = true;
            this.AllAppointmentsButton.Click += new System.EventHandler(this.AllAppointmentsButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(685, 386);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 30);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // monthDayPicker
            // 
            this.monthDayPicker.Location = new System.Drawing.Point(488, 63);
            this.monthDayPicker.MaxDate = new System.DateTime(2025, 12, 31, 0, 0, 0, 0);
            this.monthDayPicker.MinDate = new System.DateTime(2025, 1, 1, 0, 0, 0, 0);
            this.monthDayPicker.Name = "monthDayPicker";
            this.monthDayPicker.Size = new System.Drawing.Size(197, 20);
            this.monthDayPicker.TabIndex = 3;
            this.monthDayPicker.ValueChanged += new System.EventHandler(this.monthDayPicker_ValueChanged);
            // 
            // MonthComboBox
            // 
            this.MonthComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MonthComboBox.FormattingEnabled = true;
            this.MonthComboBox.Location = new System.Drawing.Point(95, 63);
            this.MonthComboBox.Name = "MonthComboBox";
            this.MonthComboBox.Size = new System.Drawing.Size(121, 21);
            this.MonthComboBox.TabIndex = 4;
            this.MonthComboBox.SelectedIndexChanged += new System.EventHandler(this.MonthComboBox_SelectedIndexChanged);
            // 
            // MonthLabel
            // 
            this.MonthLabel.AutoSize = true;
            this.MonthLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.MonthLabel.Location = new System.Drawing.Point(92, 33);
            this.MonthLabel.Name = "MonthLabel";
            this.MonthLabel.Size = new System.Drawing.Size(94, 17);
            this.MonthLabel.TabIndex = 5;
            this.MonthLabel.Text = "Monthly View:";
            // 
            // DayLabel
            // 
            this.DayLabel.AutoSize = true;
            this.DayLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.DayLabel.Location = new System.Drawing.Point(485, 35);
            this.DayLabel.Name = "DayLabel";
            this.DayLabel.Size = new System.Drawing.Size(76, 17);
            this.DayLabel.TabIndex = 6;
            this.DayLabel.Text = "Daily View:";
            // 
            // Calendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.DayLabel);
            this.Controls.Add(this.MonthLabel);
            this.Controls.Add(this.MonthComboBox);
            this.Controls.Add(this.monthDayPicker);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.AllAppointmentsButton);
            this.Controls.Add(this.dgvCalendar);
            this.Name = "Calendar";
            this.Text = "Calendar";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalendar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCalendar;
        private System.Windows.Forms.Button AllAppointmentsButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.DateTimePicker monthDayPicker;
        private System.Windows.Forms.ComboBox MonthComboBox;
        private System.Windows.Forms.Label MonthLabel;
        private System.Windows.Forms.Label DayLabel;
    }
}