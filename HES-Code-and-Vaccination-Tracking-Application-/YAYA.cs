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

namespace CMPE341_Project
{
    public partial class YAYA : Form
    {
        public YAYA()
        {
            InitializeComponent();
        }
        static string constring = ("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=Kayit;Integrated Security=True");
        SqlConnection baglan = new SqlConnection(constring);


        public void getir()
        {

            string getir = "Select * From Uyeler";
            SqlCommand komut = new SqlCommand(getir, baglan);
            SqlDataAdapter ad = new SqlDataAdapter(komut);

            DataTable dt = new DataTable();
            ad.Fill(dt);
            dataGridView1.DataSource = dt;
            baglan.Close();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglan.State == ConnectionState.Closed)
                {
                    baglan.Open();
                    string kayit = "Delete from Uyeler where uye_id=@pid";
                    SqlCommand komut = new SqlCommand(kayit, baglan);
                    komut.Parameters.AddWithValue("@pid", textBox5.Text);

                    komut.ExecuteNonQuery();



                    MessageBox.Show("Silme başarılı");

                }

            }

            catch (Exception hata)
            {

                MessageBox.Show("Bir hata var!" + hata.Message);
            }
        }

        private void YAYA_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            getir();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglan.State == ConnectionState.Closed)
                {
                    baglan.Open();
                    string kayit = "insert into Uyeler (ad_soyad,kullanici_adi,sifre,email)values(@adsoyad,@kullaniciadi,@sifre,@email)";
                    SqlCommand komut = new SqlCommand(kayit, baglan);
                    komut.Parameters.AddWithValue("@adsoyad", textBox1.Text);
                    komut.Parameters.AddWithValue("@kullaniciadi", textBox2.Text);
                    komut.Parameters.AddWithValue("@sifre", textBox3.Text);
                    komut.Parameters.AddWithValue("@email", textBox4.Text);


                    komut.ExecuteNonQuery();

                    MessageBox.Show("Kayıt ekleme başarılı");

                }

            }

            catch (Exception hata)
            {

                MessageBox.Show("Bir hata var!" + hata.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglan.State == ConnectionState.Closed)
                {
                    baglan.Open();
                    string kayit = "Update Uyeler Set ad_soyad=@value,kullanici_adi=@value1,sifre=@value2,email=@value3 where uye_id=@pid";
                    SqlCommand komut = new SqlCommand(kayit, baglan);
                    komut.Parameters.AddWithValue("@pid", textBox6.Text);
                    komut.Parameters.AddWithValue("@value", textBox1.Text);
                    komut.Parameters.AddWithValue("@value1", textBox2.Text);
                    komut.Parameters.AddWithValue("@value2", textBox3.Text);
                    komut.Parameters.AddWithValue("@value3", textBox4.Text);
                    komut.ExecuteNonQuery();

                    MessageBox.Show("Güncelleme başarılı");

                }

            }

            catch (Exception hata)
            {

                MessageBox.Show("Bir hata var!" + hata.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int seçilialan = dataGridView1.SelectedCells[0].RowIndex;
            string ad = dataGridView1.Rows[seçilialan].Cells[1].Value.ToString();
            string kullanıcı = dataGridView1.Rows[seçilialan].Cells[2].Value.ToString();
            string sifre = dataGridView1.Rows[seçilialan].Cells[3].Value.ToString();
            string email = dataGridView1.Rows[seçilialan].Cells[4].Value.ToString();
            string id= dataGridView1.Rows[seçilialan].Cells[0].Value.ToString();
            textBox1.Text = ad;
            textBox2.Text = kullanıcı;
            textBox3.Text = sifre;
            textBox4.Text = email;
            textBox6.Text = id;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglan.Open();

              
            if(radioButton1.Checked==true)
            {
            string getir = "Select * from Uyeler where ad_soyad like '%" + textBox7.Text + "%'";
            SqlCommand komut = new SqlCommand(getir, baglan);
            SqlDataAdapter ad = new SqlDataAdapter(komut);

            DataTable dt = new DataTable();
            ad.Fill(dt);
            dataGridView1.DataSource = dt;
            baglan.Close();

            }

            if (radioButton2.Checked == true)
            {
                string getir = "Select * from Uyeler where uye_id like '%" + textBox7.Text + "%'";
                SqlCommand komut = new SqlCommand(getir, baglan);
                SqlDataAdapter ad = new SqlDataAdapter(komut);

                DataTable dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt;
                baglan.Close();

            }


            if (radioButton3.Checked == true)
            {
                string getir = "Select * from Uyeler where kullanici_adi like '%" + textBox7.Text + "%'";
                SqlCommand komut = new SqlCommand(getir, baglan);
                SqlDataAdapter ad = new SqlDataAdapter(komut);

                DataTable dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt;
                baglan.Close();

            }



        }

       
    }
    }

