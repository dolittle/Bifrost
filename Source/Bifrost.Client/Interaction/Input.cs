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
using System.Windows;
using System.Windows.Input;

namespace Bifrost.Interaction
{
    /// <summary>
    /// Represents properties for working with input on elements. 
    /// </summary>
    public class Input
    {
        /// <summary>
        /// DependencyProperty that represents setting or getting <see cref="InputBindingCollection"/> on any element.
        /// This is typically very useful in styles where it is not possible to set the InputBinding property
        /// </summary>
        public static readonly DependencyProperty BindingsProperty =
            DependencyProperty.RegisterAttached(
                "Bindings",
                typeof(InputBindingCollection),
                typeof(Input),
                new FrameworkPropertyMetadata(
                    new InputBindingCollection(),
                    (sender, e) =>
                    {
                        var element = sender as UIElement;
                        if (element == null)
                        {
                            return;
                        }

                        element.InputBindings.Clear();
                        element.InputBindings.AddRange((InputBindingCollection)e.NewValue);
                    }));


        /// <summary>
        /// Get the Bindings for an <see cref="UIElement"/>
        /// </summary>
        /// <param name="element"><see cref="UIElement"/> to get for</param>
        /// <returns><see cref="InputBindingCollection"/> associated with the <see cref="UIElement"/></returns>
        public static InputBindingCollection GetBindings(UIElement element)
        {
            return (InputBindingCollection)element.GetValue(BindingsProperty);
        }

        /// <summary>
        /// Set the Bindings for an <see cref="UIElement"/>
        /// </summary>
        /// <param name="element"><see cref="UIElement"/> to get for</param>
        /// <param name="inputBindings"><see cref="InputBindingCollection"/> to set</param>
        public static void SetBindings(UIElement element, InputBindingCollection inputBindings)
        {
            element.SetValue(BindingsProperty, inputBindings);
        }
    }
}
