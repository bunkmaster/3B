﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3B
{
    public partial class StatisticalReport : Form
    {
        public StatisticalReport()
        {
            InitializeComponent();
        }

        private void StatisticalReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void StatisticalReport_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ReportMenu rm = new ReportMenu();
            rm.Show();
            this.Hide();
        }
    }
}
