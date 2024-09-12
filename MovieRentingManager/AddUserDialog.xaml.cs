using MovieRentingManager.Interfaces;
using MovieRentingManager.Models;
using MovieRentingManager.Services;
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
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUserDialog : Window
    {

        private readonly IUserService _userService;

        public User? NewUser { get; private set; }


        public AddUserDialog(IUserService userService)
        {
            InitializeComponent();
            _userService = userService;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            NewUser = new User
            {
                
                Name = nameTextBox.Text,
                Email = emailTextBox.Text
            };


            bool isAdded = _userService.AddUser(NewUser); // Add the user and get a boolean result

            if (isAdded)
            {
                MessageBox.Show("User added successfully.");
                this.DialogResult = true; // Close the dialog with OK
            }
            else
            {
                MessageBox.Show("Failed to add user.");
            }
        }
    }
}
