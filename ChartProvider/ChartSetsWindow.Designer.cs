namespace ChartProviders
{
    partial class ChartSetsWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChartSetsWindow));
            this.numericUpDownLineWidth = new System.Windows.Forms.NumericUpDown();
            this.comboBoxChartType = new System.Windows.Forms.ComboBox();
            this.checkBoxSingleColor = new System.Windows.Forms.CheckBox();
            this.labelLineWidth = new System.Windows.Forms.Label();
            this.labelChartType = new System.Windows.Forms.Label();
            this.groupBoxScale = new System.Windows.Forms.GroupBox();
            this.textBoxYint = new System.Windows.Forms.TextBox();
            this.labelXint = new System.Windows.Forms.Label();
            this.textBoxXint = new System.Windows.Forms.TextBox();
            this.labelYint = new System.Windows.Forms.Label();
            this.groupBoxAxises = new System.Windows.Forms.GroupBox();
            this.textBoxXmin = new System.Windows.Forms.TextBox();
            this.textBoxXmax = new System.Windows.Forms.TextBox();
            this.textBoxYmin = new System.Windows.Forms.TextBox();
            this.textBoxYmax = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLineWidth)).BeginInit();
            this.groupBoxScale.SuspendLayout();
            this.groupBoxAxises.SuspendLayout();
            this.SuspendLayout();
            // 
            // numericUpDownLineWidth
            // 
            this.numericUpDownLineWidth.Location = new System.Drawing.Point(218, 45);
            this.numericUpDownLineWidth.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownLineWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownLineWidth.Name = "numericUpDownLineWidth";
            this.numericUpDownLineWidth.Size = new System.Drawing.Size(60, 20);
            this.numericUpDownLineWidth.TabIndex = 59;
            this.numericUpDownLineWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownLineWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownLineWidth.ValueChanged += new System.EventHandler(this.numericUpDownLineWidth_ValueChanged);
            // 
            // comboBoxChartType
            // 
            this.comboBoxChartType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxChartType.FormattingEnabled = true;
            this.comboBoxChartType.Items.AddRange(new object[] {
            "Line",
            "Spline",
            "StepLine",
            "Column",
            "Candlestick",
            "Point"});
            this.comboBoxChartType.Location = new System.Drawing.Point(188, 12);
            this.comboBoxChartType.Name = "comboBoxChartType";
            this.comboBoxChartType.Size = new System.Drawing.Size(90, 21);
            this.comboBoxChartType.TabIndex = 58;
            this.comboBoxChartType.SelectedIndexChanged += new System.EventHandler(this.comboBoxChartType_SelectedIndexChanged);
            // 
            // checkBoxSingleColor
            // 
            this.checkBoxSingleColor.AutoSize = true;
            this.checkBoxSingleColor.Location = new System.Drawing.Point(13, 79);
            this.checkBoxSingleColor.Name = "checkBoxSingleColor";
            this.checkBoxSingleColor.Size = new System.Drawing.Size(149, 17);
            this.checkBoxSingleColor.TabIndex = 2;
            this.checkBoxSingleColor.Text = "Одинаковый цвет линий";
            this.checkBoxSingleColor.UseVisualStyleBackColor = true;
            this.checkBoxSingleColor.CheckedChanged += new System.EventHandler(this.checkBoxSingleColor_CheckedChanged);
            // 
            // labelLineWidth
            // 
            this.labelLineWidth.AutoSize = true;
            this.labelLineWidth.Location = new System.Drawing.Point(10, 47);
            this.labelLineWidth.Name = "labelLineWidth";
            this.labelLineWidth.Size = new System.Drawing.Size(86, 13);
            this.labelLineWidth.TabIndex = 1;
            this.labelLineWidth.Text = "Толщина линий";
            // 
            // labelChartType
            // 
            this.labelChartType.AutoSize = true;
            this.labelChartType.Location = new System.Drawing.Point(10, 15);
            this.labelChartType.Name = "labelChartType";
            this.labelChartType.Size = new System.Drawing.Size(72, 13);
            this.labelChartType.TabIndex = 0;
            this.labelChartType.Text = "Тип графика";
            // 
            // groupBoxScale
            // 
            this.groupBoxScale.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.groupBoxScale.Controls.Add(this.textBoxYint);
            this.groupBoxScale.Controls.Add(this.labelXint);
            this.groupBoxScale.Controls.Add(this.textBoxXint);
            this.groupBoxScale.Controls.Add(this.labelYint);
            this.groupBoxScale.Location = new System.Drawing.Point(214, 116);
            this.groupBoxScale.Name = "groupBoxScale";
            this.groupBoxScale.Size = new System.Drawing.Size(73, 122);
            this.groupBoxScale.TabIndex = 60;
            this.groupBoxScale.TabStop = false;
            this.groupBoxScale.Text = "Шаги";
            // 
            // textBoxYint
            // 
            this.textBoxYint.Location = new System.Drawing.Point(14, 92);
            this.textBoxYint.Name = "textBoxYint";
            this.textBoxYint.Size = new System.Drawing.Size(47, 20);
            this.textBoxYint.TabIndex = 8;
            this.textBoxYint.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxYint.TextChanged += new System.EventHandler(this.textBoxYint_TextChanged);
            this.textBoxYint.Enter += new System.EventHandler(this.textBox_Enter);
            this.textBoxYint.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            this.textBoxYint.Leave += new System.EventHandler(this.textBox_Leave);
            // 
            // labelXint
            // 
            this.labelXint.AutoSize = true;
            this.labelXint.Location = new System.Drawing.Point(16, 22);
            this.labelXint.Name = "labelXint";
            this.labelXint.Size = new System.Drawing.Size(37, 13);
            this.labelXint.TabIndex = 5;
            this.labelXint.Text = "Шаг X";
            // 
            // textBoxXint
            // 
            this.textBoxXint.Location = new System.Drawing.Point(14, 41);
            this.textBoxXint.Name = "textBoxXint";
            this.textBoxXint.Size = new System.Drawing.Size(47, 20);
            this.textBoxXint.TabIndex = 7;
            this.textBoxXint.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxXint.TextChanged += new System.EventHandler(this.textBoxXint_TextChanged);
            this.textBoxXint.Enter += new System.EventHandler(this.textBox_Enter);
            this.textBoxXint.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            this.textBoxXint.Leave += new System.EventHandler(this.textBox_Leave);
            // 
            // labelYint
            // 
            this.labelYint.AutoSize = true;
            this.labelYint.Location = new System.Drawing.Point(16, 73);
            this.labelYint.Name = "labelYint";
            this.labelYint.Size = new System.Drawing.Size(37, 13);
            this.labelYint.TabIndex = 6;
            this.labelYint.Text = "Шаг Y";
            // 
            // groupBoxAxises
            // 
            this.groupBoxAxises.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.groupBoxAxises.Controls.Add(this.textBoxXmin);
            this.groupBoxAxises.Controls.Add(this.textBoxXmax);
            this.groupBoxAxises.Controls.Add(this.textBoxYmin);
            this.groupBoxAxises.Controls.Add(this.textBoxYmax);
            this.groupBoxAxises.Location = new System.Drawing.Point(7, 116);
            this.groupBoxAxises.Name = "groupBoxAxises";
            this.groupBoxAxises.Size = new System.Drawing.Size(201, 122);
            this.groupBoxAxises.TabIndex = 61;
            this.groupBoxAxises.TabStop = false;
            this.groupBoxAxises.Text = "Границы осей";
            // 
            // textBoxXmin
            // 
            this.textBoxXmin.Location = new System.Drawing.Point(17, 54);
            this.textBoxXmin.Name = "textBoxXmin";
            this.textBoxXmin.Size = new System.Drawing.Size(47, 20);
            this.textBoxXmin.TabIndex = 3;
            this.textBoxXmin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxXmin.TextChanged += new System.EventHandler(this.textBoxXmin_TextChanged);
            this.textBoxXmin.Enter += new System.EventHandler(this.textBox_Enter);
            this.textBoxXmin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            this.textBoxXmin.Leave += new System.EventHandler(this.textBox_Leave);
            // 
            // textBoxXmax
            // 
            this.textBoxXmax.Location = new System.Drawing.Point(141, 54);
            this.textBoxXmax.Name = "textBoxXmax";
            this.textBoxXmax.Size = new System.Drawing.Size(47, 20);
            this.textBoxXmax.TabIndex = 2;
            this.textBoxXmax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxXmax.TextChanged += new System.EventHandler(this.textBoxXmax_TextChanged);
            this.textBoxXmax.Enter += new System.EventHandler(this.textBox_Enter);
            this.textBoxXmax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            this.textBoxXmax.Leave += new System.EventHandler(this.textBox_Leave);
            // 
            // textBoxYmin
            // 
            this.textBoxYmin.Location = new System.Drawing.Point(79, 89);
            this.textBoxYmin.Name = "textBoxYmin";
            this.textBoxYmin.Size = new System.Drawing.Size(47, 20);
            this.textBoxYmin.TabIndex = 1;
            this.textBoxYmin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxYmin.TextChanged += new System.EventHandler(this.textBoxYmin_TextChanged);
            this.textBoxYmin.Enter += new System.EventHandler(this.textBox_Enter);
            this.textBoxYmin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            this.textBoxYmin.Leave += new System.EventHandler(this.textBox_Leave);
            // 
            // textBoxYmax
            // 
            this.textBoxYmax.Location = new System.Drawing.Point(79, 19);
            this.textBoxYmax.Name = "textBoxYmax";
            this.textBoxYmax.Size = new System.Drawing.Size(47, 20);
            this.textBoxYmax.TabIndex = 0;
            this.textBoxYmax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxYmax.TextChanged += new System.EventHandler(this.textBoxYmax_TextChanged);
            this.textBoxYmax.Enter += new System.EventHandler(this.textBox_Enter);
            this.textBoxYmax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            this.textBoxYmax.Leave += new System.EventHandler(this.textBox_Leave);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(110, 247);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 62;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // ChartSetsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 279);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBoxAxises);
            this.Controls.Add(this.groupBoxScale);
            this.Controls.Add(this.numericUpDownLineWidth);
            this.Controls.Add(this.comboBoxChartType);
            this.Controls.Add(this.labelChartType);
            this.Controls.Add(this.checkBoxSingleColor);
            this.Controls.Add(this.labelLineWidth);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChartSetsWindow";
            this.Text = "Настройки графика";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ChartSetsWindow_FormClosed);
            this.Load += new System.EventHandler(this.ChartSetsWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLineWidth)).EndInit();
            this.groupBoxScale.ResumeLayout(false);
            this.groupBoxScale.PerformLayout();
            this.groupBoxAxises.ResumeLayout(false);
            this.groupBoxAxises.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDownLineWidth;
        private System.Windows.Forms.ComboBox comboBoxChartType;
        private System.Windows.Forms.CheckBox checkBoxSingleColor;
        private System.Windows.Forms.Label labelLineWidth;
        private System.Windows.Forms.Label labelChartType;
        private System.Windows.Forms.GroupBox groupBoxScale;
        private System.Windows.Forms.TextBox textBoxYint;
        private System.Windows.Forms.Label labelXint;
        private System.Windows.Forms.TextBox textBoxXint;
        private System.Windows.Forms.Label labelYint;
        private System.Windows.Forms.GroupBox groupBoxAxises;
        private System.Windows.Forms.TextBox textBoxXmin;
        private System.Windows.Forms.TextBox textBoxXmax;
        private System.Windows.Forms.TextBox textBoxYmin;
        private System.Windows.Forms.TextBox textBoxYmax;
        private System.Windows.Forms.Button buttonOK;
    }
}