using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
namespace Praktika
{
    public partial class Form1 : Form
    {
        //prisijungimo stringas
        private string connectionString = @"Data Source=localhost;Initial Catalog=praktika;User ID=sa;Password=sa";

        public Form1()
        {
            InitializeComponent();
        }
        //funkcija prisijungti prie programos
        private void button4_Click(object sender, EventArgs e)
        {
            //prisijungimo stringa paima
            SqlConnection con = new SqlConnection(connectionString);
            {
                //imami duomenys ið duomenø bazës
                string query = "SELECT COUNT(1) FROM prisijungimas WHERE naudotojo_vardas=@naudotojo_vardas AND slaptazodis=@slaptazodis";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@naudotojo_vardas", textBox1.Text);
                cmd.Parameters.AddWithValue("@slaptazodis", textBox2.Text);
                con.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                //jeigu á textboxus ávesti duomenys sutampa tada prisijungiama prie pagrindines formos
                if (count == 1)
                {
                    // Paslëpti prisijungimo formà
                    this.Hide();
                    // Rodyti pagrindinæ formà
                    Form2 mainForm = new Form2();
                    mainForm.Show();
                }
                else
                {
                    MessageBox.Show("Ávestas neteisingas naudotojo vardas arba slaptaþodis.", "Áspëjimas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //pakeièia, kad raðant kaþkà texbox2 lauke bus atvaizduojama * simboliu
            textBox2.PasswordChar = '*';
        }

       
    }
}


