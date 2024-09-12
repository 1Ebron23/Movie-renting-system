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
    /// Interaction logic for EditMovieDialog.xaml
    /// </summary>
    public partial class EditMovieDialog : Window
    {
        private readonly Movie _movie;   

        public EditMovieDialog(Movie movie)
        {
            _movie = movie;

            InitializeComponent();

            titleTextBox.Text = movie.Title;
            directorTextBox.Text = movie.Director;
            genreTextBox.Text = movie.Genre;
            releaseYearTextBox.Text = movie.ReleaseYear.ToString();
            availableCopiesTextBox.Text = movie.AvailableCopies.ToString();
            durationTextBox.Text = movie.Duration.ToString();
            ratingTextBox.Text = movie.Rating;
            descriptionTextBox.Text = movie.Description;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
           
            _movie.Title = titleTextBox.Text;
            _movie.Director = directorTextBox.Text;
            _movie.Genre = genreTextBox.Text;

            
            _movie.ReleaseYear = int.TryParse(releaseYearTextBox.Text, out int releaseYear) ? releaseYear : 0;
            _movie.AvailableCopies = int.TryParse(availableCopiesTextBox.Text, out int availableCopies) ? availableCopies : 0;

            
            _movie.Duration = int.TryParse(durationTextBox.Text, out int duration) ? duration : 0;
            _movie.Rating = ratingTextBox.Text;
            _movie.Description = descriptionTextBox.Text;

            this.DialogResult = true;
        }
    }
}
