using _20230515_1_TelefonRehberi.Properties;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnParolaGoster_Click(object sender, EventArgs e)
        {
            txtParola.UseSystemPasswordChar = !txtParola.UseSystemPasswordChar;

            btnParolaGoster.Image = (txtParola.UseSystemPasswordChar ? Resources.eye_off : Resources.eye_on);
        }

        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            TelefonRehberiEntities db = new TelefonRehberiEntities();

            var user = db.Kullanici.FirstOrDefault(x => x.Kullaniciadi == txtKullAdi.Text && x.Parola == txtParola.Text && x.Durum);

            if (user != null)
            {
                Program.kullanici = user;

                Liste liste = new Liste();
                liste.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya parola bilgisi hatalı.", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnKayitOl_Click(object sender, EventArgs e)
        {
            TelefonRehberiEntities db = new TelefonRehberiEntities();

            KayitOl kayit = new KayitOl();
            kayit.Show();
            this.Hide();
        }
    }
}
