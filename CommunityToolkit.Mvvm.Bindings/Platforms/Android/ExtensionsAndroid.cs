﻿// ****************************************************************************
// <copyright file="ExtensionsAndroid.cs" company="GalaSoft Laurent Bugnion">
// Copyright © GalaSoft Laurent Bugnion 2009-2016
// </copyright>
// ****************************************************************************
// <author>Laurent Bugnion</author>
// <email>laurent@galasoft.ch</email>
// <date>19.01.2016</date>
// <project>GalaSoft.MvvmLight</project>
// <web>http://www.mvvmlight.net</web>
// <license>
// See license.txt in this solution or http://www.galasoft.ch/license_MIT.txt
// </license>
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Input;
using Android.Views;
using Android.Widget;
using CommunityToolkit.Mvvm.Input;

namespace CommunityToolkit.Mvvm.Bindings;

/// <summary>
/// Defines extension methods for Android only.
/// </summary>
public static class ExtensionsAndroid
{
    internal static string GetDefaultEventNameForControl(this Type type)
    {
        string eventName = null;

        if (type == typeof (CheckBox)
            || typeof (CheckBox).IsAssignableFrom(type))
        {
            eventName = "CheckedChange";
        }
        else if (type == typeof (Button)
                 || typeof (Button).IsAssignableFrom(type))
        {
            eventName = "Click";
        }

        return eventName;
    }

    internal static Delegate GetCommandHandler(
        this EventInfo info,
        string eventName, 
        Type elementType, 
        ICommand command)
    {
        Delegate result;

        if (string.IsNullOrEmpty(eventName)
            && elementType == typeof (CheckBox))
        {
            EventHandler<CompoundButton.CheckedChangeEventArgs> handler = (s, args) =>
            {
                if (command.CanExecute(null))
                {
                    command.Execute(null);
                }
            };

            result = handler;
        }
        else
        {
            EventHandler handler = (s, args) =>
            {
                if (command.CanExecute(null))
                {
                    command.Execute(null);
                }
            };

            result = handler;
        }

        return result;
    }

    internal static Delegate GetCommandHandler<T>(
        this EventInfo info,
        string eventName,
        Type elementType,
        RelayCommand<T> command,
        Binding<T, T> castedBinding)
    {
        Delegate result;

        if (string.IsNullOrEmpty(eventName)
            && elementType == typeof(CheckBox))
        {
            EventHandler<CompoundButton.CheckedChangeEventArgs> handler = (s, args) =>
            {
                var param = castedBinding == null ? default(T) : castedBinding.Value;
                command.Execute(param);
            };

            result = handler;
        }
        else
        {
            EventHandler handler = (s, args) =>
            {
                var param = castedBinding == null ? default(T) : castedBinding.Value;
                command.Execute(param);
            };

            result = handler;
        }

        return result;
    }

    internal static Delegate GetCommandHandler<T>(
        this EventInfo info,
        string eventName,
        Type elementType,
        RelayCommand<T> command,
        T commandParameter)
    {
        Delegate result;

        if (string.IsNullOrEmpty(eventName)
            && elementType == typeof(CheckBox))
        {
            EventHandler<CompoundButton.CheckedChangeEventArgs> handler = (s, args) => command.Execute(commandParameter);
            result = handler;
        }
        else
        {
            EventHandler handler = (s, args) => command.Execute(commandParameter);
            result = handler;
        }

        return result;
    }
}