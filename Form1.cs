using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace my_ost
{
    public partial class Form1 : Form
    {
        WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();
        List<string> mp3Locations = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (string mp3 in mp3Locations)
            {
                player.URL = mp3;
                player.controls.play();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            player.controls.pause();
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP3 Files|*.mp3";
            openFileDialog.Title = "Select an MP3 File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                mp3Locations.Add(openFileDialog.FileName);
            }
        }
    }
}
