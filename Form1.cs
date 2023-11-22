using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TagLib;

namespace my_ost
{
    public partial class Form1 : Form
    {
        public class SongMetadata
        {
            public Image SongImg { get; set; }
            public string SongName { get; set; }
            public string Artist { get; set; }
            public string SongTags { get; set; }
            public string SongDuration { get; set; }
            public string SongLocation { get; set; }
        }

        WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();
        List<string> mp3LocationList = new List<string>();
        BindingList<SongMetadata> mp3MetadataList = new BindingList<SongMetadata>();

        public Form1()
        {
            InitializeComponent();
        }

        private void mp3MetadataList_ListChanged(object sender, ListChangedEventArgs e)
        {
            dataGridView1.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mp3MetadataList.ListChanged += new ListChangedEventHandler(mp3MetadataList_ListChanged);

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;

            dataGridView1.Columns["SongImg"].DataPropertyName = "SongImg";
            dataGridView1.Columns["SongName"].DataPropertyName = "SongName";
            dataGridView1.Columns["Artist"].DataPropertyName = "Artist";
            dataGridView1.Columns["SongTags"].DataPropertyName = "SongTags";
            dataGridView1.Columns["SongDuration"].DataPropertyName = "SongDuration";
            dataGridView1.Columns["SongLocation"].DataPropertyName = "SongLocation"; 

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
            openFileDialog.Filter = "MP3 Files|*.mp3|WAV Files|*.wav|All Files|*.*";
            openFileDialog.Title = "Select an Audio File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                mp3LocationList.Add(openFileDialog.FileName);
                var file = TagLib.File.Create(openFileDialog.FileName);

                string[] userTags = new string[] { "10PM", "Microsoft Edge" };

                var custom = (TagLib.Id3v2.Tag)file.GetTag(TagLib.TagTypes.Id3v2, true);

                // Write
                custom.SetTextFrame("MyOST_Tags", userTags);
                //custom.RemoveField("OTHER_FIELD");
                file.Save();


                mp3MetadataList.Add(new SongMetadata
                {
                    //SongImg = ResizeImage(Image.FromFile(""), 64, 64),
                    SongName = file.Tag.Title,
                    Artist = file.Tag.FirstAlbumArtist,
                    SongTags = custom.GetFrames("MyOST_Tags").ToString(),
                    SongDuration = file.Properties.Duration.TotalSeconds.ToString(),
                    SongLocation = openFileDialog.FileName
                });

            }
        }

        public static Image ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}
