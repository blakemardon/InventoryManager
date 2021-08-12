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

namespace InventoryManager2
{
    /// <summary>
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class SearchControl : UserControl
    {
        public SearchControl()
        {
            InitializeComponent();
        }

        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            EngineList.SearchParams.Name = "";
            EngineList.SearchParams.Make = "";
            EngineList.SearchParams.Model = "";
            EngineList.SearchParams.Year = -1;
            EngineList.UpdatePartsDisplay();

            Name.Text = "";
            Make.Text = "";
            Model.Text = "";
            Year.Text = "";
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            EngineList.SearchParams.Name = Name.Text;
            EngineList.SearchParams.Make = Make.Text;
            EngineList.SearchParams.Model = Model.Text;
            try
            {
                if (Year.Text == "")
                {
                    EngineList.SearchParams.Year = -1;
                }
                else
                {
                    EngineList.SearchParams.Year = int.Parse(Year.Text);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("It appears the year that has been input is invalid. Please try again", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            EngineList.UpdatePartsDisplay();
        }
    }
}
