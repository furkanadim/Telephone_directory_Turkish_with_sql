using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _20230515_1_TelefonRehberi
{
    public partial class KisiDuzenle : Form
    {
        public Kisi kisi;

        public KisiDuzenle(Kisi kisi)
        {
            InitializeComponent();
            this.kisi = kisi; // Gelen Kisi verisini özelliğe atadık
            VerileriDoldur();
        }
        private void VerileriDoldur()
        {
            // Gelen Kisi verisini kullanarak formdaki kontrolleri dolduruyoruz
            txtAd.Text = kisi.Ad;
            txtSoyad.Text = kisi.Soyad;
            txtTelefon.Text = kisi.Telefon;
            txtEmail.Text = kisi.EMail;
        }
        public string GetAd()
        {
            return txtAd.Text.Trim();
        }

        public string GetSoyad()
        {
            return txtSoyad.Text.Trim();
        }

        public string GetTelefon()
        {
            return txtTelefon.Text.Trim();
        }

        public string GetEmail()
        {
            return txtEmail.Text.Trim();
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            using (TelefonRehberiEntities db = new TelefonRehberiEntities())
            {
                var kisiToUpdate = db.Kisi.FirstOrDefault(x => x.KisiId == kisi.KisiId);

                if (kisiToUpdate != null)
                {
                    kisiToUpdate.Ad = txtAd.Text.Trim();
                    kisiToUpdate.Soyad = txtSoyad.Text.Trim();
                    kisiToUpdate.Telefon = txtTelefon.Text.Trim();
                    kisiToUpdate.EMail = txtEmail.Text.Trim();

                    try
                    {
                        db.SaveChanges();
                        MessageBox.Show("Kişi bilgileri güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Güncelleme işlemi sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Kişi bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
