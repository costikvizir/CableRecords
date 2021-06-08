using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace cable_records_app
{
    public partial class Form3 : Form
    {

        public Form3()
        {
            InitializeComponent();          
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.ActiveControl = label1;
        }

        private void typeTextBox_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void sectionTextBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void typeTextBox_Enter(object sender, EventArgs e)
        {
            textBoxEnter("  Type", typeTextBox);
        }

        private void typeTextBox_Leave(object sender, EventArgs e)
        {
            textBoxLeave("  Type", typeTextBox);
        }

        private void sectionTextBox_Enter(object sender, EventArgs e)
        {
            textBoxEnter("  Section", sectionTextBox);
        }

        private void sectionTextBox_Leave(object sender, EventArgs e)
        {
            textBoxLeave("  Section", sectionTextBox);
        }

        public void textBoxEnter(string input, Control c)
        {
            if (c.Text == input)
            {
                c.Text = "";
                c.ForeColor = Color.Black;
            }
        }

        public void textBoxLeave(string input, Control c)
        {
            if (c.Text == "")
            {
                c.Text = input;
                c.ForeColor = Color.Gray;
            }
        }      

        private void label1_Click(object sender, EventArgs e)
        {
           
        }

        private void showStatisticsButton_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source= .\sqlexpress;Database=cable_db; Integrated Security=SSPI"))
            using (SqlCommand cmd = new SqlCommand("filterProcedure", con))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {             
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = typeTextBox.Text;
                cmd.Parameters.Add("@section", SqlDbType.VarChar).Value = sectionTextBox.Text;
                 
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                chart1.DataBindTable(dt.DefaultView, "pr_date");
                chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart1.Series[1].BorderWidth = 3;
            }
        }
    }
}
