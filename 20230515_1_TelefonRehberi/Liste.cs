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
    public partial class Liste : Form
    {
        public Liste()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
        }

        private void Liste_Activated(object sender, EventArgs e)
        {
            KisileriListele();
        }

        private void KisileriListele()
        {
            TelefonRehberiEntities db = new TelefonRehberiEntities();

            var kisiler = db.Kisi.Where(x => x.Kullanici.KullaniciId == Program.kullanici.KullaniciId).ToList();
            dataGridView1.DataSource = kisiler;
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            var dialog = MessageBox.Show("Oturumunuzu kapatmak istediğinize emin misiniz?", "Uyarı!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialog == DialogResult.Yes)
            {
                Form1 form = new Form1();
                form.Show();
                this.Hide();
                Program.kullanici = null;
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            KisiEkle ekle = new KisiEkle();

            ekle.ShowDialog();
        }
        
        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int kisiId = Convert.ToInt32(selectedRow.Cells["KisiId"].Value);

                using (TelefonRehberiEntities db = new TelefonRehberiEntities())
                {
                    var kisi = db.Kisi.FirstOrDefault(x => x.KisiId == kisiId);

                    if (kisi != null)
                    {
                        KisiDuzenle duzenleForm = new KisiDuzenle(kisi);
                        DialogResult result = duzenleForm.ShowDialog();

                        if (result == DialogResult.OK)
                        {
                            kisi = db.Kisi.FirstOrDefault(x => x.KisiId == kisiId); // Kişiyi tekrar veritabanından çekiyoruz

                            // Kişinin bilgilerini güncelliyoruz
                            kisi.Ad = duzenleForm.GetAd();
                            kisi.Soyad = duzenleForm.GetSoyad();
                            kisi.Telefon = duzenleForm.GetTelefon();
                            kisi.EMail = duzenleForm.GetEmail();

                            try
                            {
                                db.SaveChanges(); // Değişiklikleri kaydediyoruz
                                KisileriListele();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Güncelleme işlemi sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Kişi bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen düzenlemek istediğiniz kişiyi seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int kisiId = Convert.ToInt32(selectedRow.Cells["KisiId"].Value);

                // Silme işlemi için onay alma
                var dialogResult = MessageBox.Show("Bu kişiyi silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    // Veritabanından kişiyi silme
                    using (TelefonRehberiEntities db = new TelefonRehberiEntities())
                    {
                        var kisi = db.Kisi.FirstOrDefault(x => x.KisiId == kisiId);

                        if (kisi != null)
                        {
                            db.Kisi.Remove(kisi);
                            db.SaveChanges();
                            MessageBox.Show("Kişi silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            KisileriListele();
                        }
                        else
                        {
                            MessageBox.Show("Kişi bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz kişiyi seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
