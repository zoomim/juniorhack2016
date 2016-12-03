using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace teslaton
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string stupne_sirka = Form1.sirka.Split(':')[0];
            string stupne_delka = Form1.delka.Split(':')[0];

            string minuty_sirka = Form1.sirka.Split(':')[1].Split(',')[0];
            string minuty_delka = Form1.delka.Split(':')[1].Split(',')[0];

            string vteriny_sirka = Form1.sirka.Split(':')[1].Split(',')[1];
            string vteriny_delka = Form1.delka.Split(':')[1].Split(',')[1];

            minuty_sirka = prevodminut(minuty_sirka);
            minuty_delka = prevodminut(minuty_delka);

            vteriny_sirka = prevodsekund(vteriny_sirka);
            vteriny_delka = prevodsekund(vteriny_delka);

            System.Diagnostics.Process.Start("https://www.google.cz/maps/place/" + stupne_sirka + "%C2%B" + minuty_sirka + "'" + vteriny_sirka + "%22N+" + stupne_delka + "%C2%B" + minuty_delka + "'" + vteriny_delka + "%22E");

            this.Close();

           // webBrowser1.Navigate("https://www.google.cz/maps/place/" + stupne_sirka + "%C2%B" + minuty_sirka + "'" + vteriny_sirka + "%22N+" + stupne_delka + "%C2%B" + minuty_delka + "'" + vteriny_delka + "%22E");
        }

        private string prevodminut(string vstup)
        {

            if (int.Parse(vstup) < 10)
            {
                vstup = "00" + vstup;
            }
            if (int.Parse(vstup) < 100)
            {
                vstup = "0" + vstup;
            }
            return vstup;
        }

        private string prevodsekund(string vstup)
        {
            if (int.Parse(vstup.Split('.')[0]) < 10)
            {
                vstup = "0" + vstup;
            }
            return vstup;
        }
    }
}
