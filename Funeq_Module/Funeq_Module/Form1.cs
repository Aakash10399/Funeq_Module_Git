using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
namespace Funeq_Module
{
    public partial class Form1 : Form
    {
        public static int c;
        public static int score=0;
        public static string[] ques;
        public static string[] ans;
        public static string[] hints;
        public static string shash, uname; 
        public static string click;
        public Form1(string unamet, string shasht, int scoret, string clickt)
        {
            
            
            InitializeComponent();
            shash = shasht; uname = unamet; score = 0; click = clickt;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            AutoScroll = true;
            c = 0;
            System.IO.StreamReader file = new System.IO.StreamReader("C:\\Users\\Public\\Main_App\\Funeq_Module\\questions.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                c++;
            }
            file.Close();
            ques = new string[c];
            ans = new string[c];
            hints = new string[c];
            System.IO.StreamReader file1 = new System.IO.StreamReader("C:\\Users\\Public\\Main_App\\Funeq_Module\\questions.txt");
            System.IO.StreamReader file2 = new System.IO.StreamReader("C:\\Users\\Public\\Main_App\\Funeq_Module\\answers.txt");
            System.IO.StreamReader file3 = new System.IO.StreamReader("C:\\Users\\Public\\Main_App\\Funeq_Module\\hints.txt");
            string line1, line2, line3; int k = 0;
            while ((line1 = file1.ReadLine()) != null && (line2 = file2.ReadLine()) != null && (line3 = file3.ReadLine()) != null)
            {
                ques[k] = line1; ans[k] = line2; hints[k] = line3;
                k++;
            }
            file1.Close(); file2.Close(); file3.Close();
            Construct();
        }
        public void Construct()
        {
            int i = 1;
           for(i=1;i<=c;i++)
           {
               var lab = new Label();
               lab.Location = new Point(1300 / 4, 100 + ((i - 1) * 150));
               lab.Text = "Q" + i.ToString() +": "+ ques[i - 1];
               lab.AutoSize = true;
               Controls.Add(lab);
               var lab2 = new Label();
               lab2.Location = new Point(1300 / 4, 150 + ((i - 1) * 150));
               lab2.Text = hints[i - 1];
               lab2.AutoSize = true;
               Controls.Add(lab2);
               var txt = new TextBox();
               txt.MaxLength = 1;
               txt.Width = 25;
               txt.Name = "txt" + i.ToString();
               txt.Location = new Point(1300/4, 200+((i-1)*150));
               Controls.Add(txt);

           }
           var btn = new Button();
           btn.Text = "Check Score!";
           btn.Location = new Point(1300 / 4, 250 + ((i - 2) * 150));
           btn.Name = "checkbtn";
           btn.Click += new EventHandler(check);
           Controls.Add(btn);
           
        }
        public void check(object sender, EventArgs e)
        {
            for(int i =0;i<c;i++)
            {
                Control ctn = this.Controls["txt" + (i + 1).ToString()];
                if(ans[i].Equals(ctn.Text))
                {
                    score++;
                }
            }
            label1.Text = "FINAL SCORE : "+score.ToString();
            Control ctn2 = this.Controls["checkbtn"];
            ctn2.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                WebClient client = new WebClient();


                string url = "http://www.cofas10399.16mb.com/update_score.php?uname=" + uname + "&hash=" + shash + "&score=" + score.ToString() + "&gtype=" + click;
                string response = client.DownloadString(url);
                response = response.Trim();

                if (response.Substring(0, 1).Equals("0"))
                {
                    Console.WriteLine("ERROR_SERVERSIDE");
                }
                if (response.Substring(0, 1).Equals("1"))
                {
                    Console.WriteLine("SUCCESS");
                }


            }
            catch
            {
                Console.WriteLine("ERROR_INTERNET");
            }
        }
    }
}
