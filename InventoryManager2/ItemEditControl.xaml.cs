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
    /// Interaction logic for ItemEditControl.xaml
    /// </summary>
    public partial class ItemEditControl : UserControl
    {
        private Engine editEngine = null;

        public ItemEditControl(Engine engineIn)
        {
            InitializeComponent();
            editEngine = engineIn;
            NameTxtbx.Text = engineIn.Name;
            MakeTxtbx.Text = engineIn.Make;
            ModelTxtbx.Text = engineIn.Model;
            YearStartCb.SelectedIndex = DateTime.Now.Year + 1 - engineIn.StartYearsOfUse;
            YearEndCb.SelectedIndex = DateTime.Now.Year + 1 - engineIn.EndYearsOfUse;
            StockCb.SelectedIndex = engineIn.Stock;
            BinNumberTxtbx.Text = engineIn.BinNumber;
            DescriptionTxtbx.Text = engineIn.Description;
        }

        private void SaveChangesBtn_Click(object sender, RoutedEventArgs e)
        {
            //Check the user input for any problems
            if (NameTxtbx.Text == "")
            {
                //display an error popup box name cant be empty
                MessageBox.Show("Invalid Input: You must enter input in to the Name text box.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (MakeTxtbx.Text == "")
            {
                //display an error popup box make cant be empty
                MessageBox.Show("Invalid Input: You must enter input in to the Make text box.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (ModelTxtbx.Text == "")
            {
                //display an error popup box Model cant be empty
                MessageBox.Show("Invalid Input: You must enter input in to the Model text box.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (int.Parse(YearStartCb.SelectedItem as string) > int.Parse(YearEndCb.SelectedItem as string))
            {
                //display an error popup box the first year must be smaller than or equal to the second year
                MessageBox.Show("Invalid Input: The start year is after the end year.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (BinNumberTxtbx.Text == "")
            {
                //display an error popup box the first year must be smaller than or equal to the second year
                MessageBox.Show("Invalid Input: You must enter input in to the Bin Number text box.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                editEngine.Name = NameTxtbx.Text;
                editEngine.Make = MakeTxtbx.Text;
                editEngine.Model = ModelTxtbx.Text;
                editEngine.StartYearsOfUse = int.Parse(YearStartCb.SelectedItem as string);
                editEngine.EndYearsOfUse = int.Parse(YearEndCb.SelectedItem as string);
                editEngine.Stock = int.Parse(StockCb.SelectedItem as string);
                editEngine.BinNumber = BinNumberTxtbx.Text;
                editEngine.Description = DescriptionTxtbx.Text;

                EngineList.UpdatePartsDisplay();
            }
            (this.Parent as ContentControl).Content = new ItemDisplayControl(editEngine);
        }

        private void YearStartCb_Initialized(object sender, EventArgs e)
        {
            YearStartCb.ItemsSource = new Years();
            //YearStartCb.SelectedIndex = 0;
            YearStartCb.MaxDropDownHeight = 150;
        }

        private void YearEndCb_Initialized(object sender, EventArgs e)
        {
            YearEndCb.ItemsSource = new Years();
            //YearEndCb.SelectedIndex = 0;
            YearEndCb.MaxDropDownHeight = 150;
        }

        private void StockCb_Initialized(object sender, EventArgs e)
        {
            StockCb.ItemsSource = new StockNumbers(0,10);
            //StockCb.SelectedIndex = 1;
            StockCb.MaxDropDownHeight = 150;
        }
    }
}
