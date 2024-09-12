using MovieRentingManager.Interfaces;
using MovieRentingManager.Models;
using MovieRentingManager.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MovieRentingManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        //Here we have the services that we will use to interact with the data
        //note that they are readonly, meaning that they cannot be changed after the constructor
        private readonly IMovieService _movieService;
        private readonly IUserService _userService;
        private readonly ILoanService _loanService;

        // Implement the INotifyPropertyChanged interface
        //No need to touch this
        public event PropertyChangedEventHandler PropertyChanged;

        // Method to call when a property changes
        //No need to touch this
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        //TODO: Create new observable collections for Movies that is similar like the one for Books/Users
        //Note that you need to have a backing field for the property and the property itself
        //and the property has setter and getter like the one for Books/Users


        /// <summary>
        /// Backing field for books observable collection
        /// </summary>
        private ObservableCollection<Movie> _movies;

       /// <summary>
       /// Public property for the Books observable collection
       /// </summary>
       /// 
       // change the name of the object
        public ObservableCollection<Movie> Movies
        {
            get
            {
                return _movies;
            }
            set
            {
                if (_movies != value)
                {
                    _movies = value;
                   
                    OnPropertyChanged(nameof(Movies)); 
                }
            }
        }

        private ObservableCollection<User> _users;

        public ObservableCollection<User> Users
        {
            get
            {
                return _users;
            }
            set
            {
                if (_users != value)
                {
                    _users = value;
                    OnPropertyChanged(nameof(Users));
                }
            }
        }


        public MainWindow()
        {
            DataContext = this;

            _userService = UserService.Instance;

            _movieService = new MovieService();
            

            Movies = new ObservableCollection<Movie>(_movieService.GetMovies());
            Users = new ObservableCollection<User>(_userService.GetUsers());

            //TODO: Initialize the observable collections for Movies
           

            InitializeComponent();
        }

        private void Movie_Search_Button_Click(object sender, RoutedEventArgs e)
        {
            string title = SearchMovieTitleTextBox.Text;
            string director = SearchMovieDirectorTextBox.Text;
            string genre = SearchMovieGenreTextBox.Text;

            
            int year;
            bool isYearValid = int.TryParse(SearchMovieYearTextBox.Text, out year);
            if (!isYearValid)
            {
                year = 0; // Use 0 value if parsing fails
            }

            
            string rating = SearchMovieRatingTextBox.Text;

            
            int duration;
            bool isDurationValid = int.TryParse(SearchMovieDurationTextBox.Text, out duration);
            if (!isDurationValid)
            {
                duration = 0; // Use 0  value if parsing fails
            }

            // Try to convert the available copies from text to an integer
            int availableCopies;
            bool isAvailableCopiesValid = int.TryParse(SearchMovieAvailableCopiesTextBox.Text, out availableCopies);
            if (!isAvailableCopiesValid)
            {
                availableCopies = 0; // Use 0 value if parsing fails
            }
            // Perform search
            var movies = _movieService.FindMovie(title, director, genre, year, rating, duration, availableCopies);
            Movies = new ObservableCollection<Movie>(movies);
        }

        private void AddMovieButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddMovieDialog();

            //TODO: Change this to use your movie service and that it uses Movie object instead Book object

            if (dialog.ShowDialog() == true)
            {
                var movie = dialog.NewMovie;
                _movieService.AddMovie(movie);
                Movies.Add(movie);
            }
        }

        private void DeleteMovieButton_Click(object sender, RoutedEventArgs e)
        {

            //TODO: Change this to use Movie object instead of Book object and make it use your movie service
            //For example change this MoviesDataGrid.SelectedItem is not Book to MoviesDataGrid.SelectedItem is not Movie


            //this checks if the selected item is null or not a movie and if the conditions are met it shows a dialog box with an error
            
            if (MoviesDataGrid.SelectedItem == null || MoviesDataGrid.SelectedItem is not Movie)
            {
                //show dialog box with error
                MessageBox.Show("Please select a book to delete");
            }

            //convert/cast selected item to book type
            Movie movie = (Movie)MoviesDataGrid.SelectedItem;

            //tries to remove movie and returns true if successful
            var removeWasSucceess = _movieService.RemoveMovie(movie.Id);

            if(removeWasSucceess)
            {
                Movies.Remove(movie);
            }
            else
            {
                //show dialog box with error
                MessageBox.Show("Movie could not be deleted");
            }
        }

        private void GetAllMovies_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Change this to use your movie service
            //it should look like this: Movies = new ObservableCollection<Movie>(_movieService.GetMovies());
            Movies = new ObservableCollection<Movie>(_movieService.GetMovies());
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
         {
            // Pass the existing IUserService instance to the dialog
            
            IUserService userService = UserService.Instance;

            var dialog = new AddUserDialog(userService);

            if (dialog.ShowDialog() == true)
            {
                // Retrieve the user added from the dialog
                var user = dialog.NewUser;

                // Ensure user is not null and has a valid ID
                if (user != null && user.Id > 0)
                {
                    Users.Add(user); // Add the new user to the collection
                }
                else
                {
                    MessageBox.Show("Failed to retrieve user details.");
                }
            }
        }

        private void EditUserButton_Click(object sender, RoutedEventArgs e)
        {
            // Ensure a user is selected in the data grid
            if (UsersDataGrid.SelectedItem == null || UsersDataGrid.SelectedItem is not User selectedUser)
            {
                MessageBox.Show("Please select a user to edit.");
                return;
            }

            // Open the EditUserDialog with the selected user information
            var dialog = new EditUserDialog
            {
                nameTextBox = { Text = selectedUser.Name },
                emailTextBox = { Text = selectedUser.Email }
            };

            // Show the dialog and check if the user pressed "Save"
            if (dialog.ShowDialog() == true)
            {
                // Retrieve updated values from the dialog
                string newName = dialog.nameTextBox.Text.Trim();
                string newEmail = dialog.emailTextBox.Text.Trim();

                // Validate the new values
                if (string.IsNullOrWhiteSpace(newName) || string.IsNullOrWhiteSpace(newEmail))
                {
                    MessageBox.Show("Name and Email cannot be empty.");
                    return;
                }

                // Update the selected user's properties within the dialog logic
                selectedUser.Name = newName;
                selectedUser.Email = newEmail;

                // Call the update service method to save the changes
                bool updateSuccess = _userService.UpdateUser(selectedUser);

                if (updateSuccess)
                {
                    // Update the ObservableCollection to reflect the changes
                    
                    int index = Users.IndexOf(selectedUser);
                    if (index >= 0)
                    {
                        Users[index] = selectedUser;
                    }

                    // Refresh the DataGrid to reflect the changes
                    UsersDataGrid.Items.Refresh();

                    // Notify the user of the successful update
                    MessageBox.Show("User details updated successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to update user details.");
                }
            }
        }




        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Implement delete user logic

            if (UsersDataGrid.SelectedItem == null || UsersDataGrid.SelectedItem is not User selectedUser)
            {
               
                MessageBox.Show("Please select a user to delete");
                return;
            }

            bool removeWasSuccess = _userService.RemoveUser(selectedUser.Id);

            if (removeWasSuccess)
            {
                // Remove the user from the observable collection
                Users.Remove(selectedUser);

                // Refresh the DataGrid to reflect the changes
                UsersDataGrid.Items.Refresh();

                
                MessageBox.Show("User successfully deleted.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
               
                MessageBox.Show("User could not be deleted. Please try again.", "Deletion Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
private void EditMovieButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Change this to use Movie object instead of Book object and make it use your movie service

            if (MoviesDataGrid.SelectedItem == null || MoviesDataGrid.SelectedItem is not Movie)
            {
                //show dialog box with error
                MessageBox.Show("Please select a movie to edit.");
            }

            Movie movie = (Movie)MoviesDataGrid.SelectedItem;

            //show edit dialog
            EditMovieDialog dialog = new EditMovieDialog(movie);
            dialog.ShowDialog();

            //update the books list so it shows the changes in ui
            Movies = new ObservableCollection<Movie>(Movies);
        }
    }
}