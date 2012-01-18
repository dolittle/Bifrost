#region License
//
// Copyright (c) 2008-2012, DoLittle Studios and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially, 
//   must obtain a commercial license.
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://bifrost.codeplex.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using Bifrost.Events;
using Bifrost.Interaction;
using System.Windows.Input;
using Bifrost.Serialization;
using System.Windows;

namespace Bifrost.Mimir.Features.General.Pivot
{
    public class ViewModel
	{
        ISerializer _serializer;

    	public ViewModel(ISerializer serializer)
        {
            Events = new ObservableCollection<EventHolder>();
            _serializer = serializer;
            ReloadCommand = DelegateCommand.Create(Reload);
            Load();
        }


        public virtual ObservableCollection<EventHolder> Events { get; private set; }
        public virtual ICommand ReloadCommand { get; private set; }


        public void Load()
        {
            

            var webClient = new WebClient();
            webClient.DownloadStringCompleted += (s, e) =>
            {
                var serializer = new DataContractSerializer(typeof(string));
                var bytes = System.Text.Encoding.UTF8.GetBytes(e.Result);
                var memoryStream = new MemoryStream(bytes);
                var eventsAsJson = (string)serializer.ReadObject(memoryStream);

                var events = _serializer.FromJson<List<EventHolder>>(eventsAsJson);
                Events.Clear();
                foreach (var @event in events)
                    Events.Add(@event);
            };

            
            var source = Application.Current.Host.Source;
            var url = string.Format("{0}://{1}{2}/Events/GetAllAsJsonString",
                source.Scheme,
                source.Host,
                source.Port == 80 ? string.Empty : ":" + source.Port);

            webClient.DownloadStringAsync(new Uri(url));
        }

        public void Reload()
        {
            Load();
        }
	}
}
