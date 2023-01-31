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
using Tesseract;
using AForge.Video;
using AForge.Video.DirectShow;
using QRCoder;
using ZXing;

namespace CMPE341_Project
{
    public partial class ARAÇ : Form
    {
        public ARAÇ()
        {
            InitializeComponent();
        }
        static string constring = ("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=Kayit;Integrated Security=True");
        SqlConnection baglan = new SqlConnection(constring);

        public void getir()
        {
            string getir = "Select * From Person";
            SqlCommand komut = new SqlCommand(getir, baglan);
            SqlDataAdapter ad = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            dataGridView1.DataSource = dt;
            baglan.Close();
        }
        public void getirvehicle()
        {
            string getir = "Select * From Vehicle";
            SqlCommand komut = new SqlCommand(getir, baglan);
            SqlDataAdapter ad1 = new SqlDataAdapter(komut);
            DataTable dt1 = new DataTable();
            ad1.Fill(dt1);
            dataGridView2.DataSource = dt1;
            baglan.Close();
        }

        public void getirvaccination()
        {
            string getir = "Select * From Vaccination";
            SqlCommand komut = new SqlCommand(getir, baglan);
            SqlDataAdapter ad2 = new SqlDataAdapter(komut);
            DataTable dt2 = new DataTable();
            ad2.Fill(dt2);
            dataGridView3.DataSource = dt2;
            baglan.Close();
        }
        public void newcar()
        {
            string kayit1 = "insert into Vehicle (PLATE_NUMBER,BRAND,MODEL,COLOR)values(@PLATE_NUMBER,@BRAND,@MODEL,@COLOR)";
            SqlCommand komut1 = new SqlCommand(kayit1, baglan);
            komut1.Parameters.AddWithValue("@BRAND", textBox11.Text);
            komut1.Parameters.AddWithValue("@MODEL", textBox12.Text);
            komut1.Parameters.AddWithValue("@COLOR", textBox13.Text);
            komut1.Parameters.AddWithValue("@PLATE_NUMBER", textBox4.Text);
            komut1.ExecuteNonQuery();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (baglan.State == ConnectionState.Closed)
                {
                    baglan.Open();
                    string kayit = "insert into Person (SSN,NAME,SURNAME,VACCINATION_STATUS,COVID_19_STATUS,PLATE_NUMBER)values(@SSN,@NAME,@SURNAME,@VACCINATION_STATUS,@COVID_19_STATUS,@PLATE_NUMBER)";
                    SqlCommand komut = new SqlCommand(kayit, baglan);
                    komut.Parameters.AddWithValue("@SSN", textBox6.Text);
                    komut.Parameters.AddWithValue("@NAME", textBox1.Text);
                    komut.Parameters.AddWithValue("@SURNAME", textBox2.Text);
                    komut.Parameters.AddWithValue("@VACCINATION_STATUS", textBox8.Text);
                    komut.Parameters.AddWithValue("@COVID_19_STATUS", textBox3.Text);
                    komut.Parameters.AddWithValue("@PLATE_NUMBER", textBox4.Text);
                    komut.ExecuteNonQuery();

                    if(textBox4.Text!="null")
                    {
                    string kayit1 = "insert into Vehicle (PLATE_NUMBER,BRAND,MODEL,COLOR)values(@PLATE_NUMBER,@BRAND,@MODEL,@COLOR)";
                    SqlCommand komut1 = new SqlCommand(kayit1, baglan);
                    komut1.Parameters.AddWithValue("@BRAND", textBox11.Text);
                    komut1.Parameters.AddWithValue("@MODEL", textBox12.Text);
                    komut1.Parameters.AddWithValue("@COLOR", textBox13.Text);
                    komut1.Parameters.AddWithValue("@PLATE_NUMBER", textBox4.Text);
                    komut1.ExecuteNonQuery();
                    }

                    string kayit2 = "insert into Vaccination (VSSN,TYPE,DOSE,SSN)values(@VSSN,@TYPE,@DOSE,@SSN)";
                    SqlCommand komut2 = new SqlCommand(kayit2, baglan);
                    komut2.Parameters.AddWithValue("@VSSN", textBox14.Text);
                    komut2.Parameters.AddWithValue("@TYPE", textBox9.Text);
                    komut2.Parameters.AddWithValue("@DOSE", textBox10.Text);
                    komut2.Parameters.AddWithValue("@SSN", textBox6.Text);
                    komut2.ExecuteNonQuery();

                    MessageBox.Show("Kayıt ekleme başarılı");

                }

            }
            catch (Exception hata)
            {

                MessageBox.Show("Bir hata var!" + hata.Message);
            }

