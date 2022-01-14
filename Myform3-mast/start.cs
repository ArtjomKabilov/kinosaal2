using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyVorm
{
    class start:System.Windows.Forms.Form
    {
        public start()
        {
            Button Start_btn = new Button
            {
                Text = "minu oma aken",
                Location = new System.Drawing.Point(10, 10)
            };
            Start_btn.Click += Start_btn_Click;
            this.Controls.Add(Start_btn);
            Button start_btn_2 = new Button
            {
                Text = "Veel aken",
                Location = new System.Drawing.Point(200, 10)
            };
            start_btn_2.Click += Start_btn_2_Click;
            this.Controls.Add(start_btn_2);
        }

        private void Start_btn_2_Click(object sender, EventArgs e)
        {
            MyForm uus_aken = new MyForm(8, 11);
            uus_aken.StartPosition = FormStartPosition.CenterScreen;
            uus_aken.ShowDialog();
        }

        private void Start_btn_Click(object sender, EventArgs e)
        {
            MyForm uus_aken = new MyForm("Ma olen ilus õun","Vali midagi","üks","kaks","kolm","neli");
            uus_aken.StartPosition = FormStartPosition.CenterScreen;
            uus_aken.ShowDialog();
        }
    }
}
