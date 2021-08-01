using Sidna.Helper;
using Sidna.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sidna
{
    public partial class Form1 : Form
    {
        public DboConnection dbo = new DboConnection();
        public Form1()
        {
            s();
         //   new CanvertData().TextTodHistory();
            InitializeComponent();
            label1.Text = "";
        }
        
        async void s()
        {
            //string selectAttachments = "SELECT TOP 5 * FROM [dbo].[History]";
            //var sourceModelResult = await dbo.Read<History>(selectAttachments);
            //var yy = 1;
        }

        async void button1_Click(object sender, EventArgs e)
        {
                string query = "SELECT COALESCE(MAX(ID), 0 ) ID FROM [dbo].[History]";
                var result = await dbo.Read<History>(query);
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                if (label1.Text == "")
                    new CanvertData(label1).TextTodHistory(result.Data.First().ID + 1, openFileDialog1.FileName);

            }

        }
    }
}
