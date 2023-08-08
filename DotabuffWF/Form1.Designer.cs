namespace DotabuffWF
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            button1 = new Button();
            comboBox1 = new ComboBox();
            comboBox2 = new ComboBox();
            comboBox3 = new ComboBox();
            comboBox4 = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            dataGridView2 = new DataGridView();
            button2 = new Button();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            label6 = new Label();
            label8 = new Label();
            label7 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            comboBox10 = new ComboBox();
            comboBox9 = new ComboBox();
            comboBox8 = new ComboBox();
            comboBox7 = new ComboBox();
            comboBox6 = new ComboBox();
            button3 = new Button();
            dataGridView3 = new DataGridView();
            comboBox5 = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView3).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(6, 8);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(915, 249);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // button1
            // 
            button1.Font = new Font("Microsoft PhagsPa", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button1.Location = new Point(933, 462);
            button1.Name = "button1";
            button1.Size = new Size(163, 46);
            button1.TabIndex = 1;
            button1.Text = "Посчитать";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(933, 108);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(163, 23);
            comboBox1.TabIndex = 2;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(933, 164);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(163, 23);
            comboBox2.TabIndex = 3;
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(933, 223);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(163, 23);
            comboBox3.TabIndex = 4;
            // 
            // comboBox4
            // 
            comboBox4.FormattingEnabled = true;
            comboBox4.Location = new Point(933, 280);
            comboBox4.Name = "comboBox4";
            comboBox4.Size = new Size(163, 23);
            comboBox4.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft PhagsPa", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(942, 52);
            label1.Name = "label1";
            label1.Size = new Size(142, 21);
            label1.TabIndex = 6;
            label1.Text = "Выберите героев";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft PhagsPa", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(964, 73);
            label2.Name = "label2";
            label2.Size = new Size(99, 21);
            label2.TabIndex = 7;
            label2.Text = "противника";
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(6, 263);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowTemplate.Height = 25;
            dataGridView2.Size = new Size(915, 245);
            dataGridView2.TabIndex = 8;
            // 
            // button2
            // 
            button2.Font = new Font("Microsoft PhagsPa", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button2.Location = new Point(933, 410);
            button2.Name = "button2";
            button2.Size = new Size(163, 46);
            button2.TabIndex = 9;
            button2.Text = "Очистить";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(2, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1118, 552);
            tabControl1.TabIndex = 10;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(comboBox5);
            tabPage1.Controls.Add(button1);
            tabPage1.Controls.Add(dataGridView2);
            tabPage1.Controls.Add(dataGridView1);
            tabPage1.Controls.Add(comboBox1);
            tabPage1.Controls.Add(button2);
            tabPage1.Controls.Add(comboBox2);
            tabPage1.Controls.Add(comboBox3);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(comboBox4);
            tabPage1.Controls.Add(label1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1110, 524);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Анализ статистики";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(label6);
            tabPage2.Controls.Add(label8);
            tabPage2.Controls.Add(label7);
            tabPage2.Controls.Add(label5);
            tabPage2.Controls.Add(label4);
            tabPage2.Controls.Add(label3);
            tabPage2.Controls.Add(comboBox10);
            tabPage2.Controls.Add(comboBox9);
            tabPage2.Controls.Add(comboBox8);
            tabPage2.Controls.Add(comboBox7);
            tabPage2.Controls.Add(comboBox6);
            tabPage2.Controls.Add(button3);
            tabPage2.Controls.Add(dataGridView3);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1110, 524);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Добление героя в избранное";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(537, 460);
            label6.Name = "label6";
            label6.Size = new Size(38, 15);
            label6.TabIndex = 22;
            label6.Text = "label6";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(834, 460);
            label8.Name = "label8";
            label8.Size = new Size(38, 15);
            label8.TabIndex = 21;
            label8.Text = "label8";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(682, 460);
            label7.Name = "label7";
            label7.Size = new Size(38, 15);
            label7.TabIndex = 20;
            label7.Text = "label6";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(384, 460);
            label5.Name = "label5";
            label5.Size = new Size(38, 15);
            label5.TabIndex = 19;
            label5.Text = "label5";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(236, 460);
            label4.Name = "label4";
            label4.Size = new Size(38, 15);
            label4.TabIndex = 18;
            label4.Text = "label4";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(74, 460);
            label3.Name = "label3";
            label3.Size = new Size(38, 15);
            label3.TabIndex = 17;
            label3.Text = "label3";
            // 
            // comboBox10
            // 
            comboBox10.FormattingEnabled = true;
            comboBox10.Location = new Point(778, 478);
            comboBox10.Name = "comboBox10";
            comboBox10.Size = new Size(143, 23);
            comboBox10.TabIndex = 16;
            // 
            // comboBox9
            // 
            comboBox9.FormattingEnabled = true;
            comboBox9.Location = new Point(629, 478);
            comboBox9.Name = "comboBox9";
            comboBox9.Size = new Size(143, 23);
            comboBox9.TabIndex = 15;
            // 
            // comboBox8
            // 
            comboBox8.FormattingEnabled = true;
            comboBox8.Location = new Point(480, 478);
            comboBox8.Name = "comboBox8";
            comboBox8.Size = new Size(143, 23);
            comboBox8.TabIndex = 14;
            // 
            // comboBox7
            // 
            comboBox7.FormattingEnabled = true;
            comboBox7.Location = new Point(331, 478);
            comboBox7.Name = "comboBox7";
            comboBox7.Size = new Size(143, 23);
            comboBox7.TabIndex = 13;
            // 
            // comboBox6
            // 
            comboBox6.FormattingEnabled = true;
            comboBox6.Location = new Point(182, 478);
            comboBox6.Name = "comboBox6";
            comboBox6.Size = new Size(143, 23);
            comboBox6.TabIndex = 12;
            // 
            // button3
            // 
            button3.Font = new Font("Microsoft PhagsPa", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button3.Location = new Point(933, 463);
            button3.Name = "button3";
            button3.Size = new Size(163, 46);
            button3.TabIndex = 10;
            button3.Text = "Добавить";
            button3.UseVisualStyleBackColor = true;
            // 
            // dataGridView3
            // 
            dataGridView3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView3.Location = new Point(6, 8);
            dataGridView3.Name = "dataGridView3";
            dataGridView3.RowTemplate.Height = 25;
            dataGridView3.Size = new Size(915, 387);
            dataGridView3.TabIndex = 9;
            // 
            // comboBox5
            // 
            comboBox5.FormattingEnabled = true;
            comboBox5.Location = new Point(933, 337);
            comboBox5.Name = "comboBox5";
            comboBox5.Size = new Size(163, 23);
            comboBox5.TabIndex = 12;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1114, 545);
            Controls.Add(tabControl1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView3).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private Button button1;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private ComboBox comboBox3;
        private ComboBox comboBox4;
        private Label label1;
        private Label label2;
        private DataGridView dataGridView2;
        private Button button2;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private DataGridView dataGridView3;
        private Label label8;
        private Label label7;
        private Label label5;
        private Label label4;
        private Label label3;
        private ComboBox comboBox10;
        private ComboBox comboBox9;
        private ComboBox comboBox8;
        private ComboBox comboBox7;
        private ComboBox comboBox6;
        private Button button3;
        private Label label6;
        private ComboBox comboBox5;
    }
}