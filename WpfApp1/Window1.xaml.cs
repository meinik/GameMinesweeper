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
  /// <summary>
  /// Interaction logic for Window1.xaml
  /// </summary>
  public partial class Window1 : Window
  {
    Game game = new Game();
    public Window1()
    {
      InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      int mine;
      int row;
      int col;
      Int32.TryParse(customMines.Text, out mine);
      Int32.TryParse(customRows.Text, out row);
      Int32.TryParse(customColumns.Text, out col);

      if (row > 100 || row <= 0 || col > 100 || col <= 0 || mine >= row*col || mine <= 0)
      {
        MessageBox.Show("Really??? Please, try again");
      }

      else
      {
        MyVariables.mine = mine;
        MyVariables.row = row;
        MyVariables.column = col;
        MyVariables.minesLeft = mine;

        this.Close();
      }
      
    }
  }
  
}
