/***********************************************************************
			b-PAC 3.0 Component Sample (TestPrint)

			(C)Copyright Brother Industries, Ltd. 2009
************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using bpac;

namespace TestPrint
{
    public partial class Form1 : Form
    {
		private const string TEMPLATE_DIRECTORY = @"C:\Program Files\Brother bPAC3 SDK\Templates\";	// Template file path
		private const string TEMPLATE_SIMPLE = "NamePlate1.LBX";	// Template file name
		private const string TEMPLATE_FRAME = "NamePlate2.LBX";		// Template file name

        public Form1()
        {
            InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			this.cmbTemplate.SelectedIndex = 0;
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			string templatePath = TEMPLATE_DIRECTORY;
			// None decoration frame
			if (cmbTemplate.SelectedIndex == 0)
			{
				templatePath += TEMPLATE_SIMPLE;
			}
			// Decoration frame
			else
			{
				templatePath += TEMPLATE_FRAME;
			}

			bpac.DocumentClass doc = new DocumentClass();
			if (doc.Open(templatePath) != false)
			{
				doc.GetObject("objCompany").Text = txtCompany.Text;
				doc.GetObject("objName").Text = txtName.Text;

				// doc.SetMediaById(doc.Printer.GetMediaId(), true);
				doc.StartPrint("", PrintOptionConstants.bpoDefault);
				doc.PrintOut(1, PrintOptionConstants.bpoDefault);
				doc.EndPrint();
				doc.Close();
			}
			else
			{
				MessageBox.Show("Open() Error: " + doc.ErrorCode);
			}
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}
    }
}