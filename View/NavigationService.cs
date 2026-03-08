using BoDoiApp.View;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

public static class NavigationService
{
    private static MainForm _mainForm;
    private static Stack<Func<UserControl>> _history = new Stack<Func<UserControl>>();

    public static void Init(MainForm mainForm)
    {
        _mainForm = mainForm;
    }

    public static void Navigate(Func<UserControl> factory)
    {
        var view = factory();

        _history.Push(factory);
        _mainForm.ShowView(view);
    }

    public static void Back()
    {
        if (_history.Count <= 1)
            return;

        _history.Pop();

        var factory = _history.Peek();
        var view = factory();

        _mainForm.ShowView(view);
    }
}