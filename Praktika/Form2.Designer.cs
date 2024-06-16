namespace Praktika
{
    partial class Form2
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
            dataGridView1 = new DataGridView();
            button1 = new Button();
            button2 = new Button();
            label1 = new Label();
            button4 = new Button();
            textBox1 = new TextBox();
            comboBox1 = new ComboBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            button5 = new Button();
            dateTimePicker1 = new DateTimePicker();
            listBox1 = new ListBox();
            pateikti = new Button();
            comboBox2 = new ComboBox();
            label5 = new Label();
            richTextBox1 = new RichTextBox();
            label6 = new Label();
            button3 = new Button();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(365, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(423, 209);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // button1
            // 
            button1.Location = new Point(446, 227);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 5;
            button1.Text = "Keisti";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(527, 227);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 6;
            button2.Text = "Trinti";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 67);
            label1.Name = "label1";
            label1.Size = new Size(240, 15);
            label1.TabIndex = 9;
            label1.Text = "Pasirinkite kokio tipo duomenys norite įvesti";
            // 
            // button4
            // 
            button4.Location = new Point(652, 403);
            button4.Name = "button4";
            button4.Size = new Size(136, 35);
            button4.TabIndex = 14;
            button4.Text = "Uždaryti";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 228);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(140, 23);
            textBox1.TabIndex = 15;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Atlyginimas", "Pardavimas", "Kuras", "Pramogos", "Maistas", "Vaistai" });
            comboBox1.Location = new Point(12, 140);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(140, 23);
            comboBox1.TabIndex = 16;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 122);
            label2.Name = "label2";
            label2.Size = new Size(123, 15);
            label2.TabIndex = 17;
            label2.Text = "Duomenų klasifikacija";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 210);
            label3.Name = "label3";
            label3.Size = new Size(78, 15);
            label3.TabIndex = 18;
            label3.Text = "Iveskite sumą";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 12);
            label4.Name = "label4";
            label4.Size = new Size(282, 15);
            label4.TabIndex = 19;
            label4.Text = "Pasirinkite datą, kokio senumo duomenims parodyti";
            // 
            // button5
            // 
            button5.Location = new Point(365, 227);
            button5.Name = "button5";
            button5.Size = new Size(75, 23);
            button5.TabIndex = 20;
            button5.Text = "Rodyti";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(12, 30);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(200, 23);
            dateTimePicker1.TabIndex = 22;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(12, 85);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(115, 34);
            listBox1.TabIndex = 23;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            listBox1.SelectedValueChanged += listBox1_SelectedValueChanged;
            // 
            // pateikti
            // 
            pateikti.Location = new Point(12, 415);
            pateikti.Name = "pateikti";
            pateikti.Size = new Size(75, 23);
            pateikti.TabIndex = 24;
            pateikti.Text = "Pateikti";
            pateikti.UseVisualStyleBackColor = true;
            pateikti.Click += pateikti_Click;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "Asmeninė Banko kortelė", "Grynieji pinigai", "Darbovietės Banko kortelė" });
            comboBox2.Location = new Point(12, 184);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(140, 23);
            comboBox2.TabIndex = 25;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 166);
            label5.Name = "label5";
            label5.Size = new Size(80, 15);
            label5.TabIndex = 26;
            label5.Text = "Pinigų šaltinis";
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(12, 272);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(200, 137);
            richTextBox1.TabIndex = 27;
            richTextBox1.Text = "";
            richTextBox1.TextChanged += richTextBox1_TextChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 254);
            label6.Name = "label6";
            label6.Size = new Size(134, 15);
            label6.TabIndex = 28;
            label6.Text = "Papildomas komentaras";
            // 
            // button3
            // 
            button3.Location = new Point(413, 402);
            button3.Name = "button3";
            button3.Size = new Size(233, 37);
            button3.TabIndex = 29;
            button3.Text = "Rodyti suformuotą ataskaitą diagramoje";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(683, 224);
            label7.Name = "label7";
            label7.Size = new Size(75, 15);
            label7.TabIndex = 30;
            label7.Text = "Realus likutis";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(683, 268);
            label8.Name = "label8";
            label8.Size = new Size(84, 15);
            label8.TabIndex = 31;
            label8.Text = "Pajamos išviso";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(683, 312);
            label9.Name = "label9";
            label9.Size = new Size(78, 15);
            label9.TabIndex = 32;
            label9.Text = "Išlaidos išviso";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(648, 242);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(140, 23);
            textBox2.TabIndex = 33;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(648, 286);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(140, 23);
            textBox3.TabIndex = 34;
            textBox3.TextChanged += textBox3_TextChanged;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(648, 330);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(140, 23);
            textBox4.TabIndex = 35;
            textBox4.TextChanged += textBox4_TextChanged;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(button3);
            Controls.Add(label6);
            Controls.Add(richTextBox1);
            Controls.Add(label5);
            Controls.Add(comboBox2);
            Controls.Add(pateikti);
            Controls.Add(listBox1);
            Controls.Add(dateTimePicker1);
            Controls.Add(button5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(comboBox1);
            Controls.Add(textBox1);
            Controls.Add(button4);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Name = "Form2";
            Text = "Form2";
            Load += Form2_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button button1;
        private Button button2;
        private Label label1;
        private Button button4;
        private TextBox textBox1;
        private ComboBox comboBox1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button button5;
        private DateTimePicker dateTimePicker1;
        private ListBox listBox1;
        private Button pateikti;
        private ComboBox comboBox2;
        private Label label5;
        private RichTextBox richTextBox1;
        private Label label6;
        private Button button3;
        private Label label7;
        private Label label8;
        private Label label9;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
    }
}