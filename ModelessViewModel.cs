﻿using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ModelessWindowSolution
{
    public class ModelessViewModel : INotifyPropertyChanged
    {
        private Element _activeElement;
        private string _name;
        private string _markValue;
        private ModelessWindowHandler _handler;

        public ModelessViewModel(ModelessWindowHandler handler)
        {
            _handler = handler;
            RevitAPI.UiApplication.SelectionChanged += HandleSelection;
            ChangeValueCommand = new RelayCommand(ChangeValue, CanChangeValue);
            handler.ViewModel = this;
        }

        public RelayCommand ChangeValueCommand { get; set; }

        public Element ActiveElement
        {
            get => _activeElement;
            set
            {
                _activeElement = value;
                OnPropertyChanged();
            }
        }
        public string MarkValue
        {
            get => _markValue;
            set
            {
                _markValue = value;
                OnPropertyChanged();
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public void HandleSelection(object sender, SelectionChangedEventArgs e)
        {
            if (e.GetSelectedElements().Count == 0)
            {
                ActiveElement = null;
                Name = string.Empty;
                _markValue = string.Empty;
            }
            else
            {
                var id = e.GetSelectedElements().First();
                ActiveElement = RevitAPI.Document.GetElement(id);
                Name = ActiveElement?.Name ?? string.Empty;

                var parameter = ActiveElement.get_Parameter(BuiltInParameter.ALL_MODEL_MARK);
                MarkValue = parameter?.AsValueString() ?? string.Empty;
            }
        }

        private void ChangeValue(object parameter)
        {
            _handler.Raise();
            RaiseCloseRequest();
        }

        private bool CanChangeValue(object parameter)
        {
            return ActiveElement != null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        public event EventHandler CloseRequest;
        private void RaiseCloseRequest()
        {
            CloseRequest?.Invoke(this, EventArgs.Empty);
        }
    }
}