#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
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
using System.Net;
using System.Windows;
using System.Windows.Input;
using Bifrost.Events;
using Bifrost.Interaction;
using Newtonsoft.Json;

namespace Bifrost.Mimir.EventViewer
{
    public class ViewModel
	{
    	public ViewModel()
        {
            Events = new ObservableCollection<EventHolder>();
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
                var bytes = System.Text.Encoding.UTF8.GetBytes(e.Result);
                var eventsAsJson = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                var events = JsonConvert.DeserializeObject<List<EventHolder>>(eventsAsJson);
                Events.Clear();
                foreach (var @event in events)
                    Events.Add(@event);
            };

            
            var source = Application.Current.Host.Source;
            var url = string.Format("{0}://{1}{2}/Events/GetAll",
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
