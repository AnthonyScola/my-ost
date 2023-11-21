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
        public class SongMetadata
        {
            public string SongImg { get; set; }
            public string SongName { get; set; }
            public string Artist { get; set; }
            public string SongTags { get; set; }
            public string SongDuration { get; set; }
        }

        WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();
        List<string> mp3LocationList = new List<string>();
        List<SongMetadata> mp3MetadataList = new List<SongMetadata>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mp3MetadataList.Add(new SongMetadata
            { 
                SongImg = "",
                SongName = "Test",
                Artist = "Artist",
                SongTags = "Tagzzz",
                SongDuration = "bizzar"
            });
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = mp3MetadataList;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (string mp3 in mp3LocationList)
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
            openFileDialog.Filter = "MP3 Files|*.mp3|OGG Files|*.ogg|All Files|*.*";
            openFileDialog.Title = "Select an Audio File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                mp3LocationList.Add(openFileDialog.FileName);

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
