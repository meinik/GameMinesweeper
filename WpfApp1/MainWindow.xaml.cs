using Minesweeper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;

namespace WpfApp1
{
  static class MyVariables
  {
    public static int row;
    public static int column;
    public static int mine;
    public static int minesLeft; // to deal with the game over count
    public static int clicks;
    }

  public partial class MainWindow : Window, INotifyPropertyChanged
  {
    Gridy gridy;
    int counter;    
 
    DispatcherTimer dt = new DispatcherTimer();
    System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
    string currentTime = string.Empty;



    Game game = new Game();

    public event PropertyChangedEventHandler PropertyChanged;

    public MainWindow()
    {
      InitializeComponent();

      dimensions.SelectedIndex = 0; // automatico  no se que hace
      this.DataContext = this;

      dt.Tick += new EventHandler(dt_Tick);
      dt.Interval = new TimeSpan(0, 0, 1);

    }

    void dt_Tick(object sender, EventArgs e)
    {
      if (stopWatch.IsRunning)
      {
        TimeSpan ts = stopWatch.Elapsed;
        currentTime = String.Format("{0:00}:{1:00}:{2:00}",
            ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
        ClockTextBlock.Text = currentTime;
      }
    }

    //creates a new blcok, and a grid out of blocks
    public void NewGame()
    {
      Minesweeper.Block blocky = new Minesweeper.Block(0, false, true);

      //make a grid with bombs and the numbers
      gridy = new Gridy(blocky, MyVariables.row, MyVariables.column);
      gridy.MineIt(MyVariables.mine);
      gridy.NumberIt();
      gridy.RemainingBombsCount();

      stopWatch.Start();
      dt.Start();
    }
   
    //remove
    private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }


    //creates a new game and loads the grid
    private void Load_Click(object sender, RoutedEventArgs e)
    {
      Load.Content = ":)";
      //start music
      game.Music();

      //resets values
      MyVariables.clicks = 0;
      bombsLeft.Text = MyVariables.minesLeft.ToString();
      ClicksCount.Text = MyVariables.clicks.ToString();
      stopWatch.Restart();

      //create new game
      CreateGrid();

      //verify it's not too hard
      string content = ((ComboBoxItem)dimensions.SelectedItem).Content as string;
      if ((content == "Easy" && gridy.The3bv() < 15) || (content == "Medium" && gridy.The3bv() < 80) || (content == "Hard" && gridy.The3bv() < 120))
      {
        CreateGrid();
      }
    }


    //selct dificulty
    private void dimensions_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      string content = ((ComboBoxItem)dimensions.SelectedItem).Content as string;
      if (content == "Easy")
      {
        MyVariables.row = 10;
        MyVariables.column = 10;
        MyVariables.mine = 10;
        MyVariables.minesLeft = 10;
      }
      else if (content == "Medium")
      {
        MyVariables.row = 15;
        MyVariables.column = 15;
        MyVariables.mine = 30;
        MyVariables.minesLeft = 30;
      }
      else if (content == "Hard")
      {
        MyVariables.row = 20;
        MyVariables.column = 20;
        MyVariables.mine = 50;
        MyVariables.minesLeft = 50;
      }

      else
      {
        //open new  window
        Window1 win = new Window1();
        win.Show();
      }


    }


