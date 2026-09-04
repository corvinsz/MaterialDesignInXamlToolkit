using ShowMeTheXAML;

namespace MaterialDesignDemo.Shared.Examples.Dialogs;

/// <summary>
/// Interaction logic for DialogHostWaitForExample.xaml
/// </summary>
public partial class DialogHostWaitForExample : UserControl
{
    private DateTime? _sample7OpenClicked;
    private DateTime? _sample7CloseClicked;

    public DialogHostWaitForExample()
    {
        InitializeComponent();
    }

    private async void Sample7_OpenButton_Click(object sender, RoutedEventArgs e)
    {
        _sample7OpenClicked = DateTime.Now;
        Sample7_OpenClicked.Text = _sample7OpenClicked.Value.TimeOfDay.ToString(@"hh\:mm\:ss\.fff");

        await Sample7DialogHost.WaitForOpened();
        var openedFinished = DateTime.Now;
        Sample7_OpenedFinished.Text = openedFinished.TimeOfDay.ToString(@"hh\:mm\:ss\.fff");

        if (_sample7OpenClicked.HasValue)
        {
            var diff = openedFinished - _sample7OpenClicked.Value;
            Sample7_OpenTimeDifference.Text = ((long)diff.TotalMilliseconds).ToString() + " ms";
        }

        await Sample7DialogHost.WaitForClosed();
        var closedFinished = DateTime.Now;
        Sample7_ClosedFinished.Text = closedFinished.TimeOfDay.ToString(@"hh\:mm\:ss\.fff");

        if (_sample7CloseClicked.HasValue)
        {
            var closeDiff = closedFinished - _sample7CloseClicked.Value;
            Sample7_CloseTimeDifference.Text = ((long)closeDiff.TotalMilliseconds).ToString() + " ms";
        }
    }

    private void Sample7_CloseButton_Click(object sender, RoutedEventArgs e)
    {
        _sample7CloseClicked = DateTime.Now;
        Sample7_CloseClicked.Text = _sample7CloseClicked.Value.TimeOfDay.ToString(@"hh\:mm\:ss\.fff");
    }
}
