using Minesweeper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Media;
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

namespace WpfApp1
{
 
  public partial class MainWindow : Window
  {
    Gridy gridy;

    int row;
    int column;
    int mine;
    
    public MainWindow()
    {
      
      using (SoundPlayer player = new SoundPlayer("C:\\OffLimits.wav"))
      {
        player.PlayLooping();
      }
      InitializeComponent();
     
      dimensions.SelectedIndex = 0;

    }
    public void NewGame()
    {
      Minesweeper.Block blocky = new Minesweeper.Block(0, false, true);

      //make a grid with bombs and the numbers
      gridy = new Gridy(blocky, row, column);
      gridy.MineIt(mine);
      gridy.NumberIt();
      gridy.RemainingBombsCount();
    }


    private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void createGrid()
    {
      NewGame();

      tablero.RowDefinitions.Clear();
      tablero.ColumnDefinitions.Clear();
      //this creates the grid's rows and columns
      for (int i = 0; i < gridy.theGrid.GetLength(0); i++)
      {
        RowDefinition row = new RowDefinition();
        row.MinHeight = 25;
        tablero.RowDefinitions.Add(row);

      }

      for (int j = 0; j < gridy.theGrid.GetLength(1); j++)
      {
        ColumnDefinition col = new ColumnDefinition();
        col.MinWidth = 25;
        tablero.ColumnDefinitions.Add(col);
      }


      for (int i = 0; i < gridy.theGrid.GetLength(0); i++)
      {
        for (int j = 0; j < gridy.theGrid.GetLength(1); j++)
        {
          Button boton = new Button();
          boton.Content = i + "," + j;
          boton.Width = 25;
          boton.Height = 25;
          boton.VerticalAlignment = VerticalAlignment.Center;
          boton.HorizontalAlignment = HorizontalAlignment.Center;
          Grid.SetRow(boton, i);
          Grid.SetColumn(boton, j);
          tablero.Children.Add(boton);

        }
      }
    }

    //creates a new game and loaads the grid
    private void Load_Click(object sender, RoutedEventArgs e)
    {
      //creates new game
      
      createGrid();
      
      string content = ((ComboBoxItem)dimensions.SelectedItem).Content as string;
      if ((content == "Easy" && gridy.The3bv() < 30) || (content == "Medium" && gridy.The3bv() < 80) || (content == "Hard" && gridy.The3bv() < 120))
      {
       createGrid();
      }

      bombsLeft.Text = gridy.RemainingBombsCount().ToString(); //tal vez necesite mover esto mas adelante porque no se si se va a refrescar estando aca

    }

    private void dimensions_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      string content = ((ComboBoxItem)dimensions.SelectedItem).Content as string;
      if (content == "Easy")
      {
        row = 10;
        column = 10;
        mine = 10;
      } else if (content == "Medium")
      {
        row = 15;
        column = 15;
        mine = 40;
      } else
      {
        row = 20;
        column = 20;
        mine = 99;
      }
        
    }
  }
}
