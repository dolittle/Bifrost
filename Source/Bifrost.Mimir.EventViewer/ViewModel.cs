#region License
//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
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
using System.Windows;
using System.Windows.Browser;
using System.Windows.Input;
using Bifrost.Events;
using Bifrost.Interaction;
using Bifrost.JSON.Concepts;
using Bifrost.JSON.Events;
using Bifrost.JSON.Serialization;
using Newtonsoft.Json;

namespace Bifrost.Mimir.EventViewer
{

    public class ViewModel
	{

        JsonSerializer _serializer;

    	public ViewModel()
        {
            CreateSerializer();
            HtmlPage.RegisterScriptableObject("eventViewerViewModel", this);
            Events = new ObservableCollection<IEvent>();
            ReloadCommand = DelegateCommand.Create(Reload);
            Load();
        }

        void CreateSerializer()
        {
            _serializer = new JsonSerializer();
            _serializer.Converters.Add(new MethodInfoConverter());
            _serializer.Converters.Add(new ConceptConverter());
            _serializer.Converters.Add(new ConceptDictionaryConverter());
            _serializer.Converters.Add(new EventSourceVersionConverter());
        }

        public virtual ObservableCollection<IEvent> Events { get; private set; }
        public virtual ICommand ReloadCommand { get; private set; }

        public void Load()
        {
            var webClient = new WebClient();
            webClient.DownloadStringCompleted += (s, e) =>
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(e.Result);
                var eventsAsJson = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);

                using (var textReader = new StringReader(eventsAsJson))
                {
                    using (var reader = new JsonTextReader(textReader))
                    {
                        var events = _serializer.Deserialize<List<ClientEvent>>(reader);
                        Events.Clear();
                        foreach (var @event in events)
                            Events.Add(@event);
                    }
                }
            };

            
            var source = Application.Current.Host.Source;
            var url = string.Format("{0}://{1}{2}/Mimir/Events/GetAll",
                source.Scheme,
                source.Host,
                source.Port == 80 ? string.Empty : ":" + source.Port);

            webClient.DownloadStringAsync(new Uri(url));
        }

        [ScriptableMember]
        public void Reload()
        {
            Load();
        }
	}
}
