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

namespace _16.projeTriggerKullanarakVeriTabanıYedekTablosuOlusturma
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-1DQCP20\SQLEXPRESS;Initial Catalog=19.projeTriggerKullanarakVeriTabanıYedek TablosuOluşturma;Integrated Security=True");
       void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from tblkıtaplar", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            bunifuDataGridView1.DataSource = dt;
        }
        void kitapadet()
        {
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("select * from tblsayac", baglanti);
            SqlDataReader dr = komut2.ExecuteReader();
            while (dr.Read())
            {
                lblKitap.Text = dr[0].ToString();
            }
            baglanti.Close();
        }
        void temizle()
        {
            txtID.Text = " ";
            txtAD.Text = " ";
            txtSayfa.Text = " ";
            txtTur.Text = " ";
            txtYayınevi.Text = " ";
            txtYazar.Text = " ";
            txtAD.Focus();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
            kitapadet();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into tblkıtaplar (AD, YAZAR, SAYFA, YAYINEVI, TUR) VALUES (@P1,@P2,@P3,@P4,@P5)", baglanti);
            komut.Parameters.AddWithValue("@P1", txtAD.Text);
            komut.Parameters.AddWithValue("@P2", txtYazar.Text);
            komut.Parameters.AddWithValue("@P3", txtSayfa.Text);
            komut.Parameters.AddWithValue("@P4", txtYayınevi.Text);
            komut.Parameters.AddWithValue("@P5", txtTur.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            listele(); 
            kitapadet();
            MessageBox.Show("Başarılı bir şekilde eklendi");
            temizle();

        }

        private void bunifuDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = bunifuDataGridView1.SelectedCells[0].RowIndex;
            txtID.Text = bunifuDataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtAD.Text = bunifuDataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtYazar.Text = bunifuDataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtSayfa.Text = bunifuDataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtYayınevi.Text = bunifuDataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtTur.Text = bunifuDataGridView1.Rows[secilen].Cells[5].Value.ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut5 = new SqlCommand("delete from TBLKITAPLAR WHERE ID =@P1", baglanti);
            komut5.Parameters.AddWithValue("@P1", txtID.Text);
            komut5.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap sistemden silindi");
            listele();
            temizle();
            kitapadet();

        }
    }
}
