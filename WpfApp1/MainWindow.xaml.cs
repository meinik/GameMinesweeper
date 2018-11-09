using Minesweeper;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace WpfApp1
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    Minesweeper.Block blocky = new Minesweeper.Block(0,false,true);

    Minesweeper.Gridy gridy;
    
    
    public MainWindow()
    {
    
      InitializeComponent();

    }
     
    private void dg_Loaded(object sender, RoutedEventArgs e)
    {
      //get values in the textboxes for row, columns and bombs
      int row = Int32.Parse(rowsNumber.Text);
      int column = Int32.Parse(columnsNumber.Text);
      int mine = Int32.Parse(minesNumber.Text);

      //make a grid with bombs and the numbers
      gridy =  new Gridy(blocky, row, column);
      gridy.MineIt(mine);
      gridy.NumberIt();
      
      //make a datatable
      DataTable dataTable = new DataTable();
      for (int j = 0; j < gridy.theGrid.GetLength(1); j++)
        dataTable.Columns.Add(new DataColumn("Column " + j.ToString()));

      for (int i = 0; i < gridy.theGrid.GetLength(0); i++)
      {
        var newRow = dataTable.NewRow();
        for (int j = 0; j < gridy.theGrid.GetLength(1); j++)
          newRow["Column " + j.ToString()] = gridy.theGrid[i, j];
        dataTable.Rows.Add(newRow);
      }

      //make it the ItemsSource for the datagrid
      this.dg.ItemsSource = dataTable.DefaultView;
      
    }

  }
}
