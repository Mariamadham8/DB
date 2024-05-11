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

namespace data_base_project
{
    public partial class customer2 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-UL6OPPL;Initial Catalog=pet_store;Integrated Security=true");
        SqlDataAdapter sda; // Declare SqlDataAdapter here
        public customer2()
        {
            InitializeComponent();
            display_eymployee();
            
        }

        private void display_eymployee()
        {
            try
            {
                con.Open();
                string quary = "select * from customer";
                sda = new SqlDataAdapter(quary, con); // Initialize SqlDataAdapter here
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        private void clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("invalid information type");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "INSERT INTO customer(cus_name, cus_add,cus_phone) " +
                                 "VALUES(@emp_name, @emp_add,@emp_phone)";
                    SqlCommand cmd = new SqlCommand(query, con);

                    // Add parameters
                    cmd.Parameters.AddWithValue("@emp_name", textBox1.Text);
                    cmd.Parameters.AddWithValue("@emp_add", textBox2.Text);
                    cmd.Parameters.AddWithValue("@emp_phone", textBox3.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("customer added");
                    con.Close();
                    display_eymployee();
                    clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int key = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value?.ToString());
            if (key == 0)
            {
                MessageBox.Show("empty file selected");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "delete from customer  where cus_id =@key";


                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@key", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("customer deleted");
                    con.Close();
                    display_eymployee();
                    clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("invalid information type");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "update customer set cus_name=@emp_name, cus_add=@emp_add , cus_phone=@emp_phone  where cus_id =@key";


                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@emp_name", textBox1.Text);
                    cmd.Parameters.AddWithValue("@emp_add", textBox2.Text);

                    cmd.Parameters.AddWithValue("@emp_phone", textBox3.Text);
                    cmd.Parameters.AddWithValue("@key", Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value?.ToString()));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("customer updated");
                    con.Close();
                    display_eymployee();
                    clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        int key = 0;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    if (dataGridView1.SelectedRows[0].Cells.Count >= 4)
                    {
                        textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value?.ToString();
                        textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value?.ToString();
                        textBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value?.ToString();
                        if (string.IsNullOrEmpty(textBox1.Text))
                        {
                            key = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value?.ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show("Selected row doesn't have enough cells.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void label13_Click(object sender, EventArgs e)
        {
            product2 obj = new product2();
            obj.Show();
            this.Hide();
        }

    }
}
