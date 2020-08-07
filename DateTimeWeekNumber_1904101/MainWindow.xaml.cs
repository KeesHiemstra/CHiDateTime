using CHiDateTimeWeekNumber;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DateTimeWeekNumber_1904101
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      Date.SelectedDate = DateTime.Now;
      GetWeekNumber();
    }

    private void GetWeekNumber()
    {
      DateTimeWeekNumber dwn = new DateTimeWeekNumber(Date.SelectedDate.Value);
      WeekNumber.Text = dwn.WeekNoCompact; //19041
      Reference.Text = $"{WeekNumber.Text}00"; //1904100
    }

    private void GetDate()
    {
      DateTime? date = DateTimeWeekNumber.WeekNoCompactToDate(InputReference.Text);
      if (date == null)
      {
        ReferenceDate.Text = "Invalidate";
      }
      else
      {
        ReferenceDate.Text = date.Value.ToString("yyyy-MM-dd");
      }
    }

    private void ButtonCopyWeekNumber_Click(object sender, RoutedEventArgs e)
    {
      Clipboard.SetText(WeekNumber.Text);
    }

    private void ButtonCopyReference_Click(object sender, RoutedEventArgs e)
    {
      Clipboard.SetText(Reference.Text);
    }

    private void Date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
    {
      GetWeekNumber();
    }

    private void ButtonCalculateDate_Click(object sender, RoutedEventArgs e)
    {
      GetDate();
    }
  }
}
