﻿/*
 * Copyright 2018 Capnode AB
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); 
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Algoloop.Model;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;

namespace Algoloop.ViewModel
{
    public class SettingsViewModel
    {
        public SettingsViewModel(SettingsModel model)
        {
            Model = model;
            OkCommand = new RelayCommand<Window>(window => OnOk(window));
        }

        public SettingsModel Model { get; private set; }

        public RelayCommand<Window> OkCommand { get; private set; }

        void OnOk(Window window)
        {
            window.DialogResult = true;
            window.Close();
        }

        internal bool Read(string fileName)
        {
            if (!File.Exists(fileName))
                return false;

            try
            {
                using (StreamReader r = new StreamReader(fileName))
                {
                    string json = r.ReadToEnd();
                    SettingsModel settings = JsonConvert.DeserializeObject<SettingsModel>(json);
                    Model.Copy(settings);
                }

                DataFromModel();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
                return false;
            }
        }

        internal bool Save(string fileName)
        {
            try
            {
                DataToModel();

                using (StreamWriter file = File.CreateText(fileName))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, Model);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
                return false;
            }
        }

        private void DataToModel()
        {
        }

        private void DataFromModel()
        {
        }
    }
}