    // creates the BOTTOM grid and adds buttons
    private void CreateGrid()
    {
      NewGame();
      bombsLeft.Text = MyVariables.mine.ToString();

      //clear everything to start anew
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

      //colors
      SolidColorBrush color = new SolidColorBrush(Colors.LightSeaGreen);
      SolidColorBrush color2 = new SolidColorBrush(Colors.MediumAquamarine);


      // create all the children
      for (int i = 0; i < gridy.theGrid.GetLength(0); i++)
      {
        for (int j = 0; j < gridy.theGrid.GetLength(1); j++)
        {

          //textblock - background
          TextBlock block = new TextBlock();
          block.Text = gridy.theGrid[i, j].Mine.ToString();
          block.FontWeight = FontWeights.UltraBold;

          //make the grid display a bomb when the array value is 9, else just the numbers of different colors
          if (gridy.theGrid[i, j].Mine == 9)
          {
            string bomb = "💣";
            block.Text = bomb;
          }
          else if (gridy.theGrid[i, j].Mine == 0)
          {
            block.Text = " ";
          }
          else if (gridy.theGrid[i, j].Mine == 1)
          {
            block.Foreground = Brushes.Green;
          }
          else if (gridy.theGrid[i, j].Mine == 2)
          {
            block.Foreground = Brushes.Orange;
          }
          else if (gridy.theGrid[i, j].Mine == 3)
          {
            block.Foreground = Brushes.DarkOrange;
          }
          else if (gridy.theGrid[i, j].Mine == 4)
          {
            block.Foreground = Brushes.Red;
          }
          else if (gridy.theGrid[i, j].Mine == 5)
          {
            block.Foreground = Brushes.Tomato;
          }
          else if (gridy.theGrid[i, j].Mine == 6)
          {
            block.Foreground = Brushes.DeepPink;
          }
          else if (gridy.theGrid[i, j].Mine == 7)
          {
            block.Foreground = Brushes.Navy;
          }
          else if (gridy.theGrid[i, j].Mine == 8)
          {
            block.Foreground = Brushes.DarkGray;
          }

          block.Background = color2;
          block.TextAlignment = TextAlignment.Center;
          block.VerticalAlignment = VerticalAlignment.Bottom;
          block.Width = 23 ;
          block.Height = 23;
          block.Padding = new Thickness(0, 5, 0, 0);
          block.VerticalAlignment = VerticalAlignment.Center;
          block.HorizontalAlignment = HorizontalAlignment.Center;
          block.Name = "b" + i + "_" + j;
          


          //boton - top--------------------------------------------

          Button boton = new Button();

          boton.Content = " "; //remove later
          boton.Background = color;
          boton.Width = 25;
          boton.Height = 25;
          boton.VerticalAlignment = VerticalAlignment.Center;
          boton.HorizontalAlignment = HorizontalAlignment.Center;
          boton.Click += new RoutedEventHandler(Button_Click);
          boton.MouseRightButtonDown += new MouseButtonEventHandler(Button_rightclick); //right click event 

          //to find the button later on
          //   boton.Name = "boton" + i + "_" + j;
          boton.Tag = i + "_" + j; //since I can't RegisterName...

          //locate and add the block
          Grid.SetRow(block, i);
          Grid.SetColumn(block, j);


          //locate and add the button
          Grid.SetRow(boton, i);
          Grid.SetColumn(boton, j);


          tablero.Children.Add(block);
          tablero.Children.Add(boton);



          //put the tablero at the bottom
          Grid.SetZIndex(tablero, 0);

        }
      }
    }

    private void Button_rightclick(object sender, RoutedEventArgs e)
    {

      ClicksCount.Text = MyVariables.clicks.ToString();
      
      int i;
      int j;
      string sentBy = (e.Source as Button).Tag.ToString();
      string[] tempArray;

      tempArray = sentBy.Split('_');
      i = Int32.Parse(tempArray[0]);
      j = Int32.Parse(tempArray[1]);

      if ((e.Source as Button).Content == "🚩")
      {
        MyVariables.mine++; 
        bombsLeft.Text = MyVariables.mine.ToString();
        (e.Source as Button).Content = " ";
        gridy.theGrid[i, j].Flag = false;
      }
      else
      {
        MyVariables.mine--;
        bombsLeft.Text = MyVariables.mine.ToString();
        (e.Source as Button).Content = "🚩";
        gridy.theGrid[i, j].Flag = true;
      }
      GameOverFlag();
    }


    private void Button_Click(object sender, RoutedEventArgs e)
    {
      counter = 0;
      MyVariables.clicks++;
      ClicksCount.Text = MyVariables.clicks.ToString(); 
      int i;
      int j;
      string sentBy = (e.Source as Button).Tag.ToString();
      string[] tempArray;

      tempArray = sentBy.Split('_');
      i = Int32.Parse(tempArray[0]);
      j = Int32.Parse(tempArray[1]);

      //flagged buttons don't hide
      if ((e.Source as Button).Content != "🚩")
      {
        (e.Source as Button).Visibility = Visibility.Hidden;
        if (gridy.theGrid[i, j].Mine == 9)
        {
          game.StopMusic();
          stopWatch.Stop();
          foreach (FrameworkElement c in tablero.Children)

          {
            if (c is Button)
            {
              (c as Button).IsEnabled = false;
            }
          }
          game.Boom();
          Load.Content = ":'(";
          MessageBox.Show("GAME OVER" + "\n" + "You lost :(");
          
        }

        else
        {
          FloodFill(i, j, gridy.theGrid[i, j]); //aca les cambio el hidden a false
                                    //reveal content when the Hidden variable is false
          for (int x = 0; x < gridy.theGrid.GetLength(0); x++)
          {
            for (int y = 0; y < gridy.theGrid.GetLength(1); y++)
            {
              foreach (FrameworkElement c in tablero.Children)
              {
                if (c is Button && gridy.theGrid[x, y].Hidden == false && (c).Tag.ToString() == x + "_" + y && (c as Button).Content != "🚩")
                {

                  (c).Visibility = Visibility.Hidden;

                }
              }
            }
          }

          gridy.theGrid[i, j].Hidden = false;
          GameOverSurvivor();
        }
      }    
    }

