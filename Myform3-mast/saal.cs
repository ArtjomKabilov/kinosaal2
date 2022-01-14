using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyVorm
{
    public partial class saal : Form
    {
        Label message = new Label();
        TableLayoutPanel tlp = new TableLayoutPanel();
        Button btn_tabel;
        static List<Pilet> piletid;
        int k, r;
        static string[] read_kohad;


        public saal(int read, int kohad)
        {
            
            this.tlp.ColumnCount = kohad;
            this.tlp.RowCount = read;
            this.tlp.ColumnStyles.Clear();
            this.tlp.RowStyles.Clear();
            piletid = new List<Pilet> { };
            int i, j;
            read_kohad = Ostetud_piletid();

            for (i = 0; i < read; i++)
            {
                this.tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 50 / read));
            }
            for (i = 0; i < kohad; i++)
            {
                this.tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50 / kohad));
            }
            this.Size = new System.Drawing.Size(kohad * 50, read * 50);
            for (int r = 0; r < read; r++)
            {
                for (int k = 0; k < kohad; k++)
                {
                    btn_tabel = new Button
                    {
                        Text = String.Format($"{r + 1}{k + 1}"),
                        Name = String.Format($"{r+1}{k+1}"),
                        Dock = DockStyle.Fill,
                        BackColor = Color.LightGreen
                    };
                    this.tlp.Controls.Add(btn_tabel, k, r);
                    btn_tabel.MouseClick += Btn_tabel_MouseClick;
                    

                }
            }
            //btn_w = (int)(100 / kohad);
            //btn_h = (int)(100 / read);
            this.tlp.Dock = DockStyle.Fill;
            //this.tlp.Size = new System.Drawing.Size(tlp.ColumnCount*btn_w*3,tlp.RowCount * btn_h*2);
            this.Controls.Add(tlp);
            message.Location = new System.Drawing.Point(10, 10);
            message.Text = "Kas tahad saada e-mailile?";
            this.Controls.Add(message);


        }
        public string[] Ostetud_piletid()
        {
            try
            {
                StreamReader f = new StreamReader(@"..\..\info.txt");
                read_kohad = f.ReadToEnd().Split(';');
                f.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return read_kohad;
        }
        
        public void Saada_piletid(List<Pilet> piletid)
        {
            string text = "Artjom ost on \n";
            foreach (var item in piletid)
            {
                text += "Pilet:\n" + "Rida: " + item.Rida + "Koht: " + item.Koht + "\n";
            }


            string email = "programmeeriminetthk@gmail.com";
            string password = "2.kuursus";
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.Credentials = new NetworkCredential(email, password);
            client.EnableSsl = true;


            try
            {

                MailMessage message = new MailMessage();
                message.To.Add(new MailAddress("programmeeriminetthk@gmail.com"));//kellele saada vaja küsida
                message.From = new MailAddress("programmeeriminetthk@gmail.com");
                message.Subject = "Ostetud piletid";
                message.Body = text;
                message.IsBodyHtml = true;
                client.Send(message);
                //await client.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /*private void MyForm_Click(object sender, EventArgs e)
        {
            Button btn_click = (Button)sender;
            MessageBox.Show("Oli valitud " + btn_click.Text + " nupp");
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "Kino";
            this.ResumeLayout(false);
        }*/

        private void Btn_tabel_MouseClick(object sender, MouseEventArgs e)
        {   
            Button b = sender as Button;
            b.BackColor = Color.Yellow;
            MessageBox.Show(b.Name.ToString());
            var rida = int.Parse(b.Name[0].ToString());
            var koht = int.Parse(b.Name[1].ToString());
            var answer = MessageBox.Show("Sinu pilet on: Rida: " + rida + " Koht: " + koht, 
                "Kas ostad?",
         MessageBoxButtons.YesNo,
         MessageBoxIcon.Information,
         MessageBoxDefaultButton.Button1);
            if (answer == DialogResult.Yes)
            {
                b.BackColor = Color.Red;
                try
                {
                    Pilet pilet = new Pilet(rida, koht);
                    piletid.Add(pilet);
                    StreamWriter ost = new StreamWriter(@"..\..\info.txt", true);
                    ost.Write(b.Name.ToString() + ';');
                    ost.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (answer == DialogResult.No)
            {
                b.BackColor = Color.Green;
            };

            if (MessageBox.Show("Sul on ostetud: " + piletid.Count() + "piletid", "Kas tahad saada neid e-mailile?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //SendMail("Text");
                Saada_piletid(piletid);
            }


        }
    }   
}