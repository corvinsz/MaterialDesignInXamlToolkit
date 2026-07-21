namespace MaterialDesignThemes.Wpf;

/// <summary>
/// Provides attached properties for customizing TreeView appearance and behavior.
/// </summary>
public static class TreeViewAssist
{
    #region AdditionalTemplate
    /// <summary>
    /// Specifies an additional template displayed alongside each tree node.
    /// </summary>
    /// <remarks>
    /// The content to be rendered is the same of the <see cref="TreeViewItem"/>; i.e the Header property, or
    /// some other content such as a view model, typically when using a <see cref="HierarchicalDataTemplate"/>.
    /// </remarks>
    public static readonly DependencyProperty AdditionalTemplateProperty = DependencyProperty.RegisterAttached(
        "AdditionalTemplate",
        typeof(DataTemplate),
        typeof(TreeViewAssist),
        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

    /// <summary>
    /// Sets the additional template.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <param name="value">The value.</param>
    public static void SetAdditionalTemplate(DependencyObject element, DataTemplate value)
    {
        element.SetValue(AdditionalTemplateProperty, value);
    }

    /// <summary>
    /// Gets the additional template.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>
    /// The <see cref="DataTemplate" />.
    /// </returns>
    public static DataTemplate GetAdditionalTemplate(DependencyObject element)
    {
        return (DataTemplate)element.GetValue(AdditionalTemplateProperty);
    }

    #endregion

    #region AdditionalTemplateSelector

    /// <summary>
    /// Specifies a selector for choosing the additional template for each tree node.
    /// </summary>
    /// <remarks>
    /// The content to be rendered is the same of the <see cref="TreeViewItem"/>; i.e the Header property, or
    /// some other content such as a view model, typically when using a <see cref="HierarchicalDataTemplate"/>.
    /// </remarks>
    public static readonly DependencyProperty AdditionalTemplateSelectorProperty = DependencyProperty.RegisterAttached(
        "AdditionalTemplateSelector",
        typeof(DataTemplateSelector),
        typeof(TreeViewAssist),
        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

    /// <summary>
    /// Sets the additional template selector.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <param name="value">The value.</param>
    public static void SetAdditionalTemplateSelector(DependencyObject element, DataTemplateSelector value)
    {
        element.SetValue(AdditionalTemplateSelectorProperty, value);
    }

    /// <summary>
    /// Gets the additional template selector.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>
    /// The <see cref="DataTemplateSelector" />.
    /// </returns>
    public static DataTemplateSelector GetAdditionalTemplateSelector(DependencyObject element)
    {
        return (DataTemplateSelector)element.GetValue(AdditionalTemplateSelectorProperty);
    }

    #endregion

    #region NoTemplate

    private static readonly Lazy<DataTemplate> NoAdditionalTemplateProvider = new Lazy<DataTemplate>(CreateEmptyGridDataTemplate);

    /// <summary>
    /// Represents a placeholder that suppresses the additional template.
    /// </summary>
    public static readonly DataTemplate SuppressAdditionalTemplate = NoAdditionalTemplateProvider.Value;

    public static DataTemplate CreateEmptyGridDataTemplate()
    {
        var xaml = "<DataTemplate><Grid /></DataTemplate>";
        var parserContext = new ParserContext();
        parserContext.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
        parserContext.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");

        using (var memoryStream = new MemoryStream(Encoding.ASCII.GetBytes(xaml)))
        {
            return (DataTemplate)XamlReader.Load(memoryStream, parserContext);
        }
    }

    #endregion

    #region ExpanderSize

    public static double GetExpanderSize(DependencyObject element)
        => (double)element.GetValue(ExpanderSizeProperty);
    public static void SetExpanderSize(DependencyObject element, double value)
        => element.SetValue(ExpanderSizeProperty, value);

    /// <summary>
    /// Controls the size of the TreeView expander.
    /// </summary>
    public static readonly DependencyProperty ExpanderSizeProperty =
        DependencyProperty.RegisterAttached("ExpanderSize", typeof(double), typeof(TreeViewAssist), new PropertyMetadata(default(double)));

    #endregion

    #region ShowSelection

    public static bool GetShowSelection(DependencyObject element)
        => (bool)element.GetValue(ShowSelectionProperty);
    public static void SetShowSelection(DependencyObject element, bool value)
        => element.SetValue(ShowSelectionProperty, value);

    /// <summary>
    /// Determines whether the selected item is visually highlighted.
    /// </summary>
    public static readonly DependencyProperty ShowSelectionProperty =
        DependencyProperty.RegisterAttached("ShowSelection", typeof(bool), typeof(TreeViewAssist), new PropertyMetadata(true));

    #endregion

    #region HasNoItemsExpanderVisibility

    public static Visibility GetHasNoItemsExpanderVisibility(DependencyObject element)
        => (Visibility)element.GetValue(HasNoItemsExpanderVisibilityProperty);
    public static void SetHasNoItemsExpanderVisibility(DependencyObject element, Visibility value)
        => element.SetValue(HasNoItemsExpanderVisibilityProperty, value);

    /// <summary>
    /// Controls the expander visibility for items without children.
    /// </summary>
    public static readonly DependencyProperty HasNoItemsExpanderVisibilityProperty =
        DependencyProperty.RegisterAttached("HasNoItemsExpanderVisibility", typeof(Visibility), typeof(TreeViewAssist), new PropertyMetadata(Visibility.Hidden));

    #endregion
}