            string metin = textBox4.Text;

            if(metin!="-")
            { 
            QRCodeGenerator qrgenerator = new QRCodeGenerator();
            QRCodeData qrcodedata = qrgenerator.CreateQrCode(metin, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrcodedata);
            Bitmap qrcodeImage = qrCode.GetGraphic(20);
            pictureBox1.Image = qrcodeImage;

            }
            else
            {
                string metin1 = textBox6.Text;
                QRCodeGenerator qrgenerator = new QRCodeGenerator();
                QRCodeData qrcodedata = qrgenerator.CreateQrCode(metin1, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrcodedata);
                Bitmap qrcodeImage = qrCode.GetGraphic(20);
                pictureBox1.Image = qrcodeImage;
            }
           
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (baglan.State == ConnectionState.Closed)
                {
                  
                    baglan.Open();
                    string kayit = "Update Person Set SSN=@SSN,NAME=@NAME,SURNAME=@SURNAME,VACCINATION_STATUS=@VACCINATION_STATUS,COVID_19_STATUS=@COVID_19_STATUS,PLATE_NUMBER=@PLATE_NUMBER where SSN=@SSN";
                    SqlCommand komut = new SqlCommand(kayit, baglan);
                    komut.Parameters.AddWithValue("@SSN", textBox6.Text);
                    komut.Parameters.AddWithValue("@NAME", textBox1.Text);
                    komut.Parameters.AddWithValue("@SURNAME", textBox2.Text);
                    komut.Parameters.AddWithValue("@VACCINATION_STATUS", textBox8.Text);
                    komut.Parameters.AddWithValue("@COVID_19_STATUS", textBox3.Text);
                    komut.Parameters.AddWithValue("@PLATE_NUMBER", textBox4.Text);
                    komut.ExecuteNonQuery();

                        newcar();
                   
                        string kayit1 = "Update Vehicle Set BRAND=@BRAND,MODEL=@MODEL,COLOR=@COLOR,PLATE_NUMBER=@PLATE_NUMBER where PLATE_NUMBER=@PLATE_NUMBER";
                        SqlCommand komut1 = new SqlCommand(kayit1, baglan);
                        komut1.Parameters.AddWithValue("@BRAND", textBox11.Text);
                        komut1.Parameters.AddWithValue("@MODEL", textBox12.Text);
                        komut1.Parameters.AddWithValue("@COLOR", textBox13.Text);
                        komut1.Parameters.AddWithValue("@PLATE_NUMBER", textBox4.Text);
                        komut1.ExecuteNonQuery();
                    
                    string kayit2 = "Update Vaccination Set VSSN=@VSSN,TYPE=@TYPE,DOSE=@DOSE,SSN=@SSN where SSN=@SSN";
                    SqlCommand komut2 = new SqlCommand(kayit2, baglan);
                    komut2.Parameters.AddWithValue("@VSSN", textBox14.Text);
                    komut2.Parameters.AddWithValue("@TYPE", textBox9.Text);
                    komut2.Parameters.AddWithValue("@DOSE", textBox10.Text);
                    komut2.Parameters.AddWithValue("@SSN", textBox6.Text);
                    komut2.ExecuteNonQuery();

                    MessageBox.Show("Güncelleme başarılı");
                }

            }

            catch (Exception hata)
            {
                MessageBox.Show("Bir hata var!" + hata.Message);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (baglan.State == ConnectionState.Closed)
                {
                    baglan.Open();
                    string kayit = "Delete from Person where SSN=@SSN";
                    SqlCommand komut = new SqlCommand(kayit, baglan);
                    komut.Parameters.AddWithValue("@SSN", textBox6.Text);
                    komut.ExecuteNonQuery();

                    string kayit1 = "Delete from Vaccination where VSSN=@VSSN";
                    SqlCommand komut1 = new SqlCommand(kayit1, baglan);
                    komut1.Parameters.AddWithValue("@VSSN", textBox14.Text);
                    komut1.ExecuteNonQuery();

                    string kayit2 = "Delete from Vehicle where PLATE_NUMBER=@PLATE_NUMBER";
                    SqlCommand komut2 = new SqlCommand(kayit2, baglan);
                    komut2.Parameters.AddWithValue("@PLATE_NUMBER", textBox4.Text);
                    komut2.ExecuteNonQuery();

                    MessageBox.Show("Silme başarılı");

                }

            }

