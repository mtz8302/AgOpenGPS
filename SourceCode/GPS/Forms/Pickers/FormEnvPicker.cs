﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace AgOpenGPS
{
    public partial class FormEnvPicker : Form
    {
        //class variables
        private readonly FormGPS mf = null;

        public FormEnvPicker(Form callingForm)
        {
            //get copy of the calling main form
            mf = callingForm as FormGPS;
            InitializeComponent();

            //this.bntOK.Text = gStr.gsForNow;
            //this.btnSave.Text = gStr.gsToFile;

            this.Text = gStr.gsLoadEnvironment;
        }

        private void FormFlags_Load(object sender, EventArgs e)
        {
            lblLast.Text = gStr.gsCurrent + mf.envFileName;
            DirectoryInfo dinfo = new DirectoryInfo(mf.envDirectory);
            FileInfo[] Files = dinfo.GetFiles("*.txt");
            if (Files.Length == 0)
            {
                Close();
                var form = new FormTimedMessage(2000, gStr.gsNoEnvironmentSaved, gStr.gsSaveAnEnvironmentFirst);
                form.Show();
            }

            foreach (FileInfo file in Files)
            {
                cboxEnv.Items.Add(Path.GetFileNameWithoutExtension(file.Name));
            }
        }

        private void cboxVeh_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool resul = mf.FileOpenEnvironment(mf.envDirectory + cboxEnv.SelectedItem.ToString() + ".txt");
            
            if (resul) DialogResult = DialogResult.OK;
            else DialogResult = DialogResult.Cancel;

            Close();
        }
    }
}