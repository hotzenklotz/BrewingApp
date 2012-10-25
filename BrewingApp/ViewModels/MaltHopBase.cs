using GalaSoft.MvvmLight;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using BrewingApp.Other;
using GalaSoft.MvvmLight.Command;
using BrewingApp.Models;
using System.Collections.ObjectModel;
using System;
using Microsoft.Phone.Shell;

namespace BrewingApp.ViewModels
{
    public class MaltHopBase <T> : ViewModelBase where T : new()
    {
        private ICommand _RemoveCommand;
        private ICommand _AddCommand;
        private ICommand _EditCommand;

        private int _EditItemIndex;

        public ObservableCollection<T> ItemList { get; set; }

        public MaltHopBase()
        {
            //MVVM LightToolkit commands for dropdown menu / applicationBar
            this._RemoveCommand = new RelayCommand<T>(RemoveAction);
            this._EditCommand = new RelayCommand<T>(EditAction);
            this._AddCommand = new RelayCommand(AddAction);
        }

        #region Action Commands

        public ICommand RemoveCommand
        {
            get { return this._RemoveCommand; }
        }

        private void RemoveAction(T item)
        {
            if (item != null)
                ItemList.Remove(item);
        }

        public ICommand AddCommand
        {
            get { return this._AddCommand; }
        }

        private void AddAction()
        {
            T item = new T();
            ItemList.Add(item);
            editItem(item);
        }

        public ICommand EditCommand
        {
            get { return this._EditCommand; }
        }

        private void EditAction(T item)
        {
            if (item != null)
                editItem(item);
        }

        #endregion 

        /// <summary>
        /// Make sure to remember the item about to be edited before calling 
        /// a second page.
        /// </summary>
        /// <param name="item"></param>
        public virtual void editItem(T item)
        {
            this._EditItemIndex = ItemList.IndexOf(item);
            PhoneApplicationService.Current.State["EditItem"] = item;
        }

        /// <summary>
        /// Once an item was edited / newly added update the Lists of Items
        /// </summary>
        public void updateView()
        {
            ItemList[this._EditItemIndex] = (T) PhoneApplicationService.Current.State["EditItem"];
        }
    }
}
