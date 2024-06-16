using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Praktika
{
    public partial class Form2 : Form
    {
        private string connectionString = @"Data Source=localhost;Initial Catalog=praktika;User ID=sa;Password=sa";

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            PopulateComboBoxes();
        }

        private void PopulateComboBoxes()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            {
                try
                {
                    connection.Open();

                    // Populate comboBox1 (Kategorija)
                    string queryKategorija = "SELECT ID, pavadinimas FROM klasifikacija";
                    SqlCommand cmd = new SqlCommand(queryKategorija, connection);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable kategorijaTable = new DataTable();
                        kategorijaTable.Load(reader);
                        comboBox1.DataSource = kategorijaTable;
                        comboBox1.DisplayMember = "pavadinimas";
                        comboBox1.ValueMember = "ID";
                    }

                    // Populate comboBox2 (Saltinis)
                    string querySaltinis = "SELECT ID, pinigu_saltinis FROM pinig_salt";
                    SqlCommand cmda = new SqlCommand(querySaltinis, connection);
                    using (SqlDataReader readera = cmda.ExecuteReader())
                    {
                        DataTable saltinisTable = new DataTable();
                        saltinisTable.Load(readera);
                        comboBox2.DataSource = saltinisTable;
                        comboBox2.DisplayMember = "pinigu_saltinis";
                        comboBox2.ValueMember = "ID";
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Nepavyko užkrauti duomenų: " + ex.Message, "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Ar norite uždaryti?", "Įspėjimas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // parodo duomenu bazes info
            SqlConnection connection = new SqlConnection(connectionString);
            {
                try
                {
                    connection.Open();
                    string query = "SELECT informacija2.ID, informacija2.data, " +
                         "informacija2.suma, dbo.tipas.tipas, " +
                         "klasifikacija.pavadinimas, " +
                         "pinig_salt.pinigu_saltinis, informacija2.pastaba " +
                         "FROM informacija2 " +
                         "INNER JOIN klasifikacija ON informacija2.klasif_ID = klasifikacija.id " +
                         "INNER JOIN pinig_salt ON informacija2.pinig_salt_id = pinig_salt.id " +
                         "INNER JOIN dbo.tipas ON klasifikacija.tipas_ID = dbo.tipas.id;";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable table = new DataTable();
                    table.Load(reader);
                    dataGridView1.DataSource = table;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Neleido: " + ex.Message, "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pateikti_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue == null || comboBox2.SelectedValue == null)
            {
                MessageBox.Show("Prašau pasirinkti tinkamą šaltinį ir kategorija", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!decimal.TryParse(textBox1.Text, out decimal suma))
            {
                MessageBox.Show("Netinkamas formatas.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //prisijungimo stringas
            SqlConnection connection = new SqlConnection(connectionString);
            {
                try
                {
                    connection.Open();
                    //insertinami duomenys į lentelę
                    string query = "INSERT INTO informacija2 (data, suma, pastaba, klasif_ID, pinig_salt_ID) " +
                                   "VALUES (@Data, @Suma, @Pastaba, @Klasif_ID, @Saltinis_ID)";

                    SqlCommand cmd = new SqlCommand(query, connection);

                    // parametrai
                    cmd.Parameters.AddWithValue("@Data", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@Suma", suma);
                    cmd.Parameters.AddWithValue("@Pastaba", richTextBox1.Text);
                    cmd.Parameters.AddWithValue("@Klasif_ID", (int)comboBox1.SelectedValue);
                    cmd.Parameters.AddWithValue("@Saltinis_ID", (int)comboBox2.SelectedValue);

                    // Vykdoma komanda
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Viskas įkelta sėkmingai", "Patvirtinimas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Nepavyko įkelti įrašus: " + ex.Message, "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        //funckija, kad duomenys būtų įkelti į datagridview
        private void LoadDataIntoDataGridView()
        {
            //prisijungimo stringas prie duomenu bazes
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //selectas paimti duomenims is duomenu bazes ir juos ikelti i datagridview
                string query = "SELECT informacija2.ID,informacija2.data, informacija2.suma, dbo.tipas.tipas, klasifikacija.pavadinimas, pinig_salt.pinigu_saltinis, informacija2.pastaba " +
                               "FROM informacija2 " +
                               "INNER JOIN klasifikacija ON informacija2.klasif_ID = klasifikacija.id " +
                               "INNER JOIN pinig_salt ON informacija2.pinig_salt_id = pinig_salt.id " +
                               "INNER JOIN dbo.tipas ON klasifikacija.tipas_ID = dbo.tipas.id;";

                SqlCommand cmd = new SqlCommand(query, connection);

                try
                    //lentelės užpildymas
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Klaida: " + ex.Message);
                }
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Patikrina ar yra pasirinkta kokia tai nors eilutė datagridview lange
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the ID of the selected row
                int selectedId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID"].Value); // Replace with actual column name

                // Validate input
                if (comboBox1.SelectedValue == null || comboBox2.SelectedValue == null)
                {
                    MessageBox.Show("Prašome pasirinkti tinkamą šaltinį ir kategoriją", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!decimal.TryParse(textBox1.Text, out decimal suma))
                {
                    MessageBox.Show("Netinkamas sumos formatas.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Create connection and command
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        // SQL update command
                        string query = "UPDATE informacija2 SET data = @Data, suma = @Suma, pastaba = @Pastaba, klasif_ID = @Klasif_ID, pinig_salt_ID = @Saltinis_ID " +
                                       "WHERE ID = @Id";

                        SqlCommand cmd = new SqlCommand(query, connection);

                        // Parameters
                        cmd.Parameters.AddWithValue("@Data", dateTimePicker1.Value);
                        cmd.Parameters.AddWithValue("@Suma", suma);
                        cmd.Parameters.AddWithValue("@Pastaba", richTextBox1.Text);
                        cmd.Parameters.AddWithValue("@Klasif_ID", (int)comboBox1.SelectedValue);
                        cmd.Parameters.AddWithValue("@Saltinis_ID", (int)comboBox2.SelectedValue);
                        cmd.Parameters.AddWithValue("@Id", selectedId);

                        // Execute command
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Įrašas atnaujintas sėkmingai", "Patvirtinimas", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Clear form fields after successful update
                            dateTimePicker1.Value = DateTime.Now; // or another default value
                            textBox1.Text = string.Empty;
                            richTextBox1.Text = string.Empty;

                            // Refresh DataGridView
                            LoadDataIntoDataGridView();
                        }
                        else
                        {
                            MessageBox.Show("Nepavyko atnaujinti įrašo", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Klaida atnaujinant įrašą: " + ex.Message, "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                // Show a message if no row is selected
                MessageBox.Show("Pasirinkite įrašą, kurį norite atnaujinti.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Patikrina ar yra pazymetas norimas laukas datagridview
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Gaunamas id pasirinkto lauko
                int selectedId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID"].Value); 
               
                    // Create the DELETE query with proper syntax
                string query = "DELETE FROM informacija2 WHERE ID = @ID";

                // prisijungimas prie duomenų bazės
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID", selectedId);
                  
                 

                    try
                    { 
                        connection.Open();

                        // Atliekama trynimo komanda
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Įrašas sėkmingai ištrintas!");
                            // Atnaujina DataGridView langa po ištrynimo
                            LoadDataIntoDataGridView();
                        }
                        else
                        {
                            MessageBox.Show("Nepavyko ištrinti įrašo.");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Show an error message if there's an exception
                        MessageBox.Show("Klaida: " + ex.Message);
                    }
                }
            }
            else
            {
                // Show a message if no row is selected
                MessageBox.Show("Pasirinkite įrašą, kurį norite ištrinti.");
            }
        }

    }
}