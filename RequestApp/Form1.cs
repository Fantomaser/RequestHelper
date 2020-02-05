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
using System.IO;

namespace RequestApp
{
    public partial class Form1 : Form
    {
        Button btnSendMSG;
        Button btnGetDB;

        Label NameL;
        TextBox textBoxName;

        Label ValueL;
        TextBox textBoxValue;

        Label Request;

        Label Responce;
        Label DBState;

        public Form1()
        {
            Size sz = new Size() { };
            sz.Width = 500;
            sz.Height = 500;
            this.BackColor = Color.White;
            this.ClientSize = sz;
            this.MinimumSize = sz;
            this.MaximumSize = sz;
            InitializeComponent();

            NameL = new Label() { };
            NameL.Text = "Enter message name:";
            NameL.Location = new Point() { X = 20, Y = 10 };
            this.Controls.Add(NameL);


            textBoxName = new TextBox() { };
            textBoxName.Name = "Name";
            sz.Width = 100;
            sz.Height = 0;
            textBoxName.Size = sz;
            textBoxName.Location = new Point() { X = 120, Y = 6 };
            this.Controls.Add(textBoxName);


            ValueL = new Label() { };
            ValueL.Text = "Enter message value";
            ValueL.Location = new Point() { X = 270, Y = 10 };
            this.Controls.Add(ValueL);


            textBoxValue = new TextBox() { };
            textBoxValue.Name = "Value";
            sz.Width = 100;
            sz.Height = 0;
            textBoxValue.Size = sz;
            textBoxValue.Location = new Point() { X = 370, Y = 6 };
            this.Controls.Add(textBoxValue);

            btnSendMSG = new Button() { };
            btnSendMSG.Location = new Point() { X = 100, Y = 35 };
            sz.Width = 300;
            sz.Height = 30;
            btnSendMSG.Size = sz;
            btnSendMSG.Text = "Send message";
            btnSendMSG.Click += MakeRequestMSG;
            this.Controls.Add(btnSendMSG);


            Responce = new Label() { };
            Responce.Text = "Request";
            sz.Width = 300;
            sz.Height = 70;
            Responce.Size = sz;
            Responce.Location = new Point() { X = 100, Y = 110 };
            this.Controls.Add(Responce);

            btnGetDB = new Button() { };
            btnGetDB.Location = new Point() { X = 100, Y = 200 };
            sz.Width = 300;
            sz.Height = 30;
            btnGetDB.Size = sz;
            btnGetDB.Text = "Get DataBase";
            btnGetDB.Click += MakeRequest;
            this.Controls.Add(btnGetDB);

            DBState = new Label() { };
            DBState.Text = "DBState";
            sz.Width = 300;
            sz.Height = 100;
            DBState.Size = sz;
            DBState.Location = new Point() { X = 100, Y = 250 };
            this.Controls.Add(DBState);

        }

        private void MakeRequestMSG(object sender, EventArgs e)
        {
            string url = @"http://localhost:5000/addMessage";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            using (var requestStream = req.GetRequestStream())
            using (var sw = new StreamWriter(requestStream))
            {
                sw.Write("{" + string.Format( "\"name\" : \"{0}\", \"value\" : {1}" , textBoxName.Text, textBoxValue.Text) + "}");
            }

            string res = "";

            using (var responseStream = req.GetResponse().GetResponseStream())
            using (var sr = new StreamReader(responseStream))
            {
                res = sr.ReadToEnd();
            }

            Responce.Text = res;

        }

        private void MakeRequest(object sender, EventArgs e)
        {

            string url = @"http://localhost:5000/makeTrack";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";

            string res = "";

            using (var responseStream = req.GetResponse().GetResponseStream())
            using (var sr = new StreamReader(responseStream))
            {
                res = sr.ReadToEnd();
            }

            DBState.Text = res;

        }

    }
}
