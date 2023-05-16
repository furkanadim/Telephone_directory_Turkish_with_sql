using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _20230515_1_TelefonRehberi
{
    public partial class KisiEkle : Form
    {
        public KisiEkle()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            Kisi kisi = new Kisi();
            kisi.Ad = txtAd.Text.Trim();
            kisi.Soyad = txtSoyad.Text.Trim();
            kisi.Telefon = txtTelefon.Text.Trim();
            kisi.EMail = txtEmail.Text.Trim();
            kisi.KullaniciId = Program.kullanici.KullaniciId;

            try
            {
                TelefonRehberiEntities db = new TelefonRehberiEntities();
                db.Kisi.Add(kisi);
                db.SaveChanges();

                MessageBox.Show(string.Format("{0} {1} kişisi başarı ile kayıt edildi.", kisi.Ad, kisi.Soyad), "Tebrikler", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Kayıt işlemi esnasında bir hata meydana geldi", "Uyarı!", MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
    }
}
