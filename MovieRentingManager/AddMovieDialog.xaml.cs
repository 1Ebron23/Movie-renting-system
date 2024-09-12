using MovieRentingManager.Models;
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
using System.Windows.Shapes;

namespace MovieRentingManager
{
    /// <summary>
    /// Interaction logic for AddBookDialog.xaml
    /// </summary>
    public partial class AddMovieDialog : Window
    {
        public Movie? NewMovie { get; private set; }


        public AddMovieDialog()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Change this to use your Movie class
            NewMovie = new Movie
            {
                Title = titleTextBox.Text,
                Director = directorTextBox.Text,
                Genre = genreTextBox.Text,
                ReleaseYear = int.TryParse(releaseYearTextBox.Text, out int releaseYear) ? releaseYear : 0,
                Rating = ratingTextBox.Text,
                Duration = int.TryParse(durationTextBox.Text, out int duration) ? duration : 0,
                Description = descriptionTextBox.Text,
                AvailableCopies = int.TryParse(availableCopiesTextBox.Text, out int availableCopies) ? availableCopies : 0


            };

            this.DialogResult = true;
        }

        private void ratingTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
