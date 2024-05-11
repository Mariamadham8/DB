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
using System.Linq.Expressions;
using System.ComponentModel.Design;
namespace data_base_project
{
    public partial class employee : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-UL6OPPL;Initial Catalog=pet_store;Integrated Security=true");
        SqlDataAdapter sda; // Declare SqlDataAdapter here

        public employee()
        {
            InitializeComponent();
            display_eymployee();
        }

        private void display_eymployee()
        {
            try
            {
                con.Open();
                string quary = "select * from employee";
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
            textBox4.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("invalid information type");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "INSERT INTO employee(emp_name, emp_add, emp_dob,emp_phone, emp_pass) " +
                                 "VALUES(@emp_name, @emp_add, @emp_dob,@emp_phone ,@emp_pass)";
                    SqlCommand cmd = new SqlCommand(query, con);

                    // Add parameters
                    cmd.Parameters.AddWithValue("@emp_name", textBox1.Text);
                    cmd.Parameters.AddWithValue("@emp_add", textBox2.Text);
                    cmd.Parameters.AddWithValue("@emp_dob", dateTimePicker1.Value.Date);
                    cmd.Parameters.AddWithValue("@emp_pass", textBox3.Text);
                    cmd.Parameters.AddWithValue("@emp_phone", textBox4.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("employee added");
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
                    if (dataGridView1.SelectedRows[0].Cells.Count >= 6)
                    {
                        textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value?.ToString();
                        textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value?.ToString();
                        textBox3.Text = dataGridView1.SelectedRows[0].Cells[4].Value?.ToString();
                        textBox4.Text = dataGridView1.SelectedRows[0].Cells[5].Value?.ToString();
                        dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[3].Value);
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

        private void button3_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("invalid information type");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "update employee set emp_name=@emp_name, emp_add=@emp_add , emp_dob=@emp_dob , emp_phone=@emp_phone , emp_pass=@emp_pass  where emp_id =@key";
                      
                                 
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@emp_name", textBox1.Text);
                    cmd.Parameters.AddWithValue("@emp_add", textBox2.Text);
                    cmd.Parameters.AddWithValue("@emp_dob", dateTimePicker1.Value.Date);
                    cmd.Parameters.AddWithValue("@emp_pass", textBox3.Text);
                    cmd.Parameters.AddWithValue("@emp_phone", textBox4.Text);
                    cmd.Parameters.AddWithValue("@key", Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value?.ToString()));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("employee updated");
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
            if (key ==0)
            {
                MessageBox.Show("empty file selected");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "delete from employee  where emp_id =@key";


                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@key",key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("employee deleted");
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

        private void label13_Click(object sender, EventArgs e)
        {
            product2 obj = new product2();
            obj.Show();
            this.Hide();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            customer2 obj = new customer2();
            obj.Show();
            this.Hide();
        }
    }

}
