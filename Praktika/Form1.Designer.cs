namespace Praktika
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
            label1 = new Label();
            textBox1 = new TextBox();
            button4 = new Button();
            textBox2 = new TextBox();
            label4 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(285, 90);
            label1.Name = "label1";
            label1.Size = new Size(101, 15);
            label1.TabIndex = 4;
            label1.Text = "Naudotojo vardas";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(285, 108);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(182, 23);
            textBox1.TabIndex = 5;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // button4
            // 
            button4.Location = new Point(330, 181);
            button4.Name = "button4";
            button4.Size = new Size(96, 23);
            button4.TabIndex = 6;
            button4.Text = "Prisijungti";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(285, 152);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(182, 23);
            textBox2.TabIndex = 13;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(285, 134);
            label4.Name = "label4";
            label4.Size = new Size(66, 15);
            label4.TabIndex = 14;
            label4.Text = "Slaptažodis";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 24F);
            label2.Location = new Point(269, 36);
            label2.Name = "label2";
            label2.Size = new Size(208, 45);
            label2.TabIndex = 15;
            label2.Text = "Prisijungimas";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(label4);
            Controls.Add(textBox2);
            Controls.Add(button4);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private TextBox textBox1;
        private Button button4;
        private TextBox textBox2;
        private Label label4;
        private Label label2;
    }
}
