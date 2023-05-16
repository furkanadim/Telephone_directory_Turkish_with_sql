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
    public partial class KayitOl : Form
    {
        public KayitOl()
        {
            InitializeComponent();
        }

        private void btnKaydol_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = txtKullaniciAdi.Text.Trim();
            string parola = txtParola.Text.Trim();
            string ad = txtAd.Text.Trim();
            string soyad = txtSoyad.Text.Trim();

            // Gerekli alanların boş olmadığını kontrol edin
            if (string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(parola) ||
                string.IsNullOrEmpty(ad) || string.IsNullOrEmpty(soyad))
            {
                MessageBox.Show("Tüm alanları doldurun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Veritabanına kayıt işlemi
            using (TelefonRehberiEntities db = new TelefonRehberiEntities())
            {
                // Kullanıcı adının kullanılmadığından emin olunmalı
                var existingUser = db.Kullanici.FirstOrDefault(x => x.Kullaniciadi == kullaniciAdi);
                if (existingUser != null)
                {
                    MessageBox.Show("Bu kullanıcı adı zaten kullanılıyor. Lütfen farklı bir kullanıcı adı seçin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Yeni kullanıcı oluşturulmalı
                Kullanici yeniKullanici = new Kullanici()
                {
                    Kullaniciadi = kullaniciAdi,
                    Parola = parola,
                    Ad = ad,
                    Soyad = soyad,
                    Durum = true // Varsayılan olarak kullanıcıyı aktif olarak ayarlayabilirsiniz
                };

                db.Kullanici.Add(yeniKullanici);
                db.SaveChanges();

                MessageBox.Show("Kayıt işlemi başarılı! Yeni kullanıcı oluşturuldu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form1 form = new Form1();
                form.Show();
                this.Hide();
                
                
            }
        }
    }
}
