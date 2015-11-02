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
        ContextMenu allMusicMenu;
        ContextMenu playlistMenu;
        public MainWindow()
        {
            InitializeComponent();
            //add allmusic
            updatePlaylistBox();

            initializeContextMenus();
            playlistListBox.SelectedItem = playlistListBox.Items[0];
            populateDatagridWithAllMusic();
        }

        void updatePlaylistBox()
        {
            playlistListBox.Items.Clear();

            ListBoxItem allMus = new ListBoxItem();
            allMus.Content = "All Music";
            allMus.MouseLeftButtonUp += (obj, e) => { populateDatagridWithAllMusic(); };
            playlistListBox.Items.Add(allMus);

            //add any existing playlists
            foreach (var play in musicLib.Playlists)
            {
                ListBoxItem pl = new ListBoxItem();
                pl.Content = play;
                pl.AllowDrop = true;
                pl.Drop += pl_Drop;
                pl.DragOver += pl_DragOver;

                pl.MouseLeftButtonUp += (obj, e) =>
                {
                    var playlist = obj as ListBoxItem;
                    populateDatagridWithPlaylist(playlist.Content.ToString());
                };
                playlistListBox.Items.Add(pl);
            }
        }

        void pl_DragOver(object sender, DragEventArgs e)
        {
            // By default, don't allow dropping
            e.Effects = DragDropEffects.None;

            // If the DataObject contains string data, extract it
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                string dataString = (string)e.Data.GetData(DataFormats.StringFormat);

                e.Effects = DragDropEffects.Copy;
                
            }

        }

        void pl_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                string dataString = (string)e.Data.GetData(DataFormats.StringFormat);

                musicLib.AddSongToPlaylist(Convert.ToInt32(dataString), (sender as ListBoxItem).Content.ToString());
            }
        }

        private void initializeContextMenus()
        {
            
            MenuItem playItem = new MenuItem();
            playItem.Header = "Play";
            playItem.Click += PlaySongFromMenu_Click;
            
            Separator sep = new Separator();

            MenuItem removeItem = new MenuItem();
            removeItem.Header = "Remove";
            removeItem.Click += removeItem_Click;

            allMusicMenu = new ContextMenu();
            allMusicMenu.Items.Add(playItem);
            allMusicMenu.Items.Add(sep);
            allMusicMenu.Items.Add(removeItem);
            //playlist context menu

            MenuItem playlistPlayItem = new MenuItem();
            playlistPlayItem.Header = "Play";
            playlistPlayItem.Click += PlaySongFromMenu_Click;
            Separator Playlistsep = new Separator();
            
            MenuItem removeItemFromPlaylist = new MenuItem();
            removeItemFromPlaylist.Click += removeItemFromPlaylist_Click;
            removeItemFromPlaylist.Header = "Remove From Playlist";
            
            playlistMenu = new ContextMenu();
            playlistMenu.Items.Add(playlistPlayItem);
            playlistMenu.Items.Add(Playlistsep);
            playlistMenu.Items.Add(removeItemFromPlaylist);
        }

        public void populateDatagridWithAllMusic()
        {  
            // Bind the data source
            musicDatagrid.ItemsSource =  musicLib.Songs.DefaultView;
            musicDatagrid.IsReadOnly = false;
            musicDatagrid.ContextMenu = allMusicMenu;
        }

        public void populateDatagridWithPlaylist(string pl)
        {
            musicDatagrid.ItemsSource = musicLib.SongsForPlaylist(pl).DefaultView;
            musicDatagrid.IsReadOnly = true;
            musicDatagrid.ContextMenu = playlistMenu;
        }

        private void newPlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            AddPlaylistWindow addplaylist = new AddPlaylistWindow();

            // Show testDialog as a modal dialog and determine if DialogResult = OK.
            if (addplaylist.ShowDialog() == true)
            {
                // Read the contents of testDialog's TextBox.
                if (addplaylist.PlaylistName != "")
                {
                    //add playlist
                    musicLib.AddPlaylist(addplaylist.PlaylistName);
                    updatePlaylistBox();
                }
            }
        }

        //http://stackoverflow.com/questions/10315188/open-file-dialog-and-select-a-file-using-wpf-controls-and-c-sharp
        private void openButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".mp3";
            dlg.Filter = "Audio Files (*.wav;*.mp3;*.m4a;*.wma)|*.wav;*.mp3;*.m4a;*.wma";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                musicLib.AddSong(filename);
                populateDatagridWithAllMusic();
            }
        }

        private void aboutButton_Click(object sender, RoutedEventArgs e)
        {
            About abt = new About();
            abt.ShowDialog();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (musicDatagrid.SelectedIndex != -1)
            {
                var songToPlay = musicDatagrid.SelectedItem as DataRowView;
                Song sng = musicLib.GetSong(Convert.ToInt32(songToPlay.Row.ItemArray[0]));
                player.Source = new Uri(sng.Filename);
                try
                {
                    player.Play();
                }
                catch
                {
                    Console.WriteLine("File may not exist");
                }
                
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            player.Stop();
        }

        private void PlaySongFromMenu_Click(object sender, RoutedEventArgs e)
        {
            var songToPlay = musicDatagrid.SelectedItem as DataRowView;
            Song sng = musicLib.GetSong(Convert.ToInt32(songToPlay.Row.ItemArray[0]));
            player.Source = new Uri(sng.Filename);
            try
            {
                player.Play();
            }
            catch
            {
                Console.WriteLine("File may not exist");
            }
        }

        private void removeItem_Click(object sender, RoutedEventArgs e)
        {
            var songToRemove = musicDatagrid.SelectedItem as DataRowView;
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure you wish to delete?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                musicLib.DeleteSong((int)songToRemove.Row.ItemArray[0]);
                populateDatagridWithAllMusic();
            }
        }

        private void removeItemFromPlaylist_Click(object sender, RoutedEventArgs e)
        {
            var songToRemove = musicDatagrid.SelectedItem as DataRowView;
            var playlist = playlistListBox.SelectedItem as ListBoxItem;
            musicLib.RemoveSongFromPlaylist(musicDatagrid.SelectedIndex + 1, Convert.ToInt32(songToRemove.Row.ItemArray[0]), playlist.Content.ToString());
            populateDatagridWithPlaylist(playlist.Content.ToString());
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            musicLib.Save();
        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            musicLib.PrintAllTables();
        }

        private void SearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox tb = sender as TextBox;
            string search = tb.Text;

            if (playlistListBox.SelectedIndex == 0)
            {
                //filter all music
                DataTable filtered = musicLib.Songs;
                filtered.DefaultView.RowFilter = " title like '%" + search + "%' or artist like '%" + search + "%' or album like '%" + search + "%' or genre like '%" + search + "%'";
                musicDatagrid.ItemsSource = filtered.DefaultView;
            }
            else
            {
                //filter a playlist
                ListBoxItem pl = playlistListBox.SelectedItem as ListBoxItem;
                DataTable filtered = musicLib.SongsForPlaylist(pl.Content.ToString());
                filtered.DefaultView.RowFilter = " title like '%" + search + "%' or artist like '%" + search + "%' or album like '%" + search + "%' or genre like '%" + search + "%'";
                musicDatagrid.ItemsSource = filtered.DefaultView;
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            tb.Text = "";
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            tb.Text = "Search...";
        }

        private Point startPoint;
        private void musicDatagrid_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            //start drag-drop if mouse has moved far enough
            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                //initiate dragging the text from the textbox
                DataRowView song = musicDatagrid.SelectedItem as DataRowView;
                DragDrop.DoDragDrop(musicDatagrid, song.Row.ItemArray[0].ToString(), DragDropEffects.Copy);
            }
        }

        private void musicDatagrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //store mouse position
            startPoint = e.GetPosition(null);
        }
    }
}
