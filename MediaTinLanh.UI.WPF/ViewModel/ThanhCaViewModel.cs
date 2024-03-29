﻿using MediaTinLanh.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaTinLanh.Control;

namespace MediaTinLanh.UI.WPF.ViewModel
{
    public class ThanhCaViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private ThanhCaModel _selectedIitem;
        public ThanhCaModel SelectedItem
        {
            get
            {
                return _selectedIitem;
            }

            set
            {
                if (value == _selectedIitem)
                {
                    return;
                }
                _selectedIitem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        private ObservableCollection<ThanhCaModel> _items;
        public ObservableCollection<ThanhCaModel> Items
        {
            get
            {
                return _items;
            }

            set
            {
                if (value == _items)
                {
                    return;
                }
                _items = value;
                OnPropertyChanged("Items");
            }
        }

        public ThanhCaViewModel()
        {
            Items = new ObservableCollection<ThanhCaModel>();
        }

        public ThanhCaModel FindItem(int thanhCaId)
        {
            var currentThanhCa = Items.Where(i => i.Id == thanhCaId).FirstOrDefault();
            if (currentThanhCa != null)
            {
                return currentThanhCa;
            }

            return new ThanhCaModel();
        }

        public void AddItem(int thanhCaId, ThanhCaModel thanhCa)
        {
            var currentThanhCa = Items.Where(i => i.Id == thanhCaId).FirstOrDefault();
            if (currentThanhCa == null)
            {
                currentThanhCa = thanhCa;
                Items.Add(currentThanhCa);
            }
        }
        //nút xóa
        public void Remove(int thanhCaId)
        {

            var currentThanhCa = Items.Where(i => i.Id == thanhCaId).FirstOrDefault();
            if (currentThanhCa != null)
            {
                Items.Remove(currentThanhCa);
            }
        }
        //nút cập nhật
        public void Update(int thanhCaId, ThanhCaModel thanhCa)
        {
            var currentThanhCa = Items.Where(i => i.Id == thanhCaId).FirstOrDefault();
            if (currentThanhCa != null)
            {
                currentThanhCa = thanhCa;
            }
        }
    }
}