    private void FloodFill(int i, int j, Minesweeper.Block bl)
    {
      int ancho = gridy.theGrid.GetLength(0);
      int alto = gridy.theGrid.GetLength(1);
           
      while (i >= 0 && i < ancho && j >= 0 && j < alto)
      {
        if (gridy.theGrid[i, j].Mine == 0 && gridy.theGrid[i, j].Hidden == true)
        {
          gridy.theGrid[i, j].Hidden = false;
          FloodFill(i, j, bl);
          if (i >= 0 && i < ancho && j >= 0 && j < alto - 1)
          {
            FloodFill(i, j + 1, bl);
            gridy.theGrid[i, j + 1].Hidden = false;
          }
          if (i >= 0 && i < ancho && j > 0 && j < alto)
          {
            FloodFill(i, j - 1, bl);
            gridy.theGrid[i, j - 1].Hidden = false;
          }
          if (i >= 0 && i < ancho - 1 && j >= 0 && j < alto)
          {
            FloodFill(i + 1, j, bl);
            gridy.theGrid[i + 1, j].Hidden = false;
          }
          if (i > 0 && i < ancho && j >= 0 && j < alto)
          {
            FloodFill(i - 1, j, bl);
            gridy.theGrid[i - 1, j].Hidden = false;
          }

          if (i > 0 && i < ancho && j > 0 && j < alto)
          {
            FloodFill(i - 1, j-1, bl);
            gridy.theGrid[i - 1, j-1].Hidden = false;
          }
          if (i >= 0 && i < ancho-1 && j >= 0 && j < alto-1)
          {
            FloodFill(i + 1, j+1, bl);
            gridy.theGrid[i + 1, j+1].Hidden = false;
          }

          if (i >= 0 && i < ancho - 1 && j > 0 && j < alto)
          {
            FloodFill(i + 1, j - 1, bl);
            gridy.theGrid[i + 1, j - 1].Hidden = false;
          }

          if (i > 0 && i < ancho && j >= 0 && j < alto-1)
          {
            FloodFill(i - 1, j + 1, bl);
            gridy.theGrid[i - 1, j + 1].Hidden = false;
          }
         
        }
        else
        {
          return;
        }
      }
    }

    //Info button
    private void instructions_Click(object sender, RoutedEventArgs e)
    {
      MessageBox.Show("Welcome to Minesweeper! "+ "\n" +
        "The objective is to find all the bombs without uncover them. " + "\n" +
        "Click to uncover a cell. " + "\n" + 
        "Right click to flag a cell, when you suspect it is a bomb and don't want to press it by mistake. " + "\n" +
        "The numbers in the open cells indicate how many adjacent bombs there are. " + "\n" +
        "Good luck!");
    }


    //to verify if the user flagged all the bombs
    private void GameOverFlag()
    {
      int count = 0;
      for (int x = 0; x < gridy.theGrid.GetLength(0); x++)
      {
        for (int y = 0; y < gridy.theGrid.GetLength(1); y++)
        {
            if ( gridy.theGrid[x, y].Mine == 9 && gridy.theGrid[x, y].Flag == true)
            {
              count++;
            // if all the bombs are flagged, and there are no extra flags laying around
            if (count == MyVariables.minesLeft && MyVariables.mine == 0)
            {
              game.StopMusic();
              stopWatch.Stop();
              Load.Content = ";)";
              foreach (FrameworkElement c in tablero.Children)

              {
                if (c is Button)
                {
                  (c as Button).IsEnabled = false;
                }
              }

              game.Cheers();
              MessageBox.Show("GAME OVER" + "\n" + "You win! They're all deactivated" + "\n" + "Your time is " + ClockTextBlock.Text);

            }
          }
          
        }
      }

      
    }


    //to verify if the person avoided every bomb and every other spot is unhidden-------------------------------------------------------------------
    private void GameOverSurvivor()
    {

      counter = MyVariables.column * MyVariables.row;

      for (int x = 0; x < gridy.theGrid.GetLength(0); x++)
      {
        for (int y = 0; y < gridy.theGrid.GetLength(1); y++)
        {
          if (gridy.theGrid[x, y].Hidden == false)
          {
            counter--;
          }
        }
      }
      if (counter  == MyVariables.minesLeft)
      {
        game.StopMusic();
        stopWatch.Stop();
        game.Cheers();
        Load.Content = ":P";
        MessageBox.Show("GAME OVER" + "\n" + "You win! You avoided them all" + "\n" + "Your time is " + ClockTextBlock.Text);
        
        foreach (FrameworkElement d in tablero.Children)
        {
          if (d is Button)
          {
            (d as Button).IsEnabled = false;
          }
        }

      }


    }
  }
}


