using System;
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
using System.Data.Entity;
using Model;


namespace EntityFramework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Model.TrackDB _dbContainer;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if(vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    int id = (row.Item as Track).TrackID;

                    Track track = _dbContainer.Tracks
                        .Where(o => o.TrackID == id)
                        .FirstOrDefault();

                    _dbContainer.Tracks.Remove(track);
                    _dbContainer.SaveChanges();
                    tbStatus.Text = id + "was removed";
                    break;
                }
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            Track track = new Track
            {
                ArtistName = ArtistNameTxt.Text,
                TrackName = TrackNameTxt.Text
            };
            _dbContainer.Tracks.Add(track);
            _dbContainer.SaveChanges();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _dbContainer = new TrackDB();
            _dbContainer.Tracks.Load();
            dgGrid.ItemsSource = _dbContainer.Tracks.Local;
        }
    }
}
