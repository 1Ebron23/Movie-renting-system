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
    /// Interaction logic for EditUserDialog.xaml
    /// </summary>
    public partial class EditUserDialog : Window
    {
        public EditUserDialog()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Implement the logic to update the user

            DialogResult = true;
            Close();



        }

    }
}

