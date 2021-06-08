using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace cable_records_app
{
    public partial class Form2 : Form
    {
        public Form2()
        {          

            InitializeComponent();

            using (SqlConnection con = new SqlConnection(@"Data Source= .\sqlexpress;Database=cable_db; Integrated Security=SSPI"))
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT pr_date AS Date, pr_order AS 'Order', line AS 'Line', c_type AS 'Type', c_section AS 'Section', c_color AS 'Color', resistance_std AS 'Standard Resistance', " +
                    "actual_resist AS 'Actual Resistance',diameter_std AS 'Standard Diameter', avg_thickness_std AS 'St.AVG Thickness', actual_avg_thick AS 'Actual.AVG Thickness', actual_diameter AS 'Actual Diameter' FROM cable_records", con);

                DataTable dt = new DataTable();

                da.Fill(dt);

                dataGridView1.DataSource = dt;
            }

             void Form2_Load(object sender, EventArgs e)
            {
                
            }

        }

        public void textBox10_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox10_Enter(object sender, EventArgs e)
        {          
            textBoxEnter("  Type", textBox10);
        }

        private void textBox10_Leave(object sender, EventArgs e)
        {          
            textBoxLeave("  Type", textBox10);
        }

        private void textBox11_Enter(object sender, EventArgs e)
        {         
            textBoxEnter("  Section", textBox11);
        }

        private void textBox11_Leave(object sender, EventArgs e)
        {          
            textBoxLeave("  Section", textBox11);
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

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("Type LIKE '{0}%' AND Section LIKE '{1}%'", textBox10.Text, textBox11.Text);
            dataGridView1.Refresh();
        }

        private void insertButton_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source= .\sqlexpress;Database=cable_db; Integrated Security=SSPI"))
            using (SqlCommand cmd = new SqlCommand("Insert_order", con))
            {              
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@date", SqlDbType.Date).Value = dateTextBox.Text;
                cmd.Parameters.Add("@order", SqlDbType.VarChar).Value = orderTextBox.Text;
                cmd.Parameters.Add("@line", SqlDbType.Char).Value = lineTextBox.Text;
                cmd.Parameters.Add("@c_type", SqlDbType.VarChar).Value = typeTextBox.Text;
                cmd.Parameters.Add("@c_section", SqlDbType.VarChar).Value = sectionTextBox.Text;
                cmd.Parameters.Add("@c_color", SqlDbType.VarChar).Value = colorTextBox.Text;
                cmd.Parameters.Add("@actual_resist", SqlDbType.Decimal).Value = resistanceTextBox.Text;
                cmd.Parameters.Add("@actual_diameter", SqlDbType.Decimal).Value = diameterTextBox.Text;
                cmd.Parameters.Add("@actual_avg_thick", SqlDbType.Decimal).Value = thicknessTextBox.Text;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                RefreshGridView();

                dateTextBox.Clear();
                orderTextBox.Clear();
                lineTextBox.Clear();
                typeTextBox.Clear();
                sectionTextBox.Clear();
                colorTextBox.Clear();
                resistanceTextBox.Clear();
                diameterTextBox.Clear();
                thicknessTextBox.Clear();                
            }
        }

        private void RefreshGridView()
        {
            if (dataGridView1.InvokeRequired)
            {
                dataGridView1.Invoke((MethodInvoker)delegate ()
                {
                    RefreshGridView();
                });
            }
            else
                dataGridView1.Refresh();
        }
    }
}
