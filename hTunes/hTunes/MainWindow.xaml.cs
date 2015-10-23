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
            populateDatagridWithAllMusic();
        }

        public void populateDatagridWithAllMusic()
        {  
            // Bind the data source
            musicDatagrid.ItemsSource =  musicLib.Songs.DefaultView;
        }
    }
}
