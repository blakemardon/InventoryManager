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
    /// Interaction logic for NewItem.xaml
    /// </summary>
    public partial class NewItemControl : UserControl
    {
        public NewItemControl()
        {
            InitializeComponent();
        }

        private void StartYearInput_Initialized(object sender, EventArgs e)
        {
            StartYearInput.ItemsSource = new Years();
            StartYearInput.SelectedIndex = 0;
            StartYearInput.MaxDropDownHeight = 150;
        }

        private void EndYearInput_Initialized(object sender, EventArgs e)
        {
            EndYearInput.ItemsSource = new Years();
            EndYearInput.SelectedIndex = 0;
            EndYearInput.MaxDropDownHeight = 150;
        }

        private void Stock_Initialized(object sender, EventArgs e)
        {
            StockInput.ItemsSource = new StockNumbers(0, 10);
            StockInput.SelectedIndex = 1;
            StockInput.MaxDropDownHeight = 150;
        }

        private void AddNewItem_Click(object sender, RoutedEventArgs e)
        {
            //Check the user input for any problems
            if (NameInput.Text == "")
            {
                //display an error popup box name cant be empty
                MessageBox.Show("Invalid Input: You must enter input in to the Name text box.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (MakeInput.Text == "")
            {
                //display an error popup box make cant be empty
                MessageBox.Show("Invalid Input: You must enter input in to the Make text box.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (ModelInput.Text == "")
            {
                //display an error popup box Model cant be empty
                MessageBox.Show("Invalid Input: You must enter input in to the Model text box.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (int.Parse(StartYearInput.SelectedItem as string) > int.Parse(EndYearInput.SelectedItem as string))
            {
                //display an error popup box the first year must be smaller than or equal to the second year
                MessageBox.Show("Invalid Input: The start year is after the end year.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (BinNumberInput.Text == "")
            {
                //display an error popup box the first year must be smaller than or equal to the second year
                MessageBox.Show("Invalid Input: You must enter input in to the Bin Number text box.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                //serch the current objects for an object matching exactly
                bool flag = false;
                int foundIndex = 0;
                for (int i = 0; i < EngineList.Engines.Count; i++)
                {
                    if (EngineList.Engines[i].Name == NameInput.Text.Trim())
                    {
                        if (EngineList.Engines[i].Make == MakeInput.Text.Trim())
                        {
                            if (EngineList.Engines[i].Model == ModelInput.Text.Trim())
                            {
                                if ((EngineList.Engines[i].StartYearsOfUse == int.Parse(StartYearInput.SelectedItem as string)) && (EngineList.Engines[i].EndYearsOfUse == int.Parse(EndYearInput.SelectedItem as string)))
                                {
                                    flag = true;
                                    foundIndex = i;
                                    break;
                                }
                            }
                        }
                    }
                }
                if (flag)
                {
                    // display popup asking if the user would like to increase the stock instead
                    MessageBoxResult result = MessageBox.Show(
                        "It seems that there is another entry matching the exact information you just entered. Would you like to increase the stock by this new amount?",
                        "Repeat Entry",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            EngineList.Engines[foundIndex].Stock += int.Parse(StockInput.SelectedItem as string);
                            EngineList.UpdatePartsDisplay();
                            break;
                        case MessageBoxResult.No:
                            break;
                    }
                }
                else
                {
                    Engine newEngine = new Engine(NameInput.Text.Trim(), MakeInput.Text.Trim(), ModelInput.Text.Trim(), int.Parse(StartYearInput.SelectedItem as string), int.Parse(EndYearInput.SelectedItem as string), int.Parse(StockInput.SelectedItem as string), BinNumberInput.Text);
                    newEngine.Description = DescriptionInput.Text;
                    EngineList.Engines.Add(newEngine);
                    EngineList.UpdatePartsDisplay();

                    NameInput.Focus();
                    NameInput.Text = "";
                    MakeInput.Text = "";
                    ModelInput.Text = "";
                    StartYearInput.SelectedIndex = 0;
                    EndYearInput.SelectedIndex = 0;
                    StockInput.SelectedIndex = 1;
                    BinNumberInput.Text = "";
                    DescriptionInput.Text = "";
                }
            }
        }
    }
}
