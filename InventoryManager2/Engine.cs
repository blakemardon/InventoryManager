using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
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
    public class Engine
    {
        public string Name { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int StartYearsOfUse { get; set; }
        public int EndYearsOfUse { get; set; }
        public int Stock { get; set; }
        public string BinNumber { get; set; }
        public string Description { get; set; }
        private StackPanel containingPanel = null;
        public Grid wpfRepresentationGrid = null;
        public Guid ID = Guid.NewGuid();

        public Engine(string nameIn, string makeIn, string modelIn, int startYearIn, int endYearIn, int stockIn, string binNumberIn)
        {
            Name = nameIn;
            Make = makeIn;
            Model = modelIn;
            StartYearsOfUse = startYearIn;
            EndYearsOfUse = endYearIn;
            Stock = stockIn;
            BinNumber = binNumberIn;
            Description = "";
        }

        //Displays the Engine in a stackpanel
        public void AddToParent(StackPanel s)
        {
            containingPanel = s;
            // Sets all column definitions for the grid
            Grid mainGrid = new Grid();
            ColumnDefinition col1 = new ColumnDefinition();
            col1.Width = new GridLength(1, GridUnitType.Star);
            mainGrid.ColumnDefinitions.Add(col1);
            ColumnDefinition col2 = new ColumnDefinition();
            col2.Width = new GridLength(1, GridUnitType.Star);
            mainGrid.ColumnDefinitions.Add(col2);
            ColumnDefinition col3 = new ColumnDefinition();
            col3.Width = new GridLength(1, GridUnitType.Star);
            mainGrid.ColumnDefinitions.Add(col3);
            ColumnDefinition col4 = new ColumnDefinition();
            col4.Width = new GridLength(1, GridUnitType.Star);
            mainGrid.ColumnDefinitions.Add(col4);
            ColumnDefinition col5 = new ColumnDefinition();
            col5.Width = new GridLength(1, GridUnitType.Star);
            mainGrid.ColumnDefinitions.Add(col5);
            s.Children.Add(mainGrid);
            wpfRepresentationGrid = mainGrid;
            mainGrid.MouseLeftButtonDown += SelectItem;

            // Add the name
            Border nameBorder = new Border();
            nameBorder.BorderThickness = new Thickness(0, 0, 0, 1);
            nameBorder.BorderBrush = Brushes.AntiqueWhite;
            Grid.SetColumn(nameBorder, 0);
            mainGrid.Children.Add(nameBorder);

            TextBlock nameTextblock = new TextBlock();
            nameTextblock.Text = Name;
            nameTextblock.HorizontalAlignment = HorizontalAlignment.Center;
            nameBorder.Child = nameTextblock;

            // Add the make
            Border makeBorder = new Border();
            makeBorder.BorderThickness = new Thickness(0, 0, 0, 1);
            makeBorder.BorderBrush = Brushes.AntiqueWhite;
            makeBorder.Background = Brushes.LightYellow;
            Grid.SetColumn(makeBorder, 1);
            mainGrid.Children.Add(makeBorder);

            TextBlock makeTextblock = new TextBlock();
            makeTextblock.Text = Make;
            makeTextblock.HorizontalAlignment = HorizontalAlignment.Center;
            makeBorder.Child = makeTextblock;

            // Add the model
            Border modelBorder = new Border();
            modelBorder.BorderThickness = new Thickness(0, 0, 0, 1);
            modelBorder.BorderBrush = Brushes.AntiqueWhite;
            Grid.SetColumn(modelBorder, 2);
            mainGrid.Children.Add(modelBorder);

            TextBlock modelTextblock = new TextBlock();
            modelTextblock.Text = Model;
            modelTextblock.HorizontalAlignment = HorizontalAlignment.Center;
            modelBorder.Child = modelTextblock;

            // Add the year range
            Border yearBorder = new Border();
            yearBorder.BorderThickness = new Thickness(0, 0, 0, 1);
            yearBorder.BorderBrush = Brushes.AntiqueWhite;
            yearBorder.Background = Brushes.LightYellow;
            Grid.SetColumn(yearBorder, 3);
            mainGrid.Children.Add(yearBorder);

            TextBlock yearTextblock = new TextBlock();
            yearTextblock.Text = StartYearsOfUse.ToString() + " - " + EndYearsOfUse.ToString(); 
            yearTextblock.HorizontalAlignment = HorizontalAlignment.Center;
            yearBorder.Child = yearTextblock;

            // Add the Stock
            Border stockBorder = new Border();
            stockBorder.BorderThickness = new Thickness(0, 0, 0, 1);
            stockBorder.BorderBrush = Brushes.AntiqueWhite;
            Grid.SetColumn(stockBorder, 4);
            mainGrid.Children.Add(stockBorder);

            // Grid for Stock
            Grid stockGrid = new Grid();
            stockBorder.Child = stockGrid;

            // column definitions for stock grid
            ColumnDefinition stockCol1 = new ColumnDefinition();
            stockCol1.Width = new GridLength(1, GridUnitType.Star);
            stockGrid.ColumnDefinitions.Add(stockCol1);
            ColumnDefinition stockCol2 = new ColumnDefinition();
            stockCol2.Width = new GridLength(1, GridUnitType.Star);
            stockGrid.ColumnDefinitions.Add(stockCol2);

            //row definitions for stock grid
            RowDefinition stockRow1 = new RowDefinition();
            stockRow1.Height = new GridLength(1, GridUnitType.Star);
            stockGrid.RowDefinitions.Add(stockRow1);
            RowDefinition stockRow2 = new RowDefinition();
            stockRow2.Height = new GridLength(1, GridUnitType.Star);
            stockGrid.RowDefinitions.Add(stockRow2);

            // content for stock grid
            TextBlock stockTxtBlock = new TextBlock();
            stockTxtBlock.Text = Stock.ToString();
            stockTxtBlock.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetColumnSpan(stockTxtBlock, 2);
            stockGrid.Children.Add(stockTxtBlock);

            Button stockIncBtn = new Button();
            stockIncBtn.Content = "+1";
            stockIncBtn.HorizontalAlignment = HorizontalAlignment.Right;
            stockIncBtn.Width = 50;
            stockIncBtn.Margin = new Thickness(0, 0, 5, 0);
            stockIncBtn.Click += this.IncreaseStock;
            Grid.SetRow(stockIncBtn, 1);
            stockGrid.Children.Add(stockIncBtn);

            Button stockDecBtn = new Button();
            stockDecBtn.Content = "-1";
            stockDecBtn.HorizontalAlignment = HorizontalAlignment.Left;
            stockDecBtn.Width = 50;
            stockDecBtn.Margin = new Thickness(5, 0, 0, 0);
            stockDecBtn.Click += this.DecreaseStock;
            Grid.SetRow(stockDecBtn, 1);
            Grid.SetColumn(stockDecBtn, 1);
            stockGrid.Children.Add(stockDecBtn);
        }

        public void IncreaseStock(object sender, RoutedEventArgs e)
        {
            Stock++;
            EngineList.UpdatePartsDisplay();
        }

        public void DecreaseStock(object sender, RoutedEventArgs e)
        {
            if (Stock == 0)
            {
                //intentionally empty
            }
            else
            {
                Stock--;
                EngineList.UpdatePartsDisplay();
            }
        }

        public void SelectItem(object sender, RoutedEventArgs e)
        {
            EngineList.UpdateSelected(ID);
        }

        public void WriteToFileXml(XmlWriter w)
        {
            w.WriteStartElement("Engine");
            w.WriteElementString("Name", Name);
            w.WriteElementString("Make", Make);
            w.WriteElementString("Model", Model);
            w.WriteElementString("StartYear", StartYearsOfUse.ToString());
            w.WriteElementString("EndYear", EndYearsOfUse.ToString());
            w.WriteElementString("Stock", Stock.ToString());
            w.WriteElementString("BinNumber", BinNumber);
            w.WriteElementString("Description", Description);
            w.WriteEndElement();
        }
    }
}
