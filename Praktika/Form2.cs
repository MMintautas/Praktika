using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Praktika
{
    public partial class Form2 : Form
    {
        //"Data Source=localhost;Initial Catalog=praktika;User ID=sa;Password=sa";
        private string connectionString = "Server=DESKTOP-TVMFB1P\\SQLEXPRESS;Database=praktika;Integrated Security=True;";
        private int daugiklis = 1; // Default skaicius 1 del "Pajamos" lenteles
        private decimal realusisLikutis = 0;
        private decimal totalPajamos = 0;
        private decimal totalIslaidos = 0;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            PopulateComboBoxes("1"); // Default i pajamas (ID = 1)
            LoadDataIntoListBox();
        }
        //I listbox lauka ikraunamas yra ID, TIPAS ir Daugiklis 
        //Vartotojas rinksis is 2 tipu pajamos ir islaidos
        private void LoadDataIntoListBox()
        {
            string duomtipas = "SELECT ID, TIPAS, DAUGIKLIS FROM dbo.tipas";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(duomtipas, connection))
            {
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable kategorijatable = new DataTable();
                        kategorijatable.Load(reader);
                        listBox1.DataSource = kategorijatable;
                        listBox1.DisplayMember = "tipas";
                        listBox1.ValueMember = "ID";
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Nepavyko užkrauti duomenų: " + ex.Message, "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void PopulateComboBoxes(string selectedType)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // uzpildomas comboBox1 (Klasifikacija)
                    string queryKategorija = "SELECT ID, pavadinimas FROM klasifikacija WHERE tipas_ID = @TipasID";
                    SqlCommand cmd = new SqlCommand(queryKategorija, connection);
                    cmd.Parameters.AddWithValue("@TipasID", selectedType);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable kategorijaTable = new DataTable();
                        kategorijaTable.Load(reader);
                        comboBox1.DataSource = kategorijaTable;
                        comboBox1.DisplayMember = "pavadinimas";
                        comboBox1.ValueMember = "ID";
                    }

                    // uzpildomas comboBox2 (pinigu_saltinis)
                    string querySaltinis = "SELECT ID, pinigu_saltinis FROM pinig_salt";
                    SqlCommand cmdSaltinis = new SqlCommand(querySaltinis, connection);

                    using (SqlDataReader readerSaltinis = cmdSaltinis.ExecuteReader())
                    {
                        DataTable saltinisTable = new DataTable();
                        saltinisTable.Load(readerSaltinis);
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
        //formos uzdarymo mygtukas
        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Ar norite uždaryti?", "Įspėjimas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                this.Close();
            }
        }
        //Ikrauna duomenys paspaudus rodyti mygtuka i lentelę
        private void button5_Click(object sender, EventArgs e)
        {
            LoadDataIntoDataGridView();
        }
        //Informacijos pateikimas/išsiuntimas į duomenu bazę
        private void pateikti_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue == null || comboBox2.SelectedValue == null)
            {
                MessageBox.Show("Prašau pasirinkti tinkamą šaltinį ir kategoriją", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!decimal.TryParse(textBox1.Text, out decimal suma))
            {
                MessageBox.Show("Netinkamas formatas.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            suma *= daugiklis; // priklausomai koks tipas parinktas bus pridedamas daugiklis jeigu pajamos dauginama is 1 jeigu islaidos is -1

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO informacija2 (data, suma, pastaba, klasif_ID, pinig_salt_ID) " +
                                   "VALUES (@Data, @Suma, @Pastaba, @Klasif_ID, @Saltinis_ID)";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Data", dateTimePicker1.Value);
                        cmd.Parameters.AddWithValue("@Suma", suma);
                        cmd.Parameters.AddWithValue("@Pastaba", richTextBox1.Text);
                        cmd.Parameters.AddWithValue("@Klasif_ID", (int)comboBox1.SelectedValue);
                        cmd.Parameters.AddWithValue("@Saltinis_ID", (int)comboBox2.SelectedValue);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Viskas įkelta sėkmingai", "Patvirtinimas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataIntoDataGridView(); // atnaujins datagridview langa po duomenu ikelimo
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Nepavyko įkelti įrašus: " + ex.Message, "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Funkcija kuri ikelia duomenys i datagridview lentele
        private void LoadDataIntoDataGridView()
        {
            string query = "SELECT informacija2.ID, informacija2.data, informacija2.suma, dbo.tipas.tipas, klasifikacija.pavadinimas, pinig_salt.pinigu_saltinis, informacija2.pastaba " +
                    "FROM informacija2 " +
                    "INNER JOIN klasifikacija ON informacija2.klasif_ID = klasifikacija.id " +
                    "INNER JOIN pinig_salt ON informacija2.pinig_salt_id = pinig_salt.id " +
                    "INNER JOIN dbo.tipas ON klasifikacija.tipas_ID = dbo.tipas.id;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;

                    // Suskaiciuojama kiek is viso buvo gauta pajamu ir kokio dydzio islaidos
                    totalPajamos = 0;
                    totalIslaidos = 0;
                    foreach (DataRow row in table.Rows)
                    {
                        decimal suma = Convert.ToDecimal(row["suma"]);
                        if (suma > 0)
                        {
                            totalPajamos += suma;
                        }
                        else
                        {
                            totalIslaidos += Math.Abs(suma);
                        }
                    }

                    // suskaiciuojamas realusis likutis
                    realusisLikutis = totalPajamos - totalIslaidos;

                    // atvaizduojamas realusis likutis ir total pajamu ir islaidu
                    textBox2.Text = realusisLikutis.ToString();
                    textBox3.Text = totalPajamos.ToString();
                    textBox4.Text = totalIslaidos.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Klaida: " + ex.Message);
            }
        }
        //Duomenu atnaujinimo funkcija/mygtukas
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID"].Value);

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

                suma *= daugiklis;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string query = "UPDATE informacija2 SET data = @Data, suma = @Suma, pastaba = @Pastaba, klasif_ID = @Klasif_ID, pinig_salt_ID = @Saltinis_ID " +
                                       "WHERE ID = @Id";
                        SqlCommand cmd = new SqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@Data", dateTimePicker1.Value);
                        cmd.Parameters.AddWithValue("@Suma", suma);
                        cmd.Parameters.AddWithValue("@Pastaba", richTextBox1.Text);
                        cmd.Parameters.AddWithValue("@Klasif_ID", (int)comboBox1.SelectedValue);
                        cmd.Parameters.AddWithValue("@Saltinis_ID", (int)comboBox2.SelectedValue);
                        cmd.Parameters.AddWithValue("@Id", selectedId);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Įrašas atnaujintas sėkmingai", "Patvirtinimas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dateTimePicker1.Value = DateTime.Now;
                            textBox1.Text = string.Empty;
                            richTextBox1.Text = string.Empty;
                            LoadDataIntoDataGridView(); // Refresh DataGridView after update
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
                MessageBox.Show("Pasirinkite įrašą, kurį norite atnaujinti.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //pasirinktu duomenų ištrynimas
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID"].Value);

                string query = "DELETE FROM informacija2 WHERE ID = @ID";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID", selectedId);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Įrašas sėkmingai ištrintas!");
                            LoadDataIntoDataGridView(); //atnaujins datagridview po duomenu ikelimo
                        }
                        else
                        {
                            MessageBox.Show("Nepavyko ištrinti įrašo.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Klaida: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Pasirinkite įrašą, kurį norite ištrinti.");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                DataRowView selectedRow = listBox1.SelectedItem as DataRowView;
                string selectedType = selectedRow["ID"].ToString();
                daugiklis = Convert.ToInt32(selectedRow["daugiklis"]); // Update daugiklis based on selection
                PopulateComboBoxes(selectedType);
            }
        }
        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            listBox1_SelectedIndexChanged(sender, e);
        }

        // Nenaudojamos funkcijos
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) { }
        private void richTextBox1_TextChanged(object sender, EventArgs e) { }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