            catch (Exception hata)
            {
                MessageBox.Show("Bir hata var!" + hata.Message);
            }
        }
    
        public void Search()
        {
            string getir = "Select * from Person where PLATE_NUMBER like '%" + textBox5.Text + "%'";

            string getir1 = "Select * from Person where SSN like '%" + textBox7.Text + "%'";

            if(textBox5.Text!=null)
            {
                SqlCommand komut = new SqlCommand(getir, baglan);
                SqlDataAdapter ad = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dataGridView2.DataSource = dt;
                dataGridView2.Rows[0].Selected = true;
                Cellclick1();
            }
            if (textBox7.Text != null)
            {
                SqlCommand komut = new SqlCommand(getir1, baglan);
                SqlDataAdapter ad = new SqlDataAdapter(komut);
                DataTable dt1 = new DataTable();
                ad.Fill(dt1);
                dataGridView1.DataSource = dt1;
                dataGridView1.Rows[0].Selected = true;
                Cellclick();
            }
        }
        private void button5_Click_1(object sender, EventArgs e)
        {
            getir();
            getirvaccination();
            getirvehicle();
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int seçilialan = dataGridView1.SelectedCells[0].RowIndex;
            string ad = dataGridView1.Rows[seçilialan].Cells[1].Value.ToString();
            string soyad = dataGridView1.Rows[seçilialan].Cells[2].Value.ToString();
            string vaccination = dataGridView1.Rows[seçilialan].Cells[3].Value.ToString();
            string covid19 = dataGridView1.Rows[seçilialan].Cells[4].Value.ToString();
            string plate = dataGridView1.Rows[seçilialan].Cells[5].Value.ToString();
            string id = dataGridView1.Rows[seçilialan].Cells[0].Value.ToString();
            textBox1.Text = ad;
            textBox2.Text = soyad;
            textBox8.Text = vaccination;
            textBox3.Text = covid19;
            textBox4.Text = plate;
            textBox6.Text = id;
            
            cellfillvaccination();
            
            if(plate != "null")
            {
              cellfillvehicle();
            }
            if (textBox3.Text == "negative")
            {
                label19.BackColor = Color.Green;
            }
            else
            {
                label19.BackColor = Color.Red;
            }

        }
        public void cellfillvaccination()
        {
            string getir = "Select * from Vaccination where SSN like '%" + textBox6.Text + "%'";
            SqlCommand komut = new SqlCommand(getir, baglan);
            SqlDataAdapter ad = new SqlDataAdapter(komut);

            DataTable dt = new DataTable();
            ad.Fill(dt);
            dataGridView4.DataSource = dt;
            string VSSN = dataGridView4.Rows[0].Cells[0].Value.ToString();
            string type = dataGridView4.Rows[0].Cells[1].Value.ToString();
            string dose = dataGridView4.Rows[0].Cells[2].Value.ToString();
            textBox14.Text = VSSN;
            textBox9.Text = type;
            textBox10.Text = dose;

        }
        public void cellfillvehicle()
        {
            string getir = "Select * from Vehicle where PLATE_NUMBER like '%" + textBox4.Text + "%'";
            SqlCommand komut = new SqlCommand(getir, baglan);
            SqlDataAdapter ad = new SqlDataAdapter(komut);

            DataTable dt = new DataTable();
            ad.Fill(dt);
            dataGridView5.DataSource = dt;
            string brand = dataGridView5.Rows[0].Cells[1].Value.ToString();
            string model = dataGridView5.Rows[0].Cells[2].Value.ToString();
            string color = dataGridView5.Rows[0].Cells[3].Value.ToString();
           
            if(textBox4.Text==null)
            {
                textBox11.Text = null;
                textBox12.Text = null;
                textBox13.Text = null;
            }
            else
            {
                textBox11.Text = brand;
                textBox12.Text = model;
                textBox13.Text = color;
            }
        }

        public void Cellclick()
        {
            int seçilialan = dataGridView1.SelectedCells[0].RowIndex;
            
            if(dataGridView1.Rows[seçilialan].Cells[1].Value!=null)
            {   
            string ad = dataGridView1.Rows[seçilialan].Cells[1].Value.ToString();
            string soyad = dataGridView1.Rows[seçilialan].Cells[2].Value.ToString();
            string vaccination = dataGridView1.Rows[seçilialan].Cells[3].Value.ToString();
            string covid19 = dataGridView1.Rows[seçilialan].Cells[4].Value.ToString();
            string plate = dataGridView1.Rows[seçilialan].Cells[5].Value.ToString();
            string id = dataGridView1.Rows[seçilialan].Cells[0].Value.ToString();
            textBox1.Text = ad;
            textBox2.Text = soyad;
            textBox8.Text = vaccination;
            textBox3.Text = covid19;
            textBox4.Text = plate;
            textBox6.Text = id;

            cellfillvaccination();

            if (plate != "null")
            {
                cellfillvehicle();
            }

            if (textBox3.Text=="negative")
            {
                label19.BackColor = Color.Green;
            }
            else
            {
                label19.BackColor = Color.Red;
            }

            }
        }
        public void Cellclick1()
        {
            int seçilialan = dataGridView2.SelectedCells[0].RowIndex;

            if (dataGridView2.Rows[seçilialan].Cells[1].Value != null)
            {
                string ad = dataGridView2.Rows[seçilialan].Cells[1].Value.ToString();
                string soyad = dataGridView2.Rows[seçilialan].Cells[2].Value.ToString();
                string vaccination = dataGridView2.Rows[seçilialan].Cells[3].Value.ToString();
                string covid19 = dataGridView2.Rows[seçilialan].Cells[4].Value.ToString();
                string plate = dataGridView2.Rows[seçilialan].Cells[5].Value.ToString();
                string id = dataGridView2.Rows[seçilialan].Cells[0].Value.ToString();
                textBox1.Text = ad;
                textBox2.Text = soyad;
                textBox8.Text = vaccination;
                textBox3.Text = covid19;
                textBox4.Text = plate;
                textBox6.Text = id;

                cellfillvaccination();

                if (plate != "null")
                {
                    cellfillvehicle();
                }

                if (textBox3.Text == "negative")
                {
                    label19.BackColor = Color.Green;
                }
                else
                {
                    label19.BackColor = Color.Red;
                }
            }
              
        }

        FilterInfoCollection fico;
        VideoCaptureDevice vcd;

        private void ARAÇ_Load(object sender, EventArgs e)
        {
            fico = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo f in fico)
            {
                comboBox1.Items.Add(f.Name);
                comboBox1.SelectedIndex = 0;
            }

            getir();
            getirvaccination();
            getirvehicle();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            vcd = new VideoCaptureDevice(fico[comboBox1.SelectedIndex].MonikerString);
            vcd.NewFrame += Vcd_NewFrame;
            vcd.Start();
            timer1.Start();
            timer2.Start();
        }

        private void Vcd_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox2.Image = (Bitmap)eventArgs.Frame.Clone();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if(pictureBox2.Image!=null)
            { BarcodeReader brd = new BarcodeReader();
                Result sonuc = brd.Decode((Bitmap)pictureBox2.Image);
            
                if(sonuc!=null)
                {
                    textBox5.Text = sonuc.ToString();
                    Search();
                  
                }
               
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
            SaveFileDialog s = new SaveFileDialog();
            s.Filter = "(*.jpg)|*.jpg";
            DialogResult dr = s.ShowDialog();
            if (dr == DialogResult.OK)
            {
                pictureBox1.Image.Save("qr");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {   
            vcd.Stop();
            timer1.Stop();
            timer2.Stop();
            pictureBox2.Image = null;
        }

    
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null)
            {
                BarcodeReader brd = new BarcodeReader();
                Result sonuc = brd.Decode((Bitmap)pictureBox2.Image);
                
                    if (sonuc != null)
                    {
                        textBox7.Text = sonuc.ToString();
                        Search();
                      
                    }
              
            }
        }
    }
}

