using ShowMeTheXAML;

namespace MaterialDesignDemo.Shared.Examples.Dialogs;

/// <summary>
/// Interaction logic for DialogHostWaitForExample.xaml
/// </summary>
public partial class DialogHostWaitForExample : UserControl
{
    private DateTime? _sampleOpenClicked;
    private DateTime? _sampleCloseClicked;

    public DialogHostWaitForExample()
    {
        InitializeComponent();
    }

    private async void Sample_OpenButton_Click(object sender, RoutedEventArgs e)
    {
        _sampleOpenClicked = DateTime.Now;
        Sample_OpenClicked.Text = _sampleOpenClicked.Value.TimeOfDay.ToString(@"hh\:mm\:ss\.fff");

        await SampleDialogHost.WaitForOpened();
        var openedFinished = DateTime.Now;
        Sample_OpenedFinished.Text = openedFinished.TimeOfDay.ToString(@"hh\:mm\:ss\.fff");

        if (_sampleOpenClicked.HasValue)
        {
            var diff = openedFinished - _sampleOpenClicked.Value;
            Sample_OpenTimeDifference.Text = ((long)diff.TotalMilliseconds).ToString() + " ms";
        }

        await SampleDialogHost.WaitForClosed();
        var closedFinished = DateTime.Now;
        Sample_ClosedFinished.Text = closedFinished.TimeOfDay.ToString(@"hh\:mm\:ss\.fff");

        if (_sampleCloseClicked.HasValue)
        {
            var closeDiff = closedFinished - _sampleCloseClicked.Value;
            Sample_CloseTimeDifference.Text = ((long)closeDiff.TotalMilliseconds).ToString() + " ms";
        }
    }

    private void Sample_CloseButton_Click(object sender, RoutedEventArgs e)
    {
        _sampleCloseClicked = DateTime.Now;
        Sample_CloseClicked.Text = _sampleCloseClicked.Value.TimeOfDay.ToString(@"hh\:mm\:ss\.fff");
    }
}
