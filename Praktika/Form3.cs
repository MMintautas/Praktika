using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace Praktika
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            InitializeControls();
        }
        //datos ir mygtuko valdymas
        private void InitializeControls()
        {
            // Connect event handlers for DateTimePickers and button
            dateTimePicker1.ValueChanged += DateTimePicker_ValueChanged;
            dateTimePicker2.ValueChanged += DateTimePicker_ValueChanged;
            button1.Click += button1_Click;

            // Initial chart load
            InitializeChart(dateTimePicker1.Value, dateTimePicker2.Value);
        }

        private void InitializeChart(DateTime startDate, DateTime endDate)
        {
            // Clear any existing series and legends to avoid duplicates
            chart1.Series.Clear();
            chart1.Legends.Clear();
            chart1.ChartAreas.Clear();

            // Create a new chart area
            ChartArea chartArea = new ChartArea();
            chart1.ChartAreas.Add(chartArea);

            // Create a new series and set its chart type to Pie
            Series series = new Series
            {
                Name = "Diagrama",
                IsVisibleInLegend = true,
                ChartType = SeriesChartType.Pie
            };

            // Add the series to the chart
            chart1.Series.Add(series);

            /*
             prisijungimo stringas ir query paima duomenys is duomenu bazes pagal datas
             */
            //"Data Source=localhost;Initial Catalog=praktika;User ID=sa;Password=sa";
            string connectionString = "Server=DESKTOP-TVMFB1P\\SQLEXPRESS;Database=praktika;Integrated Security=True;";
            string query = "SELECT klasifikacija.pavadinimas AS pavadinimas, SUM(informacija2.suma) AS suma " +
                           "FROM informacija2 " +
                           "INNER JOIN klasifikacija ON informacija2.klasif_ID = klasifikacija.id " +
                           "INNER JOIN pinig_salt ON informacija2.pinig_salt_id = pinig_salt.id " +
                           "INNER JOIN dbo.tipas ON klasifikacija.tipas_ID = dbo.tipas.id " +
                           "WHERE informacija2.data BETWEEN @StartDate AND @EndDate " +
                           "GROUP BY klasifikacija.pavadinimas";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string categoryName = reader["pavadinimas"].ToString(); // paimama klasifikacija iš klasifikacija lentelės
                            decimal value = Convert.ToDecimal(reader["suma"]); // paimama suma iš informacija2 lentelės

                            
                            series.Points.AddXY(categoryName, value);
                        }
                    }
                }

                // diagramos konfiguravimas
                chart1.Titles.Clear(); // ištrinami egzistuojantis pavadinimai prieš kuriant nauja
                chart1.Titles.Add("Duomenys atvaizduoti diagramoje");
                Legend legend = new Legend("Legend1")
                {
                    Docking = Docking.Right
                };
                chart1.Legends.Add(legend);

                // Atnaujinama diagrama
                chart1.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Klaida bandant gauti duomenys: {ex.Message}");
            }
        }

        private void DateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            DateTime startDate = dateTimePicker1.Value;
            DateTime endDate = dateTimePicker2.Value;
            InitializeChart(startDate, endDate);
        }
        //mygtukui priskirta PDF generavimo funkcija
        private void button1_Click(object sender, EventArgs e)
        {
            GeneratePDF();
        }
        //PDF generavimo funkcija
        private void GeneratePDF()
        {
            string filePath = "chart.pdf";

            try
            {
                using (PdfWriter writer = new PdfWriter(filePath))
                using (PdfDocument pdf = new PdfDocument(writer))
                using (Document document = new Document(pdf))
                {
                    Paragraph title = new Paragraph("Duomenys paimti iš duomenų bazės");
                    document.Add(title);
                    document.Add(new Paragraph("\n"));

                    Table table = new Table(new float[] { 1, 1, 1, 1, 1, 1 });
                    table.AddCell(new Cell().Add(new Paragraph("Data")));
                    table.AddCell(new Cell().Add(new Paragraph("Suma")));
                    table.AddCell(new Cell().Add(new Paragraph("Tipas")));
                    table.AddCell(new Cell().Add(new Paragraph("Pavadinimas")));
                    table.AddCell(new Cell().Add(new Paragraph("Pinigų Šaltinis")));
                    table.AddCell(new Cell().Add(new Paragraph("Pastaba")));
                    //"Data Source=localhost;Initial Catalog=praktika;User ID=sa;Password=sa";
                    string connectionString = "Server=DESKTOP-TVMFB1P\\SQLEXPRESS;Database=praktika;Integrated Security=True;";
                    string query = "SELECT informacija2.data, informacija2.suma, dbo.tipas.tipas, klasifikacija.pavadinimas, pinig_salt.pinigu_saltinis, informacija2.pastaba " +
                                   "FROM informacija2 " +
                                   "INNER JOIN klasifikacija ON informacija2.klasif_ID = klasifikacija.id " +
                                   "INNER JOIN pinig_salt ON informacija2.pinig_salt_id = pinig_salt.id " +
                                   "INNER JOIN dbo.tipas ON klasifikacija.tipas_ID = dbo.tipas.id";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                table.AddCell(new Cell().Add(new Paragraph(reader["data"].ToString())));
                                table.AddCell(new Cell().Add(new Paragraph(reader["suma"].ToString())));
                                table.AddCell(new Cell().Add(new Paragraph(reader["tipas"].ToString())));
                                table.AddCell(new Cell().Add(new Paragraph(reader["pavadinimas"].ToString())));
                                table.AddCell(new Cell().Add(new Paragraph(reader["pinigu_saltinis"].ToString())));
                                table.AddCell(new Cell().Add(new Paragraph(reader["pastaba"].ToString())));
                            }
                        }
                    }

                    document.Add(table);
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF Files|*.pdf",
                    Title = "Save PDF File",
                    FileName = "ataskaita.pdf"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.Move(filePath, saveFileDialog.FileName);
                    MessageBox.Show("PDF išsaugotas sėkmingai.");
                }
                else
                {
                    MessageBox.Show("PDF sukurimas nutrauktas.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Klaida sukuriant PDF: {ex.Message}");
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
