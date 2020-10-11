using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;


namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        public Form1()
        {

            InitializeComponent();
        }

        public void SelecetedData()
        {
            var con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=perviz;Database=testdatabase");
            con.Open();

            string sql = "SELECT * FROM users Order by id ASC;";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);

            NpgsqlDataReader rdr = cmd.ExecuteReader();

            dataGridView1.ColumnCount = 3;
            dataGridView1.Columns[0].Name = "Id";
            dataGridView1.Columns[1].Name = "name";
            dataGridView1.Columns[2].Name = "age";
            int i = 0;
           
            while (rdr.Read())
            {
                i = dataGridView1.Rows.Add();
                var nameOrdinal = rdr.GetOrdinal("name");
                var name = rdr.GetString(nameOrdinal);

                var idOrdinal = rdr.GetOrdinal("id");
                int id = rdr.GetInt32(idOrdinal);

                var ageOrdinal = rdr.GetOrdinal("age");
                var age = rdr.GetInt32(ageOrdinal);




                dataGridView1.Rows[i].Cells[0].Value = id;
                dataGridView1.Rows[i].Cells[1].Value = name;
                dataGridView1.Rows[i].Cells[2].Value = age;

                i++;




            }
            con.Close();

        }
        public void SelectData2()
        {

            NpgsqlConnection connection = new NpgsqlConnection("Server=localhost;Port=5432; User Id=postgres;Password=perviz; Database=testdatabase;");
            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM users;", connection);



            NpgsqlDataReader dr = command.ExecuteReader();
            if (dr.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
            }

            connection.Close();

        }
        private void button1_Click(object sender, EventArgs e)
        {

            SelecetedData();
           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SelectData2();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            NpgsqlConnection connection = new NpgsqlConnection("Server=localhost;Port=5432; User Id=postgres;Password=perviz; Database=testdatabase;");
            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand("DELETE from users where id=(" + id + ")", connection);
            command.ExecuteNonQuery();
            connection.Close();
            dataGridView1.Rows.Clear(); 
            SelecetedData();


        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }
        int id = 0;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
           numericUpDown1.Value =Convert.ToInt32(dataGridView1.CurrentRow.Cells[2].Value);
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            NpgsqlConnection connection = new NpgsqlConnection("Server=localhost;Port=5432; User Id=postgres;Password=perviz; Database=testdatabase;");
            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand("INSERT into users  (name,age) values('"+textBox1.Text+"','"+numericUpDown1.Value+"')", connection);
            command.ExecuteNonQuery();
            connection.Close();
            dataGridView1.Rows.Clear();

            SelecetedData();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            NpgsqlConnection connection = new NpgsqlConnection("Server=localhost;Port=5432; User Id=postgres;Password=perviz; Database=testdatabase;");
            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand("UPDATE  users set name='"+textBox1.Text.ToString()+"',age='"+numericUpDown1.Value.ToString()+"'  where id=(" + id + ")", connection);
            command.ExecuteNonQuery();
            connection.Close();
            dataGridView1.Rows.Clear();
            SelecetedData();
        }
    }
}
