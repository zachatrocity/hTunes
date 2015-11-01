using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace hTunes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MusicLib musicLib = new MusicLib();
        public MainWindow()
        {
            InitializeComponent();
            //add allmusic
            ListBoxItem allMus = new ListBoxItem();
            allMus.Content = "All Music";
            allMus.MouseLeftButtonUp += (obj, e) => { populateDatagridWithAllMusic(); };
            playlistListBox.Items.Add(allMus);

            //add any existing playlists
            foreach (var play in musicLib.Playlists)
            {
                ListBoxItem pl = new ListBoxItem();
                pl.Content = play;
                pl.MouseLeftButtonUp += (obj, e) => { 
                    var playlist = obj as ListBoxItem;
                    populateDatagridWithPlaylist(playlist.Content.ToString()); 
                };
                playlistListBox.Items.Add(pl);
            }

            populateDatagridWithAllMusic();
        }

        public void populateDatagridWithAllMusic()
        {  
            // Bind the data source
            musicDatagrid.ItemsSource =  musicLib.Songs.DefaultView;
        }

        public void populateDatagridWithPlaylist(string pl)
        {
            musicDatagrid.ItemsSource = musicLib.SongsForPlaylist(pl).DefaultView;
        }

        private void newPlaylistButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void openButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void aboutButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
